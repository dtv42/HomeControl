namespace HomeDataLib.Models
{
    #region Using Directives

    using EM300LRLib.Models;

    #endregion

    public class MeterData
    {
        /// <summary>
        /// The MeterData properties holds all EM300LR data.
        /// </summary>
        public TotalData Total { get; set; } = new TotalData();
        public Phase1Data Phase1 { get; set; } = new Phase1Data();
        public Phase2Data Phase2 { get; set; } = new Phase2Data();
        public Phase3Data Phase3 { get; set; } = new Phase3Data();
    }
}
