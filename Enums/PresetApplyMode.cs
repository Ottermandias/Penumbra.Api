namespace Penumbra.Api.Enums;

/// <summary> Different options to apply temporary settings. </summary>
public enum PresetApplyMode
{
    /// <summary> Always apply the preset as temporary settings. </summary>
    Temporary = 0,

    /// <summary> Apply the preset as temporary settings when the mod currently has temporary settings or the user uses temporary settings by default, and on the regular settings otherwise. </summary>
    Auto = 1,

    /// <summary> Always apply the preset to the regular settings. </summary>
    Permanent = 2,
}
