namespace ZipatoIoT.Models
{
    using System.Threading.Tasks;

    internal interface IMeterValues
    {
        int Index { get; set; }
        double?[] Values { get; }

        Task<bool> UpdateValuesAsync();
        Task<bool> SendValuesAsync();
    }
}
