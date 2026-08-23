using Dalamud.Plugin.Ipc;
using Luna;
using Penumbra.Api.Enums;
using Penumbra.Api.Preset;

namespace Penumbra.Api.Wrappers;

/// <summary> A wrapper for a single collection. </summary>
/// <remarks> These should generally only be used for the frame they're created in. </remarks>
public sealed class CollectionWrapper : BasicWrapper<CollectionWrapper, CollectionWrapper.Method>, IBasicWrapper<CollectionWrapper>
{
    /// <summary> Get the current internal index of the collection. </summary>
    public int Index
        => Invoke<int>(Method.GetIndex);

    /// <summary> Get the persistent identifier of the collection. </summary>
    public Guid Identifier
        => Invoke<Guid>(Method.GetId);

    /// <summary> Get the display name of the collection. </summary>
    public string Name
        => Invoke<string>(Method.GetName) ?? string.Empty;

    /// <summary> Get an anonymized display name of the collection. </summary>
    public string AnonymousName
        => Invoke<string>(Method.GetAnonymousName) ?? string.Empty;

    /// <summary> Get whether the collection is currently in use and has a cache. </summary>
    public bool HasCache
        => Invoke<bool>(Method.HasCache);

    /// <summary> Get all currently changed items in this collection as a new dictionary. </summary>
    public Dictionary<string, object?> GetChangedItems()
        => Invoke<Dictionary<string, object?>>(Method.GetChangedItems) ?? [];

    /// <summary> Get whether the given key can unlock the temporary settings for the given mod. </summary>
    /// <param name="modIndex"> The index of the mod. </param>
    /// <param name="key"> The provided key. </param>
    /// <returns> True if the mod exists and has no temporary settings, or can be unlocked by the key. </returns>
    public bool CanUnlock(int modIndex, int key)
        => Invoke<int, int, bool>(Method.CanUnlock, modIndex, key);

    /// <summary> Get the source of any temporary settings currently set for the given mod. </summary>
    /// <param name="modIndex"> The index of the mod. </param>
    /// <returns> The provided source if temporary settings exist, null otherwise. </returns>
    public string? GetTemporarySource(int modIndex)
        => Invoke<int, string>(Method.GetTemporaryOwner, modIndex);

    /// <summary> Get a preset of the given mods settings according to the specified mode. </summary>
    /// <param name="modIndex"> The index of the mod. </param>
    /// <param name="mode"> The query mode for the preset. </param>
    /// <param name="key"> An optional provided key. </param>
    /// <returns> A preset data set according to the mode or null if the mod does not exist. </returns>
    public SettingPresetData? GetPreset(int modIndex, PresetQueryMode mode = PresetQueryMode.Default, int key = 0)
        => Invoke<int, uint, int, SettingPresetData?>(Method.GetPreset, modIndex, (uint)mode, key);

    /// <summary> Apply a preset to a given mod according to the specified mode. </summary>
    /// <param name="modIndex"> The index of the mod. </param>
    /// <param name="preset"> The setting preset to apply. </param>
    /// <param name="source"> The applying source if the settings are applied temporarily. </param>
    /// <param name="mode"> The application mode for the preset. </param>
    /// <param name="key"> A key to unlock existing and lock new temporary settings. </param>
    public void ApplyPreset(int modIndex, in SettingPresetData preset, string source, PresetApplyMode mode = PresetApplyMode.Temporary,
        int key = 0)
        => Invoke(Method.ApplyPreset, modIndex, preset, (int)mode, source, key);

    /// <summary> Get the current actual settings of a mod as a setting preset. </summary>
    /// <param name="modIndex"> The index of the mod. </param>
    /// <returns> The preset if the mod exists, null otherwise. </returns>
    public SettingPresetData? GetActualSettings(int modIndex)
        => Invoke<int, SettingPresetData?>(Method.GetActualSettingsByIndex, modIndex);

    /// <summary> Get the current actual settings of a mod as a setting preset. </summary>
    /// <param name="mod"> The identifier for the mod. </param>
    /// <returns> The preset if the mod exists, null otherwise. </returns>
    public SettingPresetData? GetActualSettings(ModIdentifier mod)
        => Invoke<ModIdentifier, SettingPresetData?>(Method.GetActualSettingsByName, mod);

    /// <summary> Get the current temporary settings of a mod as a setting preset. </summary>
    /// <param name="modIndex"> The index of the mod. </param>
    /// <returns> The preset if the mod exists and has temporary settings, null otherwise. </returns>
    public SettingPresetData? GetTemporarySettings(int modIndex)
        => Invoke<int, SettingPresetData?>(Method.GetTemporarySettingsByIndex, modIndex);

    /// <summary> Get the current temporary settings of a mod as a setting preset. </summary>
    /// <param name="mod"> The identifier for the mod. </param>
    /// <returns> The preset if the mod exists and has temporary settings, null otherwise. </returns>
    public SettingPresetData? GetTemporarySettings(ModIdentifier mod)
        => Invoke<ModIdentifier, SettingPresetData?>(Method.GetTemporarySettingsByName, mod);

    /// <summary> Get the current non-inherited, non-temporary settings of a mod as a setting preset. </summary>
    /// <param name="modIndex"> The index of the mod. </param>
    /// <returns> The preset if the mod exists, null otherwise. </returns>
    public SettingPresetData? GetOwnSettings(int modIndex)
        => Invoke<int, SettingPresetData?>(Method.GetOwnSettingsByIndex, modIndex);

