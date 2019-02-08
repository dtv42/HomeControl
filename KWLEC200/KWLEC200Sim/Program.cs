// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace KWLEC200Sim
{
    #region Using Directives

    using System.Threading.Tasks;
    using BaseClassLib;

    #endregion

    /// <summary>
    /// Class providing the main entry point of the application.
    /// </summary>
    internal class Program : BaseProgram<Program, Startup>
    {
        /// <summary>
        /// The main console application entry point.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        /// <returns>The exit code.</returns>
        private static async Task<int> Main(string[] args)
        {
            var program = new Program();
            return await program.RunAsync(args);
        }
    }
}