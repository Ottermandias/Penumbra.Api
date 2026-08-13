using Penumbra.Api.Enums;

namespace Penumbra.Api.Api;

/// <summary> API methods pertaining to setting presets. </summary>
public interface IPenumbraApiPresets
{
    /// <summary> Get preset data for a mod according to the <paramref name="mode"/>. </summary>
    /// <param name="collectionId"> The collection to use the settings from. </param>
    /// <param name="mod"> The mod to fetch settings from. </param>
    /// <param name="mode"> Specify what type of preset data you want to obtain. </param>
    /// <param name="key"> The key for the settings lock. If the <paramref name="mode"/> does not have <see cref="PresetQueryMode.IgnoreTemporary"/>, settings with a key greater than 0 that is different from this will be ignored. </param>
    /// <returns> ModMissing, CollectionMissing or Success and the preset data on success. </returns>
    public (PenumbraApiEc, SettingPresetData?) GetPreset(Guid collectionId, in ModIdentifier mod, PresetQueryMode mode, int key);

    /// <summary> Get preset data for a mod according to the <paramref name="mode"/>. </summary>
    /// <param name="objectIndex"> The game object index of the object whose collection you want to query. </param>
    /// <param name="mod"> The mod to fetch settings from. </param>
    /// <param name="mode"> Specify what type of preset data you want to obtain. </param>
    /// <param name="key"> The key for the settings lock. If the <paramref name="mode"/> does not have <see cref="PresetQueryMode.IgnoreTemporary"/>, settings with a key greater than 0 that is different from this will be ignored. </param>
    /// <returns> ModMissing, CollectionMissing or Success and the preset data on success. </returns>
    public (PenumbraApiEc, SettingPresetData?) GetPresetPlayer(int objectIndex, in ModIdentifier mod, PresetQueryMode mode, int key);

    /// <summary> Try to apply a setting preset to a mod according to the <paramref name="mode"/>. </summary>
    /// <param name="collectionId"> The collection to apply the settings to. </param>
    /// <param name="mod"> The mod to apply the preset to. </param>
    /// <param name="preset"> The setting preset to apply. </param>
    /// <param name="mode"> The application mode. </param>
    /// <param name="key"> The key for the settings lock. If <paramref name="mode"/> is not <see cref="PresetApplyMode.Permanent"/> and the current key is greater than 0 and different from this, application will fail. </param>
    /// <param name="source"> A string to describe the source of those temporary settings. This is displayed to the user. </param>
    /// <returns> ModMissing, CollectionMissing, TemporarySettingDisallowed, TemporarySettingImpossible or Success. </returns>
    public PenumbraApiEc ApplyPreset(Guid collectionId, in ModIdentifier mod, in SettingPresetData preset, PresetApplyMode mode, int key, string source);

    /// <summary> Try to apply a setting preset to a mod according to the <paramref name="mode"/>. </summary>
    /// <param name="objectIndex"> The game object index of the object whose collection you want to change. </param>
    /// <param name="mod"> The mod to apply the preset to. </param>
    /// <param name="preset"> The setting preset to apply. </param>
    /// <param name="mode"> The application mode. </param>
    /// <param name="key"> The key for the settings lock. If <paramref name="mode"/> is not <see cref="PresetApplyMode.Permanent"/> and the current key is greater than 0 and different from this, application will fail. </param>
    /// <param name="source"> A string to describe the source of those temporary settings. This is displayed to the user. </param>
    /// <returns> ModMissing, InvalidArgument, TemporarySettingDisallowed, TemporarySettingImpossible or Success. </returns>
    public PenumbraApiEc ApplyPresetPlayer(int objectIndex, in ModIdentifier mod, in SettingPresetData preset, PresetApplyMode mode, int key, string source);
}
