// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewModel.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeMonitor.Models
{
    #region Using Directives

    using System.ComponentModel;
    using Xamarin.Forms;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    internal class ViewModel : INotifyPropertyChanged
    {
        private double _gaugestart1 = 0;
        private double _gaugestart2 = 0;
        private double _gaugestart3 = 0;
        private double _gaugeend1 = 100;
        private double _gaugeend2 = 100;
        private double _gaugeend3 = 100;
        private double _gaugevalue1 = 0;
        private double _gaugevalue2 = 0;
        private double _gaugevalue3 = 0;
        private Color _textcolor = Color.Black;
        private string _message = "Message";
        private string _caption = "Caption";
        private string _rowtitle = "Title";
        private string _gaugetitle1 = "Title1";
        private string _gaugetitle2 = "Title2";
        private string _gaugetitle3 = "Title3";
        private string _gaugeheader1 = "0001";
        private string _gaugeheader2 = "0002";
        private string _gaugeheader3 = "0003";

        public Color TextColor
        {
            get => _textcolor;
            set { if (_textcolor != value) { _textcolor = value; OnPropertyChanged("TextColor"); } }
        }

        public string Message
        {
            get => _message;
            set { if (_message != value) { _message = value; OnPropertyChanged("Message"); } }
        }

        public string Caption
        {
            get => _caption;
            set { if (_caption != value) { _caption = value; OnPropertyChanged("Caption"); } }
        }

        public string Title
        {
            get => _rowtitle;
            set { if (_rowtitle != value) { _rowtitle = value; OnPropertyChanged("Title"); } }
        }

        public double GaugeStart1
        {
            get => _gaugestart1;
            set { if (_gaugestart1 != value) { _gaugestart1 = value; OnPropertyChanged("GaugeStart1"); } }
        }

        public double GaugeStart2
        {
            get => _gaugestart2;
            set { if (_gaugestart2 != value) { _gaugestart2 = value; OnPropertyChanged("GaugeStart2"); } }
        }

        public double GaugeStart3
        {
            get => _gaugestart3;
            set { if (_gaugestart3 != value) { _gaugestart3 = value; OnPropertyChanged("GaugeStart3"); } }
        }

        public double GaugeEnd1
        {
            get => _gaugeend1;
            set { if (_gaugeend1 != value) { _gaugeend1 = value; OnPropertyChanged("GaugeEnd1"); } }
        }

        public double GaugeEnd2
        {
            get => _gaugeend2;
            set { if (_gaugeend2 != value) { _gaugeend2 = value; OnPropertyChanged("GaugeEnd2"); } }
        }

        public double GaugeEnd3
        {
            get => _gaugeend3;
            set { if (_gaugeend3 != value) { _gaugeend3 = value; OnPropertyChanged("GaugeEnd3"); } }
        }

        public double GaugeValue1
        {
            get => _gaugevalue1;
            set { if (_gaugevalue1 != value) { _gaugevalue1 = value; OnPropertyChanged("GaugeValue1"); } }
        }

        public double GaugeValue2
        {
            get => _gaugevalue2;
            set { if (_gaugevalue2 != value) { _gaugevalue2 = value; OnPropertyChanged("GaugeValue2"); } }
        }

        public double GaugeValue3
        {
            get => _gaugevalue3;
            set { if (_gaugevalue3 != value) { _gaugevalue3 = value; OnPropertyChanged("GaugeValue3"); } }
        }

        public string GaugeTitle1
        {
            get => _gaugetitle1;
            set { if (_gaugetitle1 != value) { _gaugetitle1 = value; OnPropertyChanged("GaugeTitle1"); } }
        }

        public string GaugeTitle2
        {
            get => _gaugetitle2;
            set { if (_gaugetitle2 != value) { _gaugetitle2 = value; OnPropertyChanged("GaugeTitle2"); } }
        }

        public string GaugeTitle3
        {
            get => _gaugetitle3;
            set { if (_gaugetitle3 != value) { _gaugetitle3 = value; OnPropertyChanged("GaugeTitle3"); } }
        }

        public string GaugeHeader1
        {
            get => _gaugeheader1;
            set { if (_gaugeheader1 != value) { _gaugeheader1 = value; OnPropertyChanged("GaugeHeader1"); } }
        }

        public string GaugeHeader2
        {
            get => _gaugeheader2;
            set { if (_gaugeheader2 != value) { _gaugeheader2 = value; OnPropertyChanged("GaugeHeader2"); } }
        }

        public string GaugeHeader3
        {
            get => _gaugeheader3;
            set { if (_gaugeheader3 != value) { _gaugeheader3 = value; OnPropertyChanged("GaugeHeader3"); } }
        }

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
