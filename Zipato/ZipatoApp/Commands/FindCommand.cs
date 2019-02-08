// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FindCommand.cs" company="DTV-Online">
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
    /// Application command "find".
    /// </summary>
    [Command(Name = "find",
             FullName = "Zipato Find Command",
             Description = "Showing selected data values from Zipato home control.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class FindCommand : BaseCommand<AppSettings>
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
        private bool OptionEA { get; set; }

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions
        {
            get => !(OptionAT ||
                     OptionBN ||
                     OptionCO ||
                     OptionDE ||
                     OptionEP ||
                     OptionRO ||
                     OptionRU ||
                     OptionSC ||
                     OptionSD ||
                     OptionTH ||
                     OptionVE ||
                     OptionEA);
        }

        #endregion

        #region Public Properties

        [Option("-at|--attribute <string>", "Shows attribute (UUID or name).", CommandOptionType.SingleValue)]
        public string Attribute { get; set; } = string.Empty;

        [Option("-bn|--binding <string>", "Shows binding (UUID or name).", CommandOptionType.SingleValue)]
        public string Binding { get; set; } = string.Empty;

        [Option("-co|--contact <string>", "Shows contact (ID or name).", CommandOptionType.SingleValue)]
        public string Contact { get; set; } = string.Empty;

        [Option("-de|--device <string>", "Shows device (UUID or name).", CommandOptionType.SingleValue)]
        public string Device { get; set; } = string.Empty;

        [Option("-ep|--endpoint <string>", "Shows endpoint (UUID or name).", CommandOptionType.SingleValue)]
        public string Endpoint { get; set; } = string.Empty;

        [Option("-ro|--room <string>", "Shows room (ID or name).", CommandOptionType.SingleValue)]
        public string Room { get; set; } = string.Empty;

        [Option("-ru|--rule <string>", "Shows rule (ID or name).", CommandOptionType.SingleValue)]
        public string Rule { get; set; } = string.Empty;

        [Option("-sc|--scene <string>", "Shows scene (UUID or name).", CommandOptionType.SingleValue)]
        public string Scene { get; set; } = string.Empty;

        [Option("-sd|--schedule <string>", "Shows schedule (UUID or name).", CommandOptionType.SingleValue)]
        public string Schedule { get; set; } = string.Empty;

        [Option("-th|--thermostat <string>", "Shows thermostat (UUID or name).", CommandOptionType.SingleValue)]
        public string Thermostat { get; set; } = string.Empty;

        [Option("-ve|--virtual <string>", "Shows virtual endpoint (UUID or name).", CommandOptionType.SingleValue)]
        public string VirtualEndpoint { get; set; } = string.Empty;

        [Uuid]
        [Option("-ea|--endpointattributes <uuid>", "Shows attributes for endpoint (UUID).", CommandOptionType.SingleValue)]
        public string EndpointUUID { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FindCommand"/> class.
        /// </summary>
        /// <param name="zipato">The Zipato instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public FindCommand(IZipato zipato,
                           ILogger<FindCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("FindCommand()");

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
                        var (allattributes, status) = await _zipato.DataReadAttributesFullAsync();

                        if (status.IsGood)
                        {
                            if (Guid.TryParse(Attribute, out Guid guid))
                            {
                                var attributes = allattributes.Where(a => a.Uuid == guid).ToList();

                                if (attributes.Count() == 1)
                                {
                                    Console.WriteLine($"Attribute: {JsonConvert.SerializeObject(attributes[0], Formatting.Indented)}");
                                }
                                else
                                {
                                    Console.WriteLine($"Attribute with UUID '{Attribute}' not found.");
                                }
                            }
                            else
                            {
                                var attributes = allattributes.Where(a => a.Name.Equals(Attribute, StringComparison.InvariantCultureIgnoreCase)).ToList();

                                if (attributes.Count() > 0)
                                {
                                    if (attributes.Count() == 1)
                                    {
                                        Console.WriteLine($"Attribute: {JsonConvert.SerializeObject(attributes[0], Formatting.Indented)}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Attributes: {JsonConvert.SerializeObject(attributes, Formatting.Indented)}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Attributes with name '{Attribute}' not found.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No attributes found.");
                        }
                    }

                    if (OptionBN)
                    {
                        var (allbindings, status) = await _zipato.DataReadBindingsFullAsync();

                        if (status.IsGood)
                        {
                            if (Guid.TryParse(Binding, out Guid guid))
                            {
                                var bindings = allbindings.Where(b => b.Uuid == guid).ToList();

                                if (bindings.Count() == 1)
                                {
                                    Console.WriteLine($"Binding: {JsonConvert.SerializeObject(bindings[0], Formatting.Indented)}");
                                }
                                else
                                {
                                    Console.WriteLine($"Binding with UUID '{Binding}' not found.");
                                }
                            }
                            else
                            {
                                var bindings = allbindings.Where(b => b.Name.Equals(Binding, StringComparison.InvariantCultureIgnoreCase)).ToList();

                                if (bindings.Count() > 0)
                                {
                                    if (bindings.Count() == 1)
                                    {
                                        Console.WriteLine($"Binding: {JsonConvert.SerializeObject(bindings[0], Formatting.Indented)}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Bindings: {JsonConvert.SerializeObject(bindings, Formatting.Indented)}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Binding with name '{Binding}' not found.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No bindings found.");
                        }
                    }

                    if (OptionCO)
                    {
                        var (allcontacts, status) = await _zipato.DataReadContactsAsync();

                        if (status.IsGood)
                        {
                            if (int.TryParse(Contact, out int id))
                            {
                                var contacts = allcontacts.Where(c => c.Id == id).ToList();

                                if (contacts.Count() == 1)
                                {
                                    Console.WriteLine($"Contact: {JsonConvert.SerializeObject(contacts[0], Formatting.Indented)}");
                                }
                                else
                                {
                                    Console.WriteLine($"Contact '{Contact}' not found.");
                                }
                            }
                            else
                            {
                                var contacts = allcontacts.Where(r => r.Name.Equals(Contact, StringComparison.InvariantCultureIgnoreCase)).ToList();

                                if (contacts.Count() > 0)
                                {
                                    if (contacts.Count() == 1)
                                    {
                                        Console.WriteLine($"Contact: {JsonConvert.SerializeObject(contacts[0], Formatting.Indented)}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Contact: {JsonConvert.SerializeObject(contacts, Formatting.Indented)}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Contact '{Contact}' not found.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No contacts found.");
                        }
                    }

                    if (OptionDE)
                    {
                        var (alldevices, status) = _zipato.DataReadDevicesFullAsync().Result;

                        if (status.IsGood)
                        {
                            if (Guid.TryParse(Device, out Guid guid))
                            {
                                var devices = alldevices.Where(d => d.Uuid == guid).ToList();

                                if (devices.Count() == 1)
                                {
                                    Console.WriteLine($"Device: {JsonConvert.SerializeObject(devices[0], Formatting.Indented)}");
                                }
                                else
                                {
                                    Console.WriteLine($"Device '{Device}' not found.");
                                }
                            }
                            else
                            {
                                var devices = alldevices.Where(d => d.Name.Equals(Device, StringComparison.InvariantCultureIgnoreCase)).ToList();

                                if (devices.Count() > 0)
                                {
                                    if (devices.Count() == 1)
                                    {
                                        Console.WriteLine($"Device: {JsonConvert.SerializeObject(devices[0], Formatting.Indented)}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Devices: {JsonConvert.SerializeObject(devices, Formatting.Indented)}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Devices '{Device}' not found.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No devices found.");
                        }
                    }

                    if (OptionEP)
                    {
                        var (allendpoints, status) = await _zipato.DataReadEndpointsFullAsync();

                        if (status.IsGood)
                        {
                            if (Guid.TryParse(Endpoint, out Guid guid))
                            {
                                var endpoints = allendpoints.Where(a => a.Uuid == guid).ToList();

                                if (endpoints.Count() == 1)
                                {
                                    Console.WriteLine($"Endpoint: {JsonConvert.SerializeObject(endpoints[0], Formatting.Indented)}");
                                }
                                else
                                {
                                    Console.WriteLine($"Endpoint '{Endpoint}' not found.");
                                }
                            }
                            else
                            {
                                var endpoints = allendpoints.Where(e => e.Name.Equals(Endpoint, StringComparison.InvariantCultureIgnoreCase)).ToList();

                                if (endpoints != null)
                                {
                                    if (endpoints.Count() == 1)
                                    {
                                        Console.WriteLine($"Endpoint: {JsonConvert.SerializeObject(endpoints[0], Formatting.Indented)}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Endpoints: {JsonConvert.SerializeObject(endpoints, Formatting.Indented)}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Endpoints '{Endpoint}' not found.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No endpoints found.");
                        }
                    }

                    if (OptionRO)
                    {
                        var (allrooms, status) = await _zipato.DataReadRoomsAsync();

                        if (status.IsGood)
                        {
                            if (int.TryParse(Room, out int id))
                            {
                                var rooms = allrooms.Where(r => r.Id == id).ToList();

                                if (rooms.Count() == 1)
                                {
                                    Console.WriteLine($"Room: {JsonConvert.SerializeObject(rooms[0], Formatting.Indented)}");
                                }
                                else
                                {
                                    Console.WriteLine($"Room '{Room}' not found.");
                                }
                            }
                            else
                            {
                                var rooms = allrooms.Where(r => r.Name.Equals(Room, StringComparison.InvariantCultureIgnoreCase)).ToList();

                                if (rooms.Count() > 0)
                                {
                                    if (rooms.Count() == 1)
                                    {
                                        Console.WriteLine($"Room: {JsonConvert.SerializeObject(rooms[0], Formatting.Indented)}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Rooms: {JsonConvert.SerializeObject(rooms, Formatting.Indented)}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Rooms '{Room}' not found.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No rooms found.");
                        }
                    }

                    if (OptionRU)
                    {
                        var (allrules, status) = await _zipato.DataReadRulesFullAsync();

                        if (status.IsGood)
                        {
                            if (int.TryParse(Rule, out int id))
                            {
                                var rules = allrules.Where(r => r.Id == id).ToList();

                                if (rules.Count() == 1)
                                {
                                    Console.WriteLine($"Rule: {JsonConvert.SerializeObject(rules[0], Formatting.Indented)}");
                                }
                                else
                                {
                                    Console.WriteLine($"Rule '{Rule}' not found.");
                                }
                            }
                            else
                            {
                                var rules = allrules.Where(r => r.Name.Equals(Rule, StringComparison.InvariantCultureIgnoreCase)).ToList();

                                if (rules.Count() > 0)
                                {
                                    if (rules.Count() == 1)
                                    {
                                        Console.WriteLine($"Rule: {JsonConvert.SerializeObject(rules[0], Formatting.Indented)}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Rule: {JsonConvert.SerializeObject(rules, Formatting.Indented)}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Rule '{Rule}' not found.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No rules found.");
                        }
                    }

                    if (OptionSC)
                    {
                        var (allscenes, status) = await _zipato.DataReadScenesFullAsync();

                        if (status.IsGood)
                        {
                            if (Guid.TryParse(Scene, out Guid guid))
                            {
                                var scenes = allscenes.Where(a => a.Uuid == guid).ToList();

                                if (scenes.Count() == 1)
                                {
                                    Console.WriteLine($"Scene: {JsonConvert.SerializeObject(scenes[0], Formatting.Indented)}");
                                }
                                else
                                {
                                    Console.WriteLine($"Scene with UUID '{Scene}' not found.");
                                }
                            }
                            else
                            {
                                var scenes = allscenes.Where(a => a.Name.Equals(Scene, StringComparison.InvariantCultureIgnoreCase)).ToList();

                                if (scenes.Count() > 0)
                                {
                                    if (scenes.Count() == 1)
                                    {
                                        Console.WriteLine($"Scenes: {JsonConvert.SerializeObject(scenes[0], Formatting.Indented)}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Scenes: {JsonConvert.SerializeObject(scenes, Formatting.Indented)}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Scenes with name '{Scene}' not found.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No scenes found.");
                        }
                    }

                    if (OptionSD)
                    {
                        var (allschedules, status) = await _zipato.DataReadSchedulesFullAsync();

                        if (status.IsGood)
                        {
                            if (Guid.TryParse(Schedule, out Guid guid))
                            {
                                var schedules = allschedules.Where(a => a.Uuid == guid).ToList();

                                if (schedules.Count() == 1)
                                {
                                    Console.WriteLine($"Schedule: {JsonConvert.SerializeObject(schedules[0], Formatting.Indented)}");
                                }
                                else
                                {
                                    Console.WriteLine($"Schedule with UUID '{Schedule}' not found.");
                                }
                            }
                            else
                            {
                                var schedules = allschedules.Where(a => a.Name.Equals(Schedule, StringComparison.InvariantCultureIgnoreCase)).ToList();

                                if (schedules.Count() > 0)
                                {
                                    if (schedules.Count() == 1)
                                    {
                                        Console.WriteLine($"Schedule: {JsonConvert.SerializeObject(schedules[0], Formatting.Indented)}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Schedules: {JsonConvert.SerializeObject(schedules, Formatting.Indented)}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Schedules with name '{Schedule}' not found.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No schedules found.");
                        }
                    }

                    if (OptionTH)
                    {
                        var (allthermostats, status) = await _zipato.DataReadThermostatsFullAsync();

                        if (status.IsGood)
                        {
                            if (Guid.TryParse(Thermostat, out Guid guid))
                            {
                                var thermostats = allthermostats.Where(a => a.Uuid == guid).ToList();

                                if (thermostats.Count() == 1)
                                {
                                    Console.WriteLine($"Thermostat: {JsonConvert.SerializeObject(thermostats[0], Formatting.Indented)}");
                                }
                                else
                                {
                                    Console.WriteLine($"Thermostat with UUID '{Thermostat}' not found.");
                                }
                            }
                            else
                            {
                                var thermostats = allthermostats.Where(a => a.Name.Equals(Thermostat, StringComparison.InvariantCultureIgnoreCase)).ToList();

                                if (thermostats.Count() > 0)
                                {
                                    if (thermostats.Count() == 1)
                                    {
                                        Console.WriteLine($"Thermostat: {JsonConvert.SerializeObject(thermostats[0], Formatting.Indented)}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Thermostats: {JsonConvert.SerializeObject(thermostats, Formatting.Indented)}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Thermostats with name '{Thermostat}' not found.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No thermostats found.");
                        }
                    }

                    if (OptionVE)
                    {
                        var (allvirtualendpoints, status) = await _zipato.DataReadVirtualEndpointsFullAsync();

                        if (status.IsGood)
                        {
                            if (Guid.TryParse(VirtualEndpoint, out Guid guid))
                            {
                                var virtualendpoints = allvirtualendpoints.Where(a => a.Uuid == guid).ToList();

                                if (virtualendpoints.Count() == 1)
                                {
                                    Console.WriteLine($"VirtualEndpoint: {JsonConvert.SerializeObject(virtualendpoints[0], Formatting.Indented)}");
                                }
                                else
                                {
                                    Console.WriteLine($"VirtualEndpoint with UUID '{VirtualEndpoint}' not found.");
                                }
                            }
                            else
                            {
                                var virtualendpoints = allvirtualendpoints.Where(a => a.Name.Equals(VirtualEndpoint, StringComparison.InvariantCultureIgnoreCase)).ToList();

                                if (virtualendpoints.Count() > 0)
                                {
                                    if (virtualendpoints.Count() == 1)
                                    {
                                        Console.WriteLine($"VirtualEndpoint: {JsonConvert.SerializeObject(virtualendpoints[0], Formatting.Indented)}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"VirtualEndpoints: {JsonConvert.SerializeObject(virtualendpoints, Formatting.Indented)}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"VirtualEndpoints with name '{VirtualEndpoint}' not found.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No virtual endpoints found.");
                        }
                    }

                    if (OptionEA)
                    {
                        var (endpoint, status) = await _zipato.ReadEndpointAsync(new Guid(EndpointUUID));

                        if (status.IsGood)
                        {
                            var attributes = endpoint.Attributes;

                            Console.WriteLine($"Endpoint UUID: {endpoint.Uuid} Name: {endpoint.Name}");
                            Console.WriteLine($"Attributes: {JsonConvert.SerializeObject(attributes, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Endpoint with UUID '{EndpointUUID}' not found.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, $"Exception FindCommand.");
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
                        case "ea": OptionEA = option.HasValue(); break;
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
