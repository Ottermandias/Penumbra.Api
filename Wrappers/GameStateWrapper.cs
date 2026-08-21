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
    }

    private GameStateWrapper(IIdDataShareAdapter adapter)
        : base(adapter)
    { }

    /// <inheritdoc/>
    static GameStateWrapper? IBasicWrapper<GameStateWrapper>.CreateWrapper(IIdDataShareAdapter? adapter)
        => adapter is null ? null : new GameStateWrapper(adapter);
}
