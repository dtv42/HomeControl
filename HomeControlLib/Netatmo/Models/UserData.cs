// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Netatmo.Models
{
    public class UserData
    {
        public class AdministrativeData
        {
            #region Public Properties

            public string Country { get; set; } = string.Empty;
            public int FeelsLikeAlgorithm { get; set; }
            public string Language { get; set; } = string.Empty;
            public int PressureUnit { get; set; }
            public string RegLocale { get; set; } = string.Empty;
            public int Unit { get; set; }
            public int WindUnit { get; set; }

            #endregion
        }

        #region Public Properties

        public string Mail { get; set; } = string.Empty;
        public AdministrativeData Administrative { get; set; } = new AdministrativeData();

        #endregion
    }
}
