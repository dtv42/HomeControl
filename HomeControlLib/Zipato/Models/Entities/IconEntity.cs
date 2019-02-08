// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IconEntity.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Entities
{
    using HomeControlLib.Zipato.Models.Dtos;

    public class IconEntity : UuidEntity
    {
        public NameEntity UserIcon { get; set; }
        public NameEntity Icon { get; set; }
        public bool? ShowIcon { get; set; }
        public string TemplateId { get; set; }
        public TypeTagDto Type { get; set; }
    }
}
