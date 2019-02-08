// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValueInfo.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models
{
    #region Using Directives

    using System;
    using ZipatoLib.Models.Info;

    #endregion

    /// <summary>
    /// Helper class for attrbute values.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValueInfo<T> where T : new()
    {
        public Guid Uuid { get; set; } = new Guid();
        public T Value { get; set; } = new T();
        public string Name { get; set; } = string.Empty;

        public static implicit operator ValueInfo<T>(AttributeInfo info)
        {
            if (info != null)
            {
                if (info.Uuid.HasValue)
                {
                    return new ValueInfo<T>()
                    {
                        Uuid = info.Uuid.Value,
                        Name = info.Name
                    };
                }
            }

            return new ValueInfo<T>();
        }
    }

    /// <summary>
    /// Helper class for string value attributes.
    /// </summary>
    public class ValueInfoString
    {
        public Guid Uuid { get; set; } = new Guid();
        public string Value { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public static implicit operator ValueInfoString(AttributeInfo info)
        {
            if (info != null)
            {
                if (info.Uuid.HasValue)
                {
                    return new ValueInfoString()
                    {
                        Uuid = info.Uuid.Value,
                        Name = info.Name
                    };
                }
            }

            return new ValueInfoString();
        }
    }
}
