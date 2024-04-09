using Dalamud.Plugin;
using Penumbra.Api.Enums;
using Penumbra.Api.Helpers;

namespace Penumbra.Api;

public static partial class Ipc
{
    /// <inheritdoc cref="Api.IPenumbraApi.CreateTemporaryCollection"/>
    public static class CreateTemporaryCollection
    {
        public const string Label = $"Penumbra.{nameof(CreateTemporaryCollection)}";

        public static FuncProvider<string, string, bool, (PenumbraApiEc, Guid)> Provider(DalamudPluginInterface pi,
            Func<string, string, bool, (PenumbraApiEc, Guid)> func)
            => new(pi, Label, func);

        [Obsolete]
        public static FuncSubscriber<string, string, bool, (PenumbraApiEc, Guid)> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="Api.IPenumbraApi.RemoveTemporaryCollection"/>
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

    /// <inheritdoc cref="Api.IPenumbraApi.CreateNamedTemporaryCollection"/>
    public static class CreateNamedTemporaryCollection
    {
        public const string Label = $"Penumbra.{nameof(CreateNamedTemporaryCollection)}";

        public static FuncProvider<string, (PenumbraApiEc, Guid)> Provider(DalamudPluginInterface pi,
            Func<string, (PenumbraApiEc, Guid)> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, (PenumbraApiEc, Guid)> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="Api.IPenumbraApi.RemoveTemporaryCollectionByName"/>
    public static class RemoveTemporaryCollectionByName
    {
        public const string Label = $"Penumbra.{nameof(RemoveTemporaryCollectionByName)}";

        public static FuncProvider<Guid, PenumbraApiEc> Provider(DalamudPluginInterface pi,
            Func<Guid, PenumbraApiEc> func)
            => new(pi, Label, func);

        public static FuncSubscriber<Guid, PenumbraApiEc> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="Api.IPenumbraApi.AssignTemporaryCollection"/>
    public static class AssignTemporaryCollection
    {
        public const string Label = $"Penumbra.{nameof(AssignTemporaryCollection)}";

        public static FuncProvider<Guid, int, bool, PenumbraApiEc> Provider(DalamudPluginInterface pi,
            Func<Guid, int, bool, PenumbraApiEc> func)
            => new(pi, Label, func);

        public static FuncSubscriber<Guid, int, bool, PenumbraApiEc> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="Api.IPenumbraApi.AddTemporaryModAll"/>
    public static class AddTemporaryModAll
    {
        public const string Label = $"Penumbra.{nameof(AddTemporaryModAll)}";

        public static FuncProvider<string, Dictionary<string, string>, string, int, PenumbraApiEc> Provider(
            DalamudPluginInterface pi, Func<string, Dictionary<string, string>, string, int, PenumbraApiEc> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, Dictionary<string, string>, string, int, PenumbraApiEc> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="Api.IPenumbraApi.AddTemporaryMod"/>
    public static class AddTemporaryMod
    {
        public const string Label = $"Penumbra.{nameof(AddTemporaryMod)}";

        public static FuncProvider<string, Guid, Dictionary<string, string>, string, int, PenumbraApiEc> Provider(
            DalamudPluginInterface pi, Func<string, Guid, Dictionary<string, string>, string, int, PenumbraApiEc> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, Guid, Dictionary<string, string>, string, int, PenumbraApiEc> Subscriber(
            DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="Api.IPenumbraApi.RemoveTemporaryModAll"/>
    public static class RemoveTemporaryModAll
    {
        public const string Label = $"Penumbra.{nameof(RemoveTemporaryModAll)}";

        public static FuncProvider<string, int, PenumbraApiEc> Provider(
            DalamudPluginInterface pi, Func<string, int, PenumbraApiEc> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, int, PenumbraApiEc> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="Api.IPenumbraApi.RemoveTemporaryMod"/>
    public static class RemoveTemporaryMod
    {
        public const string Label = $"Penumbra.{nameof(RemoveTemporaryMod)}";

        public static FuncProvider<string, Guid, int, PenumbraApiEc> Provider(
            DalamudPluginInterface pi, Func<string, Guid, int, PenumbraApiEc> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, Guid, int, PenumbraApiEc> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }
}
