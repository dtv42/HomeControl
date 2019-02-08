// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndpointConfig.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Config
{
    #region Using Directives

    using System.Collections.Generic;

    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using ZipatoLib.Models.Enums;
    using ZipatoLib.Models.Data;

    #endregion

    /// <summary>
    ///
    /// </summary>
    public class EndpointConfig : ZipaboxConfig
    {
        public List<AssociationGroupData> AssociationGroups { get; set; } = new List<AssociationGroupData> { };
        [JsonConverter(typeof(StringEnumConverter))]
        public CategoryTypes? Category { get; set; }
        public List<string> CmdClassClient { get; set; } = new List<string> { };
        public List<string> CmdClassServer { get; set; } = new List<string> { };
        public Dictionary<string, int> CmdClassVersionMap { get; set; } = new Dictionary<string, int> { };
        public JObject Configuration { get; set; }
        public string Description { get; set; }
        public string DescriptorFlags { get; set; }
        public int? EpId { get; set; }
        public string GenericDevClass { get; set; }
        public int? GroupId { get; set; }
        public bool? Hidden { get; set; }
        public bool? HiddenRules { get; set; }
        public bool? IgnoreAssoc { get; set; }
        public bool? MainEndpoint { get; set; }
        public int? MultiInstanceId { get; set; }
        public bool? Mute { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OperationTypes? OperationMode { get; set; }
        public bool? OptionalFunc { get; set; }
        public string Order { get; set; }
        public int? Room { get; set; }
        public List<string> SecurityCmdClasses { get; set; } = new List<string> { };
        public string SpecificDevClass { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public StatusTypes? Status { get; set; }
        public string Tag { get; set; }
        public List<string> Tags { get; set; } = new List<string> { };
        public string Type { get; set; }
        public string ZwIconType { get; set; }
    }
}
