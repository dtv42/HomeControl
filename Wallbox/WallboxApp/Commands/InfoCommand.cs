// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InfoCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace WallboxApp.Commands
{
    #region Using Directives

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using McMaster.Extensions.CommandLineUtils;
    using Newtonsoft.Json;

    using CommandLine.Core.Hosting.Abstractions;

    using BaseClassLib;
    using WallboxLib;
    using WallboxApp.Models;

    #endregion

    /// <summary>
    /// Application command "info".
    /// </summary>
    [Command(Name = "info",
             FullName = "Wallbox Info Command",
             Description = "Reading data values from BMW Wallbox charging station.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class InfoCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly IWallbox _wallbox;

        #endregion

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="RootCommand"/>.
        /// </summary>
        private RootCommand Parent { get; set; }

        /// <summary>
        /// Command options.
        /// </summary>
        private bool OptionI { get; set; }

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions { get => !(Option1 || Option2 || Option3 || OptionL || OptionR || OptionI); }

        #endregion

        #region Public Properties

        [Option("-1|--report1", "Gets the report 1 data.", CommandOptionType.NoValue)]
        public bool Option1 { get; set; }

        [Option("-2|--report2", "Gets the report 2 data.", CommandOptionType.NoValue)]
        public bool Option2 { get; set; }

        [Option("-3|--report3", "Gets the report 3 data.", CommandOptionType.NoValue)]
        public bool Option3 { get; set; }

        [Option("-l|--last", "Gets the last charging report data.", CommandOptionType.NoValue)]
        public bool OptionL { get; set; }

        [Option("-r|--reports", "Gets all charging reports.", CommandOptionType.NoValue)]
        public bool OptionR { get; set; }

        [Option("-i|--id <number>", "Gets a single report.", CommandOptionType.SingleValue)]
        public int ReportID { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InfoCommand"/> class.
        /// </summary>
        /// <param name="wallbox">The Wallbox instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public InfoCommand(IWallbox wallbox,
                           ILogger<InfoCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("InfoCommand()");

            // Setting the Wallbox instance.
            _wallbox = wallbox;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to run when command is executed.
        /// </summary>
        /// <returns>Zero if ok.</returns>
        public async Task<int> OnExecuteAsync(CommandLineApplication app)
        {
            try
            {
                if (CheckOptions(app))
                {
                    // Overriding Wallbox options.
                    _wallbox.HostName = Parent.HostName;
                    _wallbox.Port = Parent.Port;

                    if (Option1)
                    {
                        await _wallbox.ReadReport1Async();
                        Console.WriteLine($"Report1: {JsonConvert.SerializeObject(_wallbox.Report1, Formatting.Indented)}");
                    }

                    if (Option2)
                    {
                        await _wallbox.ReadReport2Async();
                        Console.WriteLine($"Report2: {JsonConvert.SerializeObject(_wallbox.Report2, Formatting.Indented)}");
                    }

                    if (Option3)
                    {
                        await _wallbox.ReadReport3Async();
                        Console.WriteLine($"Report3: {JsonConvert.SerializeObject(_wallbox.Report3, Formatting.Indented)}");
                    }

                    if (OptionL)
                    {
                        await _wallbox.ReadReport100Async();
                        Console.WriteLine($"Report100: {JsonConvert.SerializeObject(_wallbox.Report100, Formatting.Indented)}");
                    }

                    if (OptionR)
                    {
                        await _wallbox.ReadReportsAsync();
                        Console.WriteLine($"Reports: {JsonConvert.SerializeObject(_wallbox.Reports, Formatting.Indented)}");
                    }

                    if (OptionI)
                    {
                        switch(ReportID)
                        {
                            case 1:
                                await _wallbox.ReadReport1Async();
                                Console.WriteLine($"Report: {JsonConvert.SerializeObject(_wallbox.Report1, Formatting.Indented)}");
                                break;
                            case 2:
                                await _wallbox.ReadReport2Async();
                                Console.WriteLine($"Report: {JsonConvert.SerializeObject(_wallbox.Report2, Formatting.Indented)}");
                                break;
                            case 3:
                                await _wallbox.ReadReport3Async();
                                Console.WriteLine($"Report: {JsonConvert.SerializeObject(_wallbox.Report3, Formatting.Indented)}");
                                break;
                            case 100:
                                await _wallbox.ReadReport100Async();
                                Console.WriteLine($"Report: {JsonConvert.SerializeObject(_wallbox.Report100, Formatting.Indented)}");
                                break;
                            case int id when ((id > Wallbox.REPORTS_ID) && (id <= (Wallbox.MAX_REPORTS + Wallbox.REPORTS_ID))):
                                await _wallbox.ReadReportAsync(id);
                                Console.WriteLine($"Report: {JsonConvert.SerializeObject(_wallbox.Reports[id - Wallbox.REPORTS_ID - 1], Formatting.Indented)}");
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception InfoCommand.");
                return -1;
            }

            return 0;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Helper method to check options.
        /// </summary>
        /// <param name="app"></param>
        /// <returns>True if options are OK.</returns>
        private bool CheckOptions(CommandLineApplication app)
        {
            // Getting additional option flags.
            var options = app.GetOptions().ToList();

            foreach (var option in options)
            {
                switch (option.LongName)
                {
                    case "id": OptionI = option.HasValue(); break;
                }
            }

            if (NoOptions)
            {
                _logger?.LogError($"Select an info option.");
                return false;
            }

            if (OptionI)
            {
                switch (ReportID)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 100:
                    case int id when ((id > Wallbox.REPORTS_ID) && (id <= (Wallbox.MAX_REPORTS + Wallbox.REPORTS_ID))):
                        return true;
                    default:
                        _logger?.LogError($"Report ID {ReportID} not supported.");
                        return false;
                }
            }

            return true;
        }

        #endregion
    }
}
