namespace HomeDataTest
{
    #region Using Directives

    using System.Globalization;

    using Microsoft.Extensions.Logging;

    using Xunit;
    using Xunit.Abstractions;
    using Xunit.Logger;

    using HomeDataLib;
    using HomeDataLib.Models;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("HomeData Test Collection")]
    public class TestData : IClassFixture<HomeDataFixture>
    {
        #region Private Data Members

        private readonly ILogger<HomeData> _logger;
        private readonly IHomeData _home;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestData"/> class.
        /// </summary>
        /// <param name="outputHelper"></param>
        public TestData(HomeDataFixture fixture, ITestOutputHelper outputHelper)
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(outputHelper));
            _logger = loggerFactory.CreateLogger<HomeData>();

            _home = fixture.HomeData;
        }

        #endregion

        #region Test Methods

        [Theory]
        [InlineData("Data")]
        [InlineData("Data.Load")]
        [InlineData("Data.Demand")]
        [InlineData("Data.Surplus")]
        [InlineData("Data.Generation")]
        [InlineData("Data.LoadL1")]
        [InlineData("Data.DemandL1")]
        [InlineData("Data.SurplusL1")]
        [InlineData("Data.GenerationL1")]
        [InlineData("Data.LoadL2")]
        [InlineData("Data.DemandL2")]
        [InlineData("Data.SurplusL2")]
        [InlineData("Data.GenerationL2")]
        [InlineData("Data.LoadL3")]
        [InlineData("Data.DemandL3")]
        [InlineData("Data.SurplusL3")]
        [InlineData("Data.GenerationL3")]
        [InlineData("Load")]
        [InlineData("Demand")]
        [InlineData("Surplus")]
        [InlineData("Generation")]
        [InlineData("LoadL1")]
        [InlineData("DemandL1")]
        [InlineData("SurplusL1")]
        [InlineData("GenerationL1")]
        [InlineData("LoadL2")]
        [InlineData("DemandL2")]
        [InlineData("SurplusL2")]
        [InlineData("GenerationL2")]
        [InlineData("LoadL3")]
        [InlineData("DemandL3")]
        [InlineData("SurplusL3")]
        [InlineData("GenerationL3")]
        public void TestProperty(string property)
        {
            Assert.True(HomeData.IsProperty(property));
            Assert.NotNull(_home.GetPropertyValue(property));
        }

        #endregion
    }
}
