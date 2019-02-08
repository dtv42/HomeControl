// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageDto.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Dtos
{
    /// <summary>
    ///
    /// </summary>
    public class MessageDto
    {
        public int? Id { get; set; }
        public string Locale { get; set; }
        public string Message { get; set; }
    }
}
