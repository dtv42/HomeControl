// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WriteCommand.cs" company="DTV-Online">
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
    using System.ComponentModel.DataAnnotations;
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
    /// Application command "write".
    /// </summary>
    [Command(Name = "write",
             FullName = "Zipato Write Command",
             Description = "Writing data values to Zipato home control.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class WriteCommand : BaseCommand<AppSettings>
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
            get => !(OptionA || OptionE);
        }

        #endregion

        #region Public Properties

        [Uuid]
        [Option("-a|--attribute <uuid>", "Specifies the attribute uuid.", CommandOptionType.SingleValue)]
        public string AttributeUUID { get; set; }

        [Uuid]
        [Option("-e|--endpoint <uuid>", "Specifies the endpoint (uuid or name).", CommandOptionType.SingleValue)]
        public string EndpointUUID { get; set; }

        [Option("-n|--name <string>", "Specifies the attribute name.", CommandOptionType.SingleValue)]
        public string AttributeName { get; set; } = string.Empty;

        [Required]
        [Argument(0, "attribute value")]
        public string Value { get; set; } = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteCommand"/> class.
        /// </summary>
        /// <param name="zipato">The Zipato instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public WriteCommand(IZipato zipato,
                           ILogger<WriteCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("WriteCommand()");

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
                        Console.WriteLine($"Writing attribute value with UUID '{AttributeUUID}' to Zipato.");

                        if (await _zipato.SetValueAsync(new Guid(AttributeUUID), Value))
                        {
                            Console.WriteLine($"Writing successful.");
                        }
                        else
                        {
                            Console.WriteLine($"Writing NOT successful.");
                        }
                    }
                    else if (OptionE)
                    {
                        Console.WriteLine($"Writing value '{Value}' to attribute '{AttributeName}' of endpoint '{EndpointUUID}' to Zipato.");

                        if (await _zipato.SetValueByNameAsync(new Guid(EndpointUUID), AttributeName, Value))
                        {
                            Console.WriteLine($"Writing successful.");
                        }
                        else
                        {
                            Console.WriteLine($"Writing NOT successful.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, $"Exception WriteCommand.");
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

            if (OptionE && !OptionN)
            {
                Console.WriteLine($"Specifiy the attribute name.");
                return false;
            }

            if (OptionA && OptionN)
            {
                Console.WriteLine($"Attribute name '{AttributeName}' is ignored.");
                return false;
            }

            return true;
        }

        #endregion
    }
}
