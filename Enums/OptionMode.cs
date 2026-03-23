namespace Penumbra.Api.Enums;

/// <summary> Modes of applications for single options in multi groups. </summary>
public enum OptionMode : byte
{
    /// <summary> Enable the option regardless of its prior state. </summary>
    Enable,

    /// <summary> Disable the option regardless of its prior state. </summary>
    Disable,

    /// <summary> Switch the prior state of the option. </summary>
    Toggle,

    /// <summary> Keep the state of the option the same. </summary>
    Ignore,
}
