// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StorageEventArgs.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace BControlSim.Models
{
    #region Using Directives

    using System;

    #endregion

    public class StorageEventArgs<TPoint> : EventArgs
    {
        #region Private Data Members

        private readonly PointOperation _pointOperation;
        private readonly ushort _startingAddress;
        private readonly TPoint[] _points;

        #endregion

        #region Public Properties

        public ushort StartingAddress { get => _startingAddress; }
        public TPoint[] Points { get => _points; }
        public PointOperation Operation { get => _pointOperation; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pointOperation"></param>
        /// <param name="startingAddress"></param>
        /// <param name="points"></param>
        public StorageEventArgs(PointOperation pointOperation, ushort startingAddress, TPoint[] points)
        {
            _pointOperation = pointOperation;
            _startingAddress = startingAddress;
            _points = points;
        }

        #endregion
    }
}
