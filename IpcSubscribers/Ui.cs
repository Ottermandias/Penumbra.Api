using Dalamud.Plugin;
using Penumbra.Api.Api;
using Penumbra.Api.Enums;
using Penumbra.Api.Helpers;

namespace Penumbra.Api.IpcSubscribers;

/// <inheritdoc cref="IPenumbraApiUi.ChangedItemTooltip" />
public static class ChangedItemTooltip
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(ChangedItemTooltip)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.ChangedItemTooltip"u8;

    /// <summary> Create a new event subscriber. </summary>
    public static EventSubscriber<ChangedItemType, uint> Subscriber(IDalamudPluginInterface pi,
        params Action<ChangedItemType, uint>[] actions)
        => new(pi, Label, actions);

    /// <summary> Create a provider. </summary>
    public static EventProvider<ChangedItemType, uint> Provider(IDalamudPluginInterface pi, IPenumbraApiUi api)
        => new(pi, Label, (d => api.ChangedItemTooltip += d, d => api.ChangedItemTooltip -= d));
}

/// <inheritdoc cref="IPenumbraApiUi.ChangedItemClicked" />
public static class ChangedItemClicked
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(ChangedItemClicked)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.ChangedItemClicked"u8;

    /// <summary> Create a new event subscriber. </summary>
    public static EventSubscriber<MouseButton, ChangedItemType, uint> Subscriber(IDalamudPluginInterface pi,
        params Action<MouseButton, ChangedItemType, uint>[] actions)
        => new(pi, Label, actions);

    /// <summary> Create a provider. </summary>
    public static EventProvider<MouseButton, ChangedItemType, uint> Provider(IDalamudPluginInterface pi, IPenumbraApiUi api)
        => new(pi, Label, (d => api.ChangedItemClicked += d, d => api.ChangedItemClicked -= d));
}

/// <inheritdoc cref="IPenumbraApiUi.PreSettingsTabBarDraw" />
public static class PreSettingsTabBarDraw
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(PreSettingsTabBarDraw)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.PreSettingsTabBarDraw"u8;

    /// <summary> Create a new event subscriber. </summary>
    public static EventSubscriber<string, float, float> Subscriber(IDalamudPluginInterface pi, params Action<string, float, float>[] actions)
        => new(pi, Label, actions);

    /// <summary> Create a provider. </summary>
    public static EventProvider<string, float, float> Provider(IDalamudPluginInterface pi, IPenumbraApiUi api)
        => new(pi, Label, (d => api.PreSettingsTabBarDraw += d, d => api.PreSettingsTabBarDraw -= d));
}

/// <inheritdoc cref="IPenumbraApiUi.PreSettingsPanelDraw" />
public static class PreSettingsDraw
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(PreSettingsDraw)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.PreSettingsDraw"u8;

    /// <summary> Create a new event subscriber. </summary>
    public static EventSubscriber<string> Subscriber(IDalamudPluginInterface pi, params Action<string>[] actions)
        => new(pi, Label, actions);

    /// <summary> Create a provider. </summary>
    public static EventProvider<string> Provider(IDalamudPluginInterface pi, IPenumbraApiUi api)
        => new(pi, Label, (d => api.PreSettingsPanelDraw += d, d => api.PreSettingsPanelDraw -= d));
}

/// <inheritdoc cref="IPenumbraApiUi.PostEnabledDraw" />
public static class PostEnabledDraw
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(PostEnabledDraw)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.PostEnabledDraw"u8;

    /// <summary> Create a new event subscriber. </summary>
    public static EventSubscriber<string> Subscriber(IDalamudPluginInterface pi, params Action<string>[] actions)
        => new(pi, Label, actions);

    /// <summary> Create a provider. </summary>
    public static EventProvider<string> Provider(IDalamudPluginInterface pi, IPenumbraApiUi api)
        => new(pi, Label, (d => api.PostEnabledDraw += d, d => api.PostEnabledDraw -= d));
}

/// <inheritdoc cref="IPenumbraApiUi.PostSettingsPanelDraw" />
public static class PostSettingsDraw
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(PostSettingsDraw)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.PostSettingsDraw"u8;

    /// <summary> Create a new event subscriber. </summary>
    public static EventSubscriber<string> Subscriber(IDalamudPluginInterface pi, params Action<string>[] actions)
        => new(pi, Label, actions);

    /// <summary> Create a provider. </summary>
    public static EventProvider<string> Provider(IDalamudPluginInterface pi, IPenumbraApiUi api)
        => new(pi, Label, (d => api.PostSettingsPanelDraw += d, d => api.PostSettingsPanelDraw -= d));
}

/// <inheritdoc cref="IPenumbraApiUi.OpenMainWindow"/>
public sealed class OpenMainWindow(IDalamudPluginInterface pi)
    : FuncSubscriber<int, string, string, int>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(OpenMainWindow)}.V5";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.OpenMainWindow.V5"u8;

    /// <inheritdoc cref="IPenumbraApiUi.OpenMainWindow"/>
    public PenumbraApiEc Invoke(TabType tab, string modDirectory = "", string modName = "")
        => (PenumbraApiEc)Invoke((int)tab, modDirectory, modName);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<int, string, string, int> Provider(IDalamudPluginInterface pi, IPenumbraApiUi api)
        => new(pi, Label, (a, b, c) => (int)api.OpenMainWindow((TabType)a, b, c));
}

/// <inheritdoc cref="IPenumbraApiUi.CloseMainWindow"/>
public sealed class CloseMainWindow(IDalamudPluginInterface pi)
    : ActionSubscriber(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(CloseMainWindow)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.CloseMainWindow"u8;

    /// <inheritdoc cref="IPenumbraApiUi.CloseMainWindow"/>
    public new void Invoke()
        => base.Invoke();

    /// <summary> Create a provider. </summary>
    public static ActionProvider Provider(IDalamudPluginInterface pi, IPenumbraApiUi api)
        => new(pi, Label, api.CloseMainWindow);
}

/// <inheritdoc cref="IPenumbraApiUi.RegisterSettingsSection"/>
public sealed class RegisterSettingsSection(IDalamudPluginInterface pi)
    : FuncSubscriber<Action, int>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(RegisterSettingsSection)}";

    /// <inheritdoc cref="IPenumbraApiUi.RegisterSettingsSection"/>
    public new PenumbraApiEc Invoke(Action draw)
        => (PenumbraApiEc)base.Invoke(draw);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<Action, int> Provider(IDalamudPluginInterface pi, IPenumbraApiUi api)
        => new(pi, Label, draw => (int)api.RegisterSettingsSection(draw));
}

/// <inheritdoc cref="IPenumbraApiUi.UnregisterSettingsSection"/>
public sealed class UnregisterSettingsSection(IDalamudPluginInterface pi)
    : FuncSubscriber<Action, int>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(UnregisterSettingsSection)}";

    /// <inheritdoc cref="IPenumbraApiUi.UnregisterSettingsSection"/>
    public new PenumbraApiEc Invoke(Action draw)
        => (PenumbraApiEc)base.Invoke(draw);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<Action, int> Provider(IDalamudPluginInterface pi, IPenumbraApiUi api)
        => new(pi, Label, draw => (int)api.UnregisterSettingsSection(draw));
}
