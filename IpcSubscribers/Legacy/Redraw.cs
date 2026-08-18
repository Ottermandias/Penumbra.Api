using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Plugin;
using Luna;
using Penumbra.Api.Enums;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Penumbra.Api.IpcSubscribers.Legacy;

public sealed class RedrawAll(IDalamudPluginInterface pi)
    : ActionSubscriber<RedrawType>(pi, Label)
{
    public const string Label = $"Penumbra.{nameof(RedrawAll)}";

    public void Invoke(RedrawType type)
        => base.Invoke(type);
}

public sealed class RedrawObject(IDalamudPluginInterface pi)
    : ActionSubscriber<IGameObject, RedrawType>(pi, Label)
{
    public const string Label = $"Penumbra.{nameof(RedrawObject)}";

    public void Invoke(IGameObject gameObject, RedrawType type = RedrawType.Redraw)
        => base.Invoke(gameObject, type);
}

public sealed class RedrawObjectByIndex(IDalamudPluginInterface pi)
    : ActionSubscriber<int, RedrawType>(pi, Label)
{
    public const string Label = $"Penumbra.{nameof(RedrawObjectByIndex)}";

    public void Invoke(int gameObjectIndex, RedrawType type = RedrawType.Redraw)
        => base.Invoke(gameObjectIndex, type);
}

public sealed class RedrawObjectByName(IDalamudPluginInterface pi)
    : ActionSubscriber<string, RedrawType>(pi, Label)
{
    public const string Label = $"Penumbra.{nameof(RedrawObjectByName)}";

    public void Invoke(string gameObjectName, RedrawType type = RedrawType.Redraw)
        => base.Invoke(gameObjectName, type);
}
