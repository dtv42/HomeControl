// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyHelper.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace DataValueLib
{
    #region Using Directives

    using System.Reflection;

    #endregion Using Directives

    /// <summary>
    /// Helper class providing implementation for Property Helper methods.
    /// </summary>
    /// <typeparam name="T">The type of the class containing the property.</typeparam>
    public class PropertyHelper<T> where T : class
    {
        #region Static Methods

        /// <summary>
        /// Returns true if property with the specified name is found in the PAC3200Data class.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns true if property is found.</returns>
        public static bool IsProperty(string property) => (PropertyValue.GetPropertyInfo(typeof(T), property) != null) ? true : false;

        /// <summary>
        /// Returns the <see cref="PropertyInfo"/> data for the property with the specified name.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>The property info.</returns>
        public static PropertyInfo GetPropertyInfo(string property) => PropertyValue.GetPropertyInfo(typeof(T), property);

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the value for the property with the specified name.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>The property value.</returns>
        public object GetPropertyValue(string property) => PropertyValue.GetPropertyValue(this, property);

        /// <summary>
        /// Sets the value for the property with the specified name.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <param name="value">The property value.</param>
        public void SetPropertyValue(string property, object value) => PropertyValue.SetPropertyValue(this, property, value);

        #endregion Public Methods
    }
}