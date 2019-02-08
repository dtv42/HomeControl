// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserDto.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Dtos
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using HomeControlLib.Zipato.Models.Enums;

    #endregion

    /// <summary>
    ///
    /// </summary>
    public class UserDto
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public UserTypes? Type { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserDto Owner { get; set; }
        public List<string> UserRoles { get; set; } = new List<string> { };
        public List<string> ActiveRoles { get; set; } = new List<string> { };
        public bool? Disabled { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public GenderTypes? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PhoneHome { get; set; }
        public string PhoneMobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string BillingAddress { get; set; }
        public string BillingStreet { get; set; }
        public string BillingCity { get; set; }
        public string BillingCountry { get; set; }
        public string BillingPostalCode { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
        public int? AnnouncementSeen { get; set; }
        public string[] Tags { get; set; } = Array.Empty<string>();
        public BoxUserDto BoxUser { get; set; }
        public RealmDto Realm { get; set; }
        public long? Registered { get; set; }
        public DateTime? PasswordChangeDate { get; set; }
        public bool? PasswordChangeRequired { get; set; }
        public int? Id { get; set; }
    }
}
