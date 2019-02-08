namespace FroniusLib
{
    #region Using Directives

    using System.Threading.Tasks;
    using FroniusLib.Models;

    #endregion

    public interface IFroniusClient : IHttpClientSettings
    {
        Task<string> GetStringAsync(string request);
    }
}
