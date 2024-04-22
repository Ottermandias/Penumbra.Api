using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Plugin;
using Penumbra.Api.Enums;
using Penumbra.Api.Helpers;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Penumbra.Api.IpcSubscribers.Legacy;

public sealed class RedrawAll(DalamudPluginInterface pi)
    : ActionSubscriber<RedrawType>(pi, Label)
{
    public const string Label = $"Penumbra.{nameof(RedrawAll)}";

    public new void Invoke(RedrawType type)
        => base.Invoke(type);
}

public sealed class RedrawObject(DalamudPluginInterface pi)
    : ActionSubscriber<GameObject, RedrawType>(pi, Label)
{
    public const string Label = $"Penumbra.{nameof(RedrawObject)}";

    public new void Invoke(GameObject gameObject, RedrawType type = RedrawType.Redraw)
        => base.Invoke(gameObject, type);
}

public sealed class RedrawObjectByIndex(DalamudPluginInterface pi)
    : ActionSubscriber<int, RedrawType>(pi, Label)
{
    public const string Label = $"Penumbra.{nameof(RedrawObjectByIndex)}";

    public new void Invoke(int gameObjectIndex, RedrawType type = RedrawType.Redraw)
        => base.Invoke(gameObjectIndex, type);
}

public sealed class RedrawObjectByName(DalamudPluginInterface pi)
    : ActionSubscriber<string, RedrawType>(pi, Label)
{
    public const string Label = $"Penumbra.{nameof(RedrawObjectByName)}";

    public new void Invoke(string gameObjectName, RedrawType type = RedrawType.Redraw)
        => base.Invoke(gameObjectName, type);
}
