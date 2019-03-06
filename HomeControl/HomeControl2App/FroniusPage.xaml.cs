// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FroniusPage.xaml.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControl2App
{
    #region Using Directives

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using HomeControl2App.Models;

    #endregion

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FroniusPage : Page
    {
        #region Private Data Members

        private readonly App _application = App.Current as App;

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public FroniusHub Hub { get => _application.FroniusHub; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public FroniusPage()
        {
            this.InitializeComponent();
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

        #endregion
    }
}
