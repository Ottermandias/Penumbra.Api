using Dalamud.Plugin;

namespace Penumbra.Api.Helpers;

/// <summary> A record of an exposed plugin. </summary>
/// <param name="Name"> The user-visible name of the plugin. </param>
/// <param name="InternalName"> The unique internal name of the plugin. </param>
/// <param name="Version"> The version of the plugin. </param>
/// <param name="Info"> Additional information about the plugin. </param>
public readonly record struct CallerPlugin(string Name, string InternalName, Version Version, CallerPlugin.Flags Info)
{
    /// <summary> Create a record from the data passed by Dalamud's IPC channels. </summary>
    /// <param name="caller"> The exposed plugin from Dalamud. </param>
    public CallerPlugin(IExposedPlugin caller)
        : this(caller.Name, caller.InternalName, caller.Version, CreateFlags(caller))
    { }

    /// <summary> Additional information flags. </summary>
    [Flags]
    public enum Flags
    {
        /// <summary> The plugin is from a third party repository. </summary>
        ThirdParty = 1 << 0,

        /// <summary> The plugin is installed as a developer plugin. </summary>
        Developer = 1 << 1,

        /// <summary> The plugin is marked as outdated. </summary>
        Outdated = 1 << 2,

        /// <summary> The plugin is installed as a testing version. </summary>
        Testing = 1 << 3,

        /// <summary> The plugin is currently loaded. </summary>
        Loaded = 1 << 4,

        /// <summary> The plugins version was banned. </summary>
        Banned = 1 << 5,

        /// <summary> The plugin is decommissioned. </summary>
        Decommissioned = 1 << 6,

        /// <summary> The plugin is orphaned, i.e. its repository is unavailable. </summary>
        Orphan = 1 << 7,
    }

    private static Flags CreateFlags(IExposedPlugin caller)
    {
        Flags ret = default;
        if (caller.IsDev)
            ret |= Flags.Developer;
        if (caller.IsBanned)
            ret |= Flags.Banned;
        if (caller.IsDecommissioned)
            ret |= Flags.Decommissioned;
        if (caller.IsOutdated)
            ret |= Flags.Outdated;
        if (caller.IsOrphaned)
            ret |= Flags.Orphan;
        if (caller.IsLoaded)
            ret |= Flags.Loaded;
        if (caller.IsTesting)
            ret |= Flags.Testing;
        if (caller.IsThirdParty)
            ret |= Flags.ThirdParty;
        return ret;
    }
}
