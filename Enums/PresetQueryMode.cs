namespace Penumbra.Api.Enums;

/// <summary> Different options to query for a mod setting preset. </summary>
[Flags]
public enum PresetQueryMode : uint
{
    /// <summary> Get the preset from the current actual settings. </summary>
    Default = 0,

    /// <summary> Ignore temporary settings when getting the preset, only take own or inherited settings into account. </summary>
    IgnoreTemporary = 0x01,

    /// <summary> Ignore inherited settings when getting the preset. </summary>
    IgnoreInheritance = 0x02,

    /// <summary> Only get a preset of all available settings, with everything set to ignore. This does not require a collection. </summary>
    IgnoreSettings = 0x04,

    /// <summary> Get a preset of the default settings for a mod. This does not require a collection. </summary>
    GetDefault = 0x08,

    /// <summary> Omit all options that are disabled in the preset. </summary>
    IgnoreDisabled = 0x10,
}
