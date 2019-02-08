// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindGauge.xaml.cs" company="DTV-Online">
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

    public sealed partial class WindGauge : UserControl
    {
        public WindGauge()
        {
            Log.Debug("WindGauge()");
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

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set
            {
                SetValue(ValueProperty, value);

                if (!double.IsNaN(value))
                {
                    double firstpointervalue = 6 + value / 45;
                    if (firstpointervalue < 0) firstpointervalue += 8;
                    if (firstpointervalue > 8) firstpointervalue -= 8;
                    if (firstpointervalue == 0.0) firstpointervalue = 0.0000001;
                    Gauge.Scales[0].Pointers[0].Value = firstpointervalue;
                    double secondpointervalue = firstpointervalue + 4;
                    if (secondpointervalue > 8) secondpointervalue -= 8;
                    if (secondpointervalue == 0.0) secondpointervalue = 0.0000001;
                    Gauge.Scales[0].Pointers[1].Value = secondpointervalue;
                }

                Annotation.Text = value.ToString("0 °");
            }
        }

        // Using a DependencyProperty as the backing store for Error.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorProperty =
            DependencyProperty.Register("Error", typeof(bool), typeof(WindGauge), null);

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(WindGauge), null);

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(WindGauge), null);
    }
}
