using System.Collections.Frozen;
using Dalamud.Plugin;
using Penumbra.Api.Api;
using Penumbra.Api.Helpers;

namespace Penumbra.Api.IpcSubscribers;

/// <summary>Triggered when Penumbra starts launching before any actual initialization of the plugin. </summary>
public static class Launching
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(Launching)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.Launching"u8;

    /// <summary> Create a new event subscriber. The passed values are the major and minor API version being setup. </summary>
    public static EventSubscriber<int, int> Subscriber(IDalamudPluginInterface pi, params Action<int, int>[] actions)
        => new(pi, Label, actions);

    /// <summary> Create a provider. </summary>
    public static EventProvider<int, int> Provider(IDalamudPluginInterface pi)
        => new(pi, Label);
}

/// <summary>Triggered when the Penumbra API is initialized and ready.</summary>
public static class Initialized
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(Initialized)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.Initialized"u8;

    /// <summary> Create a new event subscriber. </summary>
    public static EventSubscriber Subscriber(IDalamudPluginInterface pi, params Action[] actions)
        => new(pi, Label, actions);

    /// <summary> Create a provider. </summary>
    public static EventProvider Provider(IDalamudPluginInterface pi)
        => new(pi, Label);
}

/// <summary>Triggered when the Penumbra API is fully disposed and unavailable.</summary>
public static class Disposed
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(Disposed)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.Disposed"u8;

    /// <summary> Create a new event subscriber. </summary>
    public static EventSubscriber Subscriber(IDalamudPluginInterface pi, params Action[] actions)
        => new(pi, Label, actions);

    /// <summary> Create a provider. </summary>
    public static EventProvider Provider(IDalamudPluginInterface pi)
        => new(pi, Label);
}

/// <inheritdoc cref="IPenumbraApiBase.ApiVersion"/>
public class ApiVersion(IDalamudPluginInterface pi)
    : FuncSubscriber<(int Breaking, int Features)>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(ApiVersion)}.V5";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.ApiVersion.V5"u8;

    /// <inheritdoc cref="IPenumbraApiBase.ApiVersion"/>
    public new (int Breaking, int Features) Invoke()
        => base.Invoke();

    /// <summary> Create a provider. </summary>
    public static FuncProvider<(int Breaking, int Features)> Provider(IDalamudPluginInterface pi, IPenumbraApiBase api)
        => new(pi, Label, () => api.ApiVersion);
}

/// <inheritdoc cref="IPenumbraApiPluginState.GetModDirectory"/>
public class GetModDirectory(IDalamudPluginInterface pi)
    : FuncSubscriber<string>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(GetModDirectory)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.GetModDirectory"u8;

    /// <inheritdoc cref="IPenumbraApiPluginState.GetModDirectory"/>
    public new string Invoke()
        => base.Invoke();

    /// <summary> Create a provider. </summary>
    public static FuncProvider<string> Provider(IDalamudPluginInterface pi, IPenumbraApiPluginState api)
        => new(pi, Label, api.GetModDirectory);
}

/// <inheritdoc cref="IPenumbraApiPluginState.GetConfiguration"/>
public class GetConfiguration(IDalamudPluginInterface pi)
    : FuncSubscriber<string>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(GetConfiguration)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.GetConfiguration"u8;

    /// <inheritdoc cref="IPenumbraApiPluginState.GetConfiguration"/>
    public new string Invoke()
        => base.Invoke();

    /// <summary> Create a provider. </summary>
    public static FuncProvider<string> Provider(IDalamudPluginInterface pi, IPenumbraApiPluginState api)
        => new(pi, Label, api.GetConfiguration);
}

/// <inheritdoc cref="IPenumbraApiPluginState.ModDirectoryChanged" />
public static class ModDirectoryChanged
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(ModDirectoryChanged)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.ModDirectoryChanged"u8;

    /// <summary> Create a new event subscriber. </summary>
    public static EventSubscriber<string, bool> Subscriber(IDalamudPluginInterface pi, params Action<string, bool>[] actions)
        => new(pi, Label, actions);

    /// <summary> Create a provider. </summary>
    public static EventProvider<string, bool> Provider(IDalamudPluginInterface pi, IPenumbraApiPluginState api)
        => new(pi, Label, (t => api.ModDirectoryChanged += t, t => api.ModDirectoryChanged -= t));
}

/// <inheritdoc cref="IPenumbraApiPluginState.GetEnabledState"/>
public class GetEnabledState(IDalamudPluginInterface pi)
    : FuncSubscriber<bool>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(GetEnabledState)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.GetEnabledState"u8;

    /// <inheritdoc cref="IPenumbraApiPluginState.GetEnabledState"/>
    public new bool Invoke()
        => base.Invoke();

    /// <summary> Create a provider. </summary>
    public static FuncProvider<bool> Provider(IDalamudPluginInterface pi, IPenumbraApiPluginState api)
        => new(pi, Label, api.GetEnabledState);
}

/// <inheritdoc cref="IPenumbraApiPluginState.EnabledChange" />
public static class EnabledChange
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(EnabledChange)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.EnabledChange"u8;

    /// <summary> Create a new event subscriber. </summary>
    public static EventSubscriber<bool> Subscriber(IDalamudPluginInterface pi, params Action<bool>[] actions)
        => new(pi, Label, actions);

    /// <summary> Create a provider. </summary>
    public static EventProvider<bool> Provider(IDalamudPluginInterface pi, IPenumbraApiPluginState api)
        => new(pi, Label, (t => api.EnabledChange += t, t => api.EnabledChange -= t));
}

/// <inheritdoc cref="IPenumbraApiPluginState.SupportedFeatures"/>
public class SupportedFeatures(IDalamudPluginInterface pi)
    : FuncSubscriber<FrozenSet<string>>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(SupportedFeatures)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.SupportedFeatures"u8;

    /// <inheritdoc cref="IPenumbraApiPluginState.SupportedFeatures"/>
    public new FrozenSet<string> Invoke()
        => base.Invoke();

    /// <summary> Create a provider. </summary>
    public static FuncProvider<FrozenSet<string>> Provider(IDalamudPluginInterface pi, IPenumbraApiPluginState api)
        => new(pi, Label, () => api.SupportedFeatures);
}

/// <inheritdoc cref="IPenumbraApiPluginState.CheckSupportedFeatures"/>
public class CheckSupportedFeatures(IDalamudPluginInterface pi)
    : FuncSubscriber<IEnumerable<string>, string[]>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(CheckSupportedFeatures)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.CheckSupportedFeatures"u8;

    /// <inheritdoc cref="IPenumbraApiPluginState.CheckSupportedFeatures"/>
    public new string[] Invoke(params IEnumerable<string> requiredFeatures)
        => base.Invoke(requiredFeatures);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<IEnumerable<string>, string[]> Provider(IDalamudPluginInterface pi, IPenumbraApiPluginState api)
        => new(pi, Label, api.CheckSupportedFeatures);
}
