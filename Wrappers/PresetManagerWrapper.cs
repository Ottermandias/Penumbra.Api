using System.Linq;
using Dalamud.Plugin.Ipc;
using Luna;
using Penumbra.Api.IpcSubscribers;

namespace Penumbra.Api.Wrappers;

/// <summary> A wrapper for the preset manager. </summary>
/// <remarks> This is persistent and can generally be kept for the lifetime of either your plugin or Penumbra itself. </remarks>
public sealed class PresetManagerWrapper
    : BasicWrapper<PresetManagerWrapper, PresetManagerWrapper.Method>, IBasicWrapper<PresetManagerWrapper>
{
    /// <summary> Get the unique identifiers of all presets associated with the given mod, or all global presets if <paramref name="mod"/> is empty. </summary>
    public IEnumerable<Guid> GetPresetIds(ModIdentifier mod)
        => Invoke<ModIdentifier, IEnumerable<Guid>>(Method.GetPresetIds, mod)!;

    /// <summary> Get the unique identifiers and their associated mods of all stored presets. </summary>
    public IEnumerable<(ModIdentifier AssociatedMod, Guid PresetId)> GetAllPresetIds()
        => Invoke<IEnumerable<(ModIdentifier AssociatedMod, Guid PresetId)>>(Method.GetAllPresetIds)!;

    /// <summary> Get a reference to a preset by its unique identifier. </summary>
    /// <remarks> This preset reference should not be kept alive long-term. Use with using. </remarks>
    public PresetWrapper? GetPreset(Guid id)
        => BasicWrapper.Create<PresetWrapper>(Invoke<Guid, IIdDataShareAdapter>(Method.GetPreset, id));

    /// <summary> Get a reference to a preset by its name and an optional restriction to a specific associated mod or globals if <paramref name="mod"/> is empty but not null. </summary>
    /// <remarks> This preset reference should not be kept alive long-term. Use with using. </remarks>
    public PresetWrapper? GetPresetByName(string name, ModIdentifier? mod = null)
        => BasicWrapper.Create<PresetWrapper>(Invoke<string, ModIdentifier?, IIdDataShareAdapter>(Method.GetPreset, name, mod));

    /// <summary> Get references to all presets associated with the given mod, or all global presets if <paramref name="mod"/> is empty. </summary>
    /// <remarks> These preset references should not be kept alive long-term. Use with using. </remarks>
    public IEnumerable<PresetWrapper> GetPresets(ModIdentifier mod)
        => Invoke<ModIdentifier, IEnumerable<IIdDataShareAdapter>>(Method.GetPresets, mod)!.Select(BasicWrapper.Create<PresetWrapper>)
            .OfType<PresetWrapper>();

    /// <summary> Get references to all stored presets together with their associated mods. </summary>
    /// <remarks> These preset references should not be kept alive long-term. Use with using. </remarks>
    public IEnumerable<(ModIdentifier AssociatedMod, PresetWrapper Preset)> GetAllPresets()
        => Invoke<IEnumerable<(ModIdentifier AssociatedMod, IIdDataShareAdapter Preset)>>(Method.GetAllPresets)!.Select(p
            => (p.AssociatedMod, BasicWrapper.Create<PresetWrapper>(p.Preset)!));

    /// <summary> Get the total number of stored presets. </summary>
    public int TotalCount
        => Invoke<int>(Method.TotalCount);

    /// <summary> Get the total number of stored presets associated with the given mod, or global if <paramref name="mod"/> is empty. </summary>
    public int Count(ModIdentifier mod)
        => Invoke<ModIdentifier, int>(Method.Count, mod);

    /// <summary> Add a preset with a given name and data. </summary>
    /// <param name="name"> The chosen name for the preset. </param>
    /// <param name="preset"> The data for the preset. </param>
    /// <param name="modIdentifier"> The mod to associate this preset to. If this is empty, it will be global. </param>
    /// <returns> A reference to the newly stored preset. </returns>
    /// <remarks> This preset reference should not be kept alive long-term. Use with using. </remarks>
    public PresetWrapper AddPreset(string name, SettingPresetData preset, ModIdentifier modIdentifier = default)
        => BasicWrapper.Create<PresetWrapper>(
            Invoke<string, SettingPresetData, ModIdentifier, IIdDataShareAdapter>(Method.AddPreset, name, preset, modIdentifier))!;

    /// <summary> Delete a preset by its unique identifier. </summary>
    /// <param name="id"> The identifier of the preset to delete. </param>
    /// <returns> True if the preset existed, false if nothing was done. </returns>
    public bool DeletePreset(Guid id)
        => Invoke<Guid, bool>(Method.DeletePreset, id);

    /// <summary> Get the applied data of a preset by its unique identifier. </summary>
    /// <param name="id"> The identifier of the preset to query. </param>
    /// <returns> The data the preset applies if it exists, null otherwise. </returns>
    public SettingPresetData? GetPresetData(Guid id)
        => Invoke<Guid, SettingPresetData>(Method.GetPresetData, id);

    /// <summary> The methods available for a mod manager adapter. </summary>
    public enum Method
    {
        /// <inheritdoc cref="PresetManagerWrapper.GetPresetIds"/>
        GetPresetIds,

        /// <inheritdoc cref="PresetManagerWrapper.GetAllPresetIds"/>
        GetAllPresetIds,

        /// <inheritdoc cref="PresetManagerWrapper.GetPreset"/>
        GetPreset,

        /// <inheritdoc cref="PresetManagerWrapper.GetPresetByName"/>
        GetPresetByName,

        /// <inheritdoc cref="PresetManagerWrapper.GetPresets"/>
        GetPresets,

        /// <inheritdoc cref="PresetManagerWrapper.GetAllPresets"/>
        GetAllPresets,

        /// <inheritdoc cref="PresetManagerWrapper.Count"/>
        Count,

        /// <inheritdoc cref="PresetManagerWrapper.TotalCount"/>
        TotalCount,

        /// <inheritdoc cref="PresetManagerWrapper.AddPreset"/>
        AddPreset,

        /// <inheritdoc cref="PresetManagerWrapper.DeletePreset"/>
        DeletePreset,

        /// <inheritdoc cref="PresetManagerWrapper.GetPresetData"/>
        GetPresetData,
    }

    /// <summary> Create a new mod manager wrapper without a connection to an adapter. </summary>
    public PresetManagerWrapper()
    { }

    private PresetManagerWrapper(IIdDataShareAdapter adapter)
        : base(adapter)
    { }

    /// <inheritdoc/>
    static PresetManagerWrapper? IBasicWrapper<PresetManagerWrapper>.CreateWrapper(IIdDataShareAdapter? adapter)
        => adapter is null ? null : new PresetManagerWrapper(adapter);

    /// <inheritdoc />
    protected override string IpcLabel
        => GetPresetManagerAdapter.Label;
}
