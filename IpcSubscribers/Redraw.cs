using Dalamud.Plugin;
using Penumbra.Api.Api;
using Penumbra.Api.Enums;
using Penumbra.Api.Helpers;

namespace Penumbra.Api.IpcSubscribers;

/// <inheritdoc cref="IPenumbraApiRedraw.RedrawObject"/>
public sealed class RedrawObject(IDalamudPluginInterface pi)
    : ActionSubscriber<int, int>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(RedrawObject)}.V5";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.RedrawObject.V5"u8;

    /// <inheritdoc cref="IPenumbraApiRedraw.RedrawObject"/>
    public void Invoke(int gameObjectIndex, RedrawType setting = RedrawType.Redraw)
        => base.Invoke(gameObjectIndex, (int)setting);

    /// <summary> Create a provider. </summary>
    public static ActionProvider<int, int> Provider(IDalamudPluginInterface pi, IPenumbraApiRedraw api)
        => new(pi, Label, (a, b) => api.RedrawObject(a, (RedrawType)b));
}

/// <inheritdoc cref="IPenumbraApiRedraw.RedrawAll"/>
public sealed class RedrawAll(IDalamudPluginInterface pi)
    : ActionSubscriber<int>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(RedrawAll)}.V5";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.RedrawAll.V5"u8;

    /// <inheritdoc cref="IPenumbraApiRedraw.RedrawAll"/>
    public void Invoke(RedrawType setting = RedrawType.Redraw)
        => base.Invoke((int)setting);

    /// <summary> Create a provider. </summary>
    public static ActionProvider<int> Provider(IDalamudPluginInterface pi, IPenumbraApiRedraw api)
        => new(pi, Label, a => api.RedrawAll((RedrawType)a));
}

/// <inheritdoc cref="IPenumbraApiRedraw.RedrawCollectionMembers" />
public sealed class RedrawCollectionMembers(IDalamudPluginInterface pi)
    : ActionSubscriber<Guid,int>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(RedrawCollectionMembers)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.RedrawCollectionMembers"u8;

    /// <inheritdoc cref="IPenumbraApiRedraw.RedrawCollectionMembers"/>
    public void Invoke(Guid collection,  RedrawType setting = RedrawType.Redraw)
        => base.Invoke(collection, (int)setting);

    /// <summary> Create a provider. </summary>
    public static ActionProvider<Guid,int> Provider(IDalamudPluginInterface pi, IPenumbraApiRedraw api)
        => new(pi, Label, (a, b) => api.RedrawCollectionMembers(a, (RedrawType)b));
}

    

/// <inheritdoc cref="IPenumbraApiRedraw.GameObjectRedrawn" />
public static class GameObjectRedrawn
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(GameObjectRedrawn)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.GameObjectRedrawn"u8;

    /// <summary> Create a new event subscriber. </summary>
    public static EventSubscriber<nint, int> Subscriber(IDalamudPluginInterface pi, params Action<nint, int>[] actions)
        => new(pi, Label, actions);

    /// <summary> Create a provider. </summary>
    public static EventProvider<nint, int> Provider(IDalamudPluginInterface pi, IPenumbraApiRedraw api)
        => new(pi, Label, t => api.GameObjectRedrawn += t.Invoke, t => api.GameObjectRedrawn -= t.Invoke);
}
