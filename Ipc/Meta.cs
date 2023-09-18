using Dalamud.Plugin;
using Penumbra.Api.Helpers;

namespace Penumbra.Api;

public static partial class Ipc
{
    /// <inheritdoc cref="IPenumbraApi.GetPlayerMetaManipulations"/>
    public static class GetPlayerMetaManipulations
    {
        public const string Label = $"Penumbra.{nameof(GetPlayerMetaManipulations)}";

        public static FuncProvider<string> Provider(DalamudPluginInterface pi, Func<string> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.GetMetaManipulations"/>
    public static class GetMetaManipulations
    {
        public const string Label = $"Penumbra.{nameof(GetMetaManipulations)}";

        public static FuncProvider<string, string> Provider(DalamudPluginInterface pi, Func<string, string> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, string> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.GetGameObjectMetaManipulations"/>
    public static class GetGameObjectMetaManipulations
    {
        public const string Label = $"Penumbra.{nameof(GetGameObjectMetaManipulations)}";

        public static FuncProvider<int, string> Provider(DalamudPluginInterface pi, Func<int, string> func)
            => new(pi, Label, func);

        public static FuncSubscriber<int, string> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }
}
