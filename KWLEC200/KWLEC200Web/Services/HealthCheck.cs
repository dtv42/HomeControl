namespace KWLEC200Web.Services
{
    #region Using Directives

    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.Diagnostics.HealthChecks;

    using BaseClassLib;
    using KWLEC200Lib;
    using KWLEC200Web.Models;

    #endregion

    public class HealthCheck : BaseClass<AppSettings>, IHealthCheck
    {
        #region Private Fields

        private readonly IKWLEC200 _kwlec200;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthCheck"/> class.
        /// </summary>
        /// <param name="kwlec200">The IKWLEC200 instance.</param>
        /// <param name="options">The application options.</param>
        /// <param name="logger">The application logger.</param>
        public HealthCheck(IKWLEC200 kwlec200,
                           IOptions<AppSettings> options,
                           ILogger<HealthCheck> logger)
            : base(logger, options)
        {
            _kwlec200 = kwlec200;
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
            if (_kwlec200.Data.IsGood)
            {
                return Task.FromResult(HealthCheckResult.Healthy("The data indicate a healthy result."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("The data indicate an unhealthy result."));
        }
    }
}
