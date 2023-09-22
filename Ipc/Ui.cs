using Dalamud.Plugin;
using Penumbra.Api.Enums;
using Penumbra.Api.Helpers;

namespace Penumbra.Api;

public static partial class Ipc
{
    /// <inheritdoc cref="IPenumbraApi.PreSettingsPanelDraw"/>
    public static class PreSettingsDraw
    {
        public const string Label = $"Penumbra.{nameof(PreSettingsDraw)}";

        public static EventProvider<string> Provider(DalamudPluginInterface pi, Action<Action<string>> sub,
            Action<Action<string>> unsub)
            => new(pi, Label, (sub, unsub));

        public static EventSubscriber<string> Subscriber(DalamudPluginInterface pi, params Action<string>[] actions)
            => new(pi, Label, actions);
    }

    /// <inheritdoc cref="IPenumbraApi.PostSettingsPanelDraw"/>
    public static class PostSettingsDraw
    {
        public const string Label = $"Penumbra.{nameof(PostSettingsDraw)}";

        public static EventProvider<string> Provider(DalamudPluginInterface pi, Action<Action<string>> sub,
            Action<Action<string>> unsub)
            => new(pi, Label, (sub, unsub));

        public static EventSubscriber<string> Subscriber(DalamudPluginInterface pi, params Action<string>[] actions)
            => new(pi, Label, actions);
    }

    /// <inheritdoc cref="IPenumbraApi.ChangedItemTooltip"/>
    public static class ChangedItemTooltip
    {
        public const string Label = $"Penumbra.{nameof(ChangedItemTooltip)}";

        public static EventProvider<ChangedItemType, uint> Provider(DalamudPluginInterface pi, Action add, Action del)
            => new(pi, Label, add, del);

        public static EventSubscriber<ChangedItemType, uint> Subscriber(DalamudPluginInterface pi,
            params Action<ChangedItemType, uint>[] actions)
            => new(pi, Label, actions);
    }

    /// <inheritdoc cref="IPenumbraApi.ChangedItemClicked"/>
    public static class ChangedItemClick
    {
        public const string Label = $"Penumbra.{nameof(ChangedItemClick)}";

        public static EventProvider<MouseButton, ChangedItemType, uint> Provider(DalamudPluginInterface pi, Action add, Action del)
            => new(pi, Label, add, del);

        public static EventSubscriber<MouseButton, ChangedItemType, uint> Subscriber(DalamudPluginInterface pi,
            params Action<MouseButton, ChangedItemType, uint>[] actions)
            => new(pi, Label, actions);
    }

    /// <inheritdoc cref="IPenumbraApi.OpenMainWindow"/>
    public static class OpenMainWindow
    {
        public const string Label = $"Penumbra.{nameof(OpenMainWindow)}";

        public static FuncProvider<TabType, string, string, PenumbraApiEc> Provider(DalamudPluginInterface pi,
            Func<TabType, string, string, PenumbraApiEc> func)
            => new(pi, Label, func);

        public static FuncSubscriber<TabType, string, string, PenumbraApiEc> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.CloseMainWindow"/>
    public static class CloseMainWindow
    {
        public const string Label = $"Penumbra.{nameof(CloseMainWindow)}";

        public static ActionProvider Provider(DalamudPluginInterface pi, Action action)
            => new(pi, Label, action);

        public static ActionSubscriber Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }
}
