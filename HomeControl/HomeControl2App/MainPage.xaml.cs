// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainPage.xaml.cs" company="DTV-Online">
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
    using System.Collections.Generic;
    using System.Linq;

    using Windows.System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Navigation;
    using Windows.UI.Xaml.Media.Animation;

    using Serilog;

    #endregion

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // List of ValueTuple holding the Navigation Tag and the relative Navigation Page
        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>
        {
            ("HomeData", typeof(HomeDataPage)),
            ("Netatmo", typeof(NetatmoPage)),
            ("ETAPU11", typeof(ETAPU11Page)),
            ("KWLEC200", typeof(KWLEC200Page)),
            ("Fronius", typeof(FroniusPage)),
            ("EM300LR", typeof(EM300LRPage)),
            ("Wallbox", typeof(WallboxPage)),
            ("Zipato", typeof(ZipatoPage)),
            ("Control", typeof(ControlPage)),
        };

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            Log.Information("MainPage initialized.");
        }

        #endregion

        #region Private Methods

        private void NavViewLoaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigated += OnNavigated;

            // NavView doesn't load any page by default, so load home page.
            NavView.SelectedItem = NavView.MenuItems[0];
            Navigate("HomeData", new EntranceNavigationTransitionInfo());

            // Add keyboard accelerators for backwards navigation.
            var goBack = new KeyboardAccelerator { Key = VirtualKey.GoBack };
            goBack.Invoked += BackInvoked;
            this.KeyboardAccelerators.Add(goBack);

            // ALT routes here
            var altLeft = new KeyboardAccelerator
            {
                Key = VirtualKey.Left,
                Modifiers = VirtualKeyModifiers.Menu
            };
            altLeft.Invoked += BackInvoked;
            this.KeyboardAccelerators.Add(altLeft);
        }

        private void NavViewItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
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

        private void NavViewBackRequested(NavigationView sender,
                                                        NavigationViewBackRequestedEventArgs args)
        {
            OnBackRequested();
        }

        private void BackInvoked(KeyboardAccelerator sender,
                                 KeyboardAcceleratorInvokedEventArgs args)
        {
            OnBackRequested();
            args.Handled = true;
        }

        private bool OnBackRequested()
        {
            if (!ContentFrame.CanGoBack)
                return false;

            // Don't go back if the nav pane is overlayed.
            if (NavView.IsPaneOpen &&
                (NavView.DisplayMode == NavigationViewDisplayMode.Compact ||
                 NavView.DisplayMode == NavigationViewDisplayMode.Minimal))
                return false;

            ContentFrame.GoBack();
            return true;
        }

        private void ContentFrameNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            Log.Error($"Failed to load Page {e.SourcePageType.FullName}");
            throw new Exception($"Failed to load Page {e.SourcePageType.FullName}");
        }

        private void Navigate(string navItemTag, NavigationTransitionInfo transitionInfo)
        {
            Log.Debug($"Navigate to {navItemTag}");
            Type pagetype;

            if (navItemTag == "Settings")
            {
                pagetype = typeof(SettingsPage);
            }
            else
            {
                pagetype = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag)).Page;
            }

            // Get the page type before navigation so you can prevent duplicate entries in the backstack.
            var preNavPageType = ContentFrame.CurrentSourcePageType;

            // Only navigate if the selected page isn't currently loaded.
            if (!(pagetype is null) && !Type.Equals(preNavPageType, pagetype))
            {
                ContentFrame.Navigate(pagetype, null, transitionInfo);
            }
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            NavView.IsBackEnabled = ContentFrame.CanGoBack;

            if (ContentFrame.SourcePageType == typeof(SettingsPage))
            {
                // SettingsItem is not part of NavView.MenuItems, and doesn't have a Tag.
                NavView.SelectedItem = (NavigationViewItem)NavView.SettingsItem;
                NavView.Header = "Settings";
            }
            else if (ContentFrame.SourcePageType != null)
            {
                var item = _pages.FirstOrDefault(p => p.Page == e.SourcePageType);

                NavView.SelectedItem = NavView.MenuItems
                    .OfType<NavigationViewItem>()
                    .First(n => n.Tag.Equals(item.Tag));

                NavView.Header =
                    ((NavigationViewItem)NavView.SelectedItem)?.Content?.ToString();
            }
        }

        #endregion
    }
}
