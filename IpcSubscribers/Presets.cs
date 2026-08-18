using Dalamud.Plugin;
using Luna;
using Penumbra.Api.Api;
using Penumbra.Api.Enums;

namespace Penumbra.Api.IpcSubscribers;

/// <inheritdoc cref="IPenumbraApiPresets.GetPreset"/>
public sealed class GetPreset(IDalamudPluginInterface pi)
    : FuncSubscriber<Guid, ModIdentifier, uint, int, (int, SettingPresetData?)>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(GetPreset)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.GetPreset"u8;

    /// <inheritdoc cref="IPenumbraApiPresets.GetPreset"/>
    public PenumbraApiEc Invoke(Guid collection, ModIdentifier mod, out SettingPresetData? data, PresetQueryMode mode = PresetQueryMode.Default,
        int key = 0)
    {
        (var ec, data) = Invoke(collection, mod, (uint)mode, key);
        return (PenumbraApiEc)ec;
    }

    /// <summary> Create a provider. </summary>
    public static FuncProvider<Guid, ModIdentifier, uint, int, (int, SettingPresetData?)> Provider(IDalamudPluginInterface pi,
        IPenumbraApiPresets api)
        => new(pi, Label, (a, b, c, d) =>
        {
            var p = api.GetPreset(a, b, (PresetQueryMode)c, d);
            return ((int)p.Item1, p.Item2);
        });
}

/// <inheritdoc cref="IPenumbraApiPresets.GetPresetPlayer"/>
public sealed class GetPresetPlayer(IDalamudPluginInterface pi)
    : FuncSubscriber<int, ModIdentifier, uint, int, (int, SettingPresetData?)>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(GetPresetPlayer)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.GetPresetPlayer"u8;

    /// <inheritdoc cref="IPenumbraApiPresets.GetPresetPlayer"/>
    public PenumbraApiEc Invoke(int objectIndex, ModIdentifier mod, out SettingPresetData? data, PresetQueryMode mode = PresetQueryMode.Default,
        int key = 0)
    {
        (var ec, data) = Invoke(objectIndex, mod, (uint)mode, key);
        return (PenumbraApiEc)ec;
    }

    /// <summary> Create a provider. </summary>
    public static FuncProvider<int, ModIdentifier, uint, int, (int, SettingPresetData?)> Provider(IDalamudPluginInterface pi,
        IPenumbraApiPresets api)
        => new(pi, Label, (a, b, c, d) =>
        {
            var p = api.GetPresetPlayer(a, b, (PresetQueryMode)c, d);
            return ((int)p.Item1, p.Item2);
        });
}

/// <inheritdoc cref="IPenumbraApiPresets.ApplyPreset"/>
public sealed class ApplyPreset(IDalamudPluginInterface pi)
    : FuncSubscriber<Guid, ModIdentifier, SettingPresetData, int, int, string, int>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(ApplyPreset)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.ApplyPreset"u8;

    /// <inheritdoc cref="IPenumbraApiPresets.ApplyPreset"/>
    public PenumbraApiEc Invoke(Guid collection, ModIdentifier mod, in SettingPresetData data, string source,
        PresetApplyMode mode = PresetApplyMode.Temporary, int key = 0)
        => (PenumbraApiEc)base.Invoke(collection, mod, data, (int)mode, key, source);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<Guid, ModIdentifier, SettingPresetData, int, int, string, int> Provider(IDalamudPluginInterface pi,
        IPenumbraApiPresets api)
        => new(pi, Label, (a, b, c, d, e, f) => (int)api.ApplyPreset(a, b, c, (PresetApplyMode)d, e, f));
}

/// <inheritdoc cref="IPenumbraApiPresets.ApplyPresetPlayer"/>
public sealed class ApplyPresetPlayer(IDalamudPluginInterface pi)
    : FuncSubscriber<int, ModIdentifier, SettingPresetData, int, int, string, int>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(ApplyPresetPlayer)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.ApplyPresetPlayer"u8;

    /// <inheritdoc cref="IPenumbraApiPresets.ApplyPresetPlayer"/>
    public PenumbraApiEc Invoke(int objectIndex, ModIdentifier mod, in SettingPresetData data, string source,
        PresetApplyMode mode = PresetApplyMode.Temporary, int key = 0)
        => (PenumbraApiEc)base.Invoke(objectIndex, mod, data, (int)mode, key, source);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<int, ModIdentifier, SettingPresetData, int, int, string, int> Provider(IDalamudPluginInterface pi,
        IPenumbraApiPresets api)
        => new(pi, Label, (a, b, c, d, e, f) => (int)api.ApplyPresetPlayer(a, b, c, (PresetApplyMode)d, e, f));
}
