// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Data
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using HomeControlLib.Zipato.Models.Enums;
    using HomeControlLib.Zipato.Models.Data.User;

    #endregion

    public class UserData
    {
        public int? Id { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public UserTypes? Type { get; set; }
        public string UserName { get; set; }
        public List<RoleInfo> UserRoles { get; set; } = new List<RoleInfo> { };
        public bool? Disabled { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public GenderTypes? Gender { get; set; }
        public DateTime? Birthdate { get; set; }
        public string PhoneMobile { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string BillingStreet { get; set; }
        public string BillingCity { get; set; }
        public string BillingCountry { get; set; }
        public string BillingPostalCode { get; set; }
        public BoxUserData BoxUser { get; set; } = new BoxUserData();
        public RealmData Realm { get; set; } = new RealmData();
        public bool? PasswordChangeRequired { get; set; }
    }
}