    /// <summary> Get the current non-inherited, non-temporary settings of a mod as a setting preset. </summary>
    /// <param name="mod"> The identifier for the mod. </param>
    /// <returns> The preset if the mod exists, null otherwise. </returns>
    public SettingPresetData? GetOwnSettings(ModIdentifier mod)
        => Invoke<ModIdentifier, SettingPresetData?>(Method.GetOwnSettingsByName, mod);

    /// <summary> Get the state of a mod in this collection. </summary>
    /// <param name="modIndex"> The index of the mod. </param>
    /// <param name="ownSettings"> The state in the non-inherited, non-temporary settings in this collection (Enabled, Disabled, Inherited). </param>
    /// <param name="temporarySettings"> Whether the returned state is from temporary settings. </param>
    /// <returns> The actual state of the mod (Enabled, Disabled). </returns>
    public ModState GetState(int modIndex, out ModState ownSettings, out bool temporarySettings)
    {
        (var actual, var own, temporarySettings) = Invoke<int, (int Actual, int Own, bool Temporary)>(Method.ModState, modIndex);
        ownSettings                              = (ModState)own;
        return (ModState)actual;
    }

    /// <summary> Get the current actual priority of a mod in this collection. </summary>
    /// <param name="modIndex"> The index of the mod. </param>
    /// <returns> The priority. </returns>
    public int GetPriority(int modIndex)
        => Invoke<int, int>(Method.ModPriority, modIndex);

    /// <summary> Enumerate all groups and options of a mod with their current actual state in this collection. </summary>
    /// <param name="modIndex"> The index of the mod. </param>
    /// <returns> An enumeration of object identifiers for every group, paired with enumerations of object identifiers and state for every option inside the respective group. </returns>
    public IEnumerable<(ModObjectIdentifier Group, IEnumerable<(ModObjectIdentifier Option, bool State)>)> EnumerateGroups(int modIndex)
        => Invoke<int, IEnumerable<(ModObjectIdentifier Group, IEnumerable<(ModObjectIdentifier Option, bool State)>)>>(Method.EnumerateGroups,
                modIndex)
         ?? [];

    /// <summary> Draw a tooltip about what changes would be applied by the given preset. </summary>
    /// <param name="modIndex"> The mod to target. </param>
    /// <param name="preset"> The preset to apply. </param>
    public void DrawPresetTooltip(int modIndex, in SettingPresetData preset)
        => Invoke(Method.DrawPresetTooltip, modIndex, preset);

    /// <summary> The available properties for collection mod adapter and wrapper. </summary>
    public enum Method
    {
        /// <inheritdoc cref="Version"/>
        Version = BasicWrapper.VersionMethod,

        /// <inheritdoc cref="CollectionWrapper.Index"/>
        GetIndex,

        /// <inheritdoc cref="CollectionWrapper.Identifier"/>
        GetId,

        /// <inheritdoc cref="CollectionWrapper.Name"/>
        GetName,

        /// <inheritdoc cref="CollectionWrapper.AnonymousName"/>
        GetAnonymousName,

        /// <inheritdoc cref="CollectionWrapper.GetChangedItems"/>
        GetChangedItems,

        /// <inheritdoc cref="CollectionWrapper.HasCache"/>
        HasCache,

        /// <inheritdoc cref="CollectionWrapper.GetActualSettings(int)"/>
        GetActualSettingsByIndex,

        /// <inheritdoc cref="CollectionWrapper.GetActualSettings(ModIdentifier)"/>
        GetActualSettingsByName,

        /// <inheritdoc cref="CollectionWrapper.GetTemporarySettings(int)"/>
        GetTemporarySettingsByIndex,

        /// <inheritdoc cref="CollectionWrapper.GetTemporarySettings(ModIdentifier)"/>
        GetTemporarySettingsByName,

        /// <inheritdoc cref="CollectionWrapper.GetOwnSettings(int)"/>
        GetOwnSettingsByIndex,

        /// <inheritdoc cref="CollectionWrapper.GetOwnSettings(ModIdentifier)"/>
        GetOwnSettingsByName,

        /// <inheritdoc cref="CollectionWrapper.CanUnlock"/>
        CanUnlock,

        /// <inheritdoc cref="CollectionWrapper.GetTemporarySource"/>
        GetTemporaryOwner,

        /// <inheritdoc cref="CollectionWrapper.GetPreset"/>
        GetPreset,

        /// <inheritdoc cref="CollectionWrapper.ApplyPreset"/>
        ApplyPreset,

        /// <inheritdoc cref="CollectionWrapper.GetState"/>
        ModState,

        /// <inheritdoc cref="CollectionWrapper.GetPriority"/>
        ModPriority,

        /// <inheritdoc cref="CollectionWrapper.EnumerateGroups"/>
        EnumerateGroups,

        /// <inheritdoc cref="CollectionWrapper.DrawPresetTooltip"/>
        DrawPresetTooltip
    }

    static CollectionWrapper? IBasicWrapper<CollectionWrapper>.CreateWrapper(IIdDataShareAdapter? adapter)
        => adapter is null ? null : new CollectionWrapper(adapter);

    private CollectionWrapper(IIdDataShareAdapter adapter)
        : base(adapter)
    { }

    /// <inheritdoc />
    protected override string IpcLabel
        => string.Empty;
}
