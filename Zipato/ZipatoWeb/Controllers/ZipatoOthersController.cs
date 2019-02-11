// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZipatoOthersController.cs" company="DTV-Online">
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
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Swashbuckle.AspNetCore.Annotations;

    using BaseClassLib;
    using DataValueLib;
    using ZipatoLib;
    using ZipatoLib.Models;
    using ZipatoLib.Models.Data;
    using ZipatoLib.Models.Others;
    using ZipatoWeb.Models;

    using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;
    using System.Linq;

    #endregion

    /// <summary>
    /// The Zipato controller for reading other Zipato data.
    /// </summary>
    [Route("api/others")]
    [Produces("application/json")]
    public class ZipatoOthersController : BaseController<AppSettings>
    {
        #region Private Fields

        private readonly IZipato _zipato;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipatoOthersController"/> class.
        /// </summary>
        /// <param name="zipato">The Zipato instance.</param>
        /// <param name="options">The application options.</param>
        /// <param name="logger">The application logger.</param>
        public ZipatoOthersController(IZipato zipato,
                                      IOptions<AppSettings> options,
                                      ILogger<ZipatoOthersController> logger)
            : base(logger, options)
        {
            _zipato = zipato;
        }

        #endregion Constructors

        #region REST Methods

        /// <summary>
        /// Returns Zipato others data.
        /// </summary>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("")]
        [SwaggerOperation(Tags = new[] { "Zipato Others API" })]
        [ProducesResponseType(typeof(ZipatoOthers), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult GetOthers()
        {
            try
            {
                _logger?.LogDebug("GetOthers()...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                return Ok(_zipato.Others);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato cameras data.
        /// </summary>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("camera")]
        [SwaggerOperation(Tags = new[] { "Zipato Others API" })]
        [ProducesResponseType(typeof(List<Camera>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult GetCameras()
        {
            try
            {
                _logger?.LogDebug("GetCameras()...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                return Ok(_zipato.Sensors.VirtualMeters);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato camera data.
        /// </summary>
        /// <param name="index">The camera index.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("camera/{index}")]
        [SwaggerOperation(Tags = new[] { "Zipato Others API" })]
        [ProducesResponseType(typeof(Camera), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult GetCamera(int index)
        {
            try
            {
                _logger?.LogDebug("GetCamera()...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                var count = _zipato.Others.Scenes.Count;

                if ((index < 0) || (index >= count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(count == 0 ? "[0]" : "[0] - [" + (count - 1))}]).");
                }

                return Ok(_zipato.Sensors.VirtualMeters[index]);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato camera saved files.
        /// </summary>
        /// <param name="index">The camera index.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("camera/{index}/files")]
        [SwaggerOperation(Tags = new[] { "Zipato Others API" })]
        [ProducesResponseType(typeof(List<FileData>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult GetCameraFiles(int index)
        {
            try
            {
                _logger?.LogDebug("GetCameraFiles()...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                var count = _zipato.Others.Cameras.Count;

                if ((index < 0) || (index >= count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(count == 0 ? "[0]" : "[0] - [" + (count - 1))}]).");
                }

                var uuid = _zipato.Others.Cameras[index].Uuid;

                return Ok(_zipato.Data.SavedFiles[uuid]);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato a single camera saved files.
        /// </summary>
        /// <param name="index">The camera index.</param>
        /// <param name="file">The file index.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("camera/{index}/files/{file}")]
        [SwaggerOperation(Tags = new[] { "Zipato Others API" })]
        [ProducesResponseType(typeof(FileData), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult GetCameraFile(int index, int file)
        {
            try
            {
                _logger?.LogDebug("GetCameraFile()...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                var count = _zipato.Others.Cameras.Count;

                if ((index < 0) || (index >= count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(count == 0 ? "[0]" : "[0] - [" + (count - 1))}]).");
                }

                var uuid = _zipato.Others.Cameras[index].Uuid;
                var files = _zipato.Others.Cameras[index].Files;

                if ((file < 0) || (file >= files.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"File Index {index} not valid ({(files.Count == 0 ? "[0]" : "[0] - [" + (files.Count - 1))}]).");
                }

                return Ok(_zipato.Data.SavedFiles[uuid]);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Deletes all camera saved files.
        /// </summary>
        /// <param name="index">The camera index.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">The operation was successful.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpDelete("camera/{index}/files")]
        [SwaggerOperation(Tags = new[] { "Zipato Others API" })]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> DeleteSavedFiles(int index)
        {
            try
            {
                _logger?.LogDebug($"DeleteSavedFile({index})...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                var count = _zipato.Others.Cameras.Count;

                if ((index < 0) || (index >= count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(count == 0 ? "[0]" : "[0] - [" + (count - 1))}]).");
                }

                var uuid = _zipato.Others.Cameras[index].Uuid;
                var files = _zipato.Others.Cameras[index].Files.Select(f => f.Id).ToList();

                if (files.Count > 0)
                {
                    var status = await _zipato.DeleteFilesBatchAsync(files);

                    if (status.IsGood)
                    {
                        _zipato.Data.SavedFiles[uuid] = new List<FileData> { };
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _zipato.Data.Status);
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Deletes a single camera saved files.
        /// </summary>
        /// <param name="index">The camera index.</param>
        /// <param name="file">The file index.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">The operation was successful.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpDelete("camera/{index}/files/{file}")]
        [SwaggerOperation(Tags = new[] { "Zipato Others API" })]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> DeleteSavedFile(int index, int file)
        {
            try
            {
                _logger?.LogDebug($"DeleteSavedFile({index}, {file})...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                var count = _zipato.Others.Cameras.Count;

                if ((index < 0) || (index >= count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(count == 0 ? "[0]" : "[0] - [" + (count - 1))}]).");
                }

                var uuid = _zipato.Others.Cameras[index].Uuid;
                var files = _zipato.Others.Cameras[index].Files;

                if ((file < 0) || (file >= files.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"File Index {index} not valid ({(files.Count == 0 ? "[0]" : "[0] - [" + (files.Count - 1))}]).");
                }

                var status = await _zipato.DeleteSavedFileAsync(files[file].Id);

                if (status.IsGood)
                {
                    files.RemoveAt(file);
                    return Ok();
                }
                else
                {
                    return StatusCode(StatusCodes.Status502BadGateway, _zipato.Data.Status);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Takes a snapshot.
        /// </summary>
        /// <param name="index">The camera index.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpPut("camera/{index}/snapshot")]
        [SwaggerOperation(Tags = new[] { "Zipato Others API" })]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult SetTakeSnapshotAsync(int index)
        {
            try
            {
                _logger?.LogDebug("GetTakeSnapshotAsync()...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                var count = _zipato.Others.Cameras.Count;

                if ((index < 0) || (index >= count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(count == 0 ? "[0]" : "[0] - [" + (count - 1))}]).");
                }

                return Ok(_zipato.Others.Cameras[index].TakeSnapshot());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Takes a 10 sec recording.
        /// </summary>
        /// <param name="index">The camera index.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpPut("camera/{index}/recording")]
        [SwaggerOperation(Tags = new[] { "Zipato Others API" })]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult SetTakeRecordingAsync(int index)
        {
            try
            {
                _logger?.LogDebug("GetTakeRecordAsync()...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                var count = _zipato.Others.Cameras.Count;

                if ((index < 0) || (index >= count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(count == 0 ? "[0]" : "[0] - [" + (count - 1))}]).");
                }

                return Ok(_zipato.Others.Cameras[index].TakeRecording());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato scenes data.
        /// </summary>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("scene")]
        [SwaggerOperation(Tags = new[] { "Zipato Others API" })]
        [ProducesResponseType(typeof(List<Scene>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult GetScenes()
        {
            try
            {
                _logger?.LogDebug("GetScenes()...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                return Ok(_zipato.Others.Scenes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato scene data.
        /// </summary>
        /// <param name="index">The scene index.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("scene/{index}")]
        [SwaggerOperation(Tags = new[] { "Zipato Others API" })]
        [ProducesResponseType(typeof(Scene), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult GetScene(int index)
        {
            try
            {
                _logger?.LogDebug("GetScene()...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                var count = _zipato.Others.Scenes.Count;

                if ((index < 0) || (index >= count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(count == 0 ? "[0]" : "[0] - [" + (count - 1))}]).");
                }

                return Ok(_zipato.Others.Scenes[index]);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Runs Zipato scene.
        /// </summary>
        /// <param name="index">The scene index.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpPut("scene/{index}/run")]
        [SwaggerOperation(Tags = new[] { "Zipato Others API" })]
        [ProducesResponseType(typeof(ValueInfo<bool>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult SetSceneRun(int index)
        {
            try
            {
                _logger?.LogDebug("GetSceneRun()...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                var count = _zipato.Others.Scenes.Count;

                if ((index < 0) || (index >= count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(count == 0 ? "[0]" : "[0] - [" + (count - 1))}]).");
                }

                return Ok(_zipato.Others.Scenes[index].Run());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion REST Methods
    }
}
