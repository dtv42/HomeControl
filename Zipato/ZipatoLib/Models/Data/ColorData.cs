// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Data
{
    #region Using Directives

    using System;

    using ZipatoLib.Models.Data.Color;

    #endregion

    public static class ColorData
    {
        #region Public Methods

        /// <summary>
        /// Returns a string representation of the RGB color value.
        /// </summary>
        /// <param name="color">The RGB color value.</param>
        /// <returns>The hex string representation.</returns>
        public static string Rgb2Hex(RGB color)
        {
            return color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }

        /// <summary>
        /// Returns a string representation of the RGBW color value.
        /// </summary>
        /// <param name="color">The RGBW color value.</param>
        /// <returns>The hex string representation.</returns>
        public static string Rgbw2Hex(RGBW color)
        {
            return color.W.ToString("X2") + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }

        /// <summary>
        /// Helper routine to convert HSV to RGB
        /// </summary>
        /// <param name="h">Hue value (0-360).</param>
        /// <param name="S">Saturation (0-1).</param>
        /// <param name="V">Value (0-1).</param>
        /// <returns>R,G,B values (0-255)</returns>
        public static RGB Hsv2Rgb(double h, double S, double V)
        {
            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;

            if (V <= 0)
            {
                R = G = B = 0;
            }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));

                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = V; // Just pretend its black/white
                        break;
                }
            }

            int r = (int)(R * 255.0);
            int g = (int)(G * 255.0);
            int b = (int)(B * 255.0);

            return new RGB()
            {
                R = (r < 0) ? (byte)0 : (r > 255) ? (byte)255 : (byte)r,
                G = (g < 0) ? (byte)0 : (g > 255) ? (byte)255 : (byte)g,
                B = (b < 0) ? (byte)0 : (b > 255) ? (byte)255 : (byte)b,
            };
        }

        #endregion
    }
}
