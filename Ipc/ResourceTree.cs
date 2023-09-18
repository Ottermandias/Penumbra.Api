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
        public const string Label = $"Penumbra.{nameof( GetGameObjectResourcePaths )}";

        public static FuncProvider< ushort[], IReadOnlyDictionary< string, string[] >?[] > Provider( DalamudPluginInterface pi, Func< ushort[], IReadOnlyDictionary< string, string[] >?[] > func )
            => new(pi, Label, func);

        public static FuncSubscriber< ushort[], IReadOnlyDictionary< string, string[] >?[] > Subscriber( DalamudPluginInterface pi )
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.GetPlayerResourcePaths"/>
    public static class GetPlayerResourcePaths
    {
        public const string Label = $"Penumbra.{nameof( GetPlayerResourcePaths )}";

        public static FuncProvider< IReadOnlyDictionary< ushort, IReadOnlyDictionary< string, string[] > > > Provider( DalamudPluginInterface pi, Func< IReadOnlyDictionary< ushort, IReadOnlyDictionary< string, string[] > > > func )
            => new(pi, Label, func);

        public static FuncSubscriber< IReadOnlyDictionary< ushort, IReadOnlyDictionary< string, string[] > > > Subscriber( DalamudPluginInterface pi )
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.GetGameObjectResourcesOfType"/>
    public static class GetGameObjectResourcesOfType
    {
        public const string Label = $"Penumbra.{nameof( GetGameObjectResourcesOfType )}";

        public static FuncProvider< ushort[], ResourceType, bool, IReadOnlyDictionary< nint, ( string, string, ChangedItemIcon ) >?[] > Provider( DalamudPluginInterface pi, Func< ushort[], ResourceType, bool, IReadOnlyDictionary< nint, ( string, string, ChangedItemIcon ) >?[] > func )
            => new(pi, Label, func);

        public static FuncSubscriber< ushort[], ResourceType, bool, IReadOnlyDictionary< nint, ( string, string, ChangedItemIcon ) >?[] > Subscriber( DalamudPluginInterface pi )
            => new(pi, Label);
    }

    /// <inheritdoc cref="IPenumbraApi.GetPlayerResourcesOfType"/>
    public static class GetPlayerResourcesOfType
    {
        public const string Label = $"Penumbra.{nameof( GetPlayerResourcesOfType )}";

        public static FuncProvider< ResourceType, bool, IReadOnlyDictionary< ushort, IReadOnlyDictionary< nint, ( string, string, ChangedItemIcon ) > > > Provider( DalamudPluginInterface pi, Func< ResourceType, bool, IReadOnlyDictionary< ushort, IReadOnlyDictionary< nint, ( string, string, ChangedItemIcon ) > > > func )
            => new(pi, Label, func);

        public static FuncSubscriber< ResourceType, bool, IReadOnlyDictionary< ushort, IReadOnlyDictionary< nint, ( string, string, ChangedItemIcon ) > > > Subscriber( DalamudPluginInterface pi )
            => new(pi, Label);
    }
}
