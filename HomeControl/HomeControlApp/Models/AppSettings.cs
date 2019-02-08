// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppSettings.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlApp.Models
{
    #region Using Directives

    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Windows.Storage;

    #endregion

    internal class AppSettings : INotifyPropertyChanged
    {
        private const string APP_SETTINGS = "AppSettings";

        private ApplicationDataContainer _settingsContainer = null;

        public string Netatmo
        {
            get => ReadSettings(nameof(Netatmo), "http://localhost:8002");
            set { SaveSettings(nameof(Netatmo), value); NotifyPropertyChanged(); }
        }

        public string KWLEC200
        {
            get => ReadSettings(nameof(KWLEC200), "http://localhost:8003");
            set { SaveSettings(nameof(KWLEC200), value); NotifyPropertyChanged(); }
        }

        public string ETAPU11
        {
            get => ReadSettings(nameof(ETAPU11), "http://localhost:8004");
            set { SaveSettings(nameof(ETAPU11), value); NotifyPropertyChanged(); }
        }

        public string EM300LR1
        {
            get => ReadSettings(nameof(EM300LR1), "http://localhost:8005");
            set { SaveSettings(nameof(EM300LR1), value); NotifyPropertyChanged(); }
        }

        public string Fronius
        {
            get => ReadSettings(nameof(Fronius), "http://localhost:8006");
            set { SaveSettings(nameof(Fronius), value); NotifyPropertyChanged(); }
        }

        public string Zipato
        {
            get => ReadSettings(nameof(Zipato), "http://localhost:8007");
            set { SaveSettings(nameof(Zipato), value); NotifyPropertyChanged(); }
        }

        public string HomeData
        {
            get => ReadSettings(nameof(HomeData), "http://localhost:8008");
            set { SaveSettings(nameof(HomeData), value); NotifyPropertyChanged(); }
        }

        public string Wallbox
        {
            get => ReadSettings(nameof(Wallbox), "http://localhost:8009");
            set { SaveSettings(nameof(Wallbox), value); NotifyPropertyChanged(); }
        }

        public string EM300LR2
        {
            get => ReadSettings(nameof(EM300LR2), "http://localhost:8010");
            set { SaveSettings(nameof(EM300LR2), value); NotifyPropertyChanged(); }
        }

        public ApplicationDataContainer LocalSettings { get; set; }

        public AppSettings()
        {
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
