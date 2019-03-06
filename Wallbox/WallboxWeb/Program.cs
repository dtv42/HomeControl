// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace WallboxWeb
{
    #region Using Directives

    using System.Threading.Tasks;

    using BaseClassLib;

    #endregion

    /// <summary>
    /// The main ASP.NET application.
    /// </summary>
    internal class Program : BaseProgramWeb<Program, Startup>
    {
        /// <summary>
        /// The main console application entry point.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        /// <returns>The exit code.</returns>
        private static async Task Main(string[] args)
        {
            var program = new Program();
            await program.RunAsync(args);
        }
    }
}
