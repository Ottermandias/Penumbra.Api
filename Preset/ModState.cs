using Luna.Generators;

namespace Penumbra.Api.Preset;

/// <summary> States to set a mod's settings to. </summary>
[NamedEnum]
[TooltipEnum]
public enum ModState : byte
{
    /// <summary> Do not change the mod's setting state in this collection. </summary>
    [Name("Keep")]
    [Tooltip("Keep this mod's state as it is. If it is currently inherited, no other options will be applied.")]
    Ignored,

    /// <summary> Remove the mod's settings from this collection. </summary>
    [Name("Inherit")]
    [Tooltip("Set this mod's state to be inherited. If temporary, uses forced inheritance. No other options will be applied.")]
    Inherited,

    /// <summary> Disable the mod in this collection. </summary>
    [Name("Disable")]
    [Tooltip("Disable this mod.")]
    Disabled,

    /// <summary> Enable the mod in this collection. </summary>
    [Name("Enable")]
    [Tooltip("Enable this mod.")]
    Enabled,

    /// <summary> Invert the state of the mod in this collection. </summary>
    [Name("Toggle")]
    [Tooltip("Toggle this mod (if it is currently enabled, disable it and vice versa). Enables the mod if it is currently inherited.")]
    Toggle,

    /// <summary> Remove temporary settings if there are any, and do nothing else. </summary>
    [Name("Remove")]
    [Tooltip("Remove temporary settings for this mod if there are any. Does nothing else.")]
    RemoveTemporary,
}

/// <summary> States to set an option group's settings to. </summary>
[NamedEnum]
[TooltipEnum]
public enum OptionState : byte
{
    /// <summary> Do not change this option's state. </summary>
    [Name("Keep")]
    [Tooltip("Keep this option's state as it is.")]
    Ignored = ModState.Ignored,

    /// <summary> Disable this option. </summary>
    [Name("Disable")]
    [Tooltip(
        "Disable this option. For single select groups, the first non-disabled, non-ignored option will be prioritized if there is no available enabled or toggled option.")]
    Disabled = ModState.Disabled,

    /// <summary> Enable this option. </summary>
    [Name("Enable")]
    [Tooltip("Enable this option. For single select groups, the first enabled option will be prioritized.")]
    Enabled = ModState.Enabled,

    /// <summary> Invert the current state of this option. </summary>
    [Name("Toggle")]
    [Tooltip(
        "Toggle this option (if it is currently enabled, disable it and vice versa). For single select groups, the first currently disabled option with toggle will be prioritized if there is no available enabled option.")]
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
