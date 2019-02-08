// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GaugesPage.xaml.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HomeControlApp.Views
{
    #region Using Directives

    using System;
    using System.Net.Http;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using Newtonsoft.Json;
    using Serilog;

    using HomeControlLib.HomeData.Models;
    using HomeControlLib.Netatmo.Models;
    using HomeControlLib.ETAPU11.Models;
    using HomeControlLib.KWLEC200.Models;
    using HomeControlLib.Fronius.Models;
    using HomeControlLib.EM300LR.Models;
    using HomeControlLib.Wallbox.Models;
    using HomeControlLib.Zipato.Models;
    using HomeControlApp.Models;

    #endregion

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GaugesPage : Page
    {
        #region Private Data Members

        private readonly object updateLock = new object();
        private DispatcherTimer _timer = new DispatcherTimer();
        private string _tag = "HomeData";
        private bool _initializedHomeData;
        private bool _initializedNetatmo;
        private bool _initializedETAPU11;
        private bool _initializedKWLEC200;
        private bool _initializedFronius;
        private bool _initializedEM300LR;
        private bool _initializedWallbox;
        private bool _initializedZipato;
        private HttpClient _clientHomeData;
        private HttpClient _clientNetatmo;
        private HttpClient _clientETAPU11;
        private HttpClient _clientKWLEC200;
        private HttpClient _clientFronius;
        private HttpClient _clientEM300LR1;
        private HttpClient _clientEM300LR2;
        private HttpClient _clientWallbox;
        private HttpClient _clientZipato;

        #endregion

        #region Constructors

        public GaugesPage()
        {
            Log.Debug("GaugesPage()");
            this.InitializeComponent();
            AppSettings settings = new AppSettings();

            _clientHomeData = new HttpClient() { BaseAddress = new Uri(settings.HomeData) };
            _clientNetatmo = new HttpClient() { BaseAddress = new Uri(settings.Netatmo) };
            _clientETAPU11 = new HttpClient() { BaseAddress = new Uri(settings.ETAPU11) };
            _clientKWLEC200 = new HttpClient() { BaseAddress = new Uri(settings.KWLEC200) };
            _clientFronius = new HttpClient() { BaseAddress = new Uri(settings.Fronius) };
            _clientEM300LR1 = new HttpClient() { BaseAddress = new Uri(settings.EM300LR1) };
            _clientEM300LR2 = new HttpClient() { BaseAddress = new Uri(settings.EM300LR2) };
            _clientWallbox = new HttpClient() { BaseAddress = new Uri(settings.Wallbox) };
            _clientZipato = new HttpClient() { BaseAddress = new Uri(settings.Zipato) };

            _timer.Tick += OnUpdateTimerElapsed;
            _timer.Interval = new TimeSpan(0, 1, 0);
        }

        #endregion

        #region Private Event Handlers

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Log.Debug("GaugesPage loaded.");
            _timer.Start();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            Log.Debug("GaugesPage unloaded.");
            _timer.Stop();

            _initializedHomeData = false;
            _initializedNetatmo = false;
            _initializedETAPU11 = false;
            _initializedKWLEC200 = false;
            _initializedFronius = false;
            _initializedEM300LR = false;
            _initializedWallbox = false;
            _initializedZipato = false;
        }

        private void OnUpdateTimerElapsed(object sender, object e)
        {
            Log.Debug("UpdateTimer elapsed.");
            UpdateGauges();
        }

        #endregion

        #region Public Methods

        public void Initialize(string tag)
        {
            Log.Debug($"Initialize({tag}).");
            _tag = tag;

            InitializeGauges();
            UpdateGauges();
        }

        #endregion

        #region Initialization Methods

        private void InitializeGauges()
        {
            Log.Debug("InitializeGauges() started.");

            switch (_tag)
            {
                case "HomeData":
                    InitializeHomeData();
                    break;
                case "Netatmo":
                    InitializeNetatmo();
                    break;
                case "ETAPU11":
                    InitializeETAPU11();
                    break;
                case "KWLEC200":
                    InitializeKWLEC200();
                    break;
                case "Fronius":
                    InitializeFronius();
                    break;
                case "EM300LR1":
                    InitializeEM300LR();
                    break;
                case "EM300LR2":
                    InitializeEM300LR();
                    break;
                case "Wallbox":
                    InitializeWallbox();
                    break;
                case "Zipato":
                    InitializeZipato();
                    break;
            }

            Log.Debug("InitializeGauges() done.");
        }

        private void InitializeHomeData()
        {
            HomeData.Visibility = Visibility.Visible;
            EM300LR.Visibility = Visibility.Collapsed;
            Netatmo.Visibility = Visibility.Collapsed;
            Fronius.Visibility = Visibility.Collapsed;
            ETAPU11.Visibility = Visibility.Collapsed;
            KWLEC200.Visibility = Visibility.Collapsed;
            Wallbox.Visibility = Visibility.Collapsed;
            Zipato.Visibility = Visibility.Collapsed;

            if (!_initializedHomeData)
            {
                Log.Debug("InitializeHomeData() started.");
                HomeDataGauge01.Initialize();
                HomeDataGauge02.Initialize();
                HomeDataGauge03.Initialize();
                HomeDataGauge04.Initialize();
                HomeDataGauge05.Initialize();
                HomeDataGauge06.Initialize();
                HomeDataGauge07.Initialize();
                HomeDataGauge08.Initialize();
                HomeDataGauge09.Initialize();
                HomeDataGauge10.Initialize();
                HomeDataGauge11.Initialize();
                HomeDataGauge12.Initialize();
                Log.Debug("InitializeHomeData() done.");

                _initializedHomeData = true;
            }
        }

        private void InitializeNetatmo()
        {
            HomeData.Visibility = Visibility.Collapsed;
            EM300LR.Visibility = Visibility.Collapsed;
            Netatmo.Visibility = Visibility.Visible;
            Fronius.Visibility = Visibility.Collapsed;
            ETAPU11.Visibility = Visibility.Collapsed;
            KWLEC200.Visibility = Visibility.Collapsed;
            Wallbox.Visibility = Visibility.Collapsed;
            Zipato.Visibility = Visibility.Collapsed;

            if (!_initializedNetatmo)
            {
                Log.Debug("InitializeNetatmo() started.");
                NetatmoGauge01.Initialize();
                NetatmoGauge02.Initialize();
                NetatmoGauge03.Initialize();
                NetatmoGauge04.Initialize();
                NetatmoGauge05.Initialize();
                NetatmoGauge06.Initialize();
                NetatmoGauge07.Initialize();
                NetatmoGauge08.Initialize();
                NetatmoGauge09.Initialize();
                NetatmoGauge10.Initialize();
                NetatmoGauge11.Initialize();
                NetatmoGauge12.Initialize();
                NetatmoGauge13.Initialize();
                NetatmoGauge14.Initialize();
                NetatmoGauge15.Initialize();
                NetatmoGauge16.Initialize();
                NetatmoGauge17.Initialize();
                NetatmoGauge18.Initialize();
                NetatmoGauge19.Initialize();
                NetatmoGauge20.Initialize();
                NetatmoGauge21.Initialize();
                Log.Debug("InitializeNetatmo() done.");

                _initializedNetatmo = true;
            }
        }

        private void InitializeETAPU11()
        {
            HomeData.Visibility = Visibility.Collapsed;
            EM300LR.Visibility = Visibility.Collapsed;
            Netatmo.Visibility = Visibility.Collapsed;
            Fronius.Visibility = Visibility.Collapsed;
            ETAPU11.Visibility = Visibility.Visible;
            KWLEC200.Visibility = Visibility.Collapsed;
            Wallbox.Visibility = Visibility.Collapsed;
            Zipato.Visibility = Visibility.Collapsed;

            if (!_initializedETAPU11)
            {
                Log.Debug("InitializeETAPU11() started.");
                ETAPU11Gauge01.Initialize();
                ETAPU11Gauge02.Initialize();
                ETAPU11Gauge03.Initialize();
                ETAPU11Gauge04.Initialize();
                ETAPU11Gauge05.Initialize();
                ETAPU11Gauge06.Initialize();
                ETAPU11Gauge07.Initialize();
                ETAPU11Gauge08.Initialize();
                ETAPU11Gauge09.Initialize();
                ETAPU11Gauge10.Initialize();
                ETAPU11Gauge11.Initialize();
                ETAPU11Gauge12.Initialize();
                ETAPU11Gauge13.Initialize();
                ETAPU11Gauge14.Initialize();
                ETAPU11Gauge15.Initialize();
                Log.Debug("InitializeETAPU11() done.");

                _initializedETAPU11 = true;
            }
        }

        private void InitializeKWLEC200()
        {
            HomeData.Visibility = Visibility.Collapsed;
            EM300LR.Visibility = Visibility.Collapsed;
            Netatmo.Visibility = Visibility.Collapsed;
            Fronius.Visibility = Visibility.Collapsed;
            ETAPU11.Visibility = Visibility.Collapsed;
            KWLEC200.Visibility = Visibility.Visible;
            Wallbox.Visibility = Visibility.Collapsed;
            Zipato.Visibility = Visibility.Collapsed;

            if (!_initializedKWLEC200)
            {
                Log.Debug("InitializeKWLEC200() started.");
                KWLEC200Gauge01.Initialize();
                KWLEC200Gauge02.Initialize();
                KWLEC200Gauge03.Initialize();
                KWLEC200Gauge04.Initialize();
                KWLEC200Gauge05.Initialize();
                KWLEC200Gauge06.Initialize();
                Log.Debug("InitializeKWLEC200() done.");

                _initializedKWLEC200 = true;
            }
        }

        private void InitializeFronius()
        {
            HomeData.Visibility = Visibility.Collapsed;
            EM300LR.Visibility = Visibility.Collapsed;
            Netatmo.Visibility = Visibility.Collapsed;
            Fronius.Visibility = Visibility.Visible;
            ETAPU11.Visibility = Visibility.Collapsed;
            KWLEC200.Visibility = Visibility.Collapsed;
            Wallbox.Visibility = Visibility.Collapsed;
            Zipato.Visibility = Visibility.Collapsed;

            if (!_initializedFronius)
            {
                Log.Debug("InitializeFronius() started.");
                FroniusGauge01.Initialize();
                FroniusGauge02.Initialize();
                FroniusGauge03.Initialize();
                FroniusGauge04.Initialize();
                FroniusGauge05.Initialize();
                FroniusGauge06.Initialize();
                Log.Debug("InitializeFronius() done.");

                _initializedFronius = true;
            }
        }

        private void InitializeEM300LR()
        {
            HomeData.Visibility = Visibility.Collapsed;
            EM300LR.Visibility = Visibility.Visible;
            Netatmo.Visibility = Visibility.Collapsed;
            Fronius.Visibility = Visibility.Collapsed;
            ETAPU11.Visibility = Visibility.Collapsed;
            KWLEC200.Visibility = Visibility.Collapsed;
            Wallbox.Visibility = Visibility.Collapsed;
            Zipato.Visibility = Visibility.Collapsed;

            if (!_initializedEM300LR)
            {
                Log.Debug("InitializeEM300LR() started.");
                EM300LRGauge01.Initialize();
                EM300LRGauge02.Initialize();
                EM300LRGauge03.Initialize();
                EM300LRGauge04.Initialize();
                EM300LRGauge05.Initialize();
                EM300LRGauge06.Initialize();
                EM300LRGauge07.Initialize();
                EM300LRGauge08.Initialize();
                EM300LRGauge09.Initialize();
                EM300LRGauge10.Initialize();
                EM300LRGauge11.Initialize();
                EM300LRGauge12.Initialize();
                EM300LRGauge13.Initialize();
                EM300LRGauge14.Initialize();
                EM300LRGauge15.Initialize();
                EM300LRGauge16.Initialize();
                EM300LRGauge17.Initialize();
                EM300LRGauge18.Initialize();
                EM300LRGauge19.Initialize();
                EM300LRGauge20.Initialize();
                EM300LRGauge21.Initialize();
                EM300LRGauge22.Initialize();
                EM300LRGauge23.Initialize();
                EM300LRGauge24.Initialize();
                Log.Debug("InitializeEM300LR() done.");

                _initializedEM300LR = true;
            }
        }

        private void InitializeWallbox()
        {
            HomeData.Visibility = Visibility.Collapsed;
            EM300LR.Visibility = Visibility.Collapsed;
            Netatmo.Visibility = Visibility.Collapsed;
            Fronius.Visibility = Visibility.Collapsed;
            ETAPU11.Visibility = Visibility.Collapsed;
            KWLEC200.Visibility = Visibility.Collapsed;
            Wallbox.Visibility = Visibility.Visible;
            Zipato.Visibility = Visibility.Collapsed;

            if (!_initializedWallbox)
            {
                Log.Debug("InitializeWallbox() started.");
                WallboxGauge01.Initialize();
                WallboxGauge02.Initialize();
                WallboxGauge03.Initialize();
                WallboxGauge04.Initialize();
                WallboxGauge05.Initialize();
                WallboxGauge06.Initialize();
                WallboxGauge07.Initialize();
                WallboxGauge08.Initialize();
                WallboxGauge09.Initialize();
                Log.Debug("InitializeWallbox() done.");

                _initializedWallbox = true;
            }
        }

        private void InitializeZipato()
        {
            HomeData.Visibility = Visibility.Collapsed;
            EM300LR.Visibility = Visibility.Collapsed;
            Netatmo.Visibility = Visibility.Collapsed;
            Fronius.Visibility = Visibility.Collapsed;
            ETAPU11.Visibility = Visibility.Collapsed;
            KWLEC200.Visibility = Visibility.Collapsed;
            Wallbox.Visibility = Visibility.Collapsed;
            Zipato.Visibility = Visibility.Visible;

            if (!_initializedZipato)
            {
                Log.Debug("InitializeZipato() started.");
                ZipatoGauge01.Initialize();
                ZipatoGauge02.Initialize();
                ZipatoGauge03.Initialize();
                ZipatoGauge04.Initialize();
                ZipatoGauge05.Initialize();
                ZipatoGauge06.Initialize();
                ZipatoGauge07.Initialize();
                ZipatoGauge08.Initialize();
                ZipatoGauge09.Initialize();
                ZipatoGauge10.Initialize();
                ZipatoGauge11.Initialize();
                ZipatoGauge12.Initialize();
                ZipatoGauge13.Initialize();
                ZipatoGauge14.Initialize();
                ZipatoGauge15.Initialize();
                Log.Debug("InitializeZipato() done.");

                _initializedZipato = true;
            }
        }

        #endregion

        #region Update Methods

        private void UpdateGauges()
        {
            Log.Debug("UpdateGauges() started.");

            lock (updateLock)
            {
                switch (_tag)
                {
                    case "HomeData":
                        UpdateHomeData();
                        break;
                    case "Netatmo":
                        UpdateNetatmo();
                        break;
                    case "ETAPU11":
                        UpdateETAPU11();
                        break;
                    case "KWLEC200":
                        UpdateKWLEC200();
                        break;
                    case "Fronius":
                        UpdateFronius();
                        break;
                    case "EM300LR1":
                        UpdateEM300LR1();
                        break;
                    case "EM300LR2":
                        UpdateEM300LR2();
                        break;
                    case "Wallbox":
                        UpdateWallbox();
                        break;
                    case "Zipato":
                        UpdateZipato();
                        break;
                }
            }

            Log.Debug("UpdateGauges() done.");
        }

        private void UpdateHomeData()
        {
            Log.Debug("UpdateHomeData() started.");
            try
            {
                var json = _clientHomeData.GetStringAsync("api/homedata/all").Result;
                var result = JsonConvert.DeserializeObject<HomeValues>(json);

                if (result.IsGood)
                {
                    HomeDataGauge01.Error = false;
                    HomeDataGauge02.Error = false;
                    HomeDataGauge03.Error = false;
                    HomeDataGauge04.Error = false;
                    HomeDataGauge05.Error = false;
                    HomeDataGauge06.Error = false;
                    HomeDataGauge07.Error = false;
                    HomeDataGauge08.Error = false;
                    HomeDataGauge09.Error = false;
                    HomeDataGauge10.Error = false;
                    HomeDataGauge11.Error = false;
                    HomeDataGauge12.Error = false;

                    HomeDataGauge01.Value = result.Load;
                    HomeDataGauge02.Value = result.Demand;
                    HomeDataGauge03.Value = result.Surplus;
                    HomeDataGauge04.Value = result.LoadL1;
                    HomeDataGauge05.Value = result.DemandL1;
                    HomeDataGauge06.Value = result.SurplusL1;
                    HomeDataGauge07.Value = result.LoadL2;
                    HomeDataGauge08.Value = result.DemandL2;
                    HomeDataGauge09.Value = result.SurplusL2;
                    HomeDataGauge10.Value = result.LoadL3;
                    HomeDataGauge11.Value = result.DemandL3;
                    HomeDataGauge12.Value = result.SurplusL3;
                    Log.Debug("UpdateHomeData() done.");
                }
                else
                {
                    HomeDataGauge01.Error = true;
                    HomeDataGauge02.Error = true;
                    HomeDataGauge03.Error = true;
                    HomeDataGauge04.Error = true;
                    HomeDataGauge05.Error = true;
                    HomeDataGauge06.Error = true;
                    HomeDataGauge07.Error = true;
                    HomeDataGauge08.Error = true;
                    HomeDataGauge09.Error = true;
                    HomeDataGauge10.Error = true;
                    HomeDataGauge11.Error = true;
                    HomeDataGauge12.Error = true;
                    Log.Error($"UpdateHomeData() not OK: {result.Status.Explanation}.");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"UpdateHomeData() exception: {ex.Message}.");
            }
        }

        private void UpdateNetatmo()
        {
            Log.Debug("UpdateNetatmo() started.");
            try
            {
                var json = _clientNetatmo.GetStringAsync("api/netatmo/all").Result;
                var result = JsonConvert.DeserializeObject<NetatmoData>(json);

                if (result.Status.IsGood)
                {
                    NetatmoGauge01.Error = false;
                    NetatmoGauge02.Error = false;
                    NetatmoGauge03.Error = false;
                    NetatmoGauge04.Error = false;
                    NetatmoGauge05.Error = false;
                    NetatmoGauge06.Error = false;
                    NetatmoGauge07.Error = false;
                    NetatmoGauge08.Error = false;
                    NetatmoGauge09.Error = false;
                    NetatmoGauge10.Error = false;
                    NetatmoGauge11.Error = false;
                    NetatmoGauge12.Error = false;
                    NetatmoGauge13.Error = false;
                    NetatmoGauge14.Error = false;
                    NetatmoGauge15.Error = false;
                    NetatmoGauge16.Error = false;
                    NetatmoGauge17.Error = false;
                    NetatmoGauge18.Error = false;
                    NetatmoGauge19.Error = false;
                    NetatmoGauge20.Error = false;
                    NetatmoGauge21.Error = false;
                    NetatmoGauge22.Error = false;
                    NetatmoGauge23.Error = false;
                    NetatmoGauge24.Error = false;
                    NetatmoGauge01.Value = result.Device.OutdoorModule.DashboardData.Temperature;
                    NetatmoGauge02.Value = result.Device.OutdoorModule.DashboardData.Humidity;
                    NetatmoGauge03.Value = result.Device.DashboardData.Pressure;
                    NetatmoGauge04.Value = result.Device.DashboardData.Temperature;
                    NetatmoGauge05.Value = result.Device.DashboardData.Humidity;
                    NetatmoGauge06.Value = result.Device.DashboardData.CO2;
                    NetatmoGauge07.Value = result.Device.IndoorModule1.DashboardData.Temperature;
                    NetatmoGauge08.Value = result.Device.IndoorModule1.DashboardData.Humidity;
                    NetatmoGauge09.Value = result.Device.IndoorModule1.DashboardData.CO2;
                    NetatmoGauge10.Value = result.Device.IndoorModule2.DashboardData.Temperature;
                    NetatmoGauge11.Value = result.Device.IndoorModule2.DashboardData.Humidity;
                    NetatmoGauge12.Value = result.Device.IndoorModule2.DashboardData.CO2;
                    NetatmoGauge13.Value = result.Device.IndoorModule3.DashboardData.Temperature;
                    NetatmoGauge14.Value = result.Device.IndoorModule3.DashboardData.Humidity;
                    NetatmoGauge15.Value = result.Device.IndoorModule3.DashboardData.CO2;
                    NetatmoGauge16.Value = result.Device.RainGauge.DashboardData.Rain;
                    NetatmoGauge17.Value = result.Device.RainGauge.DashboardData.SumRain1;
                    NetatmoGauge18.Value = result.Device.RainGauge.DashboardData.SumRain24;
                    NetatmoGauge19.Value = result.Device.WindGauge.DashboardData.WindStrength;
                    NetatmoGauge20.Value = result.Device.WindGauge.DashboardData.GustStrength;
                    NetatmoGauge21.Value = result.Device.WindGauge.DashboardData.MaxWindStrength;
                    NetatmoGauge22.Value = result.Device.WindGauge.DashboardData.WindAngle;
                    NetatmoGauge23.Value = result.Device.WindGauge.DashboardData.GustAngle;
                    NetatmoGauge24.Value = result.Device.WindGauge.DashboardData.MaxWindAngle;
                    Log.Debug("UpdateNetatmo() done.");
                }
                else
                {
                    NetatmoGauge01.Error = true;
                    NetatmoGauge02.Error = true;
                    NetatmoGauge03.Error = true;
                    NetatmoGauge04.Error = true;
                    NetatmoGauge05.Error = true;
                    NetatmoGauge06.Error = true;
                    NetatmoGauge07.Error = true;
                    NetatmoGauge08.Error = true;
                    NetatmoGauge09.Error = true;
                    NetatmoGauge10.Error = true;
                    NetatmoGauge11.Error = true;
                    NetatmoGauge12.Error = true;
                    NetatmoGauge13.Error = true;
                    NetatmoGauge14.Error = true;
                    NetatmoGauge15.Error = true;
                    NetatmoGauge16.Error = true;
                    NetatmoGauge17.Error = true;
                    NetatmoGauge18.Error = true;
                    NetatmoGauge19.Error = true;
                    NetatmoGauge20.Error = true;
                    NetatmoGauge21.Error = true;
                    NetatmoGauge22.Error = true;
                    NetatmoGauge23.Error = true;
                    NetatmoGauge24.Error = true;
                    Log.Error($"UpdateNetatmo() not OK: {result.Status.Explanation}.");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"UpdateNetatmo() exception: {ex.Message}.");
            }
        }

        private void UpdateETAPU11()
        {
            Log.Debug("UpdateETAPU11() started.");
            try
            {
                var json = _clientETAPU11.GetStringAsync("api/etapu11/all").Result;
                var result = JsonConvert.DeserializeObject<ETAPU11Data>(json);

                if (result.Status.IsGood)
                {
                    ETAPU11Gauge01.Error = false;
                    ETAPU11Gauge02.Error = false;
                    ETAPU11Gauge03.Error = false;
                    ETAPU11Gauge04.Error = false;
                    ETAPU11Gauge05.Error = false;
                    ETAPU11Gauge06.Error = false;
                    ETAPU11Gauge07.Error = false;
                    ETAPU11Gauge08.Error = false;
                    ETAPU11Gauge09.Error = false;
                    ETAPU11Gauge10.Error = false;
                    ETAPU11Gauge11.Error = false;
                    ETAPU11Gauge12.Error = false;
                    ETAPU11Gauge13.Error = false;
                    ETAPU11Gauge14.Error = false;
                    ETAPU11Gauge15.Error = false;
                    ETAPU11Gauge01.Value = result.BoilerTemperature;
                    ETAPU11Gauge02.Value = result.BoilerBottom;
                    ETAPU11Gauge03.Value = result.BoilerPressure;
                    ETAPU11Gauge04.Value = result.FlueGasTemperature;
                    ETAPU11Gauge05.Value = result.DraughtFanSpeed;
                    ETAPU11Gauge06.Value = result.ResidualO2;
                    ETAPU11Gauge07.Value = result.HeatingTemperature;
                    ETAPU11Gauge08.Value = result.Flow;
                    ETAPU11Gauge09.Value = result.RoomTemperature;
                    ETAPU11Gauge10.Value = result.HotwaterTemperature;
                    ETAPU11Gauge11.Value = result.HotwaterTarget;
                    ETAPU11Gauge12.Value = result.ChargingTimesTemperature;
                    ETAPU11Gauge13.Value = result.Stock;
                    ETAPU11Gauge14.Value = result.HopperPelletBinContents / 10.0;
                    ETAPU11Gauge15.Value = result.DischargeScrewMotorCurr;
                    Log.Debug("UpdateETAPU11() done.");
                }
                else
                {
                    ETAPU11Gauge01.Error = false;
                    ETAPU11Gauge02.Error = false;
                    ETAPU11Gauge03.Error = false;
                    ETAPU11Gauge04.Error = false;
                    ETAPU11Gauge05.Error = false;
                    ETAPU11Gauge06.Error = false;
                    ETAPU11Gauge07.Error = false;
                    ETAPU11Gauge08.Error = false;
                    ETAPU11Gauge09.Error = false;
                    ETAPU11Gauge10.Error = false;
                    ETAPU11Gauge11.Error = false;
                    ETAPU11Gauge12.Error = false;
                    ETAPU11Gauge13.Error = false;
                    ETAPU11Gauge14.Error = false;
                    ETAPU11Gauge15.Error = false;
                    Log.Error($"UpdateETAPU11() not OK: {result.Status.Explanation}.");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"UpdateETAPU11() exception: {ex.Message}.");
            }
        }

        private void UpdateKWLEC200()
        {
            Log.Debug("UpdateKWLEC200() started.");
            try
            {
                var json = _clientKWLEC200.GetStringAsync("api/kwlec200/all").Result;
                var result = JsonConvert.DeserializeObject<KWLEC200Data>(json);

                if (result.Status.IsGood)
                {
                    KWLEC200Gauge01.Error = false;
                    KWLEC200Gauge02.Error = false;
                    KWLEC200Gauge03.Error = false;
                    KWLEC200Gauge04.Error = false;
                    KWLEC200Gauge05.Error = false;
                    KWLEC200Gauge06.Error = false;
                    KWLEC200Gauge01.Value = result.TemperatureOutdoor;
                    KWLEC200Gauge02.Value = result.TemperatureExhaust;
                    KWLEC200Gauge03.Value = result.TemperatureExtract;
                    KWLEC200Gauge04.Value = result.TemperatureSupply;
                    KWLEC200Gauge05.Value = result.VentilationPercentage;
                    KWLEC200Gauge06.Value = (int)result.VentilationLevel;
                    Log.Debug("UpdateKWLEC200() done.");
                }
                else
                {
                    KWLEC200Gauge01.Error = true;
                    KWLEC200Gauge02.Error = true;
                    KWLEC200Gauge03.Error = true;
                    KWLEC200Gauge04.Error = true;
                    KWLEC200Gauge05.Error = true;
                    KWLEC200Gauge06.Error = true;
                    Log.Error($"UpdateKWLEC200() not OK: {result.Status.Explanation}.");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"UpdateKWLEC200() exception: {ex.Message}.");
            }
        }

        private void UpdateFronius()
        {
            Log.Debug("UpdateFronius() started.");
            try
            {
                var json = _clientFronius.GetStringAsync("api/fronius/common").Result;
                var result = JsonConvert.DeserializeObject<CommonData>(json);

                if (result.Status.IsGood)
                {
                    FroniusGauge01.Error = false;
                    FroniusGauge02.Error = false;
                    FroniusGauge03.Error = false;
                    FroniusGauge04.Error = false;
                    FroniusGauge05.Error = false;
                    FroniusGauge06.Error = false;
                    FroniusGauge01.Value = double.IsNaN(result.PowerAC) ? 0 : result.PowerAC / 1000.0;
                    FroniusGauge02.Value = double.IsNaN(result.DailyEnergy) ? 0 : result.DailyEnergy / 1000.0;
                    FroniusGauge03.Value = double.IsNaN(result.YearlyEnergy) ? 0 : result.YearlyEnergy / 1000.0;
                    FroniusGauge04.Value = double.IsNaN(result.CurrentDC) ? 0 : result.CurrentDC;
                    FroniusGauge05.Value = double.IsNaN(result.VoltageDC) ? 0 : result.VoltageDC;
                    FroniusGauge06.Value = double.IsNaN(result.Frequency) ? 0 : result.Frequency;
                    Log.Debug("UpdateFronius() done.");
                }
                else
                {
                    FroniusGauge01.Error = true;
                    FroniusGauge02.Error = true;
                    FroniusGauge03.Error = true;
                    FroniusGauge04.Error = true;
                    FroniusGauge05.Error = true;
                    FroniusGauge06.Error = true;
                    Log.Error($"UpdateFronius() not OK: {result.Status.Explanation}.");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"UpdateFronius() exception: {ex.Message}.");
            }
        }

        private void UpdateEM300LR1()
        {
            Log.Debug("UpdateEM300LR1() started.");
            try
            {
                var json = _clientEM300LR1.GetStringAsync("api/em300lr/total").Result;
                var total = JsonConvert.DeserializeObject<TotalData>(json);

                if (total.Status.IsGood)
                {
                    EM300LRGauge01.Error = false;
                    EM300LRGauge02.Error = false;
                    EM300LRGauge03.Error = false;
                    EM300LRGauge04.Error = false;
                    EM300LRGauge05.Error = false;
                    EM300LRGauge06.Error = false;
                    EM300LRGauge01.Value = total.ApparentPowerPlus / 1000.0;
                    EM300LRGauge02.Value = total.ActivePowerPlus / 1000.0;
                    EM300LRGauge03.Value = total.ReactivePowerPlus / 1000.0;
                    EM300LRGauge04.Value = total.ApparentPowerMinus / 1000.0;
                    EM300LRGauge05.Value = total.ActivePowerMinus / 1000.0;
                    EM300LRGauge06.Value = total.ReactivePowerMinus / 1000.0;
                    Log.Debug("UpdateEM300LR1() total done.");
                }
                else
                {
                    EM300LRGauge01.Error = true;
                    EM300LRGauge02.Error = true;
                    EM300LRGauge03.Error = true;
                    EM300LRGauge04.Error = true;
                    EM300LRGauge05.Error = true;
                    EM300LRGauge06.Error = true;
                    Log.Error($"UpdateEM300LR1() total not OK: {total.Status.Explanation}.");
                }

                json = _clientEM300LR1.GetStringAsync("api/em300lr/phase1").Result;
                var phase1 = JsonConvert.DeserializeObject<Phase1Data>(json);

                if (phase1.Status.IsGood)
                {
                    EM300LRGauge07.Error = false;
                    EM300LRGauge08.Error = false;
                    EM300LRGauge09.Error = false;
                    EM300LRGauge10.Error = false;
                    EM300LRGauge11.Error = false;
                    EM300LRGauge12.Error = false;
                    EM300LRGauge07.Value = phase1.ApparentPowerPlus / 1000.0;
                    EM300LRGauge08.Value = phase1.ActivePowerPlus / 1000.0;
                    EM300LRGauge09.Value = phase1.ReactivePowerPlus / 1000.0;
                    EM300LRGauge10.Value = phase1.ApparentPowerMinus / 1000.0;
                    EM300LRGauge11.Value = phase1.ActivePowerMinus / 1000.0;
                    EM300LRGauge12.Value = phase1.ReactivePowerMinus / 1000.0;
                    Log.Debug("UpdateEM300LR1() phase1 done.");
                }
                else
                {
                    EM300LRGauge07.Error = true;
                    EM300LRGauge08.Error = true;
                    EM300LRGauge09.Error = true;
                    EM300LRGauge10.Error = true;
                    EM300LRGauge11.Error = true;
                    EM300LRGauge12.Error = true;
                    Log.Error($"UpdateEM300LR1() phase1 not OK: {phase1.Status.Explanation}.");
                }

                json = _clientEM300LR1.GetStringAsync("api/em300lr/phase2").Result;
                var phase2 = JsonConvert.DeserializeObject<Phase2Data>(json);

                if (phase2.Status.IsGood)
                {
                    EM300LRGauge13.Error = false;
                    EM300LRGauge14.Error = false;
                    EM300LRGauge15.Error = false;
                    EM300LRGauge16.Error = false;
                    EM300LRGauge17.Error = false;
                    EM300LRGauge18.Error = false;
                    EM300LRGauge13.Value = phase2.ApparentPowerPlus / 1000.0;
                    EM300LRGauge14.Value = phase2.ActivePowerPlus / 1000.0;
                    EM300LRGauge15.Value = phase2.ReactivePowerPlus / 1000.0;
                    EM300LRGauge16.Value = phase2.ApparentPowerMinus / 1000.0;
                    EM300LRGauge17.Value = phase2.ActivePowerMinus / 1000.0;
                    EM300LRGauge18.Value = phase2.ReactivePowerMinus / 1000.0;
                    Log.Debug("UpdateEM300LR1() phase2 done.");
                }
                else
                {
                    EM300LRGauge13.Error = true;
                    EM300LRGauge14.Error = true;
                    EM300LRGauge15.Error = true;
                    EM300LRGauge16.Error = true;
                    EM300LRGauge17.Error = true;
                    EM300LRGauge18.Error = true;
                    Log.Error($"UpdateEM300LR1() phase2 not OK: {phase2.Status.Explanation}.");
                }

                json = _clientEM300LR1.GetStringAsync("api/em300lr/phase3").Result;
                var phase3 = JsonConvert.DeserializeObject<Phase3Data>(json);

                if (phase3.Status.IsGood)
                {
                    EM300LRGauge19.Error = false;
                    EM300LRGauge20.Error = false;
                    EM300LRGauge21.Error = false;
                    EM300LRGauge22.Error = false;
                    EM300LRGauge23.Error = false;
                    EM300LRGauge24.Error = false;
                    EM300LRGauge19.Value = phase3.ApparentPowerPlus / 1000.0;
                    EM300LRGauge20.Value = phase3.ActivePowerPlus / 1000.0;
                    EM300LRGauge21.Value = phase3.ReactivePowerPlus / 1000.0;
                    EM300LRGauge22.Value = phase3.ApparentPowerMinus / 1000.0;
                    EM300LRGauge23.Value = phase3.ActivePowerMinus / 1000.0;
                    EM300LRGauge24.Value = phase3.ReactivePowerMinus / 1000.0;
                    Log.Debug("UpdateEM300LR1() phase3 done.");
                }
                else
                {
                    EM300LRGauge19.Error = true;
                    EM300LRGauge20.Error = true;
                    EM300LRGauge21.Error = true;
                    EM300LRGauge22.Error = true;
                    EM300LRGauge23.Error = true;
                    EM300LRGauge24.Error = true;
                    Log.Error($"UpdateEM300LR1() phase3 not OK: {phase3.Status.Explanation}.");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"UpdateEM300LR1() exception: {ex.Message}.");
            }
        }

        private void UpdateEM300LR2()
        {
            Log.Debug("UpdateEM300LR2() started.");
            try
            {
                var json = _clientEM300LR2.GetStringAsync("api/em300lr/total").Result;
                var total = JsonConvert.DeserializeObject<TotalData>(json);

                if (total.Status.IsGood)
                {
                    EM300LRGauge01.Error = false;
                    EM300LRGauge02.Error = false;
                    EM300LRGauge03.Error = false;
                    EM300LRGauge04.Error = false;
                    EM300LRGauge05.Error = false;
                    EM300LRGauge06.Error = false;
                    EM300LRGauge01.Value = total.ApparentPowerPlus / 1000.0;
                    EM300LRGauge02.Value = total.ActivePowerPlus / 1000.0;
                    EM300LRGauge03.Value = total.ReactivePowerPlus / 1000.0;
                    EM300LRGauge04.Value = total.ApparentPowerMinus / 1000.0;
                    EM300LRGauge05.Value = total.ActivePowerMinus / 1000.0;
                    EM300LRGauge06.Value = total.ReactivePowerMinus / 1000.0;
                    Log.Debug("UpdateEM300LR2() total done.");
                }
                else
                {
                    EM300LRGauge01.Error = true;
                    EM300LRGauge02.Error = true;
                    EM300LRGauge03.Error = true;
                    EM300LRGauge04.Error = true;
                    EM300LRGauge05.Error = true;
                    EM300LRGauge06.Error = true;
                    Log.Error($"UpdateEM300LR2() total not OK: {total.Status.Explanation}.");
                }

                json = _clientEM300LR2.GetStringAsync("api/em300lr/phase1").Result;
                var phase1 = JsonConvert.DeserializeObject<Phase1Data>(json);

                if (phase1.Status.IsGood)
                {
                    EM300LRGauge07.Error = false;
                    EM300LRGauge08.Error = false;
                    EM300LRGauge09.Error = false;
                    EM300LRGauge10.Error = false;
                    EM300LRGauge11.Error = false;
                    EM300LRGauge12.Error = false;
                    EM300LRGauge07.Value = phase1.ApparentPowerPlus / 1000.0;
                    EM300LRGauge08.Value = phase1.ActivePowerPlus / 1000.0;
                    EM300LRGauge09.Value = phase1.ReactivePowerPlus / 1000.0;
                    EM300LRGauge10.Value = phase1.ApparentPowerMinus / 1000.0;
                    EM300LRGauge11.Value = phase1.ActivePowerMinus / 1000.0;
                    EM300LRGauge12.Value = phase1.ReactivePowerMinus / 1000.0;
                    Log.Debug("UpdateEM300LR2() phase1 done.");
                }
                else
                {
                    EM300LRGauge07.Error = true;
                    EM300LRGauge08.Error = true;
                    EM300LRGauge09.Error = true;
                    EM300LRGauge10.Error = true;
                    EM300LRGauge11.Error = true;
                    EM300LRGauge12.Error = true;
                    Log.Error($"UpdateEM300LR2() phase1 not OK: {phase1.Status.Explanation}.");
                }

                json = _clientEM300LR2.GetStringAsync("api/em300lr/phase2").Result;
                var phase2 = JsonConvert.DeserializeObject<Phase2Data>(json);

                if (phase2.Status.IsGood)
                {
                    EM300LRGauge13.Error = false;
                    EM300LRGauge14.Error = false;
                    EM300LRGauge15.Error = false;
                    EM300LRGauge16.Error = false;
                    EM300LRGauge17.Error = false;
                    EM300LRGauge18.Error = false;
                    EM300LRGauge13.Value = phase2.ApparentPowerPlus / 1000.0;
                    EM300LRGauge14.Value = phase2.ActivePowerPlus / 1000.0;
                    EM300LRGauge15.Value = phase2.ReactivePowerPlus / 1000.0;
                    EM300LRGauge16.Value = phase2.ApparentPowerMinus / 1000.0;
                    EM300LRGauge17.Value = phase2.ActivePowerMinus / 1000.0;
                    EM300LRGauge18.Value = phase2.ReactivePowerMinus / 1000.0;
                    Log.Debug("UpdateEM300LR2() phase2 done.");
                }
                else
                {
                    EM300LRGauge13.Error = true;
                    EM300LRGauge14.Error = true;
                    EM300LRGauge15.Error = true;
                    EM300LRGauge16.Error = true;
                    EM300LRGauge17.Error = true;
                    EM300LRGauge18.Error = true;
                    Log.Error($"UpdateEM300LR2() phase2 not OK: {phase2.Status.Explanation}.");
                }

                json = _clientEM300LR2.GetStringAsync("api/em300lr/phase3").Result;
                var phase3 = JsonConvert.DeserializeObject<Phase3Data>(json);

                if (phase3.Status.IsGood)
                {
                    EM300LRGauge19.Error = false;
                    EM300LRGauge20.Error = false;
                    EM300LRGauge21.Error = false;
                    EM300LRGauge22.Error = false;
                    EM300LRGauge23.Error = false;
                    EM300LRGauge24.Error = false;
                    EM300LRGauge19.Value = phase3.ApparentPowerPlus / 1000.0;
                    EM300LRGauge20.Value = phase3.ActivePowerPlus / 1000.0;
                    EM300LRGauge21.Value = phase3.ReactivePowerPlus / 1000.0;
                    EM300LRGauge22.Value = phase3.ApparentPowerMinus / 1000.0;
                    EM300LRGauge23.Value = phase3.ActivePowerMinus / 1000.0;
                    EM300LRGauge24.Value = phase3.ReactivePowerMinus / 1000.0;
                    Log.Debug("UpdateEM300LR2() phase3 done.");
                }
                else
                {
                    EM300LRGauge19.Error = true;
                    EM300LRGauge20.Error = true;
                    EM300LRGauge21.Error = true;
                    EM300LRGauge22.Error = true;
                    EM300LRGauge23.Error = true;
                    EM300LRGauge24.Error = true;
                    Log.Error($"UpdateEM300LR2() phase3 not OK: {phase3.Status.Explanation}.");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"UpdateEM300LR2() exception: {ex.Message}.");
            }
        }

        private void UpdateWallbox()
        {
            Log.Debug("UpdateWallbox() started.");
            try
            {
                var json = _clientWallbox.GetStringAsync("api/wallbox/report3").Result;
                var result = JsonConvert.DeserializeObject<Report3Data>(json);

                if (result.Status.IsGood)
                {
                    WallboxGauge01.Error = false;
                    WallboxGauge02.Error = false;
                    WallboxGauge03.Error = false;
                    WallboxGauge04.Error = false;
                    WallboxGauge05.Error = false;
                    WallboxGauge06.Error = false;
                    WallboxGauge07.Error = false;
                    WallboxGauge08.Error = false;
                    WallboxGauge09.Error = false;
                    WallboxGauge01.Value = result.Power;
                    WallboxGauge02.Value = result.EnergyCharging;
                    WallboxGauge03.Value = result.EnergyTotal;
                    WallboxGauge04.Value = result.CurrentL1;
                    WallboxGauge05.Value = result.CurrentL2;
                    WallboxGauge06.Value = result.CurrentL3;
                    WallboxGauge07.Value = result.VoltageL1N;
                    WallboxGauge08.Value = result.VoltageL2N;
                    WallboxGauge09.Value = result.VoltageL3N;
                    Log.Debug("UpdateWallbox() done.");
                }
                else
                {
                    WallboxGauge01.Error = true;
                    WallboxGauge02.Error = true;
                    WallboxGauge03.Error = true;
                    WallboxGauge04.Error = true;
                    WallboxGauge05.Error = true;
                    WallboxGauge06.Error = true;
                    WallboxGauge07.Error = true;
                    WallboxGauge08.Error = true;
                    WallboxGauge09.Error = true;
                    Log.Error($"UpdateWallbox() not OK: {result.Status.Explanation}.");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"UpdateWallbox() exception: {ex.Message}.");
            }
        }

        private void UpdateZipato()
        {
            Log.Debug("UpdateZipato() started.");
            try
            {
                var json = _clientZipato.GetStringAsync("api/sensors").Result;
                var result = JsonConvert.DeserializeObject<ZipatoSensors>(json);

                if (result.Status.IsGood)
                {
                    ZipatoGauge01.Error = false;
                    ZipatoGauge02.Error = false;
                    ZipatoGauge03.Error = false;
                    ZipatoGauge04.Error = false;
                    ZipatoGauge05.Error = false;
                    ZipatoGauge06.Error = false;
                    ZipatoGauge07.Error = false;
                    ZipatoGauge08.Error = false;
                    ZipatoGauge09.Error = false;
                    ZipatoGauge10.Error = false;
                    ZipatoGauge11.Error = false;
                    ZipatoGauge12.Error = false;
                    ZipatoGauge13.Error = false;
                    ZipatoGauge14.Error = false;
                    ZipatoGauge15.Error = false;
                    ZipatoGauge01.Value = result.ConsumptionMeters[0].CurrentConsumption.Value;  // Plug 1
                    ZipatoGauge02.Value = result.ConsumptionMeters[1].CurrentConsumption.Value;  // Plug 2
                    ZipatoGauge03.Value = result.ConsumptionMeters[2].CurrentConsumption.Value;  // Plug 3
                    ZipatoGauge04.Value = result.ConsumptionMeters[3].CurrentConsumption.Value;  // Plug 4
                    ZipatoGauge05.Value = result.ConsumptionMeters[4].CurrentConsumption.Value;  // Plug 5
                    ZipatoGauge06.Value = result.ConsumptionMeters[5].CurrentConsumption.Value;  // Plug 6
                    ZipatoGauge07.Value = result.ConsumptionMeters[6].CurrentConsumption.Value;  // Plug 7
                    ZipatoGauge08.Value = result.ConsumptionMeters[7].CurrentConsumption.Value;  // Plug 8
                    ZipatoGauge09.Value = result.ConsumptionMeters[8].CurrentConsumption.Value;  // Heavy Duty Switch
                    ZipatoGauge10.Value = result.TemperatureSensors[5].Temperature.Value;        // Thermostat 1
                    ZipatoGauge11.Value = result.TemperatureSensors[6].Temperature.Value;        // Thermostat 2
                    ZipatoGauge12.Value = result.TemperatureSensors[7].Temperature.Value;        // Thermostat 3
                    ZipatoGauge13.Value = result.TemperatureSensors[8].Temperature.Value;        // Thermostat 4
                    ZipatoGauge14.Value = result.TemperatureSensors[2].Temperature.Value;        // Smoke Sensor
                    ZipatoGauge15.Value = result.TemperatureSensors[4].Temperature.Value;        // Flood Sensor
                    Log.Debug("UpdateZipato() done.");
                }
                else
                {
                    ZipatoGauge01.Error = true;
                    ZipatoGauge02.Error = true;
                    ZipatoGauge03.Error = true;
                    ZipatoGauge04.Error = true;
                    ZipatoGauge05.Error = true;
                    ZipatoGauge06.Error = true;
                    ZipatoGauge07.Error = true;
                    ZipatoGauge08.Error = true;
                    ZipatoGauge09.Error = true;
                    ZipatoGauge10.Error = true;
                    ZipatoGauge11.Error = true;
                    ZipatoGauge12.Error = true;
                    ZipatoGauge13.Error = true;
                    ZipatoGauge14.Error = true;
                    ZipatoGauge15.Error = true;
                    Log.Error($"UpdateZipato() not OK: {result.Status.Explanation}.");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"UpdateZipato() exception: {ex.Message}.");
            }
        }

        #endregion
    }
}
