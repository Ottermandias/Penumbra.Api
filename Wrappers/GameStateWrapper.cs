using Dalamud.Plugin;
using Dalamud.Plugin.Ipc;
using FFXIVClientStructs.FFXIV.Client.Game.Object;
using FFXIVClientStructs.FFXIV.Client.Graphics.Scene;
using Luna;
using Penumbra.Api.Enums;
using Penumbra.Api.IpcSubscribers;

namespace Penumbra.Api.Wrappers;

/// <summary> A wrapper around some persistent functions to query the game state from Penumbra. </summary>
public sealed unsafe class GameStateWrapper : BasicWrapper<GameStateWrapper, GameStateWrapper.Method>, IBasicWrapper<GameStateWrapper>
{
    /// <summary> Request the corresponding adapter from Penumbra and create a wrapper. </summary>
    /// <param name="pluginInterface"> The plugin interface. </param>
    /// <returns> A game state wrapper. </returns>
    public static GameStateWrapper Request(IDalamudPluginInterface pluginInterface)
        => new GetGameStateAdapter(pluginInterface).Invoke();

    /// <summary> Get the game object currently being created. </summary>
    public GameObject* LastGameObject
        => (GameObject*)Invoke<nint>(Method.GetLastGameObject);

    /// <summary> Obtain the game object a draw object corresponds to, if known. </summary>
    public GameObject* GameObjectFromDrawObject(DrawObject* drawObject)
        => (GameObject*)Invoke<nint, nint>(Method.GameObjectFromDrawObject, (nint)drawObject);

    /// <summary> Redraw a game object by its index. </summary>
    public void Redraw(int objectIndex, RedrawType type = RedrawType.Redraw)
        => Invoke(Method.RedrawByIndex, objectIndex, (int)type);

    /// <summary> Obtain the parent of a cutscene actor if it is known. </summary>
    public short ResolveCutsceneActor(ushort objectIndex)
        => Invoke<ushort, short>(Method.ResolveCutsceneActor, objectIndex);

    /// <summary> Set the cutscene parent of <paramref name="changedObject"/> in Penumbras internal state to a new value. </summary>
    /// <param name="changedObject"> The index of the cutscene actor to be changed. </param>
    /// <param name="newParentIndex"> The new index of the cutscene actors parent or -1 for no parent. </param>
    /// <remarks>
    ///   Checks that the new parent exists as a game object if the value is not -1 before assigning. If it does not, nothing is done.
    ///   Please only use this for good reason and if you know what you are doing, probably only for actor copies you actually create yourself.
    /// </remarks>
    public void SetCutsceneParent(ushort changedObject, ushort newParentIndex)
        => Invoke(Method.SetCutsceneActor, changedObject, newParentIndex);

    /// <summary>
    ///   Triggered when a character base is created and a corresponding gameObject could be found,
    ///   before the Draw Object is actually created, so customize and equip data can be manipulated beforehand.
    /// </summary>
    public event InAction<CreatingCharacterBaseArguments> CreatingCharacterBase
    {
        add => AddDelegate(Method.CreatingCharacterBase, value, Convert);
        remove => RemoveDelegate(Method.CreatingCharacterBase, value);
    }

    /// <summary>
    ///   Triggered after a character base was created if a corresponding gameObject could be found,
    ///   so you can apply flag changes after finishing.
    /// </summary>
    public event InAction<CreatedCharacterBaseArguments> CreatedCharacterBase
    {
        add => AddDelegate(Method.CreatedCharacterBase, value, Convert);
        remove => RemoveDelegate(Method.CreatedCharacterBase, value);
    }

    /// <summary>
    ///   Triggered whenever a resource is redirected by Penumbra for a specific, identified game object.
    ///   Does not trigger if the resource is not requested for a known game object.
    /// </summary>
    public event InAction<GameObjectResourceResolvedArguments> GameObjectResourceResolved
    {
        add => AddDelegate(Method.GameObjectResourceResolved, value, Convert);
        remove => RemoveDelegate(Method.GameObjectResourceResolved, value);
    }

    /// <summary> The methods available for a game state adapter. </summary>
    public enum Method
    {
        /// <inheritdoc cref="GameStateWrapper.GameObjectFromDrawObject"/>
        GameObjectFromDrawObject,

        /// <inheritdoc cref="GameStateWrapper.Redraw"/>
        RedrawByIndex,

        /// <inheritdoc cref="GameStateWrapper.ResolveCutsceneActor"/>
        ResolveCutsceneActor,

        /// <inheritdoc cref="GameStateWrapper.SetCutsceneParent"/>
        SetCutsceneActor,

        /// <inheritdoc cref="GameStateWrapper.LastGameObject"/>
        GetLastGameObject,

        /// <inheritdoc cref="GameStateWrapper.CreatingCharacterBase"/>
        CreatingCharacterBase,

        /// <inheritdoc cref="GameStateWrapper.CreatedCharacterBase"/>
        CreatedCharacterBase,

        /// <inheritdoc cref="GameStateWrapper.GameObjectResourceResolved"/>
        GameObjectResourceResolved,
    }

    private GameStateWrapper(IIdDataShareAdapter adapter)
        : base(adapter)
    { }

    /// <inheritdoc/>
    static GameStateWrapper? IBasicWrapper<GameStateWrapper>.CreateWrapper(IIdDataShareAdapter? adapter)
        => adapter is null ? null : new GameStateWrapper(adapter);

    private static Action<nint, Guid, nint, nint, nint> Convert(InAction<CreatingCharacterBaseArguments> a)
        => (x, y, z, u, v) => a(new CreatingCharacterBaseArguments(x, y, z, u, v));

    private static Action<nint, Guid, nint> Convert(InAction<CreatedCharacterBaseArguments> a)
        => (x, y, z) => a(new CreatedCharacterBaseArguments(x, y, z));

    private static Action<nint, string, string> Convert(InAction<GameObjectResourceResolvedArguments> a)
        => (x, y, z) => a(new GameObjectResourceResolvedArguments(x, y, z));
}

/// <summary> The arguments for the <see cref="CreatingCharacterBase"/> event. </summary>
/// <param name="GameObject"> The game object creating its draw object. </param>
/// <param name="CollectionId"> The associated collection for the game object. </param>
/// <param name="ModelId"> A pointer to the model ID being passed to the draw object creation. </param>
/// <param name="Customize"> A pointer to the customize array being passed to the draw object creation. </param>
/// <param name="EquipData"> A pointer to the equipment data being passed to the draw object creation. </param>
public readonly record struct CreatingCharacterBaseArguments(nint GameObject, Guid CollectionId, nint ModelId, nint Customize, nint EquipData);

/// <summary> The arguments for the <see cref="CreatedCharacterBase"/> event. </summary>
/// <param name="GameObject"> The game object that created its draw object. </param>
/// <param name="CollectionId"> The associated collection for the game object. </param>
/// <param name="DrawObject"> The created draw object. </param>
public readonly record struct CreatedCharacterBaseArguments(nint GameObject, Guid CollectionId, nint DrawObject);

/// <summary> The arguments for the <see cref="GameObjectResourceResolvedArguments"/> event. </summary>
/// <param name="GameObject"> The associated game object for this resource. </param>
/// <param name="GamePath"> The original game path requested. </param>
/// <param name="LocalPath"> The actual path loaded after redirections. </param>
public readonly record struct GameObjectResourceResolvedArguments(nint GameObject, string GamePath, string LocalPath);
