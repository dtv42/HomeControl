using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.Licensing;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HomeMonitor
{
    /// <summary>
    /// 
    /// </summary>
    public partial class App : Application
    {
        public static bool SuspendUpdates { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public App()
        {
            SyncfusionLicenseProvider.RegisterLicense("NTQzMTdAMzEzNjJlMzQyZTMwSGFCRkQxV3RXTjRmYlF5MzVVSFplNWRaQjI0bXZ1R2VZZFM5bWZvT0w2WT0=;NTQzMThAMzEzNjJlMzQyZTMwRUlyZ3I5ajV2YkNJdWZPRWgvdmxQVi9maUp3S0hDbXRFSWVoZzl1ME9ROD0=;NTQzMTlAMzEzNjJlMzQyZTMwY3RyMHdwcWF3eDljejM3dCthMHRJNWdqQXBBWVVvTXZSKzA1ODA2Q1N2UT0=;NTQzMjBAMzEzNjJlMzQyZTMwWDJNOUxSM0Y5M3RyMWFCNjJTTTdjT2tka0s1cXRpcGpwRkhxNGFaY1M2ST0=;NTQzMjFAMzEzNjJlMzQyZTMwaVlaeCt6YThTenhGazZFTWlwSWNDQVR6a3l4aG5tK092ZVVmcXJBVXZ2OD0=;NTQzMjJAMzEzNjJlMzQyZTMwTXVuK2dwQWNFTkk3R1ZyTUUzNUdkNFQwRG96KzZWdTlnS2hKQW0yY3dQVT0=;NTQzMjNAMzEzNjJlMzQyZTMwTGhQQzB0V2hrM3lvRUh1dTM0ZU96S1BLV09DcXVlSUNESlRPRHZtbnFhND0=;NTQzMjRAMzEzNjJlMzQyZTMwazE0dFMxL21kMGtoamw4SlphK0lod0owNm9YQmllcHBQU0N6UEEvcVdDWT0=;NTQzMjVAMzEzNjJlMzQyZTMwaERXMXBxR3hVQlNvS1N2TGplb25HbkxRUlVGVDcvRzk2azVzLzA4cEw5MD0=");

            InitializeComponent();

            MainPage = new MainPage();
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnStart()
        {
            // Handle when your app starts
            SuspendUpdates = false;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
            SuspendUpdates = true;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnResume()
        {
            // Handle when your app resumes
            SuspendUpdates = false;
            ((MainPage)MainPage).Update();
        }
    }
}
