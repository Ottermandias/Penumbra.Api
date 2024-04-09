using Penumbra.Api.Enums;

namespace Penumbra.Api.Api;

public partial interface IPenumbraApi
{
    /// <summary>
    /// Get the given game objects' resources, as dictionaries of actual paths (that may be FS paths for redirected resources, or game paths for swapped or vanilla resources) to game paths.
    /// </summary>
    /// <param name="gameObjects"> The game object indices for which to get the resources. </param>
    /// <returns> An array of resource path dictionaries, of the same length and in the same order as the given game object index array. </returns>
    /// <remarks> This function is best called right after the game objects are redrawn, as it may fail to resolve paths if relevant mod settings have changed since then. </remarks>
    public IReadOnlyDictionary<string, string[]>?[] GetGameObjectResourcePaths(params ushort[] gameObjects);

    /// <summary>
    /// Get the player and player-owned game objects' resources, as dictionaries of actual paths (that may be FS paths for redirected resources, or game paths for swapped or vanilla resources) to game paths.
    /// </summary>
    /// <returns> A dictionary of game object indices to resource path dictionaries. </returns>
    /// <remarks> This function is best called right after the game objects are redrawn, as it may fail to resolve paths if relevant mod settings have changed since then. </remarks>
    public IReadOnlyDictionary<ushort, IReadOnlyDictionary<string, string[]>> GetPlayerResourcePaths();

    /// <summary>
    /// Get the given game objects' resources of a given type, as dictionaries of resource handles to actual paths and, optionally, names and icons.
    /// </summary>
    /// <param name="gameObjects"> The game object indices for which to get the resources. </param>
    /// <param name="type"> Type of the resources to get, for example <see cref="ResourceType.Mtrl"/> for materials. </param>
    /// <param name="withUiData"> Whether to get names and icons along with the paths. </param>
    /// <returns> An array of resource information dictionaries, of the same length and in the same order as the given game object index array. </returns>
    /// <remarks>
    /// It is the caller's responsibility to make sure the returned resource handles are still in use on the game object's draw object before using them. <para />
    /// Also, callers should not use UI data for non-UI purposes.
    /// </remarks>
    public IReadOnlyDictionary<nint, ( string, string, ChangedItemIcon )>?[] GetGameObjectResourcesOfType(ResourceType type, bool withUiData,
        params ushort[] gameObjects);

    /// <summary>
    /// Get the player and player-owned game objects' resources of a given type, as dictionaries of resource handles to actual paths and, optionally, names and icons.
    /// </summary>
    /// <param name="type"> Type of the resources to get, for example <see cref="ResourceType.Mtrl"/> for materials. </param>
    /// <param name="withUiData"> Whether to get names and icons along with the paths. </param>
    /// <returns> A dictionary of game object indices to resource information dictionaries. </returns>
    /// <remarks>
    /// It is the caller's responsibility to make sure the returned resource handles are still in use on the game object's draw object before using them. <para />
    /// Also, callers should not use UI data for non-UI purposes.
    /// </remarks>
    public IReadOnlyDictionary<ushort, IReadOnlyDictionary<nint, ( string, string, ChangedItemIcon )>> GetPlayerResourcesOfType(
        ResourceType type, bool withUiData);

    /// <summary>
    /// Get the given game objects' resource tree.
    /// </summary>
    /// <param name="withUiData"> Whether to get names and icons along with the paths. </param>
    /// <param name="gameObjects"> The game object indices for which to get the resources. </param>
    /// <returns> An array of resource trees, of the same length and in the same order as the given game object index array. </returns>
    /// <remarks>
    /// It is the caller's responsibility to make sure the returned resource handles are still in use on the game object's draw object before using them. <para />
    /// Also, callers should not use UI data for non-UI purposes.
    /// </remarks>
    public Ipc.ResourceTree?[] GetGameObjectResourceTrees(bool withUiData, params ushort[] gameObjects);

    /// <summary>
    /// Get the player and player-owned game objects' resource trees.
    /// </summary>
    /// <param name="withUiData"> Whether to get names and icons along with the paths. </param>
    /// <returns> A dictionary of game object indices to resource trees. </returns>
    /// <remarks>
    /// It is the caller's responsibility to make sure the returned resource handles are still in use on the game object's draw object before using them. <para />
    /// Also, callers should not use UI data for non-UI purposes.
    /// </remarks>
    public IReadOnlyDictionary<ushort, Ipc.ResourceTree> GetPlayerResourceTrees(bool withUiData);
}
