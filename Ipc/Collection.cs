using Dalamud.Plugin;
using Penumbra.Api.Enums;
using Penumbra.Api.Helpers;

namespace Penumbra.Api;

public static partial class Ipc
{
    /// <inheritdoc cref="IPenumbraApi.GetCollections"/>
    public static class GetCollections
    {
        public const string Label = $"Penumbra.{nameof(GetCollections)}";

        public static FuncProvider<IList<string>> Provider(DalamudPluginInterface pi, Func<IList<string>> func)
            => new(pi, Label, func);

        public static FuncSubscriber<IList<string>> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.GetCurrentCollection"/>
    public static class GetCurrentCollectionName
    {
        public const string Label = $"Penumbra.{nameof(GetCurrentCollectionName)}";

        public static FuncProvider<string> Provider(DalamudPluginInterface pi, Func<string> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.GetDefaultCollection"/>
    public static class GetDefaultCollectionName
    {
        public const string Label = $"Penumbra.{nameof(GetDefaultCollectionName)}";

        public static FuncProvider<string> Provider(DalamudPluginInterface pi, Func<string> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.GetInterfaceCollection"/>
    public static class GetInterfaceCollectionName
    {
        public const string Label = $"Penumbra.{nameof(GetInterfaceCollectionName)}";

        public static FuncProvider<string> Provider(DalamudPluginInterface pi, Func<string> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.GetCharacterCollection"/>
    public static class GetCharacterCollectionName
    {
        public const string Label = $"Penumbra.{nameof(GetCharacterCollectionName)}";

        public static FuncProvider<string, (string, bool)> Provider(DalamudPluginInterface pi, Func<string, (string, bool)> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, (string, bool)> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.GetChangedItemsForCollection"/>
    public static class GetChangedItems
    {
        public const string Label = $"Penumbra.{nameof(GetChangedItems)}";

        public static FuncProvider<string, IReadOnlyDictionary<string, object?>> Provider(DalamudPluginInterface pi,
            Func<string, IReadOnlyDictionary<string, object?>> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, IReadOnlyDictionary<string, object?>> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.GetCollectionForType"/>
    public static class GetCollectionForType
    {
        public const string Label = $"Penumbra.{nameof(GetCollectionForType)}";

        public static FuncProvider<ApiCollectionType, string> Provider(DalamudPluginInterface pi,
            Func<ApiCollectionType, string> func)
            => new(pi, Label, func);

        public static FuncSubscriber<ApiCollectionType, string> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.SetCollectionForType"/>
    public static class SetCollectionForType
    {
        public const string Label = $"Penumbra.{nameof(SetCollectionForType)}";

        public static FuncProvider<ApiCollectionType, string, bool, bool, (PenumbraApiEc, string)> Provider(DalamudPluginInterface pi,
            Func<ApiCollectionType, string, bool, bool, (PenumbraApiEc, string)> func)
            => new(pi, Label, func);

        public static FuncSubscriber<ApiCollectionType, string, bool, bool, (PenumbraApiEc, string)> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.GetCollectionForObject"/>
    public static class GetCollectionForObject
    {
        public const string Label = $"Penumbra.{nameof(GetCollectionForObject)}";

        public static FuncProvider<int, (bool, bool, string)> Provider(DalamudPluginInterface pi,
            Func<int, (bool, bool, string)> func)
            => new(pi, Label, func);

        public static FuncSubscriber<int, (bool, bool, string)> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.SetCollectionForObject"/>
    public static class SetCollectionForObject
    {
        public const string Label = $"Penumbra.{nameof(SetCollectionForObject)}";

        public static FuncProvider<int, string, bool, bool, (PenumbraApiEc, string)> Provider(DalamudPluginInterface pi,
            Func<int, string, bool, bool, (PenumbraApiEc, string)> func)
            => new(pi, Label, func);

        public static FuncSubscriber<int, string, bool, bool, (PenumbraApiEc, string)> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }
}
