// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WallboxController.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace WallboxWeb.Controllers
{
    #region Using Directives

    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Swashbuckle.AspNetCore.Annotations;

    using BaseClassLib;
    using DataValueLib;
    using WallboxLib;
    using WallboxLib.Models;
    using WallboxWeb.Models;

    using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// The Wallbox controller for reading Wallbox data items.
    /// </summary>
    [Route("api/wallbox")]
    [Produces("application/json")]
    public class WallboxController : BaseController<AppSettings>
    {
        #region Private Fields

        private readonly IWallbox _wallbox;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WallboxController"/> class.
        /// </summary>
        /// <param name="wallbox">The Wallbox instance.</param>
        /// <param name="options">The application options.</param>
        /// <param name="logger">The application logger.</param>
        public WallboxController(IWallbox wallbox,
                                 IOptions<AppSettings> options,
                                 ILogger<WallboxController> logger)
            : base(logger, options)
        {
            _wallbox = wallbox;
        }

        #endregion Constructors

        #region REST Methods

        /// <summary>
        /// Returns flag indicating that the data have been sucessfully initialized.
        /// </summary>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        [HttpGet("/api/isinitialized")]
        [SwaggerOperation(Tags = new[] { "Wallbox API" })]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult GetIsInitialized()
        {
            _logger?.LogDebug("GetIsInitialized()...");
            return Ok(_wallbox?.IsInitialized);
        }

        /// <summary>
        /// Returns all Wallbox related data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("all")]
        [SwaggerOperation(Tags = new[] { "Wallbox API" })]
        [ProducesResponseType(typeof(WallboxData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetWallboxData(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetWallboxData()...");

                if (!_wallbox.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    var status = await _wallbox.ReadAllAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                return Ok(_wallbox.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of Wallbox data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("report1")]
        [SwaggerOperation(Tags = new[] { "Wallbox API" })]
        [ProducesResponseType(typeof(Report1Data), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetReport1Data(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetReport1Data()...");

                if (!_wallbox.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    await _wallbox.ReadReport1Async();

                    if (!_wallbox.Report1.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _wallbox.Report1.Status);
                    }
                }

                return Ok(_wallbox.Report1);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of Wallbox data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("report2")]
        [SwaggerOperation(Tags = new[] { "Wallbox API" })]
        [ProducesResponseType(typeof(Report2Data), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetReport2Data(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetReport2Data()...");

                if (!_wallbox.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    await _wallbox.ReadReport2Async();

                    if (!_wallbox.Report2.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _wallbox.Report2.Status);
                    }
                }

                return Ok(_wallbox.Report2);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of Wallbox data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("report3")]
        [SwaggerOperation(Tags = new[] { "Wallbox API" })]
        [ProducesResponseType(typeof(Report1Data), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetReport3Data(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetReport3Data()...");

                if (!_wallbox.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    await _wallbox.ReadReport3Async();

                    if (!_wallbox.Report3.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _wallbox.Report3.Status);
                    }
                }

                return Ok(_wallbox.Report3);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of Wallbox data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("report100")]
        [SwaggerOperation(Tags = new[] { "Wallbox API" })]
        [ProducesResponseType(typeof(ReportsData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetReport100Data(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetReport100Data()...");

                if (!_wallbox.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    await _wallbox.ReadReport100Async();

                    if (!_wallbox.Report100.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _wallbox.Report100.Status);
                    }
                }

                return Ok(_wallbox.Report100);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of Wallbox data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("reports")]
        [SwaggerOperation(Tags = new[] { "Wallbox API" })]
        [ProducesResponseType(typeof(List<ReportsData>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetReportsData(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetReportsData()...");

                if (!_wallbox.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    await _wallbox.ReadReportsAsync();

                    if (!_wallbox.Data.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _wallbox.Data.Status);
                    }
                }

                return Ok(_wallbox.Reports);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of Wallbox data.
        /// </summary>
        /// <param name="id">The report id (101 - 130).</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="404">Not found error (index out of range).</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("reports/{id}")]
        [SwaggerOperation(Tags = new[] { "Wallbox API" })]
        [ProducesResponseType(typeof(List<ReportsData>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetReportsData(ushort id, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetReportsData()...");

                if ((id < 101) || (id >= 130))
                {
                    return NotFound($"Report ID '{id}' not supported.");
                }

                if (!_wallbox.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    await _wallbox.ReadReportAsync(id);

                    if (!_wallbox.Data.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _wallbox.Data.Status);
                    }
                }

                return Ok(_wallbox.Reports[id - Wallbox.REPORTS_ID - 1]);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a single Wallbox property.
        /// </summary>
        /// <remarks>The property name is a CamelCase name.</remarks>
        /// <param name="name">The name of the property.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">If the property is invalid.</response>
        /// <response code="404">If the property cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("property/{name}")]
        [SwaggerOperation(Tags = new[] { "Wallbox API" })]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetWallboxData(string name, bool update = false)
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger?.LogDebug($"GetWallboxData() invalid property.");
                return StatusCode(StatusCodes.Status400BadRequest, $"Property is invalid.");
            }

            try
            {
                _logger?.LogDebug($"GetWallboxData({name})...");

                if (Wallbox.IsProperty(name))
                {
                    if (!_wallbox.IsInitialized)
                    {
                        return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                    }

                    if (update)
                    {
                        var status = await _wallbox.ReadPropertyAsync(name);

                        if (!status.IsGood)
                        {
                            return StatusCode(StatusCodes.Status502BadGateway, status);
                        }
                    }

                    return Ok(_wallbox.GetPropertyValue(name));
                }
                else
                {
                    _logger?.LogDebug($"GetWallboxData('{name}') property not found.");
                    return NotFound($"Property '{name}' not found.");
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
