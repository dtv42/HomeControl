// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CircularGauge.xaml.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace HomeControlApp.Controls
{
    #region Using Directives

    using Windows.UI;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;

    using Serilog;

    #endregion

    public sealed partial class CircularGauge : UserControl
    {
        public CircularGauge()
        {
            Log.Debug("CircularGauge()");
            this.InitializeComponent();
        }

        public bool Error
        {
            get { return (bool)GetValue(ErrorProperty); }
            set
            {
                SetValue(ErrorProperty, value);
                Foreground = new SolidColorBrush(value ? Colors.Red : Colors.Black);
            }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string Format
        {
            get { return (string)GetValue(FormatProperty); }
            set { SetValue(FormatProperty, value); }
        }

        public double Interval
        {
            get { return (double)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }

        public double StartValue
        {
            get { return (double)GetValue(StartValueProperty); }
            set { SetValue(StartValueProperty, value); }
        }

        public double EndValue
        {
            get { return (double)GetValue(EndValueProperty); }
            set { SetValue(EndValueProperty, value); }
        }

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set
            {
                SetValue(ValueProperty, value);

                if (!double.IsNaN(value))
                {
                    double pointervalue = value;
                    if (value < StartValue) pointervalue = StartValue;
                    if (value > EndValue) pointervalue = EndValue;
                    if (pointervalue == 0.0) pointervalue = 0.0000001;
                    Gauge.Scales[0].Pointers[0].Value = pointervalue;
                }

                string annotation = value.ToString(Format);
                Annotation.Text = annotation;
            }
        }

        public void Initialize()
        {
            Gauge.Scales[0].Interval = Interval;
            Gauge.Scales[0].StartValue = StartValue;
            Gauge.Scales[0].EndValue = EndValue;
        }

        // Using a DependencyProperty as the backing store for Error.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorProperty =
            DependencyProperty.Register("Error", typeof(bool), typeof(WindGauge), null);

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(CircularGauge), null);

        // Using a DependencyProperty as the backing store for Format.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FormatProperty =
            DependencyProperty.Register("Format", typeof(string), typeof(CircularGauge), null);

        // Using a DependencyProperty as the backing store for StartValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartValueProperty =
            DependencyProperty.Register("StartValue", typeof(double), typeof(CircularGauge), null);

        // Using a DependencyProperty as the backing store for EndValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndValueProperty =
            DependencyProperty.Register("EndValue", typeof(double), typeof(CircularGauge), null);

        // Using a DependencyProperty as the backing store for Interval.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register("Interval", typeof(double), typeof(CircularGauge), null);

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(CircularGauge), null);
    }
}
