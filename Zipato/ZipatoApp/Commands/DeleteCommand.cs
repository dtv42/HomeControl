// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteCommand.cs" company="DTV-Online">
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

    using BaseClassLib;
    using ZipatoLib;
    using ZipatoApp.Models;

    #endregion

    /// <summary>
    /// Application command "delete".
    /// </summary>
    [Command(Name = "delete",
             FullName = "Zipato Delete Command",
             Description = "Deleting data items from Zipato home control.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class DeleteCommand : BaseCommand<AppSettings>
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
        private bool OptionBN { get; set; }
        private bool OptionCO { get; set; }
        private bool OptionDE { get; set; }
        private bool OptionEP { get; set; }
        private bool OptionRO { get; set; }
        private bool OptionRU { get; set; }
        private bool OptionSC { get; set; }
        private bool OptionSD { get; set; }
        private bool OptionTH { get; set; }
        private bool OptionUS { get; set; }
        private bool OptionVE { get; set; }

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions
        {
            get => !(OptionBN ||
                     OptionCO ||
                     OptionDE ||
                     OptionEP ||
                     OptionRO ||
                     OptionRU ||
                     OptionSC ||
                     OptionSD ||
                     OptionTH ||
                     OptionUS ||
                     OptionVE);
        }

        #endregion

        #region Public Properties

        [Uuid]
        [Option("-bn|--binding <uuid>", "Deletes a binding instance.", CommandOptionType.SingleValue)]
        public string BindingUUID { get; set; }

        [Option("-co|--contact <id>", "Deletes a contact instance.", CommandOptionType.SingleValue)]
        public int ContactID { get; set; }

        [Uuid]
        [Option("-de|--device <uuid>", "Deletes a device instance.", CommandOptionType.SingleValue)]
        public string DeviceUUID { get; set; }

        [Uuid]
        [Option("-ep|--endpoint <uuid>", "Deletes an endpoint instance.", CommandOptionType.SingleValue)]
        public string EndpointUUID { get; set; }

        [Option("-ro|--room <id>", "Deletes a room instance.", CommandOptionType.SingleValue)]
        public int RoomID { get; set; }

        [Option("-ru|--rule <id>", "Deletes a rule instance.", CommandOptionType.SingleValue)]
        public int RuleID { get; set; }

        [Uuid]
        [Option("-sc|--scene <uuid>", "Deletes a scene instance.", CommandOptionType.SingleValue)]
        public string SceneUUID { get; set; }

        [Uuid]
        [Option("-sd|--schedule <uuid>", "Deletes a schedule instance.", CommandOptionType.SingleValue)]
        public string ScheduleUUID { get; set; }

        [Uuid]
        [Option("-th|--thermostat <uuid>", "Deletes a thermostat instance.", CommandOptionType.SingleValue)]
        public string ThermostatUUID { get; set; }

        [Option("-us|--user <id>", "Deletes a thermostat instance.", CommandOptionType.SingleValue)]
        public int UserID { get; set; }

        [Uuid]
        [Option("-ve|--virtualendpoint <uuid>", "Deletes a virtual endpoint instance.", CommandOptionType.SingleValue)]
        public string VirtualEndpointUUID { get; set; }

        [Option("-x|--execute", "Executes the actual delete.", CommandOptionType.NoValue)]
        public bool OptionX { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCommand"/> class.
        /// </summary>
        /// <param name="zipato">The Zipato instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public DeleteCommand(IZipato zipato,
                           ILogger<DeleteCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("DeleteCommand()");

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
                    _zipato.StartSession();

                    if (!_zipato.IsSessionActive)
                    {
                        Console.WriteLine($"Cannot establish a communcation session.");
                        return 0;
                    }

                    if (OptionBN)
                    {
                        var uuid = new Guid(BindingUUID);
                        var (result, status) = await _zipato.ReadBindingAsync(uuid);

                        if (status.IsGood)
                        {
                            if (OptionX)
                            {
                                status = await _zipato.DeleteBindingAsync(uuid);
                                if (status.IsGood)
                                {
                                    Console.WriteLine($"Binding with UUID '{BindingUUID}' deleted.");
                                }
                                else
                                {
                                    Console.WriteLine($"Binding with UUID '{BindingUUID}' not deleted.");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Binding with UUID '{BindingUUID}' can be deleted.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Binding with UUID '{BindingUUID}' not found.");
                        }
                    }

                    if (OptionCO)
                    {
                        var (result, status) = await _zipato.ReadContactAsync(ContactID);

                        if (status.IsGood)
                        {
                            if (OptionX)
                            {
                                status = await _zipato.DeleteContactAsync(ContactID);

                                if (status.IsGood)
                                {
                                    Console.WriteLine($"Contact with ID '{ContactID}' deleted.");
                                }
                                else
                                {
                                    Console.WriteLine($"Contact with ID '{ContactID}' not deleted.");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Contact with ID '{ContactID}' can be deleted.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Contact with ID '{ContactID}' not found.");
                        }
                    }

                    if (OptionDE)
                    {
                        var uuid = new Guid(DeviceUUID);
                        var (result, status) = await _zipato.ReadDeviceAsync(uuid);

                        if (status.IsGood)
                        {
                            if (OptionX)
                            {
                                status = await _zipato.DeleteDeviceAsync(uuid);

                                if (status.IsGood)
                                {
                                    Console.WriteLine($"Device with UUID '{DeviceUUID}' deleted.");
                                }
                                else
                                {
                                    Console.WriteLine($"Device with UUID '{DeviceUUID}' not deleted.");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Device with UUID '{DeviceUUID}' can be deleted.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Device with UUID '{DeviceUUID}' not found.");
                        }
                    }

                    if (OptionEP)
                    {
                        var uuid = new Guid(EndpointUUID);
                        var (result, status) = await _zipato.ReadEndpointAsync(uuid);

                        if (status.IsGood)
                        {
                            if (OptionX)
                            {
                                status = await _zipato.DeleteEndpointAsync(uuid);

                                if (status.IsGood)
                                {
                                    Console.WriteLine($"Endpoint with UUID '{EndpointUUID}' deleted.");
                                }
                                else
                                {
                                    Console.WriteLine($"Endpoint with UUID '{EndpointUUID}' not deleted.");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Endpoint with UUID '{EndpointUUID}' can be deleted.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Endpoint with UUID '{EndpointUUID}' not found.");
                        }
                    }

                    if (OptionRO)
                    {
                        var (result, status) = await _zipato.ReadRoomsAsync();

                        if (result.FirstOrDefault(r => r.Id == RoomID) == null)
                        {
                            status = DataValueLib.DataValue.Bad;
                        }

                        if (status.IsGood)
                        {
                            if (OptionX)
                            {
                                status = await _zipato.DeleteRoomAsync(RoomID);

                                if (status.IsGood)
                                {
                                    Console.WriteLine($"Room with UUID '{RoomID}' deleted.");
                                }
                                else
                                {
                                    Console.WriteLine($"Room with UUID '{RoomID}' not deleted.");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Room with UUID '{RoomID}' can be deleted.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Room with UUID '{RoomID}' not found.");
                        }
                    }

                    if (OptionRU)
                    {
                        var (result, status) = _zipato.ReadRuleAsync(RuleID).Result;

                        if (status.IsGood)
                        {
                            if (OptionX)
                            {
                                if (_zipato.DeleteRuleAsync(RuleID).Result.IsGood)
                                {
                                    Console.WriteLine($"Rule with ID '{RuleID}' deleted.");
                                }
                                else
                                {
                                    Console.WriteLine($"Rule with ID '{RuleID}' not deleted.");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Rule with ID '{RuleID}' can be deleted.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Rule with UUID '{RuleID}' not found.");
                        }
                    }

                    if (OptionSC)
                    {
                        var uuid = new Guid(SceneUUID);
                        var (result, status) = await _zipato.ReadSceneAsync(uuid);

                        if (status.IsGood)
                        {
                            if (OptionX)
                            {
                                status = await _zipato.DeleteSceneAsync(uuid);

                                if (status.IsGood)
                                {
                                    Console.WriteLine($"Scene with UUID '{SceneUUID}' deleted.");
                                }
                                else
                                {
                                    Console.WriteLine($"Scene with UUID '{SceneUUID}' not deleted.");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Scene with UUID '{SceneUUID}' can be deleted.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Scene with UUID '{SceneUUID}' not found.");
                        }
                    }

                    if (OptionSD)
                    {
                        var uuid = new Guid(ScheduleUUID);
                        var (result, status) = await _zipato.ReadScheduleAsync(uuid);

                        if (status.IsGood)
                        {
                            if (OptionX)
                            {
                                status = await _zipato.DeleteScheduleAsync(uuid);

                                if (status.IsGood)
                                {
                                    Console.WriteLine($"Schedule with UUID '{ScheduleUUID}' deleted.");
                                }
                                else
                                {
                                    Console.WriteLine($"Schedule with UUID '{ScheduleUUID}' not deleted.");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Schedule with UUID '{ScheduleUUID}' can be deleted.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Schedule with UUID '{ScheduleUUID}' not found.");
                        }
                    }

                    if (OptionTH)
                    {
                        var uuid = new Guid(ThermostatUUID);
                        var (result, status) = await _zipato.ReadThermostatAsync(uuid);

                        if (status.IsGood)
                        {
                            if (OptionX)
                            {
                                status = await _zipato.DeleteThermostatAsync(uuid);

                                if (status.IsGood)
                                {
                                    Console.WriteLine($"Thermostat with UUID '{ThermostatUUID}' deleted.");
                                }
                                else
                                {
                                    Console.WriteLine($"Thermostat with UUID '{ThermostatUUID}' not deleted.");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Thermostat with UUID '{ThermostatUUID}' can be deleted.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Thermostat with UUID '{ThermostatUUID}' not found.");
                        }
                    }

                    if (OptionUS)
                    {
                        var (result, status) = await _zipato.ReadUsersAsync();

                        if (result.FirstOrDefault(r => r.Id == UserID) == null)
                        {
                            status = DataValueLib.DataValue.Bad;
                        }

                        if (status.IsGood)
                        {
                            if (OptionX)
                            {
                                status = await _zipato.DeleteUserAsync(UserID);

                                if (status.IsGood)
                                {
                                    Console.WriteLine($"User with ID '{UserID}' deleted.");
                                }
                                else
                                {
                                    Console.WriteLine($"User with ID '{UserID}' not deleted.");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"User with ID '{UserID}' can be deleted.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"User with UUID '{UserID}' not found.");
                        }
                    }

                    if (OptionVE)
                    {
                        var uuid = new Guid(VirtualEndpointUUID);
                        var (result, status) = await _zipato.ReadVirtualEndpointAsync(uuid);

                        if (status.IsGood)
                        {
                            if (OptionX)
                            {
                                status = await _zipato.DeleteVirtualEndpointAsync(uuid);

                                if (status.IsGood)
                                {
                                    Console.WriteLine($"Virtual Endpoint with UUID '{VirtualEndpointUUID}' deleted.");
                                }
                                else
                                {
                                    Console.WriteLine($"Virtual Endpoint with UUID '{VirtualEndpointUUID}' not deleted.");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Virtual Endpoint with UUID '{VirtualEndpointUUID}' can be deleted.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Virtual Endpoint with UUID '{VirtualEndpointUUID}' not found.");
                        }
                    }

                    if (!OptionX)
                    {
                        Console.WriteLine($"Specify -x to execute the delete operation.");
                    }
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, $"Exception DeleteCommand.");
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
                    switch (option.LongName)
                    {
                        case "binding": OptionBN = option.HasValue(); break;
                        case "contact": OptionCO = option.HasValue(); break;
                        case "device": OptionDE = option.HasValue(); break;
                        case "endpoint": OptionEP = option.HasValue(); break;
                        case "room": OptionRO = option.HasValue(); break;
                        case "rule": OptionRU = option.HasValue(); break;
                        case "scene": OptionSC = option.HasValue(); break;
                        case "schedule": OptionSD = option.HasValue(); break;
                        case "thermostat": OptionTH = option.HasValue(); break;
                        case "user": OptionUS = option.HasValue(); break;
                        case "virtual": OptionVE = option.HasValue(); break;
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
