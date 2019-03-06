// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CircularGauge.xaml.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace HomeControl2App.Controls
{
    #region Using Directives

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class CircularGauge : UserControl
    {
        #region Dependency Properties

        /// <summary>
        /// Identifies the Title dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(CircularGauge), null);

        /// <summary>
        /// Identifies the Unit dependency property.
        /// </summary>
        public static readonly DependencyProperty ValueFormatProperty =
            DependencyProperty.Register(nameof(ValueFormat), typeof(string), typeof(CircularGauge), null);

        /// <summary>
        /// Identifies the Format dependency property.
        /// </summary>
        public static readonly DependencyProperty LabelFormatProperty =
            DependencyProperty.Register(nameof(LabelFormat), typeof(string), typeof(CircularGauge), null);

        /// <summary>
        /// Identifies the Interval dependency property.
        /// </summary>
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register(nameof(Interval), typeof(double), typeof(CircularGauge), null);

        /// <summary>
        /// Identifies the Minimum dependency property.
        /// </summary>
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(nameof(Minimum), typeof(double), typeof(CircularGauge), null);

        /// <summary>
        /// Identifies the Maximum dependency property.
        /// </summary>
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(nameof(Maximum), typeof(double), typeof(CircularGauge), null);

        /// <summary>
        /// Identifies the Value dependency property.
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(double), typeof(CircularGauge), null);

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularGauge"/> class.
        /// Create a default radial gauge with a title control.
        /// </summary>
        public CircularGauge()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set
            {
                if (value != Title)
                {
                    SetValue(TitleProperty, value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ValueFormat
        {
            get => (string)GetValue(ValueFormatProperty);
            set
            {
                if (value != ValueFormat)
                {
                    SetValue(ValueFormatProperty, value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string LabelFormat
        {
            get => (string)GetValue(LabelFormatProperty);
            set
            {
                if (value != LabelFormat)
                {
                    SetValue(LabelFormatProperty, value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Interval
        {
            get => (double)GetValue(IntervalProperty);
            set
            {
                if (value != Interval)
                {
                    SetValue(IntervalProperty, value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Minimum
        {
            get => (double)GetValue(MinimumProperty);
            set
            {
                if (value != Minimum)
                {
                    SetValue(MinimumProperty, value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set
            {
                if (value != Maximum)
                {
                    SetValue(MaximumProperty, value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set
            {
                if (value != Value)
                {
                    SetValue(ValueProperty, value);
                }
            }
        }

        #endregion
    }
}
