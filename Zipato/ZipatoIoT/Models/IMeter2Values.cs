namespace ZipatoIoT.Models
{
    using System.Threading.Tasks;

    internal interface IMeter2Values
    {
        int Index1 { get; set; }
        int Index2 { get; set; }
        double?[] Values1 { get; }
        double?[] Values2 { get; }

        Task<bool> UpdateValuesAsync();
        Task<bool> SendValuesAsync();
    }
}
