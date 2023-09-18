using Dalamud.Plugin;
using Penumbra.Api.Enums;
using Penumbra.Api.Helpers;

namespace Penumbra.Api;

public static partial class Ipc
{
    /// <inheritdoc cref="IPenumbraApi.CreateTemporaryCollection"/>
    public static class CreateTemporaryCollection
    {
        public const string Label = $"Penumbra.{nameof(CreateTemporaryCollection)}";

        public static FuncProvider<string, string, bool, (PenumbraApiEc, string)> Provider(DalamudPluginInterface pi,
            Func<string, string, bool, (PenumbraApiEc, string)> func)
            => new(pi, Label, func);

        [Obsolete]
        public static FuncSubscriber<string, string, bool, (PenumbraApiEc, string)> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.RemoveTemporaryCollection"/>
    public static class RemoveTemporaryCollection
    {
        public const string Label = $"Penumbra.{nameof(RemoveTemporaryCollection)}";

        public static FuncProvider<string, PenumbraApiEc> Provider(DalamudPluginInterface pi,
            Func<string, PenumbraApiEc> func)
            => new(pi, Label, func);

        [Obsolete]
        public static FuncSubscriber<string, PenumbraApiEc> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.CreateNamedTemporaryCollection"/>
    public static class CreateNamedTemporaryCollection
    {
        public const string Label = $"Penumbra.{nameof(CreateNamedTemporaryCollection)}";

        public static FuncProvider<string, PenumbraApiEc> Provider(DalamudPluginInterface pi,
            Func<string, PenumbraApiEc> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, PenumbraApiEc> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.RemoveTemporaryCollectionByName"/>
    public static class RemoveTemporaryCollectionByName
    {
        public const string Label = $"Penumbra.{nameof(RemoveTemporaryCollectionByName)}";

        public static FuncProvider<string, PenumbraApiEc> Provider(DalamudPluginInterface pi,
            Func<string, PenumbraApiEc> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, PenumbraApiEc> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.AssignTemporaryCollection"/>
    public static class AssignTemporaryCollection
    {
        public const string Label = $"Penumbra.{nameof(AssignTemporaryCollection)}";

        public static FuncProvider<string, int, bool, PenumbraApiEc> Provider(DalamudPluginInterface pi,
            Func<string, int, bool, PenumbraApiEc> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, int, bool, PenumbraApiEc> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.AddTemporaryModAll"/>
    public static class AddTemporaryModAll
    {
        public const string Label = $"Penumbra.{nameof(AddTemporaryModAll)}";

        public static FuncProvider<string, Dictionary<string, string>, string, int, PenumbraApiEc> Provider(
            DalamudPluginInterface pi, Func<string, Dictionary<string, string>, string, int, PenumbraApiEc> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, Dictionary<string, string>, string, int, PenumbraApiEc> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.AddTemporaryMod"/>
    public static class AddTemporaryMod
    {
        public const string Label = $"Penumbra.{nameof(AddTemporaryMod)}";

        public static FuncProvider<string, string, Dictionary<string, string>, string, int, PenumbraApiEc> Provider(
            DalamudPluginInterface pi, Func<string, string, Dictionary<string, string>, string, int, PenumbraApiEc> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, string, Dictionary<string, string>, string, int, PenumbraApiEc> Subscriber(
            DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.RemoveTemporaryModAll"/>
    public static class RemoveTemporaryModAll
    {
        public const string Label = $"Penumbra.{nameof(RemoveTemporaryModAll)}";

        public static FuncProvider<string, int, PenumbraApiEc> Provider(
            DalamudPluginInterface pi, Func<string, int, PenumbraApiEc> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, int, PenumbraApiEc> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.RemoveTemporaryMod"/>
    public static class RemoveTemporaryMod
    {
        public const string Label = $"Penumbra.{nameof(RemoveTemporaryMod)}";

        public static FuncProvider<string, string, int, PenumbraApiEc> Provider(
            DalamudPluginInterface pi, Func<string, string, int, PenumbraApiEc> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, string, int, PenumbraApiEc> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }
}
