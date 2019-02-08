// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FroniusController.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace EM300LRWeb.Controllers
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
    using EM300LRLib;
    using EM300LRLib.Models;
    using EM300LRWeb.Models;

    using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

    #endregion Using Directives

    /// <summary>
    /// The EM300LR controller for reading EM300LR data items.
    /// </summary>
    [Route("api/em300lr")]
    [Produces("application/json")]
    public class EM300LRController : BaseController<AppSettings>
    {
        #region Private Fields

        private readonly IEM300LR _em300lr;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EM300LRController"/> class.
        /// </summary>
        /// <param name="em300lr">The EM300LR instance.</param>
        /// <param name="options">The application options.</param>
        /// <param name="logger">The application logger.</param>
        public EM300LRController(IEM300LR em300lr,
                                 IOptions<AppSettings> options,
                                 ILogger<EM300LRController> logger)
            : base(logger, options)
        {
            _em300lr = em300lr;
        }

        #endregion Constructors

        #region REST Methods

        /// <summary>
        /// Returns flag indicating that the data have been sucessfully initialized.
        /// </summary>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        [HttpGet("/api/isinitialized")]
        [SwaggerOperation(Tags = new[] { "EM300LR API" })]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult GetIsInitialized()
        {
            _logger?.LogDebug("GetIsInitialized()...");
            return Ok(_em300lr?.IsInitialized);
        }

        /// <summary>
        /// Returns all EM300LR related data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("all")]
        [SwaggerOperation(Tags = new[] { "EM300LR API" })]
        [ProducesResponseType(typeof(EM300LRData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetEM300LRData(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetEM300LRData()...");

                if (!_em300lr.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    var status = await _em300lr.ReadAllAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                return Ok(_em300lr.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of EM300LR data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("total")]
        [SwaggerOperation(Tags = new[] { "EM300LR API" })]
        [ProducesResponseType(typeof(TotalData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetTotalData(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetTotalData()...");

                if (!_em300lr.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    await _em300lr.ReadAllAsync();

                    if (!_em300lr.TotalData.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _em300lr.TotalData.Status);
                    }
                }

                return Ok(_em300lr.TotalData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of EM300LR data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("phase1")]
        [SwaggerOperation(Tags = new[] { "EM300LR API" })]
        [ProducesResponseType(typeof(Phase1Data), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetPhase1Data(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetPhase1Data()...");

                if (!_em300lr.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    await _em300lr.ReadAllAsync();

                    if (!_em300lr.Phase1Data.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _em300lr.Phase1Data.Status);
                    }
                }

                return Ok(_em300lr.Phase1Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of EM300LR data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("phase2")]
        [SwaggerOperation(Tags = new[] { "EM300LR API" })]
        [ProducesResponseType(typeof(Phase2Data), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetPhase2Data(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetPhase2Data()...");

                if (!_em300lr.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    await _em300lr.ReadAllAsync();

                    if (!_em300lr.Phase2Data.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _em300lr.Phase2Data.Status);
                    }
                }

                return Ok(_em300lr.Phase2Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of EM300LR data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("phase3")]
        [SwaggerOperation(Tags = new[] { "EM300LR API" })]
        [ProducesResponseType(typeof(Phase3Data), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetPhase3Data(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetPhase3Data()...");

                if (!_em300lr.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    await _em300lr.ReadAllAsync();

                    if (!_em300lr.Phase3Data.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _em300lr.Phase3Data.Status);
                    }
                }

                return Ok(_em300lr.Phase3Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a single EM300LR property.
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
        [SwaggerOperation(Tags = new[] { "EM300LR API" })]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetEM300LRData(string name, bool update = false)
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger?.LogDebug($"GetEM300LRData() invalid property.");
                return StatusCode(StatusCodes.Status400BadRequest, $"Property is invalid.");
            }

            try
            {
                _logger?.LogDebug($"GetEM300LRData({name})...");

                if (EM300LR.IsProperty(name))
                {
                    if (!_em300lr.IsInitialized)
                    {
                        return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                    }

                    if (update)
                    {
                        var status = await _em300lr.ReadAllAsync();

                        if (!status.IsGood)
                        {
                            return StatusCode(StatusCodes.Status502BadGateway, status);
                        }
                    }

                    return Ok(_em300lr.GetPropertyValue(name));
                }
                else
                {
                    _logger?.LogDebug($"GetEM300LRData('{name}') property not found.");
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