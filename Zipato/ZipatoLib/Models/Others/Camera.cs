// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Camera.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Others
{
    using Newtonsoft.Json;
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using ZipatoLib.Models.Data;

    #endregion

    /// <summary>
    /// This class represents a Zipato camera.
    /// </summary>
    public class Camera
    {
        #region Private Data Members

        private IZipato _zipato;

        #endregion

        #region Public Properties

        /// <summary>
        /// The name of the scene.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The UUID of the camera.
        /// </summary>
        public Guid Uuid { get; }

        /// <summary>
        /// The saved files for the camera.
        /// </summary>
        [JsonIgnore]
        public List<FileData> Files { get => _zipato.Data.SavedFiles[Uuid]; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class.
        /// </summary>
        public Camera()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class.
        /// </summary>
        /// <remarks>Requires a valid list of endpoints, attributes, and values.</remarks>
        /// <param name="zipato"></param>
        /// <param name="uuid"></param>
        public Camera(IZipato zipato, Guid uuid)
        {
            _zipato = zipato;
            Uuid = uuid;
            Name = zipato.GetCamera(uuid)?.Name;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Refreshes the property data (nothing done here).
        /// </summary>
        /// <remarks>Requires a valid list of values.</remarks>
        public void Refresh()
        {
        }

        /// <summary>
        /// Takes a snapshot.
        /// </summary>
        /// <returns>True if successful.</returns>
        public bool TakeSnapshot()
        {
            var (data, state) = _zipato.CameraTakeSnapshotAsync(Uuid).Result;

            if (state.IsGood && data.Success.HasValue)
            {
                return data.Success.Value;
            }

            return false;
        }

        /// <summary>
        /// Takes a 10 sec recording.
        /// </summary>
        /// <returns>True if successful.</returns>
        public bool TakeRecording()
        {
            var (data, state) = _zipato.CameraTakeRecordingAsync(Uuid).Result;

            if (state.IsGood && data.Success.HasValue)
            {
                return data.Success.Value;
            }

            return false;
        }

        #endregion
    }
}
