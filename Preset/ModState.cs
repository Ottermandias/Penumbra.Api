namespace Penumbra.Api.Preset;

/// <summary> States to set a mod's settings to. </summary>
public enum ModState : byte
{
    /// <summary> Do not change the mod's setting state in this collection. </summary>
    Ignored,

    /// <summary> Remove the mod's settings from this collection. </summary>
    Inherited,

    /// <summary> Disable the mod in this collection. </summary>
    Disabled,

    /// <summary> Enable the mod in this collection. </summary>
    Enabled,

    /// <summary> Invert the state of the mod in this collection. </summary>
    Toggle,
}

/// <summary> States to set an option group's settings to. </summary>
public enum OptionState : byte
{
    /// <summary> Do not change this option's state. </summary>
    Ignored = ModState.Ignored,

    /// <summary> Disable this option. </summary>
    Disabled = ModState.Disabled,

    /// <summary> Enable this option. </summary>
    Enabled = ModState.Enabled,

    /// <summary> Invert the current state of this option. </summary>
    Toggle = ModState.Toggle,
}

/// <summary> Extensions for the state enums. </summary>
public static class StateExtensions
{
    /// <summary> Extensions for the option state enum. </summary>
    extension(OptionState state)
    {
        /// <summary> Turn the option state into true for enabled, false for disabled and null for ignored or toggled. </summary>
        public bool? AsBool
            => state switch
            {
                OptionState.Enabled  => true,
                OptionState.Disabled => false,
                _                    => null,
            };

        /// <summary> Turn an optional bool into an OptionState. </summary>
        public static OptionState FromBool(bool? value)
            => value switch
            {
                null  => OptionState.Ignored,
                true  => OptionState.Enabled,
                false => OptionState.Disabled,
            };
    }

    /// <summary> Extensions for the mod state enum. </summary>
    extension(ModState state)
    {
        /// <summary> Turn the mod state into true for enabled, false for disabled and null for anything else. </summary>
        public bool? AsBool
            => state switch
            {
                ModState.Enabled  => true,
                ModState.Disabled => false,
                _                 => null,
            };
    }
}
