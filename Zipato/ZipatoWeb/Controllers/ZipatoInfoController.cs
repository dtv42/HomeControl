// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZipatoInfoController.cs" company="DTV-Online">
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
    using ZipatoLib.Models.Info;
    using ZipatoWeb.Models;

    using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

    #endregion

    /// <summary>
    /// The Zipato controller for reading Zipato data items.
    /// </summary>
    [ApiController]
    [Route("api/info")]
    [Produces("application/json")]
    public class ZipatoInfoController : BaseController<AppSettings>
    {
        #region Private Fields

        private readonly IZipato _zipato;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipatoInfoController"/> class.
        /// </summary>
        /// <param name="zipato">The Zipato instance.</param>
        /// <param name="options">The application options.</param>
        /// <param name="logger">The application logger.</param>
        public ZipatoInfoController(IZipato zipato,
                                    IOptions<AppSettings> options,
                                    ILogger<ZipatoInfoController> logger)
            : base(logger, options)
        {
            _zipato = zipato;
        }

        #endregion Constructors

        #region REST Methods

        /// <summary>
        /// Returns Zipato alarm info.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("alarm")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(AlarmInfo), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetAlarmAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetAlarmAsync()...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (alarm, status) = await _zipato.DataReadAlarmAsync();

                    if (status.IsGood)
                    {
                        return Ok(alarm);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    return Ok(_zipato.Data.Alarm);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato announcement data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="501">Not supported using a local connection.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("announcements")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(List<AnnouncementInfo>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(string), 501)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetAnnouncementsAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetAnnouncementsAsync()...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (_zipato.IsLocal)
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Not implemented in local connection.");
                }

                if (update)
                {
                    var (announcements, status) = await _zipato.DataReadAnnouncementsAsync();

                    if (status.IsGood)
                    {
                        return Ok(announcements);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    return Ok(_zipato.Data.Announcements);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato attribute info.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("attributes")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(List<AttributeInfo>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetAttributesFullAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetAttributesFullAsync()...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (attributes, status) = await _zipato.DataReadAttributesFullAsync();

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
                    return Ok(_zipato.Data.Attributes);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato attribute info.
        /// </summary>
        /// <param name="uuid">The attribute UUID.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The attribute cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("attributes/{uuid}")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(AttributeInfo), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetAttributeAsync(Guid uuid, bool update = false)
        {
            try
            {
                _logger?.LogDebug($"GetAttributeAsync({uuid})...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (attribute, status) = await _zipato.DataReadAttributeAsync(uuid);

                    if (status.IsGood)
                    {
                        return Ok(attribute);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var attribute = _zipato.GetAttribute(uuid);

                    if ((attribute != null) && attribute.Uuid.HasValue)
                    {
                        return Ok(attribute);
                    }
                    else
                    {
                        return NotFound($"Attribute with uuid '{uuid}' not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato binding info.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="501">Not supported using a local connection.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("bindings")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(List<BindingInfo>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(string), 501)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetBindingsFullAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetBindingsFullAsync()...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (_zipato.IsLocal)
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Not implemented in local connection.");
                }

                if (update)
                {
                    var (bindings, status) = await _zipato.DataReadBindingsFullAsync();

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
                    return Ok(_zipato.Data.Bindings);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato binding info.
        /// </summary>
        /// <param name="uuid">The binding UUID.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The binding cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("bindings/{uuid}")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(BindingInfo), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetBindingAsync(Guid uuid, bool update = false)
        {
            try
            {
                _logger?.LogDebug($"GetBindingAsync({uuid})...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (binding, status) = await _zipato.DataReadBindingAsync(uuid);

                    if (status.IsGood)
                    {
                        return Ok(binding);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var binding = _zipato.GetBinding(uuid);

                    if ((binding != null) && binding.Uuid.HasValue)
                    {
                        return Ok(binding);
                    }
                    else
                    {
                        return NotFound($"Binding with uuid '{uuid}' not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato brand info.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("brands")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(List<BrandInfo>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetBrandsFullAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetBrandsFullAsync()...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
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
                    return Ok(_zipato.Data.Brands);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato brand info.
        /// </summary>
        /// <param name="name">The brand UUID.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The brand cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("brands/{name}")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(BrandInfo), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetBrandAsync(string name, bool update = false)
        {
            try
            {
                _logger?.LogDebug($"GetBrandAsync({name})...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (brand, status) = await _zipato.DataReadBrandAsync(name);

                    if (status.IsGood)
                    {
                        return Ok(brand);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var brand = _zipato.GetBrand(name);

                    if ((brand != null) && !string.IsNullOrEmpty(brand.Name))
                    {
                        return Ok(brand);
                    }
                    else
                    {
                        return NotFound($"Brand with name '{name}' not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato box data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("box")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(BoxInfo), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetBoxAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetBoxAsync()...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (box, status) = await _zipato.DataReadBoxAsync();

                    if (status.IsGood)
                    {
                        return Ok(box);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    return Ok(_zipato.Data.Box);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato camera info.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("cameras")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(List<CameraInfo>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetCamerasFullAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetCamerasFullAsync()...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (cameras, status) = await _zipato.DataReadCamerasFullAsync();

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
                    return Ok(_zipato.Data.Cameras);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato camera info.
        /// </summary>
        /// <param name="uuid">The camera UUID.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The camera cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("cameras/{uuid}")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(CameraInfo), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetCameraAsync(Guid uuid, bool update = false)
        {
            try
            {
                _logger?.LogDebug($"GetCameraAsync({uuid})...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (camera, status) = await _zipato.DataReadCameraAsync(uuid);

                    if (status.IsGood)
                    {
                        return Ok(camera);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var camera = _zipato.GetCamera(uuid);

                    if ((camera != null) && camera.Uuid.HasValue)
                    {
                        return Ok(camera);
                    }
                    else
                    {
                        return NotFound($"Camera with uuid '{uuid}' not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato cluster info.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="501">Not supported using a local connection.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("clusters")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(List<ClusterInfo>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(string), 501)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetClustersFullAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetClustersFullAsync()...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (_zipato.IsLocal)
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Not implemented in local connection.");
                }

                                if (update)
                {
                    var (clusters, status) = await _zipato.DataReadClustersFullAsync();

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
                    return Ok(_zipato.Data.Clusters);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato cluster info.
        /// </summary>
        /// <param name="id">The cluster ID.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The cluster cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("clusters/{id}")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(ClusterInfo), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetClusterAsync(int id, bool update = false)
        {
            try
            {
                _logger?.LogDebug($"GetClusterAsync({id})...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (cluster, status) = await _zipato.DataReadClusterAsync(id);

                    if (status.IsGood)
                    {
                        return Ok(cluster);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var cluster = _zipato.GetCluster(id);

                    if ((cluster != null) && cluster.Id.HasValue)
                    {
                        return Ok(cluster);
                    }
                    else
                    {
                        return NotFound($"Cluster with id '{id}' not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato cluster endpoint info.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("clusterendpoints")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(List<ClusterEndpointInfo>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetClusterEndpointsFullAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetClusterEndpointsFullAsync()...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                                if (update)
                {
                    var (endpoints, status) = await _zipato.DataReadClusterEndpointsFullAsync();

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
                    return Ok(_zipato.Data.ClusterEndpoints);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato cluster endpoint info.
        /// </summary>
        /// <param name="uuid">The cluster endpoint UUID.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The cluster endpoint cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("clusterendpoints/{uuid}")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(ClusterEndpointInfo), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetClusterEndpointAsync(Guid uuid, bool update = false)
        {
            try
            {
                _logger?.LogDebug($"GetClusterEndpointAsync({uuid})...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (endpoint, status) = await _zipato.DataReadClusterEndpointAsync(uuid);

                    if (status.IsGood)
                    {
                        return Ok(endpoint);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var endpoint = _zipato.GetClusterEndpoint(uuid);

                    if ((endpoint != null) && endpoint.Uuid.HasValue)
                    {
                        return Ok(endpoint);
                    }
                    else
                    {
                        return NotFound($"Cluster enpoint with uuid '{uuid}' not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato contact data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("contacts")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(List<ContactInfo>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetContactsAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetContactsAsync()...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (contacts, status) = await _zipato.DataReadContactsAsync();

                    if (status.IsGood)
                    {
                        return Ok(contacts);
                    }
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    return Ok(_zipato.Data.Contacts);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato contact info.
        /// </summary>
        /// <param name="id">The contact ID.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The contact cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("contacts/{id}")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(ContactInfo), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetContactAsync(int id, bool update = false)
        {
            try
            {
                _logger?.LogDebug($"GetContactAsync({id})...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (contact, status) = await _zipato.DataReadContactAsync(id);

                    if (status.IsGood)
                    {
                        return Ok(contact);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var contact = _zipato.GetContact(id);

                    if ((contact != null) && contact.Id.HasValue)
                    {
                        return Ok(contact);
                    }
                    else
                    {
                        return NotFound($"Contact with uuid '{id}' not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato device info.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("devices")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(List<DeviceInfo>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetDevicesFullAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetDevicesFullAsync()...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (devices, status) = await _zipato.DataReadDevicesFullAsync();

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
                    return Ok(_zipato.Data.Devices);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato device info.
        /// </summary>
        /// <param name="uuid">The device UUID.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The device cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("devices/{uuid}")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(DeviceInfo), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetDeviceAsync(Guid uuid, bool update = false)
        {
            try
            {
                _logger?.LogDebug($"GetDeviceAsync({uuid})...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (device, status) = await _zipato.DataReadDeviceAsync(uuid);

                    if (status.IsGood)
                    {
                        return Ok(device);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var device = _zipato.GetDevice(uuid);

                    if ((device != null) && device.Uuid.HasValue)
                    {
                        return Ok(device);
                    }
                    else
                    {
                        return NotFound($"Device with uuid '{uuid}' not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato endpoint info.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("endpoints")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(List<EndpointInfo>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetEndpointsFullAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetEndpointsFullAsync()...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (endpoints, status) = await _zipato.DataReadEndpointsFullAsync();

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
                    return Ok(_zipato.Data.Endpoints);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato endpoint info.
        /// </summary>
        /// <param name="uuid">The endpoint uuid.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The endpoint cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("endpoints/{uuid}")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(EndpointInfo), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetEndpointAsync(Guid uuid, bool update = false)
        {
            try
            {
                _logger?.LogDebug($"GetEndpointAsync({uuid})...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (endpoint, status) = await _zipato.DataReadEndpointAsync(uuid);

                    if (status.IsGood)
                    {
                        return Ok(endpoint);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var endpoint = _zipato.GetEndpoint(uuid);

                    if ((endpoint != null) && endpoint.Uuid.HasValue)
                    {
                        return Ok(endpoint);
                    }
                    else
                    {
                        return NotFound($"Endpoint with uuid '{uuid}' not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato group info.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("groups")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(List<GroupInfo>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetGroupsFullAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetGroupsFullAsync()...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (groups, status) = await _zipato.DataReadGroupsFullAsync();

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
                    return Ok(_zipato.Data.Groups);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato group info.
        /// </summary>
        /// <param name="uuid">The group UUID.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The group cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("group/{uuid}")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(GroupInfo), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetGroupAsync(Guid uuid, bool update = false)
        {
            try
            {
                _logger?.LogDebug($"GetGroupAsync({uuid})...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (group, status) = await _zipato.DataReadGroupAsync(uuid);

                    if (status.IsGood)
                    {
                        return Ok(group);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var group = _zipato.GetGroup(uuid);

                    if ((group != null) && group.Uuid.HasValue)
                    {
                        return Ok(group);
                    }
                    else
                    {
                        return NotFound($"Group with uuid '{uuid}' not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato network info.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("networks")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(List<NetworkInfo>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetNetworksFullAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetNetworksFullAsync()...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (networks, status) = await _zipato.DataReadNetworksFullAsync();

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
                    return Ok(_zipato.Data.Networks);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato network info.
        /// </summary>
        /// <param name="uuid">The network identifier.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The network cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("networks/{uuid}")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(NetworkInfo), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetNetworkAsync(Guid uuid, bool update = false)
        {
            try
            {
                _logger?.LogDebug($"GetNetworkAsync({uuid})...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (network, status) = await _zipato.DataReadNetworkAsync(uuid);

                    if (status.IsGood)
                    {
                        return Ok(network);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var network = _zipato.GetNetwork(uuid);

                    if ((network != null) && network.Uuid.HasValue)
                    {
                        return Ok(network);
                    }
                    else
                    {
                        return NotFound($"Network with uuid '{uuid}' not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato network tree data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("networktrees")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(List<NetworkTree>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetNetworkTreesAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetNetworkTreesAsync()...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (trees, status) = await _zipato.DataReadNetworkTreesAsync();

                    if (status.IsGood)
                    {
                        return Ok(trees);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    return Ok(_zipato.Data.NetworkTrees);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato network tree info.
        /// </summary>
        /// <param name="uuid">The network tree UUID.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The network tree cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("networktrees/{uuid}")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(NetworkTree), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetNetworkTreeAsync(Guid uuid, bool update = false)
        {
            try
            {
                _logger?.LogDebug($"GetNetworkTreeAsync({uuid})...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (tree, status) = await _zipato.DataReadNetworkTreeAsync(uuid);

                    if (status.IsGood)
                    {
                        return Ok(tree);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var tree = _zipato.GetNetworkTree(uuid);

                    if ((tree != null) && tree.Uuid.HasValue)
                    {
                        return Ok(tree);
                    }
                    else
                    {
                        return NotFound($"Network tree with uuid '{uuid}' not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato room data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("rooms")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(List<RoomData>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetRoomsAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetRoomsAsync()...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (rooms, status) = await _zipato.DataReadRoomsAsync();

                    if (status.IsGood)
                    {
                        return Ok(rooms);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    return Ok(_zipato.Data.Rooms);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato room info.
        /// </summary>
        /// <param name="id">The room ID.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The room cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("room/{id}")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(RoomData), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetRoomAsync(int id, bool update = false)
        {
            try
            {
                _logger?.LogDebug($"GetRoomAsync({id})...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (rooms, status) = await _zipato.DataReadRoomsAsync();

                    if (status.IsGood)
                    {
                        var room = _zipato.GetRoom(id);

                        if ((room != null) && room.Id.HasValue)
                        {
                            return Ok(room);
                        }
                        else
                        {
                            return NotFound($"Room with id '{id}' not found.");
                        }
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var room = _zipato.GetRoom(id);

                    if ((room != null) && room.Id.HasValue)
                    {
                        return Ok(room);
                    }
                    else
                    {
                        return NotFound($"Room with id '{id}' not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato rule info.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="501">Not supported using a local connection.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("rules")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(List<RuleInfo>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(string), 501)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetRulesFullAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetRulesFullAsync()...");

                if (_zipato.IsLocal)
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Not implemented in local connection.");
                }

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (rules, status) = await _zipato.DataReadRulesFullAsync();

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
                    return Ok(_zipato.Data.Rules);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato rule info.
        /// </summary>
        /// <param name="id">The rule ID.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The rule cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("rules/{id}")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(RuleInfo), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetRuleAsync(int id, bool update = false)
        {
            try
            {
                _logger?.LogDebug($"GetRuleAsync({id})...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (rule, status) = await _zipato.DataReadRuleAsync(id);

                    if (status.IsGood)
                    {
                        return Ok(rule);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var rule = _zipato.GetRule(id);

                    if ((rule != null) && rule.Id.HasValue)
                    {
                        return Ok(rule);
                    }
                    else
                    {
                        return NotFound($"Rule with id '{id}' not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato scene info.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("scenes")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(List<SceneInfo>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetScenesFullAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetScenesFullAsync()...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (scenes, status) = await _zipato.DataReadScenesFullAsync();

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
                    return Ok(_zipato.Data.Scenes);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato scene info.
        /// </summary>
        /// <param name="uuid">The scene identifier.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The network cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("scenes/{uuid}")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(SceneInfo), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetSceneAsync(Guid uuid, bool update = false)
        {
            try
            {
                _logger?.LogDebug($"GetSceneAsync({uuid})...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (scene, status) = await _zipato.DataReadSceneAsync(uuid);

                    if (status.IsGood)
                    {
                        return Ok(scene);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var scene = _zipato.GetScene(uuid);

                    if ((scene != null) && scene.Uuid.HasValue)
                    {
                        return Ok(scene);
                    }
                    else
                    {
                        return NotFound($"Scene with uuid '{uuid}' not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato schedule info.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("schedules")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(List<ScheduleInfo>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetSchedulesFullAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetSchedulesFullAsync()...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (schedules, status) = await _zipato.DataReadSchedulesFullAsync();

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
                    return Ok(_zipato.Data.Schedules);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato schedule info.
        /// </summary>
        /// <param name="uuid">The schedule UUID.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The schedule cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("schedules/{uuid}")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(ScheduleInfo), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetScheduleAsync(Guid uuid, bool update = false)
        {
            try
            {
                _logger?.LogDebug($"GetScheduleAsync({uuid})...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (schedule, status) = await _zipato.DataReadScheduleAsync(uuid);

                    if (status.IsGood)
                    {
                        return Ok(schedule);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var schedule = _zipato.GetSchedule(uuid);

                    if ((schedule != null) && schedule.Uuid.HasValue)
                    {
                        return Ok(schedule);
                    }
                    else
                    {
                        return NotFound($"Schedule with uuid '{uuid}' not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato thermostat info.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("thermostats")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(List<ThermostatInfo>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetThermostatsFullAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetThermostatsFullAsync()...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (thermostats, status) = await _zipato.DataReadThermostatsFullAsync();

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
                    return Ok(_zipato.Data.Thermostats);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato thermostat info.
        /// </summary>
        /// <param name="uuid">The thermostat UUID.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The thermostat cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("thermostats/{uuid}")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(ThermostatInfo), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetThermostatAsync(Guid uuid, bool update = false)
        {
            try
            {
                _logger?.LogDebug($"GetThermostatAsync({uuid})...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (thermostat, status) = await _zipato.DataReadThermostatAsync(uuid);

                    if (status.IsGood)
                    {
                        return Ok(thermostat);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var thermostat = _zipato.GetThermostat(uuid);

                    if ((thermostat != null) && thermostat.Uuid.HasValue)
                    {
                        return Ok(thermostat);
                    }
                    else
                    {
                        return NotFound($"Thermostat with uuid '{uuid}' not found.");
                    }
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
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(List<VirtualEndpointInfo>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(string), 501)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetVirtualEndpointsFullAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetVirtualEndpointsFullAsync()...");

                if (_zipato.IsLocal)
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "Not implemented in local connection.");
                }

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (endpoints, status) = await _zipato.DataReadVirtualEndpointsFullAsync();

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
                    return Ok(_zipato.Data.VirtualEndpoints);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato virtual endpoint info.
        /// </summary>
        /// <param name="uuid">The virtual endpoint UUID.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The virtual endpoint cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("virtualendpoints/{uuid}")]
        [SwaggerOperation(Tags = new[] { "Zipato Info API" })]
        [ProducesResponseType(typeof(VirtualEndpointInfo), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetVirtualEndpointAsync(Guid uuid, bool update = false)
        {
            try
            {
                _logger?.LogDebug($"GetVirtualEndpointAsync({uuid})...");

                if (!_zipato.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var (endpoint, status) = await _zipato.DataReadVirtualEndpointAsync(uuid);

                    if (status.IsGood)
                    {
                        return Ok(endpoint);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var endpoint = _zipato.GetVirtualEndpoint(uuid);

                    if ((endpoint != null) && endpoint.Uuid.HasValue)
                    {
                        return Ok(endpoint);
                    }
                    else
                    {
                        return NotFound($"Virtual endpoint with uuid '{uuid}' not found.");
                    }
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
