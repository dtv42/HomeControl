// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContactInfo.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Info
{
    public class ContactInfo
    {
        public string Email { get; set; }
        public int? Id { get; set; }
        public string Locale { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string PhoneMobile { get; set; }
        public string Surname { get; set; }
        public string User { get; set; }
        public bool? UserLinked { get; set; }
    }
}
