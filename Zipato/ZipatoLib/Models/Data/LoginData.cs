// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models
{
    public class LoginData
    {
        public bool Success { get; set; }
        public string JSessionId { get; set; }
        public string Nonce { get; set; }
    }
}
