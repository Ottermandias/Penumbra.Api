using Dalamud.Plugin;
using Penumbra.Api.Api;
using Penumbra.Api.Helpers;

namespace Penumbra.Api.IpcSubscribers;

/// <inheritdoc cref="IPenumbraApiMeta.GetPlayerMetaManipulations"/>
public sealed class GetPlayerMetaManipulations(IDalamudPluginInterface pi)
    : FuncSubscriber<string>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(GetPlayerMetaManipulations)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.GetPlayerMetaManipulations"u8;

    /// <inheritdoc cref="IPenumbraApiMeta.GetPlayerMetaManipulations"/>
    public new string Invoke()
        => base.Invoke();

    /// <summary> Create a provider. </summary>
    public static FuncProvider<string> Provider(IDalamudPluginInterface pi, IPenumbraApiMeta api)
        => new(pi, Label, api.GetPlayerMetaManipulations);
}

/// <inheritdoc cref="IPenumbraApiMeta.GetMetaManipulations"/>
public sealed class GetMetaManipulations(IDalamudPluginInterface pi)
    : FuncSubscriber<int, string>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(GetMetaManipulations)}.V5";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.GetMetaManipulations.V5"u8;

    /// <inheritdoc cref="IPenumbraApiMeta.GetMetaManipulations"/>
    public new string Invoke(int gameObjectIdx)
        => base.Invoke(gameObjectIdx);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<int, string> Provider(IDalamudPluginInterface pi, IPenumbraApiMeta api)
        => new(pi, Label, api.GetMetaManipulations);
}
