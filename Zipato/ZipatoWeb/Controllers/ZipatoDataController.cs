// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZipatoDataController.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoWeb.Controllers
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Swashbuckle.AspNetCore.Annotations;

    using BaseClassLib;
    using DataValueLib;
    using ZipatoLib;
    using ZipatoLib.Models.Data;
    using ZipatoLib.Extensions;
    using ZipatoWeb.Models;

    using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

    #endregion

    /// <summary>
    /// The Zipato controller for reading Zipato data items.
    /// </summary>
    [Route("api/data")]
    [Produces("application/json")]
    public class ZipatoDataController : BaseController<AppSettings>
    {
        #region Private Fields

        private readonly IZipato _zipato;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipatoDataController"/> class.
        /// </summary>
        /// <param name="zipato">The Zipato instance.</param>
        /// <param name="options">The application options.</param>
        /// <param name="logger">The application logger.</param>
        public ZipatoDataController(IZipato zipato,
                                    IOptions<AppSettings> options,
                                    ILogger<ZipatoDataController> logger)
            : base(logger, options)
        {
            _zipato = zipato;
        }

        #endregion Constructors

        #region REST Methods

        /// <summary>
        /// Returns Zipato attribute data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("attributes")]
        [SwaggerOperation(Tags = new[] { "Zipato Data API" })]
        [ProducesResponseType(typeof(List<AttributeData>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetAttributesAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetAttributesAsync()...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    var (attributes, status) = await _zipato.DataReadAttributesAsync();

                    if (status.IsGood)
                    {
                        return Ok(attributes);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var attributes = _zipato.Data.Attributes.Select(a => a.ToAttributeData()).ToList();
                    return Ok(attributes);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato binding data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="501">Not supported using a local connection.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("bindings")]
        [SwaggerOperation(Tags = new[] { "Zipato Data API" })]
        [ProducesResponseType(typeof(List<BindingData>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(string), 501)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetBindingsAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetBindingsAsync()...");

                if (_zipato.IsLocal)
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Not implemented in local connection.");
                }

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    var (bindings, status) = await _zipato.DataReadBindingsAsync();

                    if (status.IsGood)
                    {
                        return Ok(bindings);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var bindings = _zipato.Data.Bindings.Select(b => b.ToBindingData()).ToList();
                    return Ok(bindings);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato brand data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("brands")]
        [SwaggerOperation(Tags = new[] { "Zipato Data API" })]
        [ProducesResponseType(typeof(List<BrandData>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetBrandsAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetBrandsAsync()...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                                if (update)
                {
                    var (brands, status) = await _zipato.DataReadBrandsFullAsync();

                    if (status.IsGood)
                    {
                        return Ok(brands);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var brands = _zipato.Data.Brands.Select(b => b.ToBrandData()).ToList();
                    return Ok(brands);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato camera data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("cameras")]
        [SwaggerOperation(Tags = new[] { "Zipato Data API" })]
        [ProducesResponseType(typeof(List<CameraData>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetCamerasAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetCamerasAsync()...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    var (cameras, status) = await _zipato.DataReadCamerasAsync();

                    if (status.IsGood)
                    {
                        return Ok(cameras);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var cameras = _zipato.Data.Cameras.Select(c => c.ToCameraData()).ToList();
                    return Ok(cameras);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato cluster data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="501">Not supported using a local connection.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("clusters")]
        [SwaggerOperation(Tags = new[] { "Zipato Data API" })]
        [ProducesResponseType(typeof(List<ClusterData>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(string), 501)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetClustersAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetClustersAsync()...");

                if (_zipato.IsLocal)
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Not implemented in local connection.");
                }

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    var (clusters, status) = await _zipato.DataReadClustersAsync();

                    if (status.IsGood)
                    {
                        return Ok(clusters);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var clusters = _zipato.Data.Clusters.Select(c => c.ToClusterData()).ToList();
                    return Ok(clusters);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato cluster endpoint data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="501">Not supported using a local connection.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("clusterendpoints")]
        [SwaggerOperation(Tags = new[] { "Zipato Data API" })]
        [ProducesResponseType(typeof(List<ClusterEndpointData>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetClusterEndpointsAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetClusterEndpointsAsync()...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    var (endpoints, status) = await _zipato.DataReadClusterEndpointsAsync();

                    if (status.IsGood)
                    {
                        return Ok(endpoints);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var endpoints = _zipato.Data.ClusterEndpoints.Select(e => e.ToClusterEndpointData()).ToList();
                    return Ok(endpoints);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato device data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("devices")]
        [SwaggerOperation(Tags = new[] { "Zipato Data API" })]
        [ProducesResponseType(typeof(List<DeviceData>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetDevicesAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetDevicesAsync()...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    var (devices, status) = await _zipato.DataReadDevicesAsync();

                    if (status.IsGood)
                    {
                        return Ok(devices);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var devices = _zipato.Data.Devices.Select(d => d.ToDeviceData()).ToList();
                    return Ok(devices);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato endpoint data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("endpoints")]
        [SwaggerOperation(Tags = new[] { "Zipato Data API" })]
        [ProducesResponseType(typeof(List<EndpointData>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetEndpointsAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetEndpointsAsync()...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    var (endpoints, status) = await _zipato.DataReadEndpointsAsync();

                    if (status.IsGood)
                    {
                        return Ok(endpoints);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var endpoints = _zipato.Data.Endpoints.Select(e => e.ToEndpointData()).ToList();
                    return Ok(endpoints);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato group data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("groups")]
        [SwaggerOperation(Tags = new[] { "Zipato Data API" })]
        [ProducesResponseType(typeof(List<GroupData>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetGroupsAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetGroupsAsync()...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    var (groups, status) = await _zipato.DataReadGroupsAsync();

                    if (status.IsGood)
                    {
                        return Ok(groups);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var groups = _zipato.Data.Groups.Select(g => g.ToGroupData()).ToList();
                    return Ok(groups);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato network data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("networks")]
        [SwaggerOperation(Tags = new[] { "Zipato Data API" })]
        [ProducesResponseType(typeof(List<NetworkData>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetNetworksAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetNetworksAsync()...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    var (networks, status) = await _zipato.DataReadNetworksAsync();

                    if (status.IsGood)
                    {
                        return Ok(networks);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var networks = _zipato.Data.Networks.Select(n => n.ToNetworkData()).ToList();
                    return Ok(networks);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato rule data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="501">Not supported using a local connection.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("rules")]
        [SwaggerOperation(Tags = new[] { "Zipato Data API" })]
        [ProducesResponseType(typeof(List<RuleData>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(string), 501)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetRulesAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetRulesAsync()...");

                if (_zipato.IsLocal)
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Not implemented in local connection.");
                }

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    var (rules, status) = await _zipato.DataReadRulesAsync();

                    if (status.IsGood)
                    {
                        return Ok(rules);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var rules = _zipato.Data.Rules.Select(r => r.ToRuleData()).ToList();
                    return Ok(rules);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato scene data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("scenes")]
        [SwaggerOperation(Tags = new[] { "Zipato Data API" })]
        [ProducesResponseType(typeof(List<SceneData>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetScenesAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetScenesAsync()...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    var (scenes, status) = await _zipato.DataReadScenesAsync();

                    if (status.IsGood)
                    {
                        return Ok(scenes);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var scenes = _zipato.Data.Scenes.Select(s => s.ToSceneData()).ToList();
                    return Ok(scenes);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato schedule data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("schedules")]
        [SwaggerOperation(Tags = new[] { "Zipato Data API" })]
        [ProducesResponseType(typeof(List<ScheduleData>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetZipatoScheduleData(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetSchedulesAsync()...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    var (schedules, status) = await _zipato.DataReadSchedulesAsync();

                    if (status.IsGood)
                    {
                        return Ok(schedules);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var schedules = _zipato.Data.Schedules.Select(s => s.ToScheduleData()).ToList();
                    return Ok(schedules);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato thermostat data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("thermostats")]
        [SwaggerOperation(Tags = new[] { "Zipato Data API" })]
        [ProducesResponseType(typeof(List<ThermostatData>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetThermostatsAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetThermostatsAsync()...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    var (thermostats, status) = await _zipato.DataReadThermostatsAsync();

                    if (status.IsGood)
                    {
                        return Ok(thermostats);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var thermostats = _zipato.Data.Thermostats.Select(s => s.ToThermostatData()).ToList();
                    return Ok(thermostats);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato virtual endpoint data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="501">Not supported using a local connection.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("virtualendpoints")]
        [SwaggerOperation(Tags = new[] { "Zipato Data API" })]
        [ProducesResponseType(typeof(List<VirtualEndpointData>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(string), 501)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetVirtualEndpointsAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetVirtualEndpointsAsync()...");

                if (_zipato.IsLocal)
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Not implemented in local connection.");
                }

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    var (endpoints, status) = await _zipato.DataReadVirtualEndpointsAsync();

                    if (status.IsGood)
                    {
                        return Ok(endpoints);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var endpoints = _zipato.Data.VirtualEndpoints.Select(v => v.ToVirtualEndpointData()).ToList();
                    return Ok(endpoints);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion REST Methods
    }
}
