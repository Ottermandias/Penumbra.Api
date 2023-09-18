using Dalamud.Plugin;
using Penumbra.Api.Helpers;

namespace Penumbra.Api;

public static partial class Ipc
{
    /// <summary>Triggered when the Penumbra API is initialized and ready.</summary>
    public static class Initialized
    {
        public const string Label = $"Penumbra.{nameof(Initialized)}";

        public static EventProvider Provider(DalamudPluginInterface pi)
            => new(pi, Label);

        public static EventSubscriber Subscriber(DalamudPluginInterface pi, params Action[] actions)
        {
            var ret = new EventSubscriber(pi, Label);
            foreach (var action in actions)
                ret.Event += action;

            return ret;
        }
    }

    /// <summary>Triggered when the Penumbra API is fully disposed and unavailable.</summary>
    public static class Disposed
    {
        public const string Label = $"Penumbra.{nameof(Disposed)}";

        public static EventProvider Provider(DalamudPluginInterface pi)
            => new(pi, Label);

        public static EventSubscriber Subscriber(DalamudPluginInterface pi, params Action[] actions)
        {
            var ret = new EventSubscriber(pi, Label);
            foreach (var action in actions)
                ret.Event += action;

            return ret;
        }
    }

    /// <inheritdoc cref="IPenumbraApiBase.ApiVersion"/>
    /// <remarks>Deprecated, please use <see cref="Ipc.ApiVersions"/> instead.</remarks>
    public static class ApiVersion
    {
        public const string Label = $"Penumbra.{nameof(ApiVersion)}";

        public static FuncProvider<int> Provider(DalamudPluginInterface pi, Func<int> func)
            => new(pi, Label, func);

        public static FuncSubscriber<int> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApiBase.ApiVersion"/>
    public static class ApiVersions
    {
        public const string Label = $"Penumbra.{nameof(ApiVersions)}";

        public static FuncProvider<(int Breaking, int Features)> Provider(DalamudPluginInterface pi, Func<(int, int)> func)
            => new(pi, Label, func);

        public static FuncSubscriber<(int Breaking, int Features)> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.GetEnabledState"/>
    public static class GetEnabledState
    {
        public const string Label = $"Penumbra.{nameof(GetEnabledState)}";

        public static FuncProvider<bool> Provider(DalamudPluginInterface pi, Func<bool> func)
            => new(pi, Label, func);

        public static FuncSubscriber<bool> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.EnabledChange"/>
    public static class EnabledChange
    {
        public const string Label = $"Penumbra.{nameof(EnabledChange)}";

        public static EventProvider<bool> Provider(DalamudPluginInterface pi, Action add, Action del)
            => new(pi, Label, add, del);

        public static EventSubscriber<bool> Subscriber(DalamudPluginInterface pi, params Action<bool>[] actions)
            => new(pi, Label, actions);
    }
}
