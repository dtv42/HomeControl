// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SlaveStorage.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace KWLEC200Sim.Models
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    using NModbus;

    #endregion

    /// <summary>
    /// Helper class for providing a NModbus data store.
    /// </summary>
    public class SlaveStorage : ISlaveDataStore
    {
        #region Private Data Members

        private readonly SparsePointSource<bool> _coilDiscretes;
        private readonly SparsePointSource<bool> _coilInputs;
        private readonly SparsePointSource<ushort> _holdingRegisters;
        private readonly SparsePointSource<ushort> _inputRegisters;

        #endregion

        #region Constructors

        public SlaveStorage()
        {
            _coilDiscretes = new SparsePointSource<bool>();
            _coilInputs = new SparsePointSource<bool>();
            _holdingRegisters = new SparsePointSource<ushort>();
            _inputRegisters = new SparsePointSource<ushort>();
        }

        #endregion

        #region Public Properties

        public SparsePointSource<bool> CoilDiscretes { get => _coilDiscretes; }
        public SparsePointSource<bool> CoilInputs { get => _coilInputs; }
        public SparsePointSource<ushort> HoldingRegisters { get => _holdingRegisters; }
        public SparsePointSource<ushort> InputRegisters { get => _inputRegisters; }
        IPointSource<bool> ISlaveDataStore.CoilDiscretes { get => _coilDiscretes; }
        IPointSource<bool> ISlaveDataStore.CoilInputs { get => _coilInputs; }
        IPointSource<ushort> ISlaveDataStore.HoldingRegisters { get => _holdingRegisters; }
        IPointSource<ushort> ISlaveDataStore.InputRegisters { get => _inputRegisters; }

        #endregion

    }
}