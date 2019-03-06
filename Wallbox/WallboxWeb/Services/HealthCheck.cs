namespace WallboxWeb.Services
{
    #region Using Directives

    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.Diagnostics.HealthChecks;

    using BaseClassLib;
    using WallboxLib;
    using WallboxWeb.Models;

    #endregion

    public class HealthCheck : BaseClass<AppSettings>, IHealthCheck
    {
        #region Private Fields

        private readonly IWallbox _wallbox;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthCheck"/> class.
        /// </summary>
        /// <param name="wallbox">The IWallbox instance.</param>
        /// <param name="options">The application options.</param>
        /// <param name="logger">The application logger.</param>
        public HealthCheck(IWallbox wallbox,
                           IOptions<AppSettings> options,
                           ILogger<HealthCheck> logger)
            : base(logger, options)
        {
            _wallbox = wallbox;
        }

        #endregion

        /// <summary>
        /// Checks the current health state.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (_wallbox.Data.IsGood)
            {
                return Task.FromResult(HealthCheckResult.Healthy("The data indicate a healthy result."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("The data indicate an unhealthy result."));
        }
    }
}
