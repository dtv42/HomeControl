// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControl2App
{
    #region Using Directives

    using System;

    using Windows.ApplicationModel;
    using Windows.ApplicationModel.Activation;
    using Windows.ApplicationModel.Resources;
    using Windows.Storage;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    using Microsoft.Extensions.DependencyInjection;

    using Serilog;

    using HomeControl2App.Models;

    #endregion

    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        #region Public Properties

        public EM300LRHub EM300LRHub { get; private set; }
        public ETAPU11Hub ETAPU11Hub { get; private set; }
        public FroniusHub FroniusHub { get; private set; }
        public HomeDataHub HomeDataHub { get; private set; }
        public KWLEC200Hub KWLEC200Hub { get; private set; }
        public NetatmoHub NetatmoHub { get; private set; }
        public WallboxHub WallboxHub { get; private set; }
        public ZipatoHub ZipatoHub { get; private set; }

        #endregion

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Resuming += OnResuming;
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (!(Window.Current.Content is Frame rootFrame))
            {
                // Setup logging.
                const string template = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}";
                StorageFolder folder = ApplicationData.Current.LocalFolder;
                string path = folder.Path + "\\Logs\\{Date}.log";

                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.RollingFile(path, outputTemplate: template)
                    .CreateLogger();

                // Create IOC container and add logging feature to it.
                IServiceCollection services = new ServiceCollection();
                services.AddOptions<AppSettings>();
                services.AddLogging(builder => builder.AddSerilog(dispose: true));

                // Build provider to access the logging service.
                IServiceProvider provider = services.BuildServiceProvider();

                // Setup the data hubs.
                services.AddSingleton<EM300LRHub>();
                services.AddSingleton<ETAPU11Hub>();
                services.AddSingleton<FroniusHub>();
                services.AddSingleton<HomeDataHub>();
                services.AddSingleton<KWLEC200Hub>();
                services.AddSingleton<NetatmoHub>();
                services.AddSingleton<WallboxHub>();
                services.AddSingleton<ZipatoHub>();

                // Get the data values instances.
                EM300LRHub = ActivatorUtilities.CreateInstance<EM300LRHub>(provider);
                ETAPU11Hub = ActivatorUtilities.CreateInstance<ETAPU11Hub>(provider);
                FroniusHub = ActivatorUtilities.CreateInstance<FroniusHub>(provider);
                HomeDataHub = ActivatorUtilities.CreateInstance<HomeDataHub>(provider);
                KWLEC200Hub = ActivatorUtilities.CreateInstance<KWLEC200Hub>(provider);
                NetatmoHub = ActivatorUtilities.CreateInstance<NetatmoHub>(provider);
                WallboxHub = ActivatorUtilities.CreateInstance<WallboxHub>(provider);
                ZipatoHub = ActivatorUtilities.CreateInstance<ZipatoHub>(provider);

                Log.Information("App launched.");

                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            Log.Error("Failed to load Page " + e.SourcePageType.FullName);
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being resumed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnResuming(object sender, object e)
        {
            Log.Information("App resumed.");

            await EM300LRHub.Connect();
            await ETAPU11Hub.Connect();
            await FroniusHub.Connect();
            await HomeDataHub.Connect();
            await KWLEC200Hub.Connect();
            await NetatmoHub.Connect();
            await WallboxHub.Connect();
            await ZipatoHub.Connect();
        }

        /// <summary>
        /// Invoked when application execution is being suspended. Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            Log.Information("App suspended.");

            await EM300LRHub.Disconnect();
            await ETAPU11Hub.Disconnect();
            await FroniusHub.Disconnect();
            await HomeDataHub.Disconnect();
            await KWLEC200Hub.Disconnect();
            await NetatmoHub.Disconnect();
            await WallboxHub.Disconnect();
            await ZipatoHub.Disconnect();

            Log.CloseAndFlush();
            deferral.Complete();
        }
    }
}
