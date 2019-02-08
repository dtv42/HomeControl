namespace ZipatoIoT.Models
{
    #region Using Directives

    using System.Threading.Tasks;
    using System.Net.Http;

    #endregion

    internal interface IZipatoClient : IHttpClientSettings
    {
        string BaseAddress { get; set; }

        Task<string> GetStringAsync(string request);
        Task<HttpResponseMessage> PutAsync(string request, StringContent content);
    }
}
