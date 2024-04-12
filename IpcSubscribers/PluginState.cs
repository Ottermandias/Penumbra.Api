using Dalamud.Plugin;
using Penumbra.Api.Api;
using Penumbra.Api.Helpers;

namespace Penumbra.Api.IpcSubscribers;

/// <summary>Triggered when the Penumbra API is initialized and ready.</summary>
public static class Initialized
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(Initialized)}";

    /// <summary> Create a new event subscriber. </summary>
    public static EventSubscriber Subscriber(DalamudPluginInterface pi, params Action[] actions)
        => new(pi, Label, actions);

    /// <summary> Create a provider. </summary>
    public static EventProvider Provider(DalamudPluginInterface pi)
        => new(pi, Label);
}

/// <summary>Triggered when the Penumbra API is fully disposed and unavailable.</summary>
public static class Disposed
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(Disposed)}";

    /// <summary> Create a new event subscriber. </summary>
    public static EventSubscriber Subscriber(DalamudPluginInterface pi, params Action[] actions)
        => new(pi, Label, actions);

    /// <summary> Create a provider. </summary>
    public static EventProvider Provider(DalamudPluginInterface pi)
        => new(pi, Label);
}

/// <inheritdoc cref="IPenumbraApiBase.ApiVersion"/>
public class ApiVersion(DalamudPluginInterface pi) 
    : FuncSubscriber<(int Breaking, int Features)>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(ApiVersion)}";

    /// <inheritdoc cref="IPenumbraApiBase.ApiVersion"/>
    public new (int Breaking, int Features) Invoke()
        => base.Invoke();

    /// <summary> Create a provider. </summary>
    public static FuncProvider<(int Breaking, int Features)> Provider(DalamudPluginInterface pi, IPenumbraApiBase api)
        => new(pi, Label, () => api.ApiVersion);
}

/// <inheritdoc cref="IPenumbraApiPluginState.GetModDirectory"/>
public class GetModDirectory(DalamudPluginInterface pi)
    : FuncSubscriber<string>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(GetModDirectory)}";

    /// <inheritdoc cref="IPenumbraApiPluginState.GetModDirectory"/>
    public new string Invoke()
        => base.Invoke();

    /// <summary> Create a provider. </summary>
    public static FuncProvider<string> Provider(DalamudPluginInterface pi, IPenumbraApiPluginState api)
        => new(pi, Label, api.GetModDirectory);
}

/// <inheritdoc cref="IPenumbraApiPluginState.GetConfiguration"/>
public class GetConfiguration(DalamudPluginInterface pi)
    : FuncSubscriber<string>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(GetConfiguration)}";

    /// <inheritdoc cref="IPenumbraApiPluginState.GetConfiguration"/>
    public new string Invoke()
        => base.Invoke();

    /// <summary> Create a provider. </summary>
    public static FuncProvider<string> Provider(DalamudPluginInterface pi, IPenumbraApiPluginState api)
        => new(pi, Label, api.GetConfiguration);
}

/// <inheritdoc cref="IPenumbraApiPluginState.ModDirectoryChanged" />
public static class ModDirectoryChanged
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(ModDirectoryChanged)}";

    /// <summary> Create a new event subscriber. </summary>
    public static EventSubscriber<string, bool> Subscriber(DalamudPluginInterface pi, params Action<string, bool>[] actions)
        => new(pi, Label, actions);

    /// <summary> Create a provider. </summary>
    public static EventProvider<string, bool> Provider(DalamudPluginInterface pi, IPenumbraApiPluginState api)
        => new(pi, Label, (t => api.ModDirectoryChanged += t, t => api.ModDirectoryChanged -= t));
}

/// <inheritdoc cref="IPenumbraApiPluginState.GetEnabledState"/>
public class GetEnabledState(DalamudPluginInterface pi)
    : FuncSubscriber<bool>(pi, Label)
{
    public const string Label = $"Penumbra.{nameof(GetEnabledState)}";

    /// <inheritdoc cref="IPenumbraApiPluginState.GetEnabledState"/>
    public new bool Invoke()
        => base.Invoke();

    /// <summary> Create a provider. </summary>
    public static FuncProvider<bool> Provider(DalamudPluginInterface pi, IPenumbraApiPluginState api)
        => new(pi, Label, api.GetEnabledState);
}

/// <inheritdoc cref="IPenumbraApiPluginState.EnabledChange" />
public static class EnabledChange
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(EnabledChange)}";

    /// <summary> Create a new event subscriber. </summary>
    public static EventSubscriber<bool> Subscriber(DalamudPluginInterface pi, params Action<bool>[] actions)
        => new(pi, Label, actions);

    /// <summary> Create a provider. </summary>
    public static EventProvider<bool> Provider(DalamudPluginInterface pi, IPenumbraApiPluginState api)
        => new(pi, Label, (t => api.EnabledChange += t, t => api.EnabledChange -= t));
}
