namespace HomeDataLib
{
    #region Using Directives

    using System.Threading.Tasks;
    using HomeDataLib.Models;

    #endregion

    public interface IHomeDataClient1 : IHttpClientSettings
    {
        string BaseAddress { get; set; }

        Task<string> GetStringAsync(string request);
    }
}
