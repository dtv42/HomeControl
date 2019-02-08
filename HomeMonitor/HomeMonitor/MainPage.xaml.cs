namespace HomeMonitor
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    using Xamarin.Forms;

    using HomeMonitor.Models;
    using System.Threading.Tasks;
    using Acr.UserDialogs;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public partial class MainPage : ContentPage
    {
        #region Private Data Members

        /// <summary>
        /// 
        /// </summary>
        private static Settings _settings = new Settings();

        /// <summary>
        /// 
        /// </summary>
        private readonly object _updateLock = new object();

        /// <summary>
        /// 
        /// </summary>
        private List<ServiceData> _servicelist = new List<ServiceData>
        {
            new HomeData(_settings.HomeData),
            new Netatmo(_settings.Netatmo),
            new ETAPU11(_settings.ETAPU11),
            new EM300LR(_settings.EM300LR),
            new KWLEC200(_settings.KWLEC200),
            new Fronius(_settings.Fronius),
            new Wallbox(_settings.Wallbox),
            new Zipato(_settings.Zipato)
        };

        private RowData _currentRow = null;
        private int _serviceIndex;
        private int _rowIndex;

        #endregion

        #region Private Properties

        /// <summary>
        /// 
        /// </summary>
        private ViewModel ViewModel { get; set; } = new ViewModel();

        /// <summary>
        /// 
        /// </summary>
        private int MaxServiceIndex { get => _servicelist.Count - 1; }

        /// <summary>
        /// 
        /// </summary>
        private int MaxRowIndex { get => _servicelist[_serviceIndex].Rows.Count - 1; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            BindingContext = ViewModel;

            // Update the gauge information based on the first service (_serviceIndex == 0).
            UpdateService();

            // Start a repeating timer for automatic update every minute.
            Device.StartTimer(TimeSpan.FromSeconds(60), () =>
            {
                Update();
                return true;
            });
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                UpdateGauges((IServiceData)_servicelist[_serviceIndex]);
            });
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width > height)
            {
                Gauge1.CircularCoefficient = 0.9F;
                Gauge2.CircularCoefficient = 0.9F;
                Gauge3.CircularCoefficient = 0.9F;
                Gauge1.TranslationY = -height / 20;
                Gauge2.TranslationY = -height / 20;
                Gauge3.TranslationY = -height / 20;
                GaugeTitle1.TranslationY = height / 20;
                GaugeTitle2.TranslationY = height / 20;
                GaugeTitle3.TranslationY = height / 20;
                GaugesLayout.Direction = FlexDirection.Row;
                CombinedCaption.IsVisible = false;
                TopCaption.IsVisible = true;
            }
            else
            {
                Gauge1.CircularCoefficient = 1.0F;
                Gauge2.CircularCoefficient = 1.0F;
                Gauge3.CircularCoefficient = 1.0F;
                Gauge1.TranslationY = 0.0;
                Gauge2.TranslationY = 0.0;
                Gauge3.TranslationY = 0.0;
                GaugeTitle1.TranslationY = 0.0;
                GaugeTitle2.TranslationY = 0.0;
                GaugeTitle3.TranslationY = 0.0;
                GaugesLayout.Direction = FlexDirection.Column;
                CombinedCaption.IsVisible = true;
                TopCaption.IsVisible = false;
            }
        }

        #endregion

        #region Event Handler

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSwipeLeft(object sender, EventArgs e)
        {
            GotoPreviousService();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSwipeRight(object sender, EventArgs e)
        {
            GotoNextService();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSwipeUp(object sender, EventArgs e)
        {
            GotoPreviousRow();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSwipeDown(object sender, EventArgs e)
        {
            GotoNextRow();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnUpdateButtonClick(object sender, EventArgs e)
        {
            UpdateRow();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnSetupButtonClick(object sender, EventArgs e)
        {
            var service = _servicelist[_serviceIndex];
            var config = new PromptConfig()
            {
                Message = "URI",
                Title = service.Name,
                Text = _settings.GetValue(service),
                InputType = InputType.Url
            };
            var result = await UserDialogs.Instance.PromptAsync(config);

            if (result.Ok)
            {
                Type servicetype = service.GetType();
                _settings.SetValue(service, result.Text);
                _servicelist[_serviceIndex] = (ServiceData)Activator.CreateInstance(servicetype, result.Text);
            }

            UpdateRow();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPreviousButtonClick(object sender, EventArgs e)
        {
            GotoPreviousService();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNextButtonClick(object sender, EventArgs e)
        {
            GotoNextService();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnUpButtonClick(object sender, EventArgs e)
        {
            GotoPreviousRow();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDownButtonClick(object sender, EventArgs e)
        {
            GotoNextRow();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        private void GotoPreviousService()
        {
            if (--_serviceIndex < 0) _serviceIndex = MaxServiceIndex;
            UpdateService();
        }

        /// <summary>
        /// 
        /// </summary>
        private void GotoNextService()
        {
            if (++_serviceIndex > MaxServiceIndex) _serviceIndex = 0;
            UpdateService();
        }

        /// <summary>
        /// 
        /// </summary>
        private void GotoPreviousRow()
        {
            if (--_rowIndex < 0) _rowIndex = MaxRowIndex;
            UpdateRow();
        }

        /// <summary>
        /// 
        /// </summary>
        private void GotoNextRow()
        {
            if (++_rowIndex > MaxRowIndex) _rowIndex = 0;
            UpdateRow();
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateService()
        {
            _rowIndex = 0;
            UpdateRow();
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateRow()
        {
            var service = (IServiceData)_servicelist[_serviceIndex];
            _currentRow = service.Rows[_rowIndex];

            ViewModel.Caption = service.Name;
            ViewModel.Title = _currentRow.Caption;

            ViewModel.GaugeTitle1 = _currentRow.Gauge1.Title;
            ViewModel.GaugeStart1 = _currentRow.Gauge1.StartValue;
            ViewModel.GaugeEnd1 = _currentRow.Gauge1.EndValue;

            ViewModel.GaugeTitle2 = _currentRow.Gauge2.Title;
            ViewModel.GaugeStart2 = _currentRow.Gauge2.StartValue;
            ViewModel.GaugeEnd2 = _currentRow.Gauge2.EndValue;

            ViewModel.GaugeTitle3 = _currentRow.Gauge3.Title;
            ViewModel.GaugeStart3 = _currentRow.Gauge3.StartValue;
            ViewModel.GaugeEnd3 = _currentRow.Gauge3.EndValue;

            UpdateGauges(service);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        private void UpdateGauges(IServiceData service)
        {
            if (!App.SuspendUpdates)
            {
                lock (_updateLock)
                {
                    if (service.UpdateValues())
                    {
                        ViewModel.GaugeValue1 = service.Rows[_rowIndex].Gauge1.Value;
                        ViewModel.GaugeValue2 = service.Rows[_rowIndex].Gauge2.Value;
                        ViewModel.GaugeValue3 = service.Rows[_rowIndex].Gauge3.Value;
                        ViewModel.GaugeHeader1 = service.Rows[_rowIndex].Gauge1.Header;
                        ViewModel.GaugeHeader2 = service.Rows[_rowIndex].Gauge2.Header;
                        ViewModel.GaugeHeader3 = service.Rows[_rowIndex].Gauge3.Header;
                        ViewModel.Message = $"Data values last updated at {DateTime.Now.ToString("HH:mm:ss")}";
                        ViewModel.TextColor = Color.Gray;
                    }
                    else
                    {
                        ViewModel.GaugeValue1 = service.Rows[_rowIndex].Gauge1.Value;
                        ViewModel.GaugeValue2 = service.Rows[_rowIndex].Gauge2.Value;
                        ViewModel.GaugeValue3 = service.Rows[_rowIndex].Gauge3.Value;
                        ViewModel.GaugeHeader1 = service.Rows[_rowIndex].Gauge1.Header;
                        ViewModel.GaugeHeader2 = service.Rows[_rowIndex].Gauge2.Header;
                        ViewModel.GaugeHeader3 = service.Rows[_rowIndex].Gauge3.Header;
                        ViewModel.Message = $"{service.Message} at {DateTime.Now.ToString("HH:mm:ss")}";
                        ViewModel.TextColor = Color.Red;
                    }
                }
            }
        }

        #endregion
    }
}
