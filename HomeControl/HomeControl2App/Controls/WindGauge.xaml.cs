// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindGauge.xaml.cs" company="DTV-Online">
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

    public sealed partial class WindGauge : UserControl
    {
        #region Dependency Properties

        /// <summary>
        /// Identifies the Title dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(WindGauge), null);

        /// <summary>
        /// Identifies the Value dependency property.
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(double), typeof(WindGauge), null);

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WindGauge"/> class.
        /// Create a special radial gauge with a title control.
        /// </summary>
        public WindGauge()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Properties

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
