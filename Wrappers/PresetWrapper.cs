using Dalamud.Plugin.Ipc;
using Luna;
using Penumbra.Api.Enums;
using Penumbra.Api.Preset;

namespace Penumbra.Api.Wrappers;

/// <summary> A wrapper for a single preset. </summary>
/// <remarks> These should generally only be used for the frame they're created in. </remarks>
public sealed class PresetWrapper : BasicWrapper<PresetWrapper, PresetWrapper.Method>, IBasicWrapper<PresetWrapper>
{
    /// <summary> The mod this preset belongs to, or an empty identifier if it is global. </summary>
    public ModIdentifier AssociatedMod
        => Invoke<ModIdentifier>(Method.AssociatedMod);

    /// <summary> The unique identifier of this preset. </summary>
    public Guid Identifier
        => Invoke<Guid>(Method.Identifier);

    /// <summary> The name of this preset. </summary>
    public string Name
    {
        get => Invoke<string>(Method.Name)!;
        set => Invoke(Method.Name, value);
    }

    /// <summary> The last time this preset was edited. </summary>
    public DateTimeOffset LastEdit
        => Invoke<DateTimeOffset>(Method.LastEdit);

    /// <summary> The last time this preset was applied. </summary>
    public DateTimeOffset LastApplication
        => Invoke<DateTimeOffset>(Method.LastApplication);

    /// <summary> The immutable option and group data this preset applies to the mod. </summary>
    public ReadOnlyWrapper<ModObjectIdentifier, ModObjectIdentifier, byte, bool> Data
        => new(Invoke<SettingsDictionary>(Method.Data)!);

    /// <summary> The priority, if any, this preset applies to the mod. </summary>
    public int? Priority
    {
        get => Invoke<int?>(Method.Priority);
        set => Invoke(Method.Priority, value);
    }

    /// <summary> The state, if any, this preset applies to the mod. </summary>
    public ModState State
    {
        get => (ModState)Invoke<byte>(Method.State);
        set => Invoke(Method.State, (byte)value);
    }

    /// <summary> Delete this preset. </summary>
    public void Delete()
        => Invoke(Method.Delete);

    /// <summary> Update everything this preset applies to the new values. </summary>
    /// <param name="newData"> The new values. </param>
    public void Update(SettingPresetData newData)
        => Invoke<SettingPresetData, bool>(Method.UpdateFull, newData);

    /// <summary> Update only the option and group data this preset applies to the new values. </summary>
    /// <param name="newData"> The new values. </param>
    public void Update(SettingsDictionary newData)
        => Invoke<SettingsDictionary, bool>(Method.UpdateOptions, newData);

    /// <summary> Apply this preset to the given mod in the given collection. </summary>
    /// <param name="collection"> The collection to edit. </param>
    /// <param name="mod"> The mod to apply the preset to. </param>
    /// <param name="mode"> The application mode. </param>
    /// <param name="key"> The key for the settings lock. If <paramref name="mode"/> is not <see cref="PresetApplyMode.Permanent"/> and the current key is greater than 0 and different from this, application will fail. </param>
    /// <param name="source"> A string to describe the source of those temporary settings. This is displayed to the user. </param>
    /// <returns> ModMissing, CollectionMissing, TemporarySettingDisallowed, TemporarySettingImpossible or Success. </returns>
    public PenumbraApiEc Apply(Guid collection, in ModIdentifier mod, PresetApplyMode mode, int key, string source)
        => (PenumbraApiEc)Invoke<Guid, ModIdentifier, int, int, string, int>(Method.Apply, collection, mod, (int)mode, key, source);

    /// <summary> Try to apply a setting preset to a mod according to the <paramref name="mode"/>. </summary>
    /// <param name="objectIndex"> The game object index of the object whose collection you want to change. </param>
    /// <param name="mod"> The mod to apply the preset to. </param>
    /// <param name="mode"> The application mode. </param>
    /// <param name="key"> The key for the settings lock. If <paramref name="mode"/> is not <see cref="PresetApplyMode.Permanent"/> and the current key is greater than 0 and different from this, application will fail. </param>
    /// <param name="source"> A string to describe the source of those temporary settings. This is displayed to the user. </param>
    /// <returns> ModMissing, InvalidArgument, TemporarySettingDisallowed, TemporarySettingImpossible or Success. </returns>
    public PenumbraApiEc ApplyToPlayer(int objectIndex, in ModIdentifier mod, PresetApplyMode mode, int key, string source)
        => (PenumbraApiEc)Invoke<int, ModIdentifier, int, int, string, int>(Method.ApplyToPlayer, objectIndex, mod, (int)mode, key, source);

    /// <summary> The available properties for the mod adapter and wrapper. </summary>
    public enum Method
    {
        /// <inheritdoc cref="PresetWrapper.AssociatedMod"/>
        AssociatedMod,

        /// <inheritdoc cref="PresetWrapper.Identifier"/>
        Identifier,

        /// <inheritdoc cref="PresetWrapper.Name"/>
        Name,

        /// <inheritdoc cref="PresetWrapper.LastEdit"/>
        LastEdit,

        /// <inheritdoc cref="PresetWrapper.LastApplication"/>
        LastApplication,

        /// <inheritdoc cref="PresetWrapper.Data"/>
        Data,

        /// <inheritdoc cref="PresetWrapper.Priority"/>
        Priority,

        /// <inheritdoc cref="PresetWrapper.State"/>
        State,

        /// <inheritdoc cref="PresetWrapper.Delete"/>
        Delete,

        /// <inheritdoc cref="PresetWrapper.Update(SettingPresetData)"/>
        UpdateFull,

        /// <inheritdoc cref="PresetWrapper.Update(SettingsDictionary)"/>
        UpdateOptions,

        /// <inheritdoc cref="PresetWrapper.Apply"/>
        Apply,

        /// <inheritdoc cref="PresetWrapper.ApplyToPlayer"/>
        ApplyToPlayer,
    }

    private PresetWrapper(IIdDataShareAdapter adapter)
        : base(adapter)
    { }

    /// <inheritdoc/>
    static PresetWrapper? IBasicWrapper<PresetWrapper>.CreateWrapper(IIdDataShareAdapter? adapter)
        => adapter is null ? null : new PresetWrapper(adapter);

    /// <inheritdoc />
    protected override string IpcLabel
        => string.Empty;
}
