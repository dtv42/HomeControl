// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZipatoExtensions.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Extensions
{
    #region Using Directives

    using HomeControlLib.Zipato.Models.Dtos;
    using HomeControlLib.Zipato.Models.Data;
    using HomeControlLib.Zipato.Models.Info;

    #endregion

    /// <summary>
    /// Extension methods for the data conversion between the Zipato data and full data instances.
    /// </summary>
    public static class ZipatoExtensions
    {
        #region Public Methods

        #region Attribute Methods

        /// <summary>
        /// Helper method to convert an <see cref="AttributeValueDto"/> instance to a boolean value.
        /// </summary>
        /// <param name="data">The data instance.</param>
        /// <returns>The converted data.</returns>
        public static bool? ToBool(this AttributeValueDto data)
        {
            switch (data?.Value.ToLower())
            {
                case "true": return true;
                case "false": return false;
                default: return null;
            }
        }

        /// <summary>
        /// Helper method to convert an <see cref="AttributeValueDto"/> instance to an integer value.
        /// </summary>
        /// <param name="data">The data instance.</param>
        /// <returns>The converted data.</returns>
        public static int? ToNumber(this AttributeValueDto data)
        {
            if (int.TryParse(data?.Value, out int number))
            {
                return number;
            }

            return null;
        }

        /// <summary>
        /// Helper method to convert an <see cref="AttributeValueDto"/> instance to a double value.
        /// </summary>
        /// <param name="data">The data instance.</param>
        /// <returns>The converted data.</returns>
        public static double? ToDouble(this AttributeValueDto data)
        {
            if (double.TryParse(data?.Value, out double number))
            {
                return number;
            }

            return null;
        }

        /// <summary>
        /// Helper method to convert an <see cref="AttributeInfo"/> instance
        /// to a <see cref="ValueData"/> instance data?.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <returns>The converted data.</returns>
        public static ValueData ToValueData(this AttributeInfo info)
        {
            return new ValueData()
            {
                Uuid = info?.Uuid,
                Value = info?.Value
            };
        }

        /// <summary>
        /// Helper method to convert a <see cref="ValueData"/> instance
        /// to an <see cref="AttributeInfo"/> instance.
        /// </summary>
        /// <param name="data">The data instance.</param>
        /// <returns>The converted data.</returns>
        public static AttributeInfo ToAttributeInfo(this ValueData data)
        {
            return new AttributeInfo()
            {
                Uuid = data?.Uuid,
                Value = data?.Value
            };
        }

        /// <summary>
        /// Helper method to convert an <see cref="AttributeData"/> instance
        /// to an <see cref="AttributeInfo"/> instance.
        /// </summary>
        /// <param name="data">The data instance.</param>
        /// <returns>The converted data.</returns>
        public static AttributeInfo ToAttributeInfo(this AttributeData data)
        {
            return new AttributeInfo()
            {
                Uuid = data?.Uuid,
                Name = data?.AttributeName,
                AttributeId = data?.AttributeId,
                RoomId = data?.Room
            };
        }

        /// <summary>
        /// Helper method to convert an <see cref="AttributeInfo"/> instance
        /// to an <see cref="AttributeData"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <returns>The converted data.</returns>
        public static AttributeData ToAttributeData(this AttributeInfo info)
        {
            return new AttributeData()
            {
                Uuid = info?.Uuid,
                AttributeId = info?.AttributeId,
                AttributeName = info?.Name,
                Room = info?.RoomId
            };
        }

        /// <summary>
        /// Helper method to copy data from an <see cref="AttributeData"/>
        /// to an <see cref="AttributeInfo"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <param name="data">The data instance.</param>
        public static void CopyFrom(this AttributeInfo info, AttributeData data)
        {
            info.Name = data?.AttributeName;
            info.AttributeId = data?.AttributeId;
            info.RoomId = data?.Room;
        }

        /// <summary>
        /// Helper method to copy missing data of an <see cref="AttributeInfo"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        public static void CopyData(this AttributeInfo info)
        {
            info.RoomId = info?.Room?.Id;
        }

        #endregion

        #region Binding Methods

        /// <summary>
        /// Helper method to convert a <see cref="BindingInfo"/> instance
        /// to a <see cref="BindingData"/> instance.
        /// </summary>
        /// <param name="data">The data instance.</param>
        /// <returns>The converted data.</returns>
        public static BindingInfo ToBindingInfo(this BindingData data)
        {
            return new BindingInfo()
            {
                Uuid = data?.Uuid,
                Link = data?.Link,
                Name = data?.Name
            };
        }

        /// <summary>
        /// Helper method to convert a <see cref="BindingInfo"/> instance
        /// to a <see cref="BindingData"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <returns>The converted data.</returns>
        public static BindingData ToBindingData(this BindingInfo info)
        {
            return new BindingData()
            {
                Uuid = info?.Uuid,
                Link = info?.Link,
                Name = info?.Name
            };
        }

        /// <summary>
        /// Helper method to copy data from a <see cref="BindingData"/>
        /// to a <see cref="BindingInfo"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <param name="data">The data instance.</param>
        public static void CopyFrom(this BindingInfo info, BindingData data)
        {
            info.Link = data?.Link;
            info.Name = data?.Name;
        }

        #endregion

        #region Brand Methods

        /// <summary>
        /// Helper method to convert a <see cref="BrandInfo"/> instance
        /// to a <see cref="BrandData"/> instance.
        /// </summary>
        /// <param name="data">The data instance.</param>
        /// <returns>The converted data.</returns>
        public static BrandInfo ToBrandInfo(this BrandData data)
        {
            return new BrandInfo()
            {
                Name = data?.Name,
                Description = data?.Description,
                Link = data?.Link,
                Order = data?.Order,
                RoleName = data?.Role
            };
        }

        /// <summary>
        /// Helper method to convert a <see cref="BrandInfo"/> instance
        /// to a <see cref="BrandData"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <returns>The converted data.</returns>
        public static BrandData ToBrandData(this BrandInfo info)
        {
            return new BrandData()
            {
                Name = info?.Name,
                Description = info?.Description,
                Link = info?.Link,
                Order = info?.Order,
                Role = info?.RoleName
            };
        }

        /// <summary>
        /// Helper method to copy data from a <see cref="BrandData"/>
        /// to a <see cref="BrandInfo"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <param name="data">The data instance.</param>
        public static void CopyFrom(this BrandInfo info, BrandData data)
        {
            info.Description = data?.Description;
            info.Link = data?.Link;
            info.Order = data?.Order;
            info.RoleName = data?.Role;
        }

        /// <summary>
        /// Helper method to copy missing data of a <see cref="BrandInfo"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        public static void CopyData(this BrandInfo info)
        {
            info.RoleName = info?.Role?.Name;
        }

        #endregion

        #region Camera Methods

        /// <summary>
        /// Helper method to convert a <see cref="CameraData"/> instance
        /// to a <see cref="CameraInfo"/> instance.
        /// </summary>
        /// <param name="data">The data instance.</param>
        /// <returns>The converted data.</returns>
        public static CameraInfo ToCameraInfo(this CameraData data)
        {
            return new CameraInfo()
            {
                Uuid = data?.Uuid,
                Link = data?.Link,
                Name = data?.Name,
                RoomId = data?.Room
            };
        }

        /// <summary>
        /// Helper method to convert a <see cref="CameraInfo"/> instance
        /// to a <see cref="CameraData"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <returns>The converted data.</returns>
        public static CameraData ToCameraData(this CameraInfo info)
        {
            return new CameraData()
            {
                Uuid = info?.Uuid,
                Link = info?.Link,
                Name = info?.Name,
                Room = info?.RoomId
            };
        }

        /// <summary>
        /// Helper method to copy data from a <see cref="CameraData"/>
        /// to a <see cref="CameraInfo"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <param name="data">The data instance.</param>
        public static void CopyFrom(this CameraInfo info, CameraData data)
        {
            info.Link = data?.Link;
            info.Name = data?.Name;
            info.RoomId = data?.Room;
        }

        /// <summary>
        /// Helper method to copy missing data of a <see cref="CameraInfo"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        public static void CopyData(this CameraInfo info)
        {
            info.RoomId = info?.Room?.Id;
        }

        #endregion

        #region Cluster Methods

        /// <summary>
        /// Helper method to convert a <see cref="ClusterData"/> instance
        /// to a <see cref="ClusterInfo"/> instance.
        /// </summary>
        /// <param name="data">The data instance.</param>
        /// <returns>The converted data.</returns>
        public static ClusterInfo ToClusterInfo(this ClusterData data)
        {
            return new ClusterInfo()
            {
                /* TODO: Finish ToClusterinfo?. [peter, 14.08.2018] */
            };
        }

        /// <summary>
        /// Helper method to convert a <see cref="ClusterInfo"/> instance
        /// to a <see cref="ClusterData"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <returns>The converted data.</returns>
        public static ClusterData ToClusterData(this ClusterInfo info)
        {
            return new ClusterData()
            {
                // TODO: Finish ToClusterdata?. [peter, 14.08.2018]
            };
        }

        /// <summary>
        /// Helper method to copy data from a <see cref="ClusterData"/>
        /// to a <see cref="ClusterInfo"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <param name="data">The data instance.</param>
        public static void CopyFrom(this ClusterInfo info, ClusterData data)
        {
            // TODO: Finish CopyFrom. [peter, 14.08.2018]
        }

        /// <summary>
        /// Helper method to copy missing data of a <see cref="ClusterInfo"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        public static void CopyData(this ClusterInfo info)
        {
            // TODO: Finish Copydata?. [peter, 14.08.2018]
        }

        #endregion

        #region ClusterEndpoint Methods

        /// <summary>
        /// Helper method to convert a <see cref="ClusterEndpointData"/> instance
        /// to a <see cref="ClusterEndpointInfo"/> instance.
        /// </summary>
        /// <param name="data">The data instance.</param>
        /// <returns>The converted data.</returns>
        public static ClusterEndpointInfo ToClusterEndpointInfo(this ClusterEndpointData data)
        {
            return new ClusterEndpointInfo()
            {
                Uuid = data?.Uuid,
                Link = data?.Link,
                Name = data?.Name,
                RoomId = data?.Room,
                Tags = data?.Tags
            };
        }

        /// <summary>
        /// Helper method to convert a <see cref="ClusterEndpointInfo"/> instance
        /// to a <see cref="ClusterEndpointData"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <returns>The converted data.</returns>
        public static ClusterEndpointData ToClusterEndpointData(this ClusterEndpointInfo info)
        {
            return new ClusterEndpointData()
            {
                Uuid = info?.Uuid,
                Link = info?.Link,
                Name = info?.Name,
                Room = info?.RoomId,
                Tags = info?.Tags
            };
        }

        /// <summary>
        /// Helper method to copy data from a <see cref="ClusterEndpointData"/>
        /// to a <see cref="ClusterEndpointInfo"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <param name="data">The data instance.</param>
        public static void CopyFrom(this ClusterEndpointInfo info, ClusterEndpointData data)
        {
            info.Link = data?.Link;
            info.Name = data?.Name;
            info.RoomId = data?.Room;
            info.Tags = data?.Tags;
        }

        /// <summary>
        /// Helper method to copy missing data of a <see cref="ClusterEndpointInfo"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        public static void CopyData(this ClusterEndpointInfo info)
        {
            info.RoomId = info?.Room?.Id;
        }

        #endregion

        #region Device Methods

        /// <summary>
        /// Helper method to convert a <see cref="DeviceData"/> instance
        /// to a <see cref="DeviceInfo"/> instance.
        /// </summary>
        /// <param name="data">The data instance.</param>
        /// <returns>The converted data.</returns>
        public static DeviceInfo ToDeviceInfo(this DeviceData data)
        {
            return new DeviceInfo()
            {
                Uuid = data?.Uuid,
                Link = data?.Link,
                Name = data?.Name,
                RoomId = data?.Room,
                Tags = data?.Tags
            };
        }

        /// <summary>
        /// Helper method to convert a <see cref="DeviceInfo"/> instance
        /// to a <see cref="DeviceData"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <returns>The converted data.</returns>
        public static DeviceData ToDeviceData(this DeviceInfo info)
        {
            return new DeviceData()
            {
                Uuid = info?.Uuid,
                Link = info?.Link,
                Name = info?.Name,
                Room = info?.RoomId,
                Tags = info?.Tags
            };
        }

        /// <summary>
        /// Helper method to copy data from a <see cref="DeviceData"/>
        /// to a <see cref="DeviceInfo"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <param name="data">The data instance.</param>
        public static void CopyFrom(this DeviceInfo info, DeviceData data)
        {
            info.Link = data?.Link;
            info.Name = data?.Name;
            info.RoomId = data?.Room;
            info.Tags = data?.Tags;
        }

        /// <summary>
        /// Helper method to copy missing data of a <see cref="DeviceInfo"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        public static void CopyData(this DeviceInfo info)
        {
            info.RoomId = info?.Room?.Id;
            info.Tags = info?.Config.Tags;
        }

        #endregion

        #region Endpoint Methods

        /// <summary>
        /// Helper method to convert an <see cref="EndpointData"/> instance
        /// to an <see cref="EndpointInfo"/> instance.
        /// </summary>
        /// <param name="data">The data instance.</param>
        /// <returns>The converted data.</returns>
        public static EndpointInfo ToEndpointInfo(this EndpointData data)
        {
            return new EndpointInfo()
            {
                Uuid = data?.Uuid,
                Link = data?.Link,
                Name = data?.Name,
                RoomId = data?.Room,
                Tags = data?.Tags
            };
        }

        /// <summary>
        /// Helper method to convert an <see cref="EndpointInfo"/> instance
        /// to an <see cref="EndpointData"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <returns>The converted data.</returns>
        public static EndpointData ToEndpointData(this EndpointInfo info)
        {
            return new EndpointData()
            {
                Uuid = info?.Uuid,
                Link = info?.Link,
                Name = info?.Name,
                Room = info?.RoomId,
                Tags = info?.Tags
            };
        }

        /// <summary>
        /// Helper method to copy data from an <see cref="EndpointData"/>
        /// to an <see cref="EndpointInfo"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <param name="data">The data instance.</param>
        public static void CopyFrom(this EndpointInfo info, EndpointData data)
        {
            info.Link = data?.Link;
            info.Name = data?.Name;
            info.RoomId = data?.Room;
            info.Tags = data?.Tags;
        }

        /// <summary>
        /// Helper method to copy missing data of an <see cref="EndpointInfo"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        public static void CopyData(this EndpointInfo info)
        {
            info.RoomId = info?.Room?.Id;
            info.Tags = info?.Config?.Tags;
        }

        #endregion

        #region Group Methods

        /// <summary>
        /// Helper method to convert a <see cref="GroupData"/> instance
        /// to a <see cref="GroupInfo"/> instance.
        /// </summary>
        /// <param name="data">The data instance.</param>
        /// <returns>The converted data.</returns>
        public static GroupInfo ToGroupInfo(this GroupData data)
        {
            return new GroupInfo()
            {
                Uuid = data?.Uuid,
                Link = data?.Link,
                Name = data?.Name
            };
        }

        /// <summary>
        /// Helper method to convert a <see cref="GroupInfo"/> instance
        /// to a <see cref="GroupData"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <returns>The converted data.</returns>
        public static GroupData ToGroupData(this GroupInfo info)
        {
            return new GroupData()
            {
                Uuid = info?.Uuid,
                Link = info?.Link,
                Name = info?.Name
            };
        }

        /// <summary>
        /// Helper method to copy data from a <see cref="GroupData"/>
        /// to a <see cref="GroupInfo"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <param name="data">The data instance.</param>
        public static void CopyFrom(this GroupInfo info, GroupData data)
        {
            info.Link = data?.Link;
            info.Name = data?.Name;
        }

        #endregion

        #region Network Methods

        /// <summary>
        /// Helper method to convert a <see cref="NetworkData"/> instance
        /// to a <see cref="NetworkInfo"/> instance.
        /// </summary>
        /// <param name="data">The data instance.</param>
        /// <returns>The converted data.</returns>
        public static NetworkInfo ToNetworkInfo(this NetworkData data)
        {
            return new NetworkInfo()
            {
                Uuid = data?.Uuid,
                Link = data?.Link,
                Name = data?.Name
            };
        }

        /// <summary>
        /// Helper method to convert a <see cref="NetworkInfo"/> instance
        /// to a <see cref="NetworkData"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <returns>The converted data.</returns>
        public static NetworkData ToNetworkData(this NetworkInfo info)
        {
            return new NetworkData()
            {
                Uuid = info?.Uuid,
                Link = info?.Link,
                Name = info?.Name
            };
        }

        /// <summary>
        /// Helper method to copy missing data of a <see cref="NetworkInfo"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <param name="data">The data instance.</param>
        public static void CopyFrom(this NetworkInfo info, NetworkData data)
        {
            info.Link = data?.Link;
            info.Name = data?.Name;
        }

        #endregion

        #region Rule Methods

        /// <summary>
        /// Helper method to convert a <see cref="RuleData"/> instance
        /// to a <see cref="RuleInfo"/> instance.
        /// </summary>
        /// <param name="data">The data instance.</param>
        /// <returns>The converted data.</returns>
        public static RuleInfo ToRuleInfo(this RuleData data)
        {
            return new RuleInfo()
            {
                Id = data?.Id,
                Created = data?.Created,
                Deleted = data?.Deleted,
                Description = data?.Description,
                Disabled = data?.Disabled,
                Invalid = data?.Invalid,
                Modified = data?.Modified,
                Name = data?.Name,
                Order = data?.Order,
                Tags = data?.Tags,
                Type = data?.Type
            };
        }

        /// <summary>
        /// Helper method to convert a <see cref="RuleInfo"/> instance
        /// to a <see cref="RuleData"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <returns>The converted data.</returns>
        public static RuleData ToRuleData(this RuleInfo info)
        {
            return new RuleData()
            {
                Id = info?.Id,
                Created = info?.Created,
                Deleted = info?.Deleted,
                Description = info?.Description,
                Disabled = info?.Disabled,
                Invalid = info?.Invalid,
                Modified = info?.Modified,
                Name = info?.Name,
                Order = info?.Order,
                Tags = info?.Tags,
                Type = info?.Type
            };
        }

        /// <summary>
        /// Helper method to copy data from a <see cref="RuleData"/>
        /// to a <see cref="RuleInfo"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <param name="data">The data instance.</param>
        public static void CopyFrom(this RuleInfo info, RuleData data)
        {
            info.Created = data?.Created;
            info.Deleted = data?.Deleted;
            info.Description = data?.Description;
            info.Disabled = data?.Disabled;
            info.Invalid = data?.Invalid;
            info.Modified = data?.Modified;
            info.Name = data?.Name;
            info.Order = data?.Order;
            info.Tags = data?.Tags;
            info.Type = data?.Type;
        }

        #endregion

        #region Scene Methods

        /// <summary>
        /// Helper method to convert a <see cref="SceneData"/> instance
        /// to a <see cref="SceneInfo"/> instance.
        /// </summary>
        /// <param name="data">The data instance.</param>
        /// <returns>The converted data.</returns>
        public static SceneInfo ToSceneInfo(this SceneData data)
        {
            return new SceneInfo()
            {
                Uuid = data?.Uuid,
                Link = data?.Link,
                Name = data?.Name,
                Tags = data?.Tags
            };
        }

        /// <summary>
        /// Helper method to convert a <see cref="SceneInfo"/> instance
        /// to a <see cref="SceneData"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <returns>The converted data.</returns>
        public static SceneData ToSceneData(this SceneInfo info)
        {
            return new SceneData()
            {
                Uuid = info?.Uuid,
                Link = info?.Link,
                Name = info?.Name,
                Tags = info?.Tags
            };
        }

        /// <summary>
        /// Helper method to copy data from a <see cref="SceneData"/>
        /// to a <see cref="SceneInfo"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <param name="data">The data instance.</param>
        public static void CopyFrom(this SceneInfo info, SceneData data)
        {
            info.Link = data?.Link;
            info.Name = data?.Name;
            info.Tags = data?.Tags;
        }

        #endregion

        #region Schedule Methods

        /// <summary>
        /// Helper method to convert a <see cref="ScheduleData"/> instance
        /// to a <see cref="ScheduleInfo"/> instance.
        /// </summary>
        /// <param name="data">The data instance.</param>
        /// <returns>The converted data.</returns>
        public static ScheduleInfo ToScheduleInfo(this ScheduleData data)
        {
            return new ScheduleInfo()
            {
                Uuid = data?.Uuid,
                Link = data?.Link,
                Name = data?.Name
            };
        }

        /// <summary>
        /// Helper method to convert a <see cref="ScheduleInfo"/> instance
        /// to a <see cref="ScheduleData"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <returns>The converted data.</returns>
        public static ScheduleData ToScheduleData(this ScheduleInfo info)
        {
            return new ScheduleData()
            {
                Uuid = info?.Uuid,
                Link = info?.Link,
                Name = info?.Name
            };
        }

        /// <summary>
        /// Helper method to copy data from a <see cref="ScheduleData"/>
        /// to a <see cref="ScheduleInfo"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <param name="data">The data instance.</param>
        public static void CopyFrom(this ScheduleInfo info, ScheduleData data)
        {
            info.Link = data?.Link;
            info.Name = data?.Name;
        }

        #endregion

        #region Thermostat Methods

        /// <summary>
        /// Helper method to convert a <see cref="ThermostatData"/> instance
        /// to a <see cref="ThermostatInfo"/> instance.
        /// </summary>
        /// <param name="data">The data instance.</param>
        /// <returns>The converted data.</returns>
        public static ThermostatInfo ToThermostatInfo(this ThermostatData data)
        {
            return new ThermostatInfo()
            {
                Uuid = data?.Uuid,
                Link = data?.Link,
                Name = data?.Name
            };
        }

        /// <summary>
        /// Helper method to convert a <see cref="ThermostatInfo"/> instance
        /// to a <see cref="ThermostatData"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <returns>The converted data.</returns>
        public static ThermostatData ToThermostatData(this ThermostatInfo info)
        {
            return new ThermostatData()
            {
                Uuid = info?.Uuid,
                Link = info?.Link,
                Name = info?.Name
            };
        }

        /// <summary>
        /// Helper method to copy data from a <see cref="ThermostatData"/>
        /// to a <see cref="ThermostatInfo"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <param name="data">The data instance.</param>
        public static void CopyFrom(this ThermostatInfo info, ThermostatData data)
        {
            info.Link = data?.Link;
            info.Name = data?.Name;
        }

        #endregion

        #region VirtualEndpoint Methods

        /// <summary>
        /// Helper method to convert a <see cref="VirtualEndpointData"/> instance
        /// to a <see cref="VirtualEndpointInfo"/> instance.
        /// </summary>
        /// <param name="data">The data instance.</param>
        /// <returns>The converted data.</returns>
        public static VirtualEndpointInfo ToVirtualEndpointInfo(this VirtualEndpointData data)
        {
            return new VirtualEndpointInfo()
            {
                Uuid = data?.Uuid,
                Link = data?.Link,
                Name = data?.Name,
                RoomId = data?.Room
            };
        }

        /// <summary>
        /// Helper method to convert a <see cref="VirtualEndpointInfo"/> instance
        /// to a <see cref="VirtualEndpointData"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <returns>The converted data.</returns>
        public static VirtualEndpointData ToVirtualEndpointData(this VirtualEndpointInfo info)
        {
            return new VirtualEndpointData()
            {
                Uuid = info?.Uuid,
                Link = info?.Link,
                Name = info?.Name,
                Room = info?.RoomId
            };
        }

        /// <summary>
        /// Helper method to copy data from a <see cref="VirtualEndpointData"/>
        /// to a <see cref="VirtualEndpointInfo"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        /// <param name="data">The data instance.</param>
        public static void CopyFrom(this VirtualEndpointInfo info, VirtualEndpointData data)
        {
            info.Link = data?.Link;
            info.Name = data?.Name;
            info.RoomId = data?.Room;
        }

        /// <summary>
        /// Helper method to copy missing data of a <see cref="VirtualEndpointInfo"/> instance.
        /// </summary>
        /// <param name="info">The full data instance.</param>
        public static void CopyData(this VirtualEndpointInfo info)
        {
            info.RoomId = info?.Room?.Id;
        }

        #endregion

        #endregion
    }
}
