// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InfoCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace BControlApp.Commands
{
    #region Using Directives

    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using McMaster.Extensions.CommandLineUtils;
    using Newtonsoft.Json;

    using CommandLine.Core.Hosting.Abstractions;

    using BaseClassLib;
    using BControlLib;
    using BControlApp.Models;

    #endregion

    /// <summary>
    /// Application command "info".
    /// </summary>
    [Command(Name = "info",
             FullName = "BControl Info Command",
             Description = "Reading data values from a TQ energy meter.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class InfoCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly IBControl _bcontrol;

        #endregion

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="RootCommand"/>.
        /// </summary>
        private RootCommand Parent { get; set; }

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions { get => !(OptionA || OptionI || OptionE || OptionP || OptionS); }

        #endregion

        #region Public Properties

        [Option("-a|--all", "Gets all data.", CommandOptionType.NoValue)]
        public bool OptionA { get; set; }

        [Option("-i|--internal", "Gets the Internal data.", CommandOptionType.NoValue)]
        public bool OptionI { get; set; }

        [Option("-e|--energy", "Gets the energy data.", CommandOptionType.NoValue)]
        public bool OptionE { get; set; }

        [Option("-p|--pnp", "Gets the pnp data.", CommandOptionType.NoValue)]
        public bool OptionP { get; set; }

        [Option("-s|--sunspec", "Gets the SunSpec data.", CommandOptionType.NoValue)]
        public bool OptionS { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InfoCommand"/> class.
        /// </summary>
        /// <param name="bcontrol">The BControl instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public InfoCommand(IBControl bcontrol,
                           ILogger<InfoCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("InfoCommand()");

            // Setting the BControl instance.
            _bcontrol = bcontrol;
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
                    // Overriding BControl options.
                    _bcontrol.TcpSlave.Address = Parent.Address;
                    _bcontrol.TcpSlave.Port = Parent.Port;
                    _bcontrol.TcpSlave.ID = Parent.SlaveID;

                    await _bcontrol.ReadBlockAsync();

                    if (OptionA)
                    {
                        Console.WriteLine($"Data: {JsonConvert.SerializeObject(_bcontrol.Data, Formatting.Indented)}");
                    }

                    if (OptionI)
                    {
                        Console.WriteLine($"Internal: {JsonConvert.SerializeObject(_bcontrol.InternalData, Formatting.Indented)}");
                    }

                    if (OptionE)
                    {
                        Console.WriteLine($"Energy: {JsonConvert.SerializeObject(_bcontrol.EnergyData, Formatting.Indented)}");
                    }

                    if (OptionP)
                    {
                        Console.WriteLine($"PnP: {JsonConvert.SerializeObject(_bcontrol.PnPData, Formatting.Indented)}");
                    }

                    if (OptionS)
                    {
                        Console.WriteLine($"SunSpec: {JsonConvert.SerializeObject(_bcontrol.SunSpecData, Formatting.Indented)}");
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
            if (NoOptions)
            {
                Console.WriteLine($"Select an info option.");
                return false;
            }

            return true;
        }

        #endregion
    }
}
