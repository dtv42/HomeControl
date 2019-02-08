namespace WallboxLib.Models
{
    #region Using Directives

    using System;

    #endregion

    public enum ComModulePresent
    {
        NotPresent = 0,
        Present = 1
    }

    public enum BackendPresent
    {
        NotPresent = 0,
        Present = 1
    }

    [Flags]
    public enum DipSwitches
    {
        DWS1_1 = 0b1000000000000000,
        DWS1_2 = 0b0100000000000000,
        DWS1_3 = 0b0010000000000000,
        DWS1_4 = 0b0001000000000000,
        DWS1_5 = 0b0000100000000000,
        DWS1_6 = 0b0000010000000000,
        DWS1_7 = 0b0000001000000000,
        DWS1_8 = 0b0000000100000000,

        DWS2_1 = 0b0000000010000000,
        DWS2_2 = 0b0000000001000000,
        DWS2_3 = 0b0000000000100000,
        DWS2_4 = 0b0000000000010000,
        DWS2_5 = 0b0000000000001000,
        DWS2_6 = 0b0000000000000100,
        DWS2_7 = 0b0000000000000010,
        DWS2_8 = 0b0000000000000001,
    }

    public enum ChargingStates
    {
        Startup = 0,
        NotReady = 1,
        Ready = 2,
        Charging = 3,
        Error = 4,
        Interrupted = 5
    }

    public enum PlugStates
    {
        Unplugged = 0,
        PluggedStation = 1,
        LockedStation = 3,
        PluggedVehicle = 5,
        LockedVehicle = 7
    }

    public enum AuthorizationFunction
    {
        Deactivated = 0,
        Activated = 1
    }

    public enum AuthorizationRequired
    {
        NotRequired = 0,
        Required = 1
    }

    public enum ChargingEnabled
    {
        CannotEnable = 0,
        CanEnable = 1
    }

    public enum UserEnabled
    {
        Disabled = 0,
        Enabled = 1
    }

    public enum InputStates
    {
        Off = 0,
        On = 1
    }

    public enum Reasons
    {
        NotEnded = 0,
        Terminated = 1,
        Deauthorized = 10
    }
}
