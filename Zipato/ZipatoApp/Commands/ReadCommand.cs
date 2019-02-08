// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReadCommand.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoApp.Commands
{
    #region Using Directives

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using CommandLine.Core.Hosting.Abstractions;
    using McMaster.Extensions.CommandLineUtils;
    using Newtonsoft.Json;

    using BaseClassLib;
    using ZipatoLib;
    using ZipatoApp.Models;

    #endregion

    /// <summary>
    /// Application command "read".
    /// </summary>
    [Command(Name = "read",
             FullName = "Zipato Read Command",
             Description = "Reading data from Zipato home control.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class ReadCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly IZipato _zipato;

        #endregion

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="RootCommand"/>.
        /// </summary>
        private RootCommand Parent { get; set; }

        /// <summary>
        /// Command options.
        /// </summary>
        private bool OptionP { get; set; }

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions
        {
            get => !(OptionP  || OptionV  || OptionBX || OptionAL || OptionAN || OptionAT || OptionBN ||
                     OptionBR || OptionCA || OptionCE || OptionCL || OptionCO || OptionDE || OptionEP || OptionGR ||
                     OptionNW || OptionNT || OptionRO || OptionRU || OptionSC || OptionSD || OptionTH || OptionVE); }

        #endregion

        #region Public Properties

        [Option("-bx|--box", "Reads the Zipatile box data.", CommandOptionType.NoValue)]
        public bool OptionBX { get; set; }

        [Option("-al|--alarm", "Reads alarm data.", CommandOptionType.NoValue)]
        public bool OptionAL { get; set; }

        [Option("-an|--announcements", "Reads announcement data.", CommandOptionType.NoValue)]
        public bool OptionAN { get; set; }

        [Option("-at|--attributes", "Reads all attributes (opt. -f).", CommandOptionType.NoValue)]
        public bool OptionAT { get; set; }

        [Option("-bn|--bindings", "Reads all bindings (opt. -f).", CommandOptionType.NoValue)]
        public bool OptionBN { get; set; }

        [Option("-br|--brands", "Reads all brands.", CommandOptionType.NoValue)]
        public bool OptionBR { get; set; }

        [Option("-ca|--cameras", "Reads all camera data (opt. -f).", CommandOptionType.NoValue)]
        public bool OptionCA { get; set; }

        [Option("-cl|--clusters", "Reads all clusters (opt. -f).", CommandOptionType.NoValue)]
        public bool OptionCL { get; set; }

        [Option("-ce|--clusterendpoints", "Reads all cluster endpoints (opt. -f).", CommandOptionType.NoValue)]
        public bool OptionCE { get; set; }

        [Option("-co|--contacts", "Reads all contacts.", CommandOptionType.NoValue)]
        public bool OptionCO { get; set; }

        [Option("-de|--devices", "Reads all devices (opt. -f).", CommandOptionType.NoValue)]
        public bool OptionDE { get; set; }

        [Option("-ep|--endpoints", "Reads all endpoints (opt. -f).", CommandOptionType.NoValue)]
        public bool OptionEP { get; set; }

        [Option("-gr|--groups", "Reads all groups (opt. -f).", CommandOptionType.NoValue)]
        public bool OptionGR { get; set; }

        [Option("-nw|--networks", "Reads all networks (opt. -f).", CommandOptionType.NoValue)]
        public bool OptionNW { get; set; }

        [Option("-nt|--networktrees", "Reads all network tree data.", CommandOptionType.NoValue)]
        public bool OptionNT { get; set; }

        [Option("-ro|--rooms", "Reads all rooms.", CommandOptionType.NoValue)]
        public bool OptionRO { get; set; }

        [Option("-ru|--rules", "Reads all rules (opt. -f).", CommandOptionType.NoValue)]
        public bool OptionRU { get; set; }

        [Option("-sc|--scenes", "Reads all scenes (opt. -f).", CommandOptionType.NoValue)]
        public bool OptionSC { get; set; }

        [Option("-sd|--schedules", "Reads all schedules (opt. -f).", CommandOptionType.NoValue)]
        public bool OptionSD { get; set; }

        [Option("-th|--thermostats", "Reads all thermostats (opt. -f).", CommandOptionType.NoValue)]
        public bool OptionTH { get; set; }

        [Option("-ve|--virtualendpoints", "Reads all virtual endpoints (opt. -f).", CommandOptionType.NoValue)]
        public bool OptionVE { get; set; }

        [Option("-v|--values", "Reads all attribute values.", CommandOptionType.NoValue)]
        public bool OptionV { get; set; }

        [Option("-f|--full", "Reads all data.", CommandOptionType.NoValue)]
        public bool OptionF { get; set; }

        [Option("-p|--property <string>", "Reads the named property.", CommandOptionType.SingleValue)]
        public string Property { get; set; } = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadCommand"/> class.
        /// </summary>
        /// <param name="zipato">The Zipato instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public ReadCommand(IZipato zipato,
                           ILogger<ReadCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("InfoCommand()");

            // Setting the Zipato instance.
            _zipato = zipato;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to run when command is executed.
        /// </summary>
        /// <returns>Zero if ok.</returns>
        public async Task<int> OnExecuteAsync(CommandLineApplication app)
        {
            if (CheckOptions(app))
            {
                try
                {
                    // Overriding Zipato options.
                    _zipato.BaseAddress = Parent.BaseAddress;
                    _zipato.Timeout = Parent.Timeout;
                    _zipato.User = Parent.User;
                    _zipato.Password = Parent.Password;
                    _zipato.IsLocal = Parent.IsLocal;
                    _zipato.StartSession();

                    if (!_zipato.IsSessionActive)
                    {
                        Console.WriteLine($"Cannot establish a communcation session.");
                        return 0;
                    }

                    if (NoOptions)
                    {
                        Console.WriteLine($"Reading all data from Zipato.");

                        var status = await _zipato.ReadAllAsync();
                        Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                        Console.WriteLine($"Data: {JsonConvert.SerializeObject(_zipato.Data, Formatting.Indented)}");
                    }

                    if (OptionAL)
                    {
                        Console.WriteLine($"Reading alarm data from Zipato.");

                        var (data, status) = await _zipato.DataReadAlarmAsync();
                        Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                        Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                    }

                    if (OptionAN)
                    {
                        Console.WriteLine($"Reading announcement data from Zipato.");

                        var (data, status) = await _zipato.DataReadAnnouncementsAsync();
                        Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                        Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                    }

                    if (OptionBX)
                    {
                        Console.WriteLine($"Reading box data from Zipato.");

                        var (data, status) = await _zipato.DataReadBoxAsync();
                        Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                        Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                    }

                    if (OptionAT)
                    {
                        if (OptionF)
                        {
                            Console.WriteLine($"Reading all attributes (full) from Zipato.");

                            var (data, status) = await _zipato.DataReadAttributesFullAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Reading all attributes from Zipato.");

                            var (data, status) = await _zipato.DataReadAttributesAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                    }

                    if (OptionBN)
                    {
                        if (OptionF)
                        {
                            Console.WriteLine($"Reading all bindings (full) from Zipato.");

                            var (data, status) = await _zipato.DataReadBindingsFullAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Reading all bindings from Zipato.");

                            var (data, status) = await _zipato.DataReadBindingsAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                    }

                    if (OptionCE)
                    {
                        if (OptionF)
                        {
                            Console.WriteLine($"Reading all cluster endpoints (full) from Zipato.");

                            var (data, status) = await _zipato.DataReadClusterEndpointsFullAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Reading all cluster endpoints from Zipato.");

                            var (data, status) = await _zipato.DataReadClusterEndpointsAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                    }

                    if (OptionCL)
                    {
                        if (OptionF)
                        {
                            Console.WriteLine($"Reading all clusters (full) from Zipato.");

                            var (data, status) = await _zipato.DataReadClustersFullAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Reading all clusters from Zipato.");

                            var (data, status) = await _zipato.DataReadClustersAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                    }

                    if (OptionBR)
                    {
                        Console.WriteLine($"Reading all brands from Zipato.");

                        var (data, status) = await _zipato.DataReadBrandsAsync();
                        Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                        Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                    }

                    if (OptionCA)
                    {
                        if (OptionF)
                        {
                            Console.WriteLine($"Reading all camera data (full) from Zipato.");

                            var (data, status) = await _zipato.DataReadCamerasFullAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Reading all camera data from Zipato.");

                            var (data, status) = await _zipato.DataReadCamerasAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                    }

                    if (OptionCO)
                    {
                        Console.WriteLine($"Reading all contacts from Zipato.");

                        var (data, status) = await _zipato.DataReadContactsAsync();
                        Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                        Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                    }

                    if (OptionDE)
                    {
                        if (OptionF)
                        {
                            Console.WriteLine($"Reading all devices (full) from Zipato.");

                            var (data, status) = await _zipato.DataReadDevicesFullAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Reading all devices from Zipato.");

                            var (data, status) = await _zipato.DataReadDevicesAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                    }

                    if (OptionEP)
                    {
                        if (OptionF)
                        {
                            Console.WriteLine($"Reading all endpoints (full) from Zipato.");

                            var (data, status) = await _zipato.DataReadEndpointsFullAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Reading all endpoints from Zipato.");

                            var (data, status) = await _zipato.DataReadEndpointsAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                    }

                    if (OptionGR)
                    {
                        if (OptionF)
                        {
                            Console.WriteLine($"Reading all groups (full) from Zipato.");

                            var (data, status) = await _zipato.DataReadGroupsFullAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Reading all groups from Zipato.");

                            var (data, status) = await _zipato.DataReadGroupsAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                    }

                    if (OptionNW)
                    {
                        if (OptionF)
                        {
                            Console.WriteLine($"Reading all networks (full) from Zipato.");

                            var (data, status) = await _zipato.DataReadNetworksFullAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Reading all networks from Zipato.");

                            var (data, status) = await _zipato.DataReadNetworksAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                    }

                    if (OptionNT)
                    {
                        Console.WriteLine($"Reading all network trees from Zipato.");

                        var (data, status) = await _zipato.DataReadNetworkTreesAsync();
                        Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                        Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                    }

                    if (OptionRO)
                    {
                        Console.WriteLine($"Reading all rooms from Zipato.");

                        var (data, status) = await _zipato.DataReadRoomsAsync();
                        Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                        Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                    }

                    if (OptionRU)
                    {
                        if (OptionF)
                        {
                            Console.WriteLine($"Reading all rules (full) from Zipato.");

                            var (data, status) = await _zipato.DataReadRulesFullAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Reading all rules from Zipato.");

                            var (data, status) = await _zipato.DataReadRulesAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                    }

                    if (OptionSC)
                    {
                        if (OptionF)
                        {
                            Console.WriteLine($"Reading all scenes (full) from Zipato.");

                            var (data, status) = await _zipato.DataReadScenesFullAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Reading all scenes from Zipato.");

                            var (data, status) = await _zipato.DataReadScenesAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                    }

                    if (OptionSD)
                    {
                        if (OptionF)
                        {
                            Console.WriteLine($"Reading all schedules (full) from Zipato.");

                            var (data, status) = await _zipato.DataReadSchedulesFullAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Reading all schedules from Zipato.");

                            var (data, status) = await _zipato.DataReadSchedulesAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                    }

                    if (OptionTH)
                    {
                        if (OptionF)
                        {
                            Console.WriteLine($"Reading all thermostats (full) from Zipato.");

                            var (data, status) = await _zipato.DataReadThermostatsFullAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Reading all thermostats from Zipato.");

                            var (data, status) = await _zipato.DataReadThermostatsAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                    }

                    if (OptionVE)
                    {
                        if (OptionF)
                        {
                            Console.WriteLine($"Reading all virtual endpoints (full) from Zipato.");

                            var (data, status) = await _zipato.DataReadVirtualEndpointsFullAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Reading all virtual endpoints from Zipato.");

                            var (data, status) = await _zipato.DataReadVirtualEndpointsAsync();
                            Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                            Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                        }
                    }

                    if (OptionV)
                    {
                        Console.WriteLine($"Reading all attribute values from Zipato.");

                        var (data, status) = await _zipato.DataReadValuesAsync();
                        Console.WriteLine($"Status: {JsonConvert.SerializeObject(status, Formatting.Indented)}");
                        Console.WriteLine($"Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");
                    }

                    if (OptionP)
                    {
                        dynamic value = null;

                        Console.WriteLine($"Reading property '{Property}' from Zipato.");
                        await _zipato.ReadPropertyAsync(Property);
                        value = _zipato.GetPropertyValue(Property);
                        Console.WriteLine($"Value of property '{Property}' = {JsonConvert.SerializeObject(value, Formatting.Indented)}");
                    }
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, $"Exception ReadCommand.");
                    return -1;
                }
                finally
                {
                    _zipato.EndSession();
                }
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
            var options = app.GetOptions().ToList();

            foreach (var option in options)
            {
                if (option.Inherited == false)
                {
                    switch (option.ShortName)
                    {
                        case "p": OptionP = option.HasValue(); break;
                    }
                }
            }

            if (OptionP)
            {
                if (!Zipato.IsProperty(Property))
                {
                    Console.WriteLine($"Property '{Property}' not found.");
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
