using Dalamud.Plugin.Ipc;
using Luna;
using Penumbra.Api.Enums;
using Penumbra.Api.Preset;

namespace Penumbra.Api.Wrappers;

public sealed class CollectionWrapper : BasicWrapper<CollectionWrapper, CollectionWrapper.Method>, IBasicWrapper<CollectionWrapper>
{
    public int Index
        => Invoke<int>(Method.GetIndex);

    public Guid Identifier
        => Invoke<Guid>(Method.GetId);

    public string Name
        => Invoke<string>(Method.GetName) ?? string.Empty;

    public string AnonymousName
        => Invoke<string>(Method.GetAnonymousName) ?? string.Empty;

    public bool HasCache
        => Invoke<bool>(Method.HasCache);

    public Dictionary<string, object?> GetChangedItems()
        => Invoke<Dictionary<string, object?>>(Method.GetChangedItems) ?? [];

    public bool CanUnlock(int modIndex, int key)
        => Invoke<int, int, bool>(Method.CanUnlock, modIndex, key);

    public string? GetTemporarySource(int modIndex)
        => Invoke<int, string>(Method.GetTemporaryOwner, modIndex);

    public SettingPresetData? GetPreset(int modIndex, PresetQueryMode mode = PresetQueryMode.Default, int key = 0)
        => Invoke<int, uint, int, SettingPresetData?>(Method.GetPreset, modIndex, (uint)mode, key);

    public void ApplyPreset(int modIndex, in SettingPresetData preset, string source, PresetApplyMode mode = PresetApplyMode.Temporary,
        int key = 0)
        => Invoke<int, SettingPresetData, int, string, int>(Method.ApplyPreset, modIndex, preset, (int)mode, source, key);

    public SettingPresetData? GetActualSettings(int modIndex)
        => Invoke<int, SettingPresetData?>(Method.GetActualSettingsByIndex, modIndex);

    public SettingPresetData? GetActualSettings(ModIdentifier mod)
        => Invoke<ModIdentifier, SettingPresetData?>(Method.GetActualSettingsByName, mod);

    public SettingPresetData? GetTemporarySettings(int modIndex)
        => Invoke<int, SettingPresetData?>(Method.GetTemporarySettingsByIndex, modIndex);

    public SettingPresetData? GetTemporarySettings(ModIdentifier mod)
        => Invoke<ModIdentifier, SettingPresetData?>(Method.GetTemporarySettingsByName, mod);

    public SettingPresetData? GetOwnSettings(int modIndex)
        => Invoke<int, SettingPresetData?>(Method.GetOwnSettingsByIndex, modIndex);

    public SettingPresetData? GetOwnSettings(ModIdentifier mod)
        => Invoke<ModIdentifier, SettingPresetData?>(Method.GetOwnSettingsByName, mod);

    public ModState GetState(int modIndex, out ModState ownSettings, out bool temporarySettings)
    {
        (var actual, var own, temporarySettings) = Invoke<int, (int Actual, int Own, bool Temporary)>(Method.ModState, modIndex);
        ownSettings                              = (ModState)own;
        return (ModState)actual;
    }

    public int GetPriority(int modIndex)
        => Invoke<int, int>(Method.ModPriority, modIndex);

    public IEnumerable<(ModObjectIdentifier Group, IEnumerable<(ModObjectIdentifier Option, bool State)>)> EnumerateGroups(int modIndex)
        => Invoke<int, IEnumerable<(ModObjectIdentifier Group, IEnumerable<(ModObjectIdentifier Option, bool State)>)>>(Method.EnumerateGroups,
                modIndex)
         ?? [];

    public enum Method
    {
        GetIndex,
        GetId,
        GetName,
        GetAnonymousName,
        GetChangedItems,
        HasCache,
        GetActualSettingsByIndex,
        GetActualSettingsByName,
        GetTemporarySettingsByIndex,
        GetTemporarySettingsByName,
        GetOwnSettingsByIndex,
        GetOwnSettingsByName,
        CanUnlock,
        GetTemporaryOwner,
        GetPreset,
        ApplyPreset,
        ModState,
        ModPriority,
        EnumerateGroups,
    }

    public static CollectionWrapper? Create(IIdDataShareAdapter? adapter)
        => adapter is null ? null : new CollectionWrapper(adapter);

    private CollectionWrapper(IIdDataShareAdapter adapter)
        : base(adapter)
    { }
}
