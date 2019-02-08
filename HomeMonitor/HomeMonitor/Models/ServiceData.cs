// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeMonitor.Models
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    internal class ServiceData
    {
        #region Protected Properties

        protected HttpClient _client { get; set; }

        #endregion

        #region Public Properties

        public string Address
        {
            get => _client.BaseAddress.OriginalString;
            set
            {
                if (value != _client?.BaseAddress.OriginalString)
                {
                    if (_client != null)
                    {
                        _client.Dispose();
                    }

                    _client = new HttpClient()
                    {
                        BaseAddress = new Uri(value)
                    };
                }
            }
        }

        public string Message { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public List<RowData> Rows { get; set; } = new List<RowData> { };

        #endregion
    }
}
