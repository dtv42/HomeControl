// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Scene.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Others
{
    #region Using Directives

    using System;

    #endregion

    /// <summary>
    /// This class represents a Zipato scene.
    /// </summary>
    public class Scene
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
        /// The UUID of the scene.
        /// </summary>
        public Guid Uuid { get; }
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Scene"/> class.
        /// </summary>
        public Scene()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Scene"/> class.
        /// </summary>
        /// <remarks>Requires a valid list of endpoints, attributes, and values.</remarks>
        /// <param name="zipato"></param>
        /// <param name="uuid"></param>
        public Scene(IZipato zipato, Guid uuid)
        {
            _zipato = zipato;
            Uuid = uuid;
            Name = zipato.GetScene(uuid)?.Name;
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
        /// Runs the scene.
        /// </summary>
        /// <returns>True if successful.</returns>
        public bool Run()
        {
            var (data, state) = _zipato.ReadSceneRunAsync(Uuid).Result;

            return (state.IsGood);
        }

        #endregion

    }
}
