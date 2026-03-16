namespace Penumbra.Api.Enums;

/// <summary>
/// The selection type for mod option groups.
/// </summary>
public enum GroupType
{
    /// <summary>
    /// Exactly one option of this group has to be selected (if any exist).
    /// </summary>
    Single,

    /// <summary>
    /// Any number of options in this group can be toggled on or off at the same time.
    /// Limits the number of options in a single group to 32 at the most.
    /// Each option is its own data container, which are independent of each other.
    /// </summary>
    Multi,

    /// <summary>
    /// Any number of options in this group can be toggled on or off at the same time.
    /// Affects a single IMC entry, to manipulate different parts of a model in a user-facing way.
    /// </summary>
    Imc,

    /// <summary>
    /// Any number of options in this group can be toggled on or off at the same time.
    /// Limits the number of options in a single group to 8 at the most.
    /// Each combination of options is its own data container, resulting in 2^N separate data containers.
    /// </summary>
    Combining,

    /// <summary>
    /// A group consisting of multiple separate subgroups where the options can depend on each other.
    /// Each subgroup behaves the same way as its regular group type, just with optional dependencies on the other options.
    /// The total number of options is still limited by the settings bit size.
    /// </summary>
    Complex,
}
