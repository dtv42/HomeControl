// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UuidAttribute.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoApp.Commands
{
    #region Using Directives

    using System;
    using System.ComponentModel.DataAnnotations;

    #endregion

    public class UuidAttribute : ValidationAttribute
    {
        public UuidAttribute()
            : base("The value for {0} must be a valid UUID.")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (Guid.TryParse((string)value, out Guid uuid))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(context.DisplayName));
        }
    }
}
