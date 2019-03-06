// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlPage.xaml.cs" company="DTV-Online">
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

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;

    using HomeControl2App.Models;

    #endregion

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ControlPage : Page
    {
        #region Private Data Members

        private readonly App _application = App.Current as App;

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public ZipatoHub Hub { get => _application.ZipatoHub; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public ControlPage()
        {
            this.InitializeComponent();
            this.DataContext = Hub;
        }

        #endregion

        #region Public Methods

        public void SetIntensityDimmer1(double value)
        {
            Hub.Devices.Dimmers[0].Intensity.Value = (int)value;
        }

        #endregion

        #region Event Handler

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            await Hub.Connect();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnPageUnloaded(object sender, RoutedEventArgs e)
        {
            await Hub.Disconnect();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnSceneClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var index = int.Parse((string)button.Tag);
            await Hub.RunScene(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnSwitchClick(object sender, RoutedEventArgs e)
        {
            var button = sender as ToggleButton;
            var index = int.Parse((string)button.Tag);
            await Hub.ToggleOnOff(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnPlugClick(object sender, RoutedEventArgs e)
        {
            var button = sender as ToggleButton;
            var index = int.Parse((string)button.Tag);
            await Hub.TogglePlug(index);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnSetDimmer(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var index = int.Parse((string)button.Tag);
            await Hub.SetDimmer(index, Hub.Devices.Dimmers[index].Intensity.Value);
        }

        #endregion
    }
}
