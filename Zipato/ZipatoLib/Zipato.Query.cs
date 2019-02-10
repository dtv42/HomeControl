// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zipato.Query.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib
{
    #region Using Directives

    using System;
    using ZipatoLib.Models.Enums;
    using ZipatoLib.Models.Flags;

    #endregion

    /// <summary>
    /// Class holding data from the Zipato Zipatile home control (Query Methods).
    /// The data properties are based on the online specification found at (https://my.zipato.com/zipato-web/api/).
    /// </summary>
    public partial class Zipato
    {
        #region Query Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        private string CreateQuery(AlarmPartitionAttributeFlags flags)
        {
            string query = string.Empty;

            query += $"?definition={flags.HasFlag(AlarmPartitionAttributeFlags.DEFINITION).ToString().ToLower()}";
            query += $"&config={flags.HasFlag(AlarmPartitionAttributeFlags.CONFIG).ToString().ToLower()}";
            query += $"&icon={flags.HasFlag(AlarmPartitionAttributeFlags.ICONS).ToString().ToLower()}";
            query += $"&value={flags.HasFlag(AlarmPartitionAttributeFlags.VALUE).ToString().ToLower()}";

            return query;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        private string CreateQuery(AlarmPartitionEventFlags flags = AlarmPartitionEventFlags.NEEDACK)
        {
            string query = string.Empty;

            query += $"?needAck={flags.HasFlag(AlarmPartitionEventFlags.NEEDACK).ToString().ToLower()}";

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        private string CreateQuery(DateTime timestamp, AlarmPartitionEventFlags flags = AlarmPartitionEventFlags.NEEDACK)
        {
            string query = string.Empty;
            long unixTimestamp = new DateTimeOffset(timestamp).ToUnixTimeSeconds();

            query += $"?timestamp={unixTimestamp}";
            query += $"&needAck={flags.HasFlag(AlarmPartitionEventFlags.NEEDACK).ToString().ToLower()}";

            return query;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        private string CreateQuery(AlarmPartitionZoneFlags flags)
        {
            string query = string.Empty;

            query += $"?network={flags.HasFlag(AlarmPartitionZoneFlags.NETWORK).ToString().ToLower()}";
            query += $"&device={flags.HasFlag(AlarmPartitionZoneFlags.DEVICE).ToString().ToLower()}";
            query += $"&endpoint={flags.HasFlag(AlarmPartitionZoneFlags.ENDPOINT).ToString().ToLower()}";
            query += $"&clusterEndpoint={flags.HasFlag(AlarmPartitionZoneFlags.CLUSTERENDPOINT).ToString().ToLower()}";
            query += $"&attribute={flags.HasFlag(AlarmPartitionZoneFlags.ATTRIBUTE).ToString().ToLower()}";
            query += $"&config={flags.HasFlag(AlarmPartitionZoneFlags.CONFIG).ToString().ToLower()}";
            query += $"&value={flags.HasFlag(AlarmPartitionZoneFlags.VALUE).ToString().ToLower()}";
            query += $"&state={flags.HasFlag(AlarmPartitionZoneFlags.STATE).ToString().ToLower()}";
            query += $"&full={flags.HasFlag(AlarmPartitionZoneFlags.FULL).ToString().ToLower()}";

            return query;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        private string CreateQuery(AttributeFlags flags)
        {
            string query = string.Empty;

            query += $"?network={flags.HasFlag(AttributeFlags.NETWORK).ToString().ToLower()}";
            query += $"&endpoint={flags.HasFlag(AttributeFlags.ENDPOINT).ToString().ToLower()}";
            query += $"&clusterEndpoint={flags.HasFlag(AttributeFlags.CLUSTERENDPOINT).ToString().ToLower()}";
            query += $"&definition={flags.HasFlag(AttributeFlags.DEFINITION).ToString().ToLower()}";
            query += $"&config={flags.HasFlag(AttributeFlags.CONFIG).ToString().ToLower()}";
            query += $"&room={flags.HasFlag(AttributeFlags.ROOM).ToString().ToLower()}";
            query += $"&icons={flags.HasFlag(AttributeFlags.ICONS).ToString().ToLower()}";
            query += $"&value={flags.HasFlag(AttributeFlags.VALUE).ToString().ToLower()}";
            query += $"&parent={flags.HasFlag(AttributeFlags.PARENT).ToString().ToLower()}";
            query += $"&children={flags.HasFlag(AttributeFlags.CHILDREN).ToString().ToLower()}";
            query += $"&full={flags.HasFlag(AttributeFlags.FULL).ToString().ToLower()}";
            query += $"&type={flags.HasFlag(AttributeFlags.TYPE).ToString().ToLower()}";

            return query;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        private string CreateQuery(BindingFlags flags)
        {
            string query = string.Empty;

            query += $"?network={flags.HasFlag(BindingFlags.NETWORK).ToString().ToLower()}";
            query += $"&device={flags.HasFlag(BindingFlags.DEVICE).ToString().ToLower()}";
            query += $"&endpoint={flags.HasFlag(BindingFlags.ENDPOINT).ToString().ToLower()}";
            query += $"&full={flags.HasFlag(BindingFlags.FULL).ToString().ToLower()}";

            return query;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        private string CreateQuery(CameraFlags flags)
        {
            string query = string.Empty;

            query += $"?local={flags.HasFlag(CameraFlags.LOCAL).ToString().ToLower()}";
            query += $"&network={flags.HasFlag(CameraFlags.NETWORK).ToString().ToLower()}";
            query += $"&device={flags.HasFlag(CameraFlags.DEVICE).ToString().ToLower()}";
            query += $"&clusterEndpoints={flags.HasFlag(CameraFlags.CLUSTERENDPOINTS).ToString().ToLower()}";
            query += $"&config={flags.HasFlag(CameraFlags.CONFIG).ToString().ToLower()}";
            query += $"&icons={flags.HasFlag(CameraFlags.ICONS).ToString().ToLower()}";
            query += $"&descriptor={flags.HasFlag(CameraFlags.DESCRIPTOR).ToString().ToLower()}";
            query += $"&room={flags.HasFlag(CameraFlags.ROOM).ToString().ToLower()}";
            query += $"&full={flags.HasFlag(CameraFlags.FULL).ToString().ToLower()}";
            query += $"&attributes={flags.HasFlag(CameraFlags.ATTRIBUTES).ToString().ToLower()}";

            return query;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        private string CreateQuery(ClusterEndpointFlags flags)
        {
            string query = string.Empty;

            query += $"?config={flags.HasFlag(ClusterEndpointFlags.CONFIG).ToString().ToLower()}";
            query += $"&network={flags.HasFlag(ClusterEndpointFlags.NETWORK).ToString().ToLower()}";
            query += $"&device={flags.HasFlag(ClusterEndpointFlags.DEVICE).ToString().ToLower()}";
            query += $"&endpoint={flags.HasFlag(ClusterEndpointFlags.ENDPOINT).ToString().ToLower()}";
            query += $"&attributes={flags.HasFlag(ClusterEndpointFlags.ATTRIBUTES).ToString().ToLower()}";
            query += $"&room={flags.HasFlag(ClusterEndpointFlags.ROOM).ToString().ToLower()}";
            query += $"&icons={flags.HasFlag(ClusterEndpointFlags.ICONS).ToString().ToLower()}";
            query += $"&descriptor={flags.HasFlag(ClusterEndpointFlags.DESCRIPTOR).ToString().ToLower()}";
            query += $"&info={flags.HasFlag(ClusterEndpointFlags.INFO).ToString().ToLower()}";
            query += $"&actions={flags.HasFlag(ClusterEndpointFlags.ACTIONS).ToString().ToLower()}";
            query += $"&events={flags.HasFlag(ClusterEndpointFlags.EVENTS).ToString().ToLower()}";
            query += $"&full={flags.HasFlag(ClusterEndpointFlags.FULL).ToString().ToLower()}";

            return query;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        private string CreateQuery(EndpointFlags flags)
        {
            string query = string.Empty;

            query += $"?network={flags.HasFlag(EndpointFlags.NETWORK).ToString().ToLower()}";
            query += $"&device={flags.HasFlag(EndpointFlags.DEVICE).ToString().ToLower()}";
            query += $"&clusterEndpoints={flags.HasFlag(EndpointFlags.CLUSTERENDPOINTS).ToString().ToLower()}";
            query += $"&config={flags.HasFlag(EndpointFlags.CONFIG).ToString().ToLower()}";
            query += $"&icons={flags.HasFlag(EndpointFlags.ICONS).ToString().ToLower()}";
            query += $"&type={flags.HasFlag(EndpointFlags.TYPE).ToString().ToLower()}";
            query += $"&bindings={flags.HasFlag(EndpointFlags.BINDINGS).ToString().ToLower()}";
            query += $"&descriptor={flags.HasFlag(EndpointFlags.DESCRIPTOR).ToString().ToLower()}";
            query += $"&room={flags.HasFlag(EndpointFlags.ROOM).ToString().ToLower()}";
            query += $"&info={flags.HasFlag(EndpointFlags.INFO).ToString().ToLower()}";
            query += $"&actions={flags.HasFlag(EndpointFlags.ACTIONS).ToString().ToLower()}";
            query += $"&unsupported={flags.HasFlag(EndpointFlags.UNSUPPORTED).ToString().ToLower()}";
            query += $"&full={flags.HasFlag(EndpointFlags.FULL).ToString().ToLower()}";
            query += $"&attributes={flags.HasFlag(EndpointFlags.ATTRIBUTES).ToString().ToLower()}";
            query += $"&events={flags.HasFlag(EndpointFlags.EVENTS).ToString().ToLower()}";

            return query;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        private string CreateQuery(DeviceFlags flags)
        {
            string query = string.Empty;

            query += $"?network={flags.HasFlag(DeviceFlags.NETWORK).ToString().ToLower()}";
            query += $"&endpoints={flags.HasFlag(DeviceFlags.ENDPOINTS).ToString().ToLower()}";
            query += $"&type={flags.HasFlag(DeviceFlags.TYPE).ToString().ToLower()}";
            query += $"&config={flags.HasFlag(DeviceFlags.CONFIG).ToString().ToLower()}";
            query += $"&state={flags.HasFlag(DeviceFlags.STATE).ToString().ToLower()}";
            query += $"&icons={flags.HasFlag(DeviceFlags.ICONS).ToString().ToLower()}";
            query += $"&info={flags.HasFlag(DeviceFlags.INFO).ToString().ToLower()}";
            query += $"&descriptor={flags.HasFlag(DeviceFlags.DESCRIPTOR).ToString().ToLower()}";
            query += $"&room={flags.HasFlag(DeviceFlags.ROOM).ToString().ToLower()}";
            query += $"&unsupported={flags.HasFlag(DeviceFlags.UNSUPPORTED).ToString().ToLower()}";
            query += $"&full={flags.HasFlag(DeviceFlags.FULL).ToString().ToLower()}";

            return query;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        private string CreateQuery(GroupFlags flags)
        {
            string query = string.Empty;

            query += $"?endpoints={flags.HasFlag(GroupFlags.ENDPOINTS).ToString().ToLower()}";
            query += $"&type={flags.HasFlag(GroupFlags.TYPE).ToString().ToLower()}";
            query += $"&config={flags.HasFlag(GroupFlags.CONFIG).ToString().ToLower()}";
            query += $"&icons={flags.HasFlag(GroupFlags.ICONS).ToString().ToLower()}";
            query += $"&info={flags.HasFlag(GroupFlags.INFO).ToString().ToLower()}";
            query += $"&full={flags.HasFlag(GroupFlags.FULL).ToString().ToLower()}";

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="until"></param>
        /// <param name="count"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="sticky"></param>
        /// <returns></returns>
        private string CreateQuery(DateTime? from = null, DateTime? until = null, int count = 100, SortOrderTypes order = SortOrderTypes.DESC, DateTime? start = null, bool sticky = false)
        {
            string query = string.Empty;

            if (from.HasValue && until.HasValue)
            {
                query += $"?from={from.Value.ToString("o")}";
                query += $"&until={until.Value.ToString("o")}";
                query += $"&count={count}";
            }
            else
            {
                query += $"?count={count}";
            }

            query += $"&order={order.ToString().ToLower()}";

            if (start.HasValue)
            {
                query += $"?start={start.Value.ToString("o")}";
                query += $"?sticky={sticky.ToString().ToLower()}";
            }

            return query;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        private string CreateQuery(MeteoAttributeFlags flags)
        {
            string query = string.Empty;

            query += $"?definition={flags.HasFlag(MeteoAttributeFlags.DEFINITION).ToString().ToLower()}";
            query += $"&config={flags.HasFlag(MeteoAttributeFlags.CONFIG).ToString().ToLower()}";
            query += $"&value={flags.HasFlag(MeteoAttributeFlags.VALUE).ToString().ToLower()}";

            return query;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        private string CreateQuery(NetworkFlags flags)
        {
            string query = string.Empty;

            query += $"?config={flags.HasFlag(NetworkFlags.CONFIG).ToString().ToLower()}";
            query += $"&devices={flags.HasFlag(NetworkFlags.DEVICES).ToString().ToLower()}";
            query += $"&actions={flags.HasFlag(NetworkFlags.ACTIONS).ToString().ToLower()}";
            query += $"&state={flags.HasFlag(NetworkFlags.STATE).ToString().ToLower()}";
            query += $"&full={flags.HasFlag(NetworkFlags.FULL).ToString().ToLower()}";

            return query;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        private string CreateQuery(ScheduleFlags flags)
        {
            string query = string.Empty;

            query += $"?config={flags.HasFlag(ScheduleFlags.CONFIG).ToString().ToLower()}";
            query += $"&devices={flags.HasFlag(ScheduleFlags.DEVICES).ToString().ToLower()}";
            query += $"&state={flags.HasFlag(ScheduleFlags.STATE).ToString().ToLower()}";
            query += $"&full={flags.HasFlag(ScheduleFlags.FULL).ToString().ToLower()}";

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <param name="from"></param>
        /// <param name="until"></param>
        /// <param name="type"></param>
        /// <param name="refresh"></param>
        /// <returns></returns>
        private string CreateQuery(int start, int size, DateTime? from = null, DateTime? until = null, FileTypes? type = null, bool refresh = false)
        {
            string query = string.Empty;

            query += $"?page={start}";
            query += $"&pageSize={size}";

            if (from.HasValue && until.HasValue)
            {
                query += $"&from={from.Value.ToString("o")}";
                query += $"&until={until.Value.ToString("o")}";
            }

            if (type.HasValue)
            {
                query += $"&fileType={type.ToString().ToUpper()}";
            }

            query += $"&refresh={refresh.ToString().ToLower()}";

            return query;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        private string CreateQuery(ThermostatFlags flags)
        {
            string query = string.Empty;

            query += $"?network={flags.HasFlag(ThermostatFlags.NETWORK).ToString().ToLower()}";
            query += $"&endpoints={flags.HasFlag(ThermostatFlags.ENDPOINTS).ToString().ToLower()}";
            query += $"&config={flags.HasFlag(ThermostatFlags.CONFIG).ToString().ToLower()}";
            query += $"&icons={flags.HasFlag(ThermostatFlags.ICONS).ToString().ToLower()}";
            query += $"&clusterEndpoints={flags.HasFlag(ThermostatFlags.CLUSTERENDPOINTS).ToString().ToLower()}";
            query += $"&operations={flags.HasFlag(ThermostatFlags.OPERATIONS).ToString().ToLower()}";
            query += $"&type={flags.HasFlag(ThermostatFlags.TYPE).ToString().ToLower()}";
            query += $"&bindings={flags.HasFlag(ThermostatFlags.BINDINGS).ToString().ToLower()}";
            query += $"&full={flags.HasFlag(ThermostatFlags.FULL).ToString().ToLower()}";
            query += $"&attributes={flags.HasFlag(ThermostatFlags.ATTRIBUTES).ToString().ToLower()}";

            return query;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        private string CreateQuery(VirtualEndpointFlags flags)
        {
            string query = string.Empty;

            query += $"?network={flags.HasFlag(VirtualEndpointFlags.NETWORK).ToString().ToLower()}";
            query += $"&device={flags.HasFlag(VirtualEndpointFlags.DEVICE).ToString().ToLower()}";
            query += $"&clusterEndpoints={flags.HasFlag(VirtualEndpointFlags.CLUSTERENDPOINTS).ToString().ToLower()}";
            query += $"&config={flags.HasFlag(VirtualEndpointFlags.CONFIG).ToString().ToLower()}";
            query += $"&icons={flags.HasFlag(VirtualEndpointFlags.ICONS).ToString().ToLower()}";
            query += $"&type={flags.HasFlag(VirtualEndpointFlags.TYPE).ToString().ToLower()}";
            query += $"&bindings={flags.HasFlag(VirtualEndpointFlags.BINDINGS).ToString().ToLower()}";
            query += $"&descriptor={flags.HasFlag(VirtualEndpointFlags.DESCRIPTOR).ToString().ToLower()}";
            query += $"&room={flags.HasFlag(VirtualEndpointFlags.ROOM).ToString().ToLower()}";
            query += $"&info={flags.HasFlag(VirtualEndpointFlags.INFO).ToString().ToLower()}";
            query += $"&full={flags.HasFlag(VirtualEndpointFlags.FULL).ToString().ToLower()}";
            query += $"&attributes={flags.HasFlag(VirtualEndpointFlags.ATTRIBUTES).ToString().ToLower()}";

            return query;
        }

        #endregion
    }
}
