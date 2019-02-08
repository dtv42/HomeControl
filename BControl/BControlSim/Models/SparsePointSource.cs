// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SparsePointSource.cs" company="DTV-Online">
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
    using System.Collections.Generic;

    using NModbus;

    #endregion

    /// <summary>
    /// Sparse storage for points.
    /// </summary>
    public class SparsePointSource<TPoint> : IPointSource<TPoint>
    {
        #region Private Data Members

        private readonly Dictionary<ushort, TPoint> _values = new Dictionary<ushort, TPoint>();

        #endregion

        #region Public Events

        public event EventHandler<StorageEventArgs<TPoint>> StorageOperationOccurred;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the value of an individual point wih tout
        /// </summary>
        /// <param name="registerIndex"></param>
        /// <returns></returns>
        public TPoint this[ushort registerIndex]
        {
            get
            {
                TPoint value;

                if (_values.TryGetValue(registerIndex, out value))
                    return value;

                return default(TPoint);
            }
            set { _values[registerIndex] = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startAddress"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public TPoint[] ReadPoints(ushort startAddress, ushort numberOfPoints)
        {
            var points = new TPoint[numberOfPoints];

            for (ushort index = 0; index < numberOfPoints; index++)
            {
                points[index] = this[(ushort)(index + startAddress)];
            }

            StorageOperationOccurred?.Invoke(this,
                new StorageEventArgs<TPoint>(PointOperation.Read, startAddress, points));

            return points;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startAddress"></param>
        /// <param name="points"></param>
        public void WritePoints(ushort startAddress, TPoint[] points)
        {
            for (ushort index = 0; index < points.Length; index++)
            {
                this[(ushort)(index + startAddress)] = points[index];
            }

            StorageOperationOccurred?.Invoke(this,
                new StorageEventArgs<TPoint>(PointOperation.Write, startAddress, points));
        }

        #endregion
    }
}
