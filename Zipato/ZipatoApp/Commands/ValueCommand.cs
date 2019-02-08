// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValueCommand.cs" company="DTV-Online">
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
    /// Application command "value".
    /// </summary>
    [Command(Name = "value",
             FullName = "Zipato Value Command",
             Description = "Reading data values from Zipato home control.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class ValueCommand : BaseCommand<AppSettings>
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
        private bool OptionA { get; set; }
        private bool OptionE { get; set; }
        private bool OptionN { get; set; }

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions
        {
            get => !(OptionA || OptionE || OptionN);
        }

        #endregion

        #region Public Properties

        [Uuid]
        [Option("-a|--attribute <uuid>", "Specifies the attribute uuid.", CommandOptionType.SingleValue)]
        public string AttributeUUID { get; set; }

        [Uuid]
        [Option("-e|--endpoint <uuid>", "Specifies the endpoint uuid).", CommandOptionType.SingleValue)]
        public string EndpointUUID { get; set; }

        [Option("-n|--name <string>", "Specifies the attribute name.", CommandOptionType.SingleValue)]
        public string AttributeName { get; set; } = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueCommand"/> class.
        /// </summary>
        /// <param name="zipato">The Zipato instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public ValueCommand(IZipato zipato,
                           ILogger<ValueCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("ValueCommand()");

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

                    if (OptionA)
                    {
                        var data = await _zipato.DataReadValueAsync(new Guid(AttributeUUID));
                        Console.WriteLine($"Reading value of attribute with UUID '{AttributeUUID}' from Zipato.");
                        Console.WriteLine($"Value of attribute with UUID '{AttributeUUID}' is '{data}'.");
                    }
                    else if (OptionE)
                    {
                        Console.WriteLine($"Reading value of attribute '{AttributeName}' from endpoint with UUID '{EndpointUUID}' from Zipato.");
                        var data = await _zipato.DataReadValueAsync(new Guid(EndpointUUID), AttributeName);
                        Console.WriteLine($"Value of attribute '{AttributeName}'  from endpoint with UUID '{EndpointUUID}' is '{data}'.");
                    }
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, $"Exception ValueCommand.");
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
                        case "a": OptionA = option.HasValue(); break;
                        case "e": OptionE = option.HasValue(); break;
                        case "n": OptionN = option.HasValue(); break;
                    }
                }
            }

            if (NoOptions)
            {
                app.ShowHelp();
                return false;
            }

            if (!(OptionA || OptionE))
            {
                _logger?.LogError($"Specifiy the attribute (uuid or endpoint and name).");
                return false;
            }

            if (OptionE && !OptionN)
            {
                _logger?.LogError($"Specifiy the attribute name.");
                return false;
            }

            return true;
        }

        #endregion
    }
}
