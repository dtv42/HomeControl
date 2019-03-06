// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppSettings.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControl2App.Models
{
    #region Using Directives

    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Windows.ApplicationModel.Resources;
    using Windows.Storage;

    #endregion

    public class AppSettings : INotifyPropertyChanged
    {
        private const string APP_SETTINGS = "AppSettings";

        private readonly string _em300lr = string.Empty;
        private readonly string _etapu11 = string.Empty;
        private readonly string _fronius = string.Empty;
        private readonly string _homedata = string.Empty;
        private readonly string _kwlec200 = string.Empty;
        private readonly string _netatmo = string.Empty;
        private readonly string _wallbox = string.Empty;
        private readonly string _zipato = string.Empty;

        private ApplicationDataContainer _settingsContainer = null;

        public string Netatmo
        {
            get => ReadSettings(nameof(Netatmo), _netatmo);
            set { SaveSettings(nameof(Netatmo), value); NotifyPropertyChanged(); }
        }

        public string KWLEC200
        {
            get => ReadSettings(nameof(KWLEC200), _kwlec200);
            set { SaveSettings(nameof(KWLEC200), value); NotifyPropertyChanged(); }
        }

        public string ETAPU11
        {
            get => ReadSettings(nameof(ETAPU11), _etapu11);
            set { SaveSettings(nameof(ETAPU11), value); NotifyPropertyChanged(); }
        }

        public string EM300LR
        {
            get => ReadSettings(nameof(EM300LR), _em300lr);
            set { SaveSettings(nameof(EM300LR), value); NotifyPropertyChanged(); }
        }

        public string Fronius
        {
            get => ReadSettings(nameof(Fronius), _fronius);
            set { SaveSettings(nameof(Fronius), value); NotifyPropertyChanged(); }
        }

        public string Zipato
        {
            get => ReadSettings(nameof(Zipato), _zipato);
            set { SaveSettings(nameof(Zipato), value); NotifyPropertyChanged(); }
        }

        public string HomeData
        {
            get => ReadSettings(nameof(HomeData), _homedata);
            set { SaveSettings(nameof(HomeData), value); NotifyPropertyChanged(); }
        }

        public string Wallbox
        {
            get => ReadSettings(nameof(Wallbox), _wallbox);
            set { SaveSettings(nameof(Wallbox), value); NotifyPropertyChanged(); }
        }

        public AppSettings()
        {
            var loader = new ResourceLoader();

            _em300lr = loader.GetString("EM300LR");
            _etapu11 = loader.GetString("ETAPU11");
            _fronius = loader.GetString("Fronius");
            _homedata = loader.GetString("HomeData");
            _kwlec200 = loader.GetString("KWLEC200");
            _netatmo = loader.GetString("Netatmo");
            _wallbox = loader.GetString("Wallbox");
            _zipato = loader.GetString("Zipato");

            _settingsContainer = ApplicationData.Current.LocalSettings;
        }

        private void SaveSettings(string key, object value)
        {
            ApplicationDataCompositeValue composite = new ApplicationDataCompositeValue();

            if (_settingsContainer.Values.ContainsKey(APP_SETTINGS))
            {
                composite = (ApplicationDataCompositeValue)_settingsContainer.Values[APP_SETTINGS];
            }

            composite[key] = value;
            _settingsContainer.Values[APP_SETTINGS] = composite;
        }

        private T ReadSettings<T>(string key, T defaultValue)
        {
            if (_settingsContainer.Values.ContainsKey(APP_SETTINGS))
            {
                var composite = (ApplicationDataCompositeValue)_settingsContainer.Values[APP_SETTINGS];

                if (composite.ContainsKey(key))
                {
                    return (T)composite[key];
                }
            }

            if (defaultValue != null)
            {
                SaveSettings(key, defaultValue);
                return defaultValue;
            }

            return default(T);
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
