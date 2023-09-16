using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Plugin;
using Penumbra.Api.Helpers;

namespace Penumbra.Api;

public static partial class Ipc
{
    /// <inheritdoc cref="IPenumbraApi.GetGameObjectResourcePaths"/>
    public static class GetGameObjectResourcePaths
    {
        public const string Label = $"Penumbra.{nameof( GetGameObjectResourcePaths )}";

        public static FuncProvider< GameObject[], bool, IReadOnlyDictionary< string, string[] >?[] > Provider( DalamudPluginInterface pi, Func< GameObject[], bool, IReadOnlyDictionary< string, string[] >?[] > func )
            => new(pi, Label, func);

        public static FuncSubscriber< GameObject[], bool, IReadOnlyDictionary< string, string[] >?[] > Subscriber( DalamudPluginInterface pi )
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.GetPlayerResourcePaths"/>
    public static class GetPlayerResourcePaths
    {
        public const string Label = $"Penumbra.{nameof( GetPlayerResourcePaths )}";

        public static FuncProvider< bool, IReadOnlyDictionary< GameObject, IReadOnlyDictionary< string, string[] > > > Provider( DalamudPluginInterface pi, Func< bool, IReadOnlyDictionary< GameObject, IReadOnlyDictionary< string, string[] > > > func )
            => new(pi, Label, func);

        public static FuncSubscriber< bool, IReadOnlyDictionary< GameObject, IReadOnlyDictionary< string, string[] > > > Subscriber( DalamudPluginInterface pi )
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.GetGameObjectResourcesOfType"/>
    public static class GetGameObjectResourcesOfType
    {
        public const string Label = $"Penumbra.{nameof( GetGameObjectResourcesOfType )}";

        public static FuncProvider< GameObject[], uint, bool, IReadOnlyDictionary< nint, ( string, string, uint ) >?[] > Provider( DalamudPluginInterface pi, Func< GameObject[], uint, bool, IReadOnlyDictionary< nint, ( string, string, uint ) >?[] > func )
            => new(pi, Label, func);

        public static FuncSubscriber< GameObject[], uint, bool, IReadOnlyDictionary< nint, ( string, string, uint ) >?[] > Subscriber( DalamudPluginInterface pi )
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.GetPlayerResourcesOfType"/>
    public static class GetPlayerResourcesOfType
    {
        public const string Label = $"Penumbra.{nameof( GetPlayerResourcesOfType )}";

        public static FuncProvider< uint, bool, IReadOnlyDictionary< GameObject, IReadOnlyDictionary< nint, ( string, string, uint ) > > > Provider( DalamudPluginInterface pi, Func< uint, bool, IReadOnlyDictionary< GameObject, IReadOnlyDictionary< nint, ( string, string, uint ) > > > func )
            => new(pi, Label, func);

        public static FuncSubscriber< uint, bool, IReadOnlyDictionary< GameObject, IReadOnlyDictionary< nint, ( string, string, uint ) > > > Subscriber( DalamudPluginInterface pi )
            => new(pi, Label);
    }
}
