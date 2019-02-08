// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InfoCommand.cs" company="DTV-Online">
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
    /// Application command "info".
    /// </summary>
    [Command(Name = "info",
             FullName = "Zipato Info Command",
             Description = "Reading data information from Zipato home control.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class InfoCommand : BaseCommand<AppSettings>
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
        private bool OptionAT { get; set; }
        private bool OptionBN { get; set; }
        private bool OptionCO { get; set; }
        private bool OptionDE { get; set; }
        private bool OptionEP { get; set; }
        private bool OptionRO { get; set; }
        private bool OptionRU { get; set; }
        private bool OptionSC { get; set; }
        private bool OptionSD { get; set; }
        private bool OptionTH { get; set; }
        private bool OptionVE { get; set; }

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions { get => !(OptionAT ||
                                          OptionBN ||
                                          OptionCO ||
                                          OptionDE ||
                                          OptionEP ||
                                          OptionRO ||
                                          OptionRU ||
                                          OptionSC ||
                                          OptionSD ||
                                          OptionTH ||
                                          OptionVE); }

        #endregion

        #region Public Properties

        [Uuid]
        [Option("-at|--attribute <uuid>", "Shows attribute info.", CommandOptionType.SingleValue)]
        public string AttributeUUID { get; set; }

        [Uuid]
        [Option("-bn|--binding <uuid>", "Shows binding info.", CommandOptionType.SingleValue)]
        public string BindingUUID { get; set; }

        [Option("-co|--contact <uuid>", "Shows contact info.", CommandOptionType.SingleValue)]
        public int ContactID { get; set; }

        [Uuid]
        [Option("-de|--device <uuid>", "Shows device info.", CommandOptionType.SingleValue)]
        public string DeviceUUID { get; set; }

        [Uuid]
        [Option("-ep|--endpoint <uuid>", "Shows endpoint info.", CommandOptionType.SingleValue)]
        public string EndpointUUID { get; set; }

        [Option("-ro|--room <id>", "Shows room info.", CommandOptionType.SingleValue)]
        public int RoomID { get; set; }

        [Option("-ru|--rule <id>", "Shows rule info.", CommandOptionType.SingleValue)]
        public int RuleID { get; set; }

        [Uuid]
        [Option("-sc|--scene <uuid>", "Shows scene info.", CommandOptionType.SingleValue)]
        public string SceneUUID { get; set; }

        [Uuid]
        [Option("-sd|--schedule <uuid>", "Shows schedule info.", CommandOptionType.SingleValue)]
        public string ScheduleUUID { get; set; }

        [Uuid]
        [Option("-th|--thermostat <uuid>", "Shows thermostat data.", CommandOptionType.SingleValue)]
        public string ThermostatUUID { get; set; }

        [Uuid]
        [Option("-ve|--virtual <uuid>", "Shows virtual endpoint data.", CommandOptionType.SingleValue)]
        public string VirtualUUID { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InfoCommand"/> class.
        /// </summary>
        /// <param name="zipato">The Zipato instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public InfoCommand(IZipato zipato,
                           ILogger<InfoCommand> logger,
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

                    if (OptionAT)
                    {
                        var (result, status) = await _zipato.DataReadAttributeAsync(new Guid(AttributeUUID));

                        if (status.IsGood)
                        {
                            Console.WriteLine($"Attribute Info: {JsonConvert.SerializeObject(result, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Attribute with UUID '{AttributeUUID}' not found.");
                        }
                    }

                    if (OptionBN)
                    {
                        var (result, status) = await _zipato.DataReadBindingAsync(new Guid(BindingUUID));

                        if (status.IsGood)
                        {
                            Console.WriteLine($"Binding Info: {JsonConvert.SerializeObject(result)}");
                        }
                        else
                        {
                            Console.WriteLine($"Binding with UUID '{BindingUUID}' not found.");
                        }
                    }

                    if (OptionCO)
                    {
                        var (result, status) = await _zipato.DataReadContactAsync(ContactID);

                        if (status.IsGood)
                        {
                            Console.WriteLine($"Contact Info: {JsonConvert.SerializeObject(result)}");
                        }
                        else
                        {
                            Console.WriteLine($"Contact with ID '{ContactID}' not found.");
                        }
                    }

                    if (OptionDE)
                    {
                        var (result, status) = await _zipato.DataReadDeviceAsync(new Guid(DeviceUUID));

                        if (status.IsGood)
                        {
                            Console.WriteLine($"Device Info: {JsonConvert.SerializeObject(result, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Device with UUID '{DeviceUUID}' not found.");
                        }
                    }

                    if (OptionEP)
                    {
                        var (result, status) = await _zipato.DataReadEndpointAsync(new Guid(EndpointUUID));

                        if (status.IsGood)
                        {
                            Console.WriteLine($"Endpoint Info: {JsonConvert.SerializeObject(result, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Endpoint with UUID '{EndpointUUID}' not found.");
                        }
                    }

                    if (OptionRO)
                    {
                        var (result, status) = await _zipato.DataReadRoomsAsync();
                        var room = _zipato.GetRoom(RoomID);

                        if ((status.IsGood) && (room?.Id == RoomID))
                        {

                            Console.WriteLine($"Room Info: {JsonConvert.SerializeObject(room, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Room with UUID '{RoomID}' not found.");
                        }
                    }

                    if (OptionRU)
                    {
                        var (result, status) = await _zipato.DataReadRuleAsync(RuleID);

                        if (status.IsGood)
                        {
                            Console.WriteLine($"Rule Info: {JsonConvert.SerializeObject(result, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Rule with UUID '{RuleID}' not found.");
                        }
                    }

                    if (OptionSC)
                    {
                        var (result, status) = await _zipato.DataReadSceneAsync(new Guid(SceneUUID));

                        if (status.IsGood)
                        {
                            Console.WriteLine($"Scene Info: {JsonConvert.SerializeObject(result, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Scene with UUID '{SceneUUID}' not found.");
                        }
                    }

                    if (OptionSD)
                    {
                        var (result, status) = await _zipato.DataReadScheduleAsync(new Guid(ScheduleUUID));

                        if (status.IsGood)
                        {
                            Console.WriteLine($"Schedule Info: {JsonConvert.SerializeObject(result, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Schedule with UUID '{ScheduleUUID}' not found.");
                        }
                    }

                    if (OptionTH)
                    {
                        var (result, status) = await _zipato.DataReadThermostatAsync(new Guid(ThermostatUUID));

                        if (status.IsGood)
                        {
                            Console.WriteLine($"Thermostat Info: {JsonConvert.SerializeObject(result, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Thermostat with UUID '{ThermostatUUID}' not found.");
                        }
                    }

                    if (OptionVE)
                    {
                        var (result, status) = await _zipato.DataReadVirtualEndpointAsync(new Guid(VirtualUUID));

                        if (status.IsGood)
                        {
                            Console.WriteLine($"Virtual Endpoint Info: {JsonConvert.SerializeObject(result, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Virtual Endpoint with UUID '{VirtualUUID}' not found.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, $"Exception InfoCommand.");
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
                        case "at": OptionAT = option.HasValue(); break;
                        case "bn": OptionBN = option.HasValue(); break;
                        case "co": OptionCO = option.HasValue(); break;
                        case "de": OptionDE = option.HasValue(); break;
                        case "ep": OptionEP = option.HasValue(); break;
                        case "ro": OptionRO = option.HasValue(); break;
                        case "ru": OptionRU = option.HasValue(); break;
                        case "sc": OptionSC = option.HasValue(); break;
                        case "sd": OptionSD = option.HasValue(); break;
                        case "th": OptionTH = option.HasValue(); break;
                        case "ve": OptionVE = option.HasValue(); break;
                    }
                }
            }

            if (NoOptions)
            {
                app.ShowHelp();
                return false;
            }

            return true;
        }

        #endregion
    }
}
