// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Settings.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeMonitor.Models
{
    #region Using Directives

    using Plugin.Settings;
    using Plugin.Settings.Abstractions;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    internal class Settings
    {
        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #endregion

        #region Public Properties

        public string Netatmo
        {
            get => AppSettings.GetValueOrDefault(nameof(Netatmo), "http://localhost:8003");
            set => AppSettings.AddOrUpdateValue(nameof(Netatmo), value);
        }

        public string KWLEC200
        {
            get => AppSettings.GetValueOrDefault(nameof(KWLEC200), "http://localhost:8003");
            set => AppSettings.AddOrUpdateValue(nameof(KWLEC200), value);
        }

        public string ETAPU11
        {
            get => AppSettings.GetValueOrDefault(nameof(ETAPU11), "http://localhost:8004");
            set => AppSettings.AddOrUpdateValue(nameof(ETAPU11), value);
        }

        public string EM300LR
        {
            get => AppSettings.GetValueOrDefault(nameof(EM300LR), "http://localhost:8005");
            set => AppSettings.AddOrUpdateValue(nameof(EM300LR), value);
        }

        public string Fronius
        {
            get => AppSettings.GetValueOrDefault(nameof(Fronius), "http://localhost:8006");
            set => AppSettings.AddOrUpdateValue(nameof(Fronius), value);
        }

        public string Zipato
        {
            get => AppSettings.GetValueOrDefault(nameof(Zipato), "http://localhost:8007");
            set => AppSettings.AddOrUpdateValue(nameof(Zipato), value);
        }

        public string HomeData
        {
            get => AppSettings.GetValueOrDefault(nameof(HomeData), "http://localhost:8008");
            set => AppSettings.AddOrUpdateValue(nameof(HomeData), value);
        }

        public string Wallbox
        {
            get => AppSettings.GetValueOrDefault(nameof(Wallbox), "http://localhost:8009");
            set => AppSettings.AddOrUpdateValue(nameof(Wallbox), value);
        }

        public string EM300LR2
        {
            get => AppSettings.GetValueOrDefault(nameof(EM300LR2), "http://localhost:8010");
            set => AppSettings.AddOrUpdateValue(nameof(EM300LR2), value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public void SetValue(object obj, string value)
        {
            switch (obj)
            {
                case Netatmo service:  Netatmo = value; break;
                case HomeData service: HomeData = value; break;
                case ETAPU11 service: ETAPU11 = value; break;
                case KWLEC200 service: KWLEC200 = value; break;
                case Fronius service: Fronius = value; break;
                case EM300LR service: EM300LR = value; break;
                case Wallbox service: Wallbox = value; break;
                case Zipato service: Zipato = value; break;
                case null: break;
                default: break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetValue(object obj)
        {
            switch (obj)
            {
                case Netatmo service: return Netatmo;
                case HomeData service: return HomeData;
                case ETAPU11 service: return ETAPU11;
                case KWLEC200 service: return KWLEC200;
                case Fronius service: return Fronius;
                case EM300LR service: return EM300LR;
                case Wallbox service: return Wallbox;
                case Zipato service: return Zipato;
                case null:
                default: return string.Empty;
            }
        }

        #endregion
    }
}
