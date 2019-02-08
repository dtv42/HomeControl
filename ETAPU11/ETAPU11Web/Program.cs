namespace ETAPU11Web
{
    #region Using Directives

    using BaseClassLib;
    using System.Threading.Tasks;

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
