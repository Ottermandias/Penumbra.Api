using Dalamud.Plugin;
using Penumbra.Api.Enums;
using Penumbra.Api.Helpers;

namespace Penumbra.Api;

using CurrentSettings = ValueTuple<PenumbraApiEc, (bool, int, IDictionary<string, IList<string>>, bool)?>;

public static partial class Ipc
{
    /// <inheritdoc cref="Api.IPenumbraApi.GetAvailableModSettings"/>
    public static class GetAvailableModSettings
    {
        public const string Label = $"Penumbra.{nameof(GetAvailableModSettings)}";

        public static FuncProvider<string, string, IDictionary<string, (IList<string>, GroupType)>?> Provider(
            DalamudPluginInterface pi, Func<string, string, IDictionary<string, (IList<string>, GroupType)>?> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, string, IDictionary<string, (IList<string>, GroupType)>?> Subscriber(
            DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="Api.IPenumbraApi.GetCurrentModSettings"/>
    public static class GetCurrentModSettings
    {
        public const string Label = $"Penumbra.{nameof(GetCurrentModSettings)}";

        public static FuncProvider<string, string, string, bool, CurrentSettings> Provider(DalamudPluginInterface pi,
            Func<string, string, string, bool, CurrentSettings> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, string, string, bool, CurrentSettings> Subscriber(
            DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="Api.IPenumbraApi.TryInheritMod"/>
    public static class TryInheritMod
    {
        public const string Label = $"Penumbra.{nameof(TryInheritMod)}";

        public static FuncProvider<string, string, string, bool, PenumbraApiEc> Provider(DalamudPluginInterface pi,
            Func<string, string, string, bool, PenumbraApiEc> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, string, string, bool, PenumbraApiEc> Subscriber(
            DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="Api.IPenumbraApi.TrySetMod"/>
    public static class TrySetMod
    {
        public const string Label = $"Penumbra.{nameof(TrySetMod)}";

        public static FuncProvider<string, string, string, bool, PenumbraApiEc> Provider(DalamudPluginInterface pi,
            Func<string, string, string, bool, PenumbraApiEc> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, string, string, bool, PenumbraApiEc> Subscriber(
            DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="Api.IPenumbraApi.TrySetModPriority"/>
    public static class TrySetModPriority
    {
        public const string Label = $"Penumbra.{nameof(TrySetModPriority)}";

        public static FuncProvider<string, string, string, int, PenumbraApiEc> Provider(DalamudPluginInterface pi,
            Func<string, string, string, int, PenumbraApiEc> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, string, string, int, PenumbraApiEc> Subscriber(
            DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="Api.IPenumbraApi.TrySetModSetting"/>
    public static class TrySetModSetting
    {
        public const string Label = $"Penumbra.{nameof(TrySetModSetting)}";

        public static FuncProvider<string, string, string, string, string, PenumbraApiEc> Provider(DalamudPluginInterface pi,
            Func<string, string, string, string, string, PenumbraApiEc> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, string, string, string, string, PenumbraApiEc> Subscriber(
            DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="Api.IPenumbraApi.TrySetModSettings"/>
    public static class TrySetModSettings
    {
        public const string Label = $"Penumbra.{nameof(TrySetModSettings)}";

        public static FuncProvider<string, string, string, string, IReadOnlyList<string>, PenumbraApiEc> Provider(
            DalamudPluginInterface pi,
            Func<string, string, string, string, IReadOnlyList<string>, PenumbraApiEc> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, string, string, string, IReadOnlyList<string>, PenumbraApiEc> Subscriber(
            DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="Api.IPenumbraApi.ModSettingChanged"/>
    public static class ModSettingChanged
    {
        public const string Label = $"Penumbra.{nameof(ModSettingChanged)}";

        public static EventProvider<ModSettingChange, Guid, string, bool> Provider(DalamudPluginInterface pi, Action add, Action del)
            => new(pi, Label, add, del);

        public static EventSubscriber<ModSettingChange, Guid, string, bool> Subscriber(DalamudPluginInterface pi,
            params Action<ModSettingChange, Guid, string, bool>[] actions)
            => new(pi, Label, actions);
    }

    /// <inheritdoc cref="Api.IPenumbraApi.CopyModSettings"/>
    public static class CopyModSettings
    {
        public const string Label = $"Penumbra.{nameof(CopyModSettings)}";

        public static FuncProvider<string, string, string, PenumbraApiEc> Provider(DalamudPluginInterface pi,
            Func<string, string, string, PenumbraApiEc> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, string, string, PenumbraApiEc> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }
}
