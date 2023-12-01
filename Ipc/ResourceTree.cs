using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Plugin;
using Penumbra.Api.Enums;
using Penumbra.Api.Helpers;

namespace Penumbra.Api;

public static partial class Ipc
{
    /// <inheritdoc cref="IPenumbraApi.GetGameObjectResourcePaths"/>
    public static class GetGameObjectResourcePaths
    {
        public const string Label = $"Penumbra.{nameof(GetGameObjectResourcePaths)}";

        public static FuncProvider<ushort[], IReadOnlyDictionary<string, string[]>?[]> Provider(DalamudPluginInterface pi,
            Func<ushort[], IReadOnlyDictionary<string, string[]>?[]> func)
            => new(pi, Label, func);

        public static ParamsFuncSubscriber<ushort, IReadOnlyDictionary<string, string[]>?[]> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.GetPlayerResourcePaths"/>
    public static class GetPlayerResourcePaths
    {
        public const string Label = $"Penumbra.{nameof(GetPlayerResourcePaths)}";

        public static FuncProvider<IReadOnlyDictionary<ushort, IReadOnlyDictionary<string, string[]>>> Provider(DalamudPluginInterface pi,
            Func<IReadOnlyDictionary<ushort, IReadOnlyDictionary<string, string[]>>> func)
            => new(pi, Label, func);

        public static FuncSubscriber<IReadOnlyDictionary<ushort, IReadOnlyDictionary<string, string[]>>> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.GetGameObjectResourcesOfType"/>
    public static class GetGameObjectResourcesOfType
    {
        public const string Label = $"Penumbra.{nameof(GetGameObjectResourcesOfType)}";

        public static FuncProvider<ResourceType, bool, ushort[], IReadOnlyDictionary<nint, ( string, string, ChangedItemIcon )>?[]> Provider(
            DalamudPluginInterface pi,
            Func<ResourceType, bool, ushort[], IReadOnlyDictionary<nint, ( string, string, ChangedItemIcon )>?[]> func)
            => new(pi, Label, func);

        public static ParamsFuncSubscriber<ResourceType, bool, ushort, IReadOnlyDictionary<nint, ( string, string, ChangedItemIcon )>?[]>
            Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.GetPlayerResourcesOfType"/>
    public static class GetPlayerResourcesOfType
    {
        public const string Label = $"Penumbra.{nameof(GetPlayerResourcesOfType)}";

        public static FuncProvider<ResourceType, bool,
            IReadOnlyDictionary<ushort, IReadOnlyDictionary<nint, ( string, string, ChangedItemIcon )>>> Provider(
            DalamudPluginInterface pi,
            Func<ResourceType, bool, IReadOnlyDictionary<ushort, IReadOnlyDictionary<nint, ( string, string, ChangedItemIcon )>>> func)
            => new(pi, Label, func);

        public static FuncSubscriber<ResourceType, bool,
            IReadOnlyDictionary<ushort, IReadOnlyDictionary<nint, ( string, string, ChangedItemIcon )>>> Subscriber(
            DalamudPluginInterface pi)
            => new(pi, Label);
    }

    public record ResourceTree
    {
        public required string Name { get; init; }
        public required ushort RaceCode { get; init; }
        public required List<ResourceNode> Nodes { get; init; }
    }

    public record ResourceNode
    {
        public required ResourceType Type { get; init; }
        public required ChangedItemIcon Icon { get; init; }
        public required string? Name { get; init; }
        public required string? GamePath { get; init; }
        public required string ActualPath { get; init; }
        public required nint ObjectAddress { get; init; }
        public required nint ResourceHandle { get; init; }
        public required List<ResourceNode> Children { get; init; }
    }

    /// <inheritdoc cref="IPenumbraApi.GetGameObjectResourceTrees"/>
    public static class GetGameObjectResourceTrees
    {
        public const string Label = $"Penumbra.{nameof(GetGameObjectResourceTrees)}";

        public static FuncProvider<bool, ushort[], ResourceTree?[]> Provider(DalamudPluginInterface pi,
            Func<bool, ushort[], ResourceTree?[]> func)
            => new(pi, Label, func);

        public static ParamsFuncSubscriber<bool, ushort, ResourceTree?[]> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.GetPlayerResourceTrees"/>
    public static class GetPlayerResourceTrees
    {
        public const string Label = $"Penumbra.{nameof(GetPlayerResourceTrees)}";

        public static FuncProvider<bool, IReadOnlyDictionary<ushort, ResourceTree>> Provider(DalamudPluginInterface pi,
            Func<bool, IReadOnlyDictionary<ushort, ResourceTree>> func)
            => new(pi, Label, func);

        public static FuncSubscriber<bool, IReadOnlyDictionary<ushort, ResourceTree>> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }
}
