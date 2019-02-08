// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainPage.xaml.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HomeControlApp
{
    #region Using Directives

    using System;
    using System.Linq;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;
    using Windows.UI.Xaml.Media.Animation;

    using Serilog;
    using HomeControlApp.Views;

    #endregion

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Constructors

        public MainPage()
        {
            this.InitializeComponent();
            Log.Information("MainPage initialized.");
        }

        #endregion

        #region Private Methods

        private void NavigationViewControlLoaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigated += OnNavigated;

            Navigate("HomeData", new EntranceNavigationTransitionInfo());
        }

        private void NavigationViewControlItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked == true)
            {
                Navigate("Settings", args.RecommendedNavigationTransitionInfo);
            }
            else if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void ContentFrameNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            Log.Error($"Failed to load Page {e.SourcePageType.FullName}");
            throw new Exception($"Failed to load Page {e.SourcePageType.FullName}");
        }

        private void Navigate(string navItemTag, NavigationTransitionInfo transitionInfo)
        {
            Log.Debug($"Navigate to {navItemTag}");
            Type pagetype = typeof(GaugesPage);

            if (navItemTag == "Settings")
            {
                pagetype = typeof(SettingsPage);
            }

            // Get the page type before navigation so you can prevent duplicate entries in the backstack.
            var preNavPageType = ContentFrame.CurrentSourcePageType;

            if (!Type.Equals(preNavPageType, pagetype))
            {
                ContentFrame.Navigate(pagetype, navItemTag, transitionInfo);
            }

            if (ContentFrame.SourcePageType == typeof(GaugesPage))
            {
                ((GaugesPage)ContentFrame.Content).Initialize(navItemTag);
                NavigationViewControl.Header = navItemTag;
            }
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            string navItemTag = (string)e.Parameter;

            if (ContentFrame.SourcePageType == typeof(SettingsPage))
            {
                NavigationViewControl.SelectedItem = (NavigationViewItem)NavigationViewControl.SettingsItem;
                NavigationViewControl.Header = "Settings";
            }
            else if (ContentFrame.SourcePageType != null)
            {
                NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems
                    .OfType<NavigationViewItem>()
                    .First(n => n.Tag.Equals(navItemTag));

                NavigationViewControl.Header =
                    ((NavigationViewItem)NavigationViewControl.SelectedItem)?.Content?.ToString();
            }
        }

        #endregion
    }
}
