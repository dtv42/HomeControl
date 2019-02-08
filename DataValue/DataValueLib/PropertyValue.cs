// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyValue.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace DataValueLib
{
    #region Using Directives

    using System;
    using System.Collections;
    using System.Reflection;

    #endregion Using Directives

    public static class PropertyValue
    {
        #region Static Methods

        /// <summary>
        /// Returns the property info if the named property can be found.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static PropertyInfo GetPropertyInfo(Type type, string property)
        {
            if ((type != null) && !string.IsNullOrEmpty(property))
            {
                string[] parts = property.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                PropertyInfo propertyinfo = null;
                Type propertytype = type;

                for (int i = 0; i < parts.Length; i++)
                {
                    var propertydata = GetPropertyInfoAndType(propertytype, parts[i]);
                    propertyinfo = propertydata.Info;
                    propertytype = propertydata.Type;
                }

                return propertyinfo;
            }

            return null;
        }

        /// <summary>
        /// Gets the property value for the named property.
        /// </summary>
        /// <param name="obj">The object with the property.</param>
        /// <param name="property">The property name.</param>
        /// <returns>The property value.</returns>
        public static object GetPropertyValue(object obj, string property)
        {
            if ((obj != null) && !string.IsNullOrEmpty(property))
            {
                string[] parts = property.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);

                PropertyInfo propertyinfo = null;
                object propertyvalue = obj;

                for (int i = 0; i < parts.Length; i++)
                {
                    var (propinfo, propvalue) = GetPropertyInfoAndValue(propertyvalue, parts[i]);
                    propertyinfo = propinfo;
                    propertyvalue = propvalue;
                }

                return propertyvalue;
            }

            return null;
        }

        /// <summary>
        /// Sets the property value of the named property.
        /// </summary>
        /// <param name="obj">The object with the property.</param>
        /// <param name="property">The property name.</param>
        /// <param name="value">The property value.</param>
        /// <returns>True if successful.</returns>
        public static void SetPropertyValue(object obj, string property, object value)
        {
            string[] parts = property.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            PropertyInfo propertyinfo = null;
            object propertyvalue = obj;

            for (int i = 0; i < parts.Length - 1; i++)
            {
                var (propinfo, propvalue) = GetPropertyInfoAndValue(propertyvalue, parts[i]);
                propertyinfo = propinfo;
                propertyvalue = propvalue;
            }

            string name = parts[parts.Length - 1];

            var (islist, listname, index) = GetListInfo(name);

            if (islist)
            {
                var (propinfo, propvalue) = GetPropertyInfoAndValue(propertyvalue, name);
                var listinfo = propinfo;
                IList list = (IList)listinfo.GetValue(propertyvalue);
                list[index] = value;
                listinfo.SetValue(propertyvalue, list);
            }
            else
            {
                propertyinfo = propertyvalue.GetType().GetProperty(name);
                propertyinfo.SetValue(propertyvalue, value);
            }
        }

        #endregion Static Methods

        #region Private Methods

        /// <summary>
        /// Helper function to return a flag indicating that the string contains a pair square brackets,
        /// the base string (without the brackets), and the index (if found).
        /// </summary>
        /// <param name="list">A string to be checked.</param>
        /// <returns>The flag, name, and index.</returns>
        private static (bool Flag, string Name, int Index) GetListInfo(string list)
        {
            if (!string.IsNullOrEmpty(list) && list.Contains("[") && list.Contains("]"))
            {
                string[] parts = list.Split(new string[] { "[", "]" }, 3, StringSplitOptions.None);
                string name = parts[0];

                if (parts.Length == 3)
                {
                    if (int.TryParse(parts[1], out int index))
                    {
                        return (true, name, index);
                    }
                    else
                    {
                        return (true, name, 0);
                    }
                }

                return (true, name, 0);
            }

            return (false, string.Empty, 0);
        }

        /// <summary>
        /// Returns property info and type for the specified property.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="property"></param>
        /// <returns>The property info and type.</returns>
        private static (PropertyInfo Info, Type Type) GetPropertyInfoAndType(Type type, string property)
        {
            if ((type != null) && !string.IsNullOrEmpty(property))
            {
                var (islist, listname, index) = GetListInfo(property);

                if (islist)
                {
                    var propertyinfo = type?.GetProperty(listname);
                    var propertytype = propertyinfo?.PropertyType;

                    if (propertytype != null)
                    {
                        if (propertytype.HasElementType && propertytype.IsArray)
                        {
                            propertytype = propertyinfo?.PropertyType?.GetElementType();
                        }
                        else if (propertytype.IsConstructedGenericType && (propertytype.GenericTypeArguments.Length == 1))
                        {
                            propertytype = propertytype.GenericTypeArguments[0];
                        }
                    }

                    return (propertyinfo, propertytype);
                }
                else
                {
                    var propertyinfo = type?.GetProperty(property);
                    var propertytype = propertyinfo?.PropertyType;
                    return (propertyinfo, propertytype);
                }
            }

            return (null, null);
        }

        /// <summary>
        /// Returns property info and object for the specified property.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="property"></param>
        /// <returns>The property info and object.</returns>
        private static (PropertyInfo Info, object Value) GetPropertyInfoAndValue(object obj, string property)
        {
            if ((obj != null) && !string.IsNullOrEmpty(property))
            {
                var (islist, listname, index) = GetListInfo(property);

                if (islist)
                {
                    var listinfo = obj.GetType()?.GetProperty(listname);

                    if (listinfo != null)
                    {
                        var listtype = listinfo.PropertyType;
                        object listobject = null;
                        IList list = null;

                        if ((listtype.HasElementType && listtype.IsArray) ||
                            (listtype.IsConstructedGenericType && (listtype.GenericTypeArguments.Length == 1)))
                        {
                            list = (IList)listinfo.GetValue(obj);

                            if ((index >= 0) && (index < list.Count))
                            {
                                listobject = list[index];
                            }
                        }

                        return (listinfo, listobject);
                    }
                }
                else
                {
                    var propertyinfo = obj.GetType()?.GetProperty(property);
                    var propertyvalue = propertyinfo?.GetValue(obj);
                    return (propertyinfo, propertyvalue);
                }
            }

            return (null, null);
        }

        #endregion Private Methods
    }
}