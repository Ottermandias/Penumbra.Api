using Dalamud.Plugin;
using Penumbra.Api.Enums;
using Penumbra.Api.Helpers;

namespace Penumbra.Api;

public static partial class Ipc
{
    /// <inheritdoc cref="Api.IPenumbraApi.GetCollections"/>
    public static class GetCollections
    {
        public const string Label = $"Penumbra.{nameof(GetCollections)}";

        public static FuncProvider<IList<Guid>> Provider(DalamudPluginInterface pi, Func<IList<Guid>> func)
            => new(pi, Label, func);

        public static FuncSubscriber<IList<Guid>> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="Api.IPenumbraApi.GetCurrentCollection"/>
    public static class GetCurrentCollectionName
    {
        public const string Label = $"Penumbra.{nameof(GetCurrentCollectionName)}";

        public static FuncProvider<Guid> Provider(DalamudPluginInterface pi, Func<Guid> func)
            => new(pi, Label, func);

        public static FuncSubscriber<Guid> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="Api.IPenumbraApi.GetDefaultCollection"/>
    public static class GetDefaultCollectionName
    {
        public const string Label = $"Penumbra.{nameof(GetDefaultCollectionName)}";

        public static FuncProvider<Guid> Provider(DalamudPluginInterface pi, Func<Guid> func)
            => new(pi, Label, func);

        public static FuncSubscriber<Guid> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="Api.IPenumbraApi.GetInterfaceCollection"/>
    public static class GetInterfaceCollectionName
    {
        public const string Label = $"Penumbra.{nameof(GetInterfaceCollectionName)}";

        public static FuncProvider<Guid> Provider(DalamudPluginInterface pi, Func<Guid> func)
            => new(pi, Label, func);

        public static FuncSubscriber<Guid> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="Api.IPenumbraApi.GetCharacterCollection"/>
    public static class GetCharacterCollectionName
    {
        public const string Label = $"Penumbra.{nameof(GetCharacterCollectionName)}";

        public static FuncProvider<string, (Guid, bool)> Provider(DalamudPluginInterface pi, Func<string, (Guid, bool)> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, (Guid, bool)> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="Api.IPenumbraApi.GetChangedItemsForCollection"/>
    public static class GetChangedItems
    {
        public const string Label = $"Penumbra.{nameof(GetChangedItems)}";

        public static FuncProvider<Guid, IReadOnlyDictionary<string, object?>> Provider(DalamudPluginInterface pi,
            Func<Guid, IReadOnlyDictionary<string, object?>> func)
            => new(pi, Label, func);

        public static FuncSubscriber<Guid, IReadOnlyDictionary<string, object?>> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="Api.IPenumbraApi.GetCollectionForType"/>
    public static class GetCollectionForType
    {
        public const string Label = $"Penumbra.{nameof(GetCollectionForType)}";

        public static FuncProvider<ApiCollectionType, Guid> Provider(DalamudPluginInterface pi,
            Func<ApiCollectionType, Guid> func)
            => new(pi, Label, func);

        public static FuncSubscriber<ApiCollectionType, Guid> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="Api.IPenumbraApi.SetCollectionForType"/>
    public static class SetCollectionForType
    {
        public const string Label = $"Penumbra.{nameof(SetCollectionForType)}";

        public static FuncProvider<ApiCollectionType, Guid, bool, bool, (PenumbraApiEc, Guid)> Provider(DalamudPluginInterface pi,
            Func<ApiCollectionType, Guid, bool, bool, (PenumbraApiEc, Guid)> func)
            => new(pi, Label, func);

        public static FuncSubscriber<ApiCollectionType, Guid, bool, bool, (PenumbraApiEc, Guid)> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="Api.IPenumbraApi.GetCollectionForObject"/>
    public static class GetCollectionForObject
    {
        public const string Label = $"Penumbra.{nameof(GetCollectionForObject)}";

        public static FuncProvider<int, (bool, bool, Guid)> Provider(DalamudPluginInterface pi,
            Func<int, (bool, bool, Guid)> func)
            => new(pi, Label, func);

        public static FuncSubscriber<int, (bool, bool, Guid)> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="Api.IPenumbraApi.SetCollectionForObject"/>
    public static class SetCollectionForObject
    {
        public const string Label = $"Penumbra.{nameof(SetCollectionForObject)}";

        public static FuncProvider<int, Guid, bool, bool, (PenumbraApiEc, Guid)> Provider(DalamudPluginInterface pi,
            Func<int, Guid, bool, bool, (PenumbraApiEc, Guid)> func)
            => new(pi, Label, func);

        public static FuncSubscriber<int, Guid, bool, bool, (PenumbraApiEc, Guid)> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }
}
