using Dalamud.Plugin;
using Penumbra.Api.Enums;
using Penumbra.Api.Helpers;

namespace Penumbra.Api;

public static partial class Ipc
{
    /// <inheritdoc cref="IPenumbraApi.ConvertTextureFile"/>
    public static class ConvertTextureFile
    {
        public const string Label = $"Penumbra.{nameof(ConvertTextureFile)}";

        public static FuncProvider<string, string, TextureType, bool, Task> Provider(DalamudPluginInterface pi,
            Func<string, string, TextureType, bool, Task> func)
            => new(pi, Label, func);

        public static FuncSubscriber<string, string, TextureType, bool, Task> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.ConvertTextureData"/>
    public static class ConvertTextureData
    {
        public const string Label = $"Penumbra.{nameof(ConvertTextureData)}";

        public static FuncProvider<byte[], int, string, TextureType, bool, Task> Provider(DalamudPluginInterface pi,
            Func<byte[], int, string, TextureType, bool, Task> func)
            => new(pi, Label, func);

        public static FuncSubscriber<byte[], int, string, TextureType, bool, Task> Subscriber(DalamudPluginInterface pi)
            => new(pi, Label);
    }
}
