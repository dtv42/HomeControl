// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WallboxClient.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace WallboxLib
{
    #region Using Directives

    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using BaseClassLib;
    using WallboxLib.Models;

    #endregion

    public class WallboxClient : BaseClass, IWallboxClient
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the UDP host name.
        /// </summary>
        public string HostName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the UDP port number.
        /// </summary>
        public int Port { get; set; } = 7090;

        /// <summary>
        /// Gets or sets the UDP receive timeout in seconds.
        /// </summary>
        public double Timeout { get; set; } = 10;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WallboxClient"/> class.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="logger"></param>
        public WallboxClient(ISettingsData settings,
                             ILogger<WallboxClient> logger) : base(logger)
        {
            HostName = settings.HostName;
            Port = settings.Port;
            Timeout = settings.Timeout;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Helper method to send a request and return the response as a string.
        /// </summary>
        /// <param name="message">The UDP message</param>
        /// <returns>The string result.</returns>
        public async Task<string> SendReceiveAsync(string message)
        {
            try
            {
                var bytes = Encoding.ASCII.GetBytes(message);
                var client = new UdpClient(Port);
                client.Connect(HostName, Port);
                var count = await client.SendAsync(bytes, bytes.Length);

                if (count == bytes.Length)
                {
                    var timeToWait = TimeSpan.FromSeconds(Timeout);
                    var asyncResult = client.BeginReceive(null, null);
                    asyncResult.AsyncWaitHandle.WaitOne(timeToWait);
                    if (asyncResult.IsCompleted)
                    {
                        IPEndPoint remoteEP = null;
                        bytes = client.EndReceive(asyncResult, ref remoteEP);
                        client.Close();
                        return Encoding.ASCII.GetString(bytes);
                    }
                    else
                    {
                        _logger?.LogError("SendReceiveAsync timeout receiving.");
                    }
                }
                else
                {
                    _logger?.LogError("SendReceiveAsync not all bytes sent.");
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError("SendReceiveAsync exception: {0}.", ex.Message);
            }

            return string.Empty;
        }

        #endregion
    }
}
