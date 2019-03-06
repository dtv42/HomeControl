// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseHub.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControl2App.Models
{
    #region Using Directives

    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.Extensions.Logging;

    #endregion

    /// <summary>
    /// The INotifyPropertyChanged interface is used to notify binding clients that a property value has changed.
    /// </summary>
    public class BaseHub : INotifyPropertyChanged
    {
        #region Private Data Members

        private bool _isconnected = false;
        private string _message = string.Empty;

        #endregion

        #region Protected Data Members

        protected readonly ILogger<BaseHub> _logger;
        protected readonly IHubSettings _settings;
        protected readonly HubConnection _hub;
        protected readonly string _url;

        #endregion

        #region Public Properties

        public string Message
        {
            get { return _message; }
            set { _message = value; NotifyPropertyChanged(); }
        }

        public bool IsConnected
        {
            get { return _isconnected; }
            set { _isconnected = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="options"></param>
        public BaseHub(ILogger<BaseHub> logger, IHubSettings settings)
        {
            _logger = logger;
            _settings = settings;
            _logger.LogDebug("BaseHub()");

            if (string.IsNullOrEmpty(settings?.Uri))
            {
                _logger.LogDebug("Error: Invalid hub settings.");
                throw new ArgumentException("Invalid hub settings.");
            }

            _hub = new HubConnectionBuilder()
                .ConfigureLogging(logging => { logging.SetMinimumLevel(LogLevel.Trace); })
                .WithUrl(_settings.Uri)
                .Build();

            if (_hub is null)
            {
                _logger.LogDebug("Error: Cannot build hub.");
                throw new NullReferenceException("Cannot build hub.");
            }

            _hub.HandshakeTimeout = TimeSpan.FromSeconds(_settings.HandshakeTimeout);
            _hub.ServerTimeout = TimeSpan.FromSeconds(_settings.ServerTimeout);

            _hub.Closed += async (error) =>
            {
                if (error != null)
                {
                    _logger.LogDebug("Hub() has been closed - trying to restart.", error);
                    await Task.Delay(new Random().Next(0, 5) * 1000);
                    await _hub.StartAsync();
                }
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Start the hub connection.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<bool> Connect()
        {
            IsConnected = false;

            try
            {
                if (_hub is null)
                {
                    _logger.LogError("Hub not set.");
                    Message = $"Error: Hub not set at {(DateTime.Now.ToString("HH: mm: ss"))}.";
                    return false;
                }
                else
                {
                    if (_hub.State != HubConnectionState.Connected)
                    {
                        await _hub.StartAsync();

                        if (_hub.State == HubConnectionState.Connected)
                        {
                            _logger.LogDebug("Hub connected.");
                            Message = $"Hub connected at {(DateTime.Now.ToString("HH: mm: ss"))}.";
                            IsConnected = true;
                            return true;
                        }
                        else
                        {
                            _logger.LogError("Hub not connected.");
                            Message = $"Hub not connected at {(DateTime.Now.ToString("HH: mm: ss"))}.";
                            return false;
                        }
                    }
                    else
                    {
                        _logger.LogWarning("Hub already connected.");
                        await _hub.InvokeAsync("UpdateData");
                        IsConnected = true;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception in Connect(): {ex.Message}.");
                Message = $"Exception connecting at {(DateTime.Now.ToString("HH: mm: ss"))}.";
                return false;
            }
        }

        /// <summary>
        /// Close the hub connection.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<bool> Disconnect()
        {
            IsConnected = false;

            try
            {
                if (_hub is null)
                {
                    _logger.LogError("Hub not set.");
                    Message = $"Error: Hub not set at {(DateTime.Now.ToString("HH: mm: ss"))}.";
                    return false;
                }
                else
                {
                    if (_hub.State == HubConnectionState.Connected)
                    {
                        await _hub.StopAsync();
                        _logger.LogDebug("Hub disconnected.");
                        Message = $"Hub disconnected at {(DateTime.Now.ToString("HH: mm: ss"))}.";
                        return true;
                    }
                    else
                    {
                        _logger.LogWarning("Hub not connected.");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception in Disconnect(): {ex.Message}.");
                Message = $"Exception disconnecting at {(DateTime.Now.ToString("HH: mm: ss"))}.";
                return false;
            }
        }

        /// <summary>
        /// Event to indicate that a property has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Raise the PropertyChanged event, passing the name of the property whose value has changed.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName != "Message")
            {
                Message = $"Last updated at {(DateTime.Now.ToString("HH:mm:ss"))}.";
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
