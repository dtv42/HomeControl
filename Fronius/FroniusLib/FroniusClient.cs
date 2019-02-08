namespace FroniusLib
{
    #region Using Directives

    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using BaseClassLib;

    #endregion

    public class FroniusClient : BaseClass, IFroniusClient
    {
        #region Private Data Members

        private HttpClient _client;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the base address of the Internet resource used when sending requests.
        /// </summary>
        public string BaseAddress
        {
            get => _client.BaseAddress.OriginalString;
            set => _client.BaseAddress = new Uri(value);
        }

        /// <summary>
        /// Gets or sets the timespan to wait before the request times out.
        /// </summary>
        public int Timeout
        {
            get => (int)_client.Timeout.TotalSeconds;
            set => _client.Timeout = TimeSpan.FromSeconds(value);
        }

        /// <summary>
        /// Gets or sets the number of retries sending a request.
        /// </summary>
        public int Retries { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FroniusClient"/> class.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="logger"></param>
        public FroniusClient(HttpClient client,
                             ILogger<FroniusClient> logger) : base(logger)
        {
            _client = client;
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("User-Agent", "FroniusClient");
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Helper method to perform a GET request and return the response as a string.
        /// </summary>
        /// <param name="request">The HTTP request</param>
        /// <returns>The string result.</returns>
        public async Task<string> GetStringAsync(string request)
            => await _client.GetStringAsync(request);

        #endregion
    }
}
