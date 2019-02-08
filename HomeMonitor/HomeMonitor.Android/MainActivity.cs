// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainActivity.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeMonitor.Droid
{
    using Acr.UserDialogs;
    using Android.App;
    using Android.Content.PM;
    using Android.OS;

    using ButtonCircle.FormsPlugin.Droid;

    [Activity(Label = "HomeMonitor", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            ButtonCircleRenderer.Init();
            UserDialogs.Init(this);
            LoadApplication(new App());
        }
    }
}
