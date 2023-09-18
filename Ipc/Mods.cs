using Dalamud.Plugin;
using Penumbra.Api.Enums;
using Penumbra.Api.Helpers;

namespace Penumbra.Api;

public static partial class Ipc
{
    /// <inheritdoc cref="IPenumbraApi.GetModList"/>
    public static class GetMods
    {
        public const string Label = $"Penumbra.{nameof(GetMods)}";

        public static FuncProvider<IList<(string, string)>> Provider(DalamudPluginInterface pi, Func<IList<(string, string)>> func)
            => new(pi, Label, func);

        public static FuncSubscriber<IList<(string, string)>> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.ReloadMod"/>
    public static class ReloadMod
    {
        public const string Label = $"Penumbra.{nameof(ReloadMod)}";

        public static FuncProvider<string, string, PenumbraApiEc> Provider(DalamudPluginInterface pi,
            Func<string, string, PenumbraApiEc> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, string, PenumbraApiEc> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.InstallMod"/>
    public static class InstallMod
    {
        public const string Label = $"Penumbra.{nameof(InstallMod)}";

        public static FuncProvider<string, PenumbraApiEc> Provider(DalamudPluginInterface pi,
            Func<string, PenumbraApiEc> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, PenumbraApiEc> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.AddMod"/>
    public static class AddMod
    {
        public const string Label = $"Penumbra.{nameof(AddMod)}";

        public static FuncProvider<string, PenumbraApiEc> Provider(DalamudPluginInterface pi,
            Func<string, PenumbraApiEc> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, PenumbraApiEc> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.DeleteMod"/>
    public static class DeleteMod
    {
        public const string Label = $"Penumbra.{nameof(DeleteMod)}";

        public static FuncProvider<string, string, PenumbraApiEc> Provider(DalamudPluginInterface pi,
            Func<string, string, PenumbraApiEc> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, string, PenumbraApiEc> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.ModDeleted" />
    public static class ModDeleted
    {
        public const string Label = $"Penumbra.{nameof(ModDeleted)}";

        public static EventProvider<string> Provider(DalamudPluginInterface pi, Action add, Action del)
            => new(pi, Label, add, del);

        public static EventSubscriber<string> Subscriber(DalamudPluginInterface pi, params Action<string>[] actions)
            => new(pi, Label, actions);
    }

    /// <inheritdoc cref="IPenumbraApi.ModAdded" />
    public static class ModAdded
    {
        public const string Label = $"Penumbra.{nameof(ModAdded)}";

        public static EventProvider<string> Provider(DalamudPluginInterface pi, Action add, Action del)
            => new(pi, Label, add, del);

        public static EventSubscriber<string> Subscriber(DalamudPluginInterface pi, params Action<string>[] actions)
            => new(pi, Label, actions);
    }

    /// <inheritdoc cref="IPenumbraApi.ModMoved" />
    public static class ModMoved
    {
        public const string Label = $"Penumbra.{nameof(ModMoved)}";

        public static EventProvider<string, string> Provider(DalamudPluginInterface pi, Action add, Action del)
            => new(pi, Label, add, del);

        public static EventSubscriber<string, string> Subscriber(DalamudPluginInterface pi, params Action<string, string>[] actions)
            => new(pi, Label, actions);
    }

    /// <inheritdoc cref="IPenumbraApi.GetModPath"/>
    public static class GetModPath
    {
        public const string Label = $"Penumbra.{nameof(GetModPath)}";

        public static FuncProvider<string, string, (PenumbraApiEc, string, bool)> Provider(DalamudPluginInterface pi,
            Func<string, string, (PenumbraApiEc, string, bool)> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, string, (PenumbraApiEc, string, bool)> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.SetModPath"/>
    public static class SetModPath
    {
        public const string Label = $"Penumbra.{nameof(SetModPath)}";

        public static FuncProvider<string, string, string, PenumbraApiEc> Provider(DalamudPluginInterface pi,
            Func<string, string, string, PenumbraApiEc> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, string, string, PenumbraApiEc> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }
}
