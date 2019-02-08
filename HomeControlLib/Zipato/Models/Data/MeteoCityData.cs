// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MeteoCityData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Data
{
    #region Using Directives

    using System.Collections.Generic;

    using Newtonsoft.Json;

    #endregion

    public class MeteoCityData
    {
        public class ResultData
        {
            public string Name { get; set; }
            public string Type { get; set; }
            //public string Zmw { get; set; }
            [JsonProperty(PropertyName = "tz")]
            public string Timezone { get; set; }
            [JsonProperty(PropertyName = "tzs")]
            public string TimezoneCode { get; set; }

            //public string L { get; set; }
            public string Query { get; set; }
            public string Region { get; set; }
            [JsonProperty(PropertyName = "c")]
            public string CountryCode { get; set; }
        }

        List<ResultData> Results { get; set; } = new List<ResultData> { };
    }
}
