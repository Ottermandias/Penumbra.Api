using Dalamud.Game.ClientState.Objects.Types;
using Lumina.Data;
using Penumbra.Api.Enums;

namespace Penumbra.Api;

public interface IPenumbraApi : IPenumbraApiBase
{
    #region Plugin State

    /// <returns>The current penumbra root directory.</returns>
    public string GetModDirectory();

    /// <returns>The entire current penumbra configuration as a json encoded string.</returns>
    public string GetConfiguration();

    /// <summary>
    /// Fired whenever a mod directory change is finished.
    /// </summary>
    /// <returns>The full path of the mod directory and whether Penumbra treats it as valid.</returns>
    public event Action<string, bool>? ModDirectoryChanged;

    /// <returns>True if Penumbra is enabled, false otherwise.</returns>
    public bool GetEnabledState();

    /// <summary>
    /// Fired whenever the enabled state of Penumbra changes.
    /// </summary>
    /// <returns>True if the new state is enabled, false if the new state is disabled</returns>
    public event Action<bool>? EnabledChange;

    #endregion

    #region UI

    /// <summary>
    /// Triggered when the user hovers over a listed changed object in a mod tab.<para />
    /// Can be used to append tooltips.
    /// </summary>
    /// <returns><inheritdoc cref="ChangedItemHover"/></returns>
    public event ChangedItemHover? ChangedItemTooltip;

    /// <summary>
    /// Triggered before the content of a mod settings panel is drawn.
    /// </summary>
    /// <returns>The directory name of the currently selected mod.</returns>
    public event Action<string>? PreSettingsPanelDraw;

    /// <summary>
    /// Triggered after the content of a mod settings panel is drawn, but still in the child window.
    /// </summary>
    /// <returns>The directory name of the currently selected mod.</returns>
    public event Action<string>? PostSettingsPanelDraw;

    /// <summary>
    /// Triggered when the user clicks a listed changed object in a mod tab.
    /// </summary>
    /// <returns><inheritdoc cref="ChangedItemClick"/></returns>
    public event ChangedItemClick? ChangedItemClicked;

    /// <summary>
    /// Open the Penumbra main config window.
    /// </summary>
    /// <param name="tab">Open the window at a specific tab. Use TabType.None to not change the tab. </param>
    /// <param name="modDirectory">Select a mod specified via its directory name in the mod tab, empty if none.</param>
    /// <param name="modName">Select a mod specified via its mod name in the mod tab, empty if none.</param>
    /// <returns>InvalidArgument if <paramref name="tab"/> is invalid,
    /// ModMissing if <paramref name="modDirectory"/> or <paramref name="modName"/> are set non-empty and the mod does not exist,
    /// Success otherwise.</returns>
    /// <remarks>If <paramref name="tab"/> is not TabType.Mods, the mod will not be selected regardless of other parameters and ModMissing will not be returned.</remarks>
    public PenumbraApiEc OpenMainWindow(TabType tab, string modDirectory, string modName);

    /// <summary> Close the Penumbra main config window. </summary>
    public void CloseMainWindow();

    #endregion

    #region Redrawing

    /// <summary>
    /// Queue redrawing of all actors of the given <paramref name="name"/> with the given RedrawType <paramref name="setting"/>.
    /// </summary>
    /// <param name="name" />
    /// <param name="setting" />
    public void RedrawObject(string name, RedrawType setting);

    /// <summary>
    /// Queue redrawing of the specific actor <paramref name="gameObject"/> with the given RedrawType <paramref name="setting"/>. Should only be used when the actor is sure to be valid.
    /// </summary>
    /// <param name="gameObject" />
    /// <param name="setting" />
    public void RedrawObject(GameObject gameObject, RedrawType setting);

    /// <summary>
    /// Queue redrawing of the actor with the given object <paramref name="tableIndex" />, if it exists, with the given RedrawType <paramref name="setting"/>.
    /// </summary>
    /// <param name="tableIndex" />
    /// <param name="setting" />
    public void RedrawObject(int tableIndex, RedrawType setting);

    /// <summary>
    /// Queue redrawing of all currently available actors with the given RedrawType <paramref name="setting"/>.
    /// </summary>
    /// <param name="setting" />
    public void RedrawAll(RedrawType setting);

    /// <summary>
    /// Triggered whenever a game object is redrawn via Penumbra.
    /// </summary>
    /// /<returns><inheritdoc cref="GameObjectRedrawnDelegate"/></returns>
    public event GameObjectRedrawnDelegate? GameObjectRedrawn;

    #endregion

    #region Game State

    /// <param name="drawObject"></param>
    /// <returns>The game object associated with the given <paramref name="drawObject">draw object</paramref> and the name of the collection associated with this game object.</returns>
    public (IntPtr, string) GetDrawObjectInfo(IntPtr drawObject);

    /// <summary>
    /// Obtain the parent game object index for an unnamed cutscene actor by its <paramref name="actorIdx">index</paramref>.
    /// </summary>
    /// <param name="actorIdx"></param>
    /// <returns>The parent game object index.</returns>
    public int GetCutsceneParentIndex(int actorIdx);

    /// <summary>
    /// Set the cutscene parent of <paramref name="copyIdx"/> in Penumbras internal state to a new value.
    /// </summary>
    /// <param name="copyIdx"> The index of the cutscene actor to be changed. </param>
    /// <param name="newParentIdx"> The new index of the cutscene actors parent or -1 for no parent. </param>
    /// <returns> Success when the new parent could be set, or InvalidArgument if either index is out of its respective range. </returns>
    /// <remarks>
    /// Checks that the new parent exists as a game object if the value is not -1 before assigning. If it does not, InvalidArgument is given, too.
    /// Please only use this for good reason and if you know what you are doing, probably only for actor copies you actually create yourself.
    /// </remarks>
    public PenumbraApiEc SetCutsceneParentIndex(int copyIdx, int newParentIdx);

    /// <summary>
    /// Triggered when a character base is created and a corresponding gameObject could be found,
    /// before the Draw Object is actually created, so customize and equipdata can be manipulated beforehand.
    /// </summary>
    /// <returns><inheritdoc cref="CreatingCharacterBaseDelegate"/></returns>
    public event CreatingCharacterBaseDelegate? CreatingCharacterBase;

    /// <summary>
    /// Triggered after a character base was created if a corresponding gameObject could be found,
    /// so you can apply flag changes after finishing.
    /// </summary>
    /// <returns><inheritdoc cref="CreatedCharacterBaseDelegate"/></returns>
    public event CreatedCharacterBaseDelegate? CreatedCharacterBase;

    /// <summary>
    /// Triggered whenever a resource is redirected by Penumbra for a specific, identified game object.
    /// Does not trigger if the resource is not requested for a known game object.
    /// </summary>
    /// <returns><inheritdoc cref="GameObjectResourceResolvedDelegate"/></returns>
    public event GameObjectResourceResolvedDelegate? GameObjectResourceResolved;

    #endregion

    #region Resolving

    /// <summary>
    /// Resolve a given <paramref name="gamePath" /> via Penumbra using the Base collection.
    /// </summary>
    /// <returns>The resolved path, or the given path if Penumbra would not manipulate it.</returns>
    public string ResolveDefaultPath(string gamePath);

    /// <summary>
    /// Resolve a given <paramref name="gamePath" /> via Penumbra using the Interface collection.
    /// </summary>
    /// <returns>The resolved path, or the given path if Penumbra would not manipulate it.</returns>
    public string ResolveInterfacePath(string gamePath);

    /// <summary>
    /// Resolve a given <paramref name="gamePath" /> via Penumbra using the character collection
    /// for <paramref name="characterName" /> or the Base collection if none exists.
    /// </summary>
    /// <returns>The resolved path, or the given path if Penumbra would not manipulate it.</returns>
    public string ResolvePath(string gamePath, string characterName);

    /// <summary>
    /// Resolve a given <paramref name="gamePath" /> via Penumbra using collection applying to the <paramref name="gameObjectIdx"/> 
    /// given by its index in the game object table.
    /// </summary>
    /// <remarks>If the object does not exist in the table, the default collection is used.</remarks>
    /// <returns>The resolved path, or the given path if Penumbra would not manipulate it.</returns>
    public string ResolveGameObjectPath(string gamePath, int gameObjectIdx);

    /// <summary>
    /// Resolve a given <paramref name="gamePath" /> via Penumbra using the collection currently applying to the player character.
    /// </summary>
    /// <returns>The resolved path, or the given path if Penumbra would not manipulate it.</returns>
    public string ResolvePlayerPath(string gamePath);

    /// <summary>
    /// Reverse resolves a given local <paramref name="moddedPath" /> into its replacement in form of all applicable game paths
    /// for the character collection for <paramref name="characterName" />.
    /// </summary>
    /// <returns>A list of game paths resolving to the modded path.</returns>
    public string[] ReverseResolvePath(string moddedPath, string characterName);

    /// <summary>
    /// Reverse resolves a given local <paramref name="moddedPath" /> into its replacement in form of all applicable game paths
    /// for the collection applying to the <paramref name="gameObjectIdx"/>th game object in the game object table.
    /// </summary>
    /// <remarks>If the object does not exist in the table, the default collection is used.</remarks>
    /// <returns>A list of game paths resolving to the modded path.</returns>
    public string[] ReverseResolveGameObjectPath(string moddedPath, int gameObjectIdx);

    /// <summary>
    /// Reverse resolves a given local <paramref name="moddedPath" /> into its replacement in form of all applicable game paths
    /// for the collection currently applying to the player character.
    /// </summary>
    /// <returns>A list of game paths resolving to the modded path.</returns>
    public string[] ReverseResolvePlayerPath(string moddedPath);

    /// <summary>
    /// Resolve all game paths in <paramref name="forward"/> and reserve all paths in <paramref name="reverse"/> at once.
    /// </summary>
    /// <param name="forward">Paths to forward-resolve.</param>
    /// <param name="reverse">Paths to reverse-resolve.</param>
    /// <returns>A pair of an array of forward-resolved single paths of the same length as <paramref name="forward"/> and an array of arrays of reverse-resolved paths.
    /// The outer array has the same length as <paramref name="reverse"/> while each inner array can have arbitrary length.</returns>
    public (string[], string[][]) ResolvePlayerPaths(string[] forward, string[] reverse);

    /// <summary>
    /// Resolve all game paths in <paramref name="forward"/> and reserve all paths in <paramref name="reverse"/> at once asynchronously.
    /// </summary>
    /// <inheritdoc cref="ResolvePlayerPaths"/>
    /// <remarks> Can be called from outside of framework. Can theoretically produce incoherent state when collections change during evaluation. </remarks>
    public Task<(string[], string[][])> ResolvePlayerPathsAsync(string[] forward, string[] reverse);

    /// <summary>
    /// Try to load a given <paramref name="gamePath" /> with the resolved path from Penumbras Base collection.
    /// </summary>
    /// <returns>The file of type T if successful, null otherwise.</returns>
    public T? GetFile<T>(string gamePath) where T : FileResource;

    /// <summary>
    /// Try to load a given <paramref name="gamePath" /> with the resolved path from Penumbra
    /// using the character collection for <paramref name="characterName" />.
    /// </summary>
    /// <returns>The file of type T if successful, null otherwise.</returns>
    public T? GetFile<T>(string gamePath, string characterName) where T : FileResource;

    #endregion

    #region Collections

    /// <returns>A list of the names of all currently installed collections.</returns>
    public IList<string> GetCollections();

    /// <returns>The name of the currently selected collection.</returns>
    public string GetCurrentCollection();

    /// <returns>The name of the default collection.</returns>
    public string GetDefaultCollection();

    /// <returns>The name of the interface collection.</returns>
    public string GetInterfaceCollection();

    /// <returns>The name of the collection associated with <paramref name="characterName"/> and whether it exists as character collection.</returns>
    public (string, bool) GetCharacterCollection(string characterName);

    /// <returns>A dictionary of affected items in <paramref name="collectionName"/> via name and known objects or null.</returns>
    public IReadOnlyDictionary<string, object?> GetChangedItemsForCollection(string collectionName);

    /// <returns>The name of the collection assigned to the given <paramref name="type"/> or an empty string if none is assigned or type is invalid.</returns>
    public string GetCollectionForType(ApiCollectionType type);

    /// <summary>
    /// Set a collection by name for a specific type.
    /// </summary>
    /// <param name="type">The collection type to set.</param>
    /// <param name="collectionName">The name of the collection to set it to.</param>
    /// <param name="allowCreateNew">Allow only setting existing types or also creating an unset type.</param>
    /// <param name="allowDelete">Allow deleting existing collections if <paramref name="collectionName"/> is empty.</param>
    /// <returns>InvalidArgument if type is invalid,
    /// NothingChanged if the new collection is the same as the old,<br />
    /// AssignmentDeletionDisallowed if <paramref name="collectionName"/> is empty and <paramref name="allowDelete"/> is false, and the assignment exists,<br />
    /// or if Default, Current or Interface would be deleted.<br />
    /// CollectionMissing if the new collection can not be found,<br />
    /// AssignmentCreationDisallowed if <paramref name="allowCreateNew"/> is false and the assignment does not exist,<br />
    /// or Success, as well as the name of the previous collection (empty if no assignment existed).
    /// </returns>
    public (PenumbraApiEc, string OldCollection) SetCollectionForType(ApiCollectionType type, string collectionName, bool allowCreateNew,
        bool allowDelete);

    /// <returns>Return whether the object at <paramref name="gameObjectIdx" /> produces a valid identifier, if the identifier has a collection assigned, and the collection that affects the object.</returns>
    public (bool ObjectValid, bool IndividualSet, string EffectiveCollection) GetCollectionForObject(int gameObjectIdx);

    /// <summary>
    /// Set a collection by name for a specific game object.
    /// </summary>
    /// <param name="gameObjectIdx">The index of the desired game object in the object table.</param>
    /// <param name="collectionName">The name of the collection to set it to.</param>
    /// <param name="allowCreateNew">Allow only setting existing individuals or also creating a new individual assignment.</param>
    /// <param name="allowDelete">Allow deleting existing individual assignments if <paramref name="collectionName"/> is empty.</param>
    /// <returns>InvalidIdentifier if <paramref name="gameObjectIdx"/> does not produce an existing game object or the object is not indentifiable,
    /// NothingChanged if the new collection is the same as the old,<br />
    /// AssignmentDeletionDisallowed if <paramref name="collectionName"/> is empty and <paramref name="allowDelete"/> is false, and the assignment exists,<br />
    /// CollectionMissing if the new collection can not be found,<br />
    /// AssignmentCreationDisallowed if <paramref name="allowCreateNew"/> is false and the assignment does not exist,<br />
    /// or Success, as well as the name of the previous collection (empty if no assignment existed).</returns>
    public (PenumbraApiEc, string OldCollection) SetCollectionForObject(int gameObjectIdx, string collectionName, bool allowCreateNew,
        bool allowDelete);

    #endregion

    #region Meta

    /// <returns>A base64 encoded, zipped json-string with a prepended version-byte of the current manipulations
    /// in the collection currently associated with the player.</returns>
    public string GetPlayerMetaManipulations();

    /// <returns>A base64 encoded, zipped json-string with a prepended version-byte of the current manipulations
    /// in the given collection associated with the character name, or the default collection.</returns>
    public string GetMetaManipulations(string characterName);

    /// <returns>A base64 encoded, zipped json-string with a prepended version-byte of the current manipulations
    /// in the given collection applying to the given game object or the default collection if it does not exist.</returns>
    public string GetGameObjectMetaManipulations(int gameObjectIdx);

    #endregion

    #region Mods

    /// <returns>A list of all installed mods. The first string is their directory name, the second string is their mod name.</returns>
    public IList<(string, string)> GetModList();

    /// <summary> Try to unpack and install a valid mod file (.pmp, .ttmp, .ttmp2) as if installed manually. </summary>
    /// <param name="path">The file that should be unpacked.</param>
    /// <returns>Success, MissingFile. Success does not indicate successful installing, just successful queueing for install.</returns>
    public PenumbraApiEc InstallMod(string path);

    /// <summary> Try to reload an existing mod given by its <paramref name="modDirectory" /> name or <paramref name="modName" />.</summary>
    /// <remarks>Reload is the same as if triggered by button press and might delete the mod if it is not valid anymore.</remarks>
    /// <returns>ModMissing if the mod can not be found or Success</returns>
    public PenumbraApiEc ReloadMod(string modDirectory, string modName);

    /// <summary> Try to add a new mod inside the mod root directory.</summary>
    /// <remarks>Note that success does only imply a successful call, not a successful mod load.</remarks>
    /// <param name="modDirectory">The name (not full name) of the mod directory.</param>
    /// <returns>FileMissing if <paramref name="modDirectory" /> does not exist, Success otherwise.</returns>
    public PenumbraApiEc AddMod(string modDirectory);

    /// <summary>Try to delete a mod  given by its <paramref name="modDirectory" /> name or <paramref name="modName" />.</summary>
    /// <remarks>Note that success does only imply a successful call, not successful deletion.</remarks>
    /// <returns>NothingDone if the mod can not be found, Success otherwise.</returns>
    public PenumbraApiEc DeleteMod(string modDirectory, string modName);

    /// <summary> Triggers whenever a mod is deleted. </summary>
    /// <returns>The base directory name of the deleted mod.</returns>
    public event Action<string>? ModDeleted;

    /// <summary> Triggers whenever a mod is deleted. </summary>
    /// <returns>The base directory name of the new mod.</returns>
    public event Action<string>? ModAdded;

    /// <summary> Triggers whenever a mods base name is changed from inside Penumbra. </summary>
    /// <returns>The previous base directory name of the mod and the new base directory name of the mod.</returns>
    public event Action<string, string>? ModMoved;

    /// <summary>
    /// Get the internal full filesystem path including search order for the specified mod
    /// given by its <paramref name="modDirectory" /> name or <paramref name="modName" />.
    /// </summary>
    /// <returns>On Success, the full path and a bool indicating whether this is default (false) or manually set (true).
    /// Otherwise returns ModMissing if the mod can not be found.</returns>
    public (PenumbraApiEc, string, bool) GetModPath(string modDirectory, string modName);

    /// <summary>
    /// Set the internal search order and filesystem path of the specified mod
    /// given by its <paramref name="modDirectory" /> name or <paramref name="modName" />
    /// to the <paramref name="newPath" />.
    /// </summary>
    /// <returns>InvalidArgument if <paramref name="newPath" /> is empty, ModMissing if the mod can not be found,
    /// PathRenameFailed if <paramref name="newPath"/> could not be set or Success.</returns>
    public PenumbraApiEc SetModPath(string modDirectory, string modName, string newPath);

    #endregion

    #region Mod Settings

    /// <summary>
    /// Obtain the potential settings of a mod given by its <paramref name="modDirectory" /> name or <paramref name="modName" />.
    /// </summary>
    /// <returns>A dictionary of group names to lists of option names and the group type. Null if the mod could not be found.</returns>
    public IDictionary<string, (IList<string>, GroupType)>? GetAvailableModSettings(string modDirectory, string modName);

    /// <summary>
    /// Obtain the enabled state, the priority, the settings of a mod given by its <paramref name="modDirectory" /> name or <paramref name="modName" /> in the specified collection.
    /// </summary>
    /// <param name="collectionName">Specify the collection.</param>
    /// <param name="modDirectory">Specify the mod via its directory name.</param>
    /// <param name="modName">Specify the mod via its (non-unique) display name.</param>
    /// <param name="allowInheritance">Whether the settings need to be from the given collection or can be inherited from any other by it.</param>
    /// <returns>ModMissing, CollectionMissing or Success. <para />
    /// On Success, a tuple of Enabled State, Priority, a dictionary of option group names and lists of enabled option names and a bool whether the settings are inherited or not.</returns>
    public (PenumbraApiEc, (bool, int, IDictionary<string, IList<string>>, bool)?) GetCurrentModSettings(string collectionName,
        string modDirectory, string modName, bool allowInheritance);

    /// <summary> Try to set the inheritance state of a mod in a collection. </summary>
    /// <returns>ModMissing, CollectionMissing, NothingChanged or Success.</returns>
    public PenumbraApiEc TryInheritMod(string collectionName, string modDirectory, string modName, bool inherit);

    /// <summary> Try to set the enabled state of a mod in a collection. </summary>
    /// <returns>ModMissing, CollectionMissing, NothingChanged or Success.</returns>
    public PenumbraApiEc TrySetMod(string collectionName, string modDirectory, string modName, bool enabled);

    /// <summary> Try to set the priority of a mod in a collection. </summary>
    /// <returns>ModMissing, CollectionMissing, NothingChanged or Success.</returns>
    public PenumbraApiEc TrySetModPriority(string collectionName, string modDirectory, string modName, int priority);

    /// <summary> Try to set a specific option group of a mod in the given collection to a specific value. </summary>
    /// <remarks>Removes inheritance. Single Selection groups should provide a single option, Multi Selection can provide multiple.
    /// If any setting can not be found, it will not change anything.</remarks>
    /// <returns>ModMissing, CollectionMissing, OptionGroupMissing, SettingMissing, NothingChanged or Success.</returns>
    public PenumbraApiEc TrySetModSetting(string collectionName, string modDirectory, string modName, string optionGroupName, string option);

    /// <inheritdoc cref="TrySetModSetting"/>
    public PenumbraApiEc TrySetModSettings(string collectionName, string modDirectory, string modName, string optionGroupName,
        IReadOnlyList<string> options);

    /// <summary> This event gets fired when any setting in any collection changes. </summary>
    /// <returns><inheritdoc cref="ModSettingChangedDelegate" /></returns>
    public event ModSettingChangedDelegate? ModSettingChanged;

    /// <summary>
    /// Copy all current settings for a mod to another mod.
    /// </summary>
    /// <param name="collectionName">Specify the collection to work in, leave empty or null to do it in all collections.</param>
    /// <param name="modDirectoryFrom">Specify the mod to take the settings from via its directory name.</param>
    /// <param name="modDirectoryTo">Specify the mod to put the settings on via its directory name. If the mod does not exist, it will be added as unused settings.</param>
    /// <returns>CollectionMissing if collectionName is not empty but does not exist or Success.</returns>
    /// <remarks>If the target mod exists, the settings will be fixed before being applied. If the source mod does not exist, it will use unused settings if available and remove existing settings otherwise.</remarks>
    public PenumbraApiEc CopyModSettings(string? collectionName, string modDirectoryFrom, string modDirectoryTo);

    #endregion

    #region Editing

    /// <summary>
    /// Convert the given texture file into a different type or format and potentially add mip maps.
    /// </summary>
    /// <param name="inputFile"> The path to the input file, which may be of .dds, .tex or .png format. </param>
    /// <param name="outputFile"> The desired output path. Can be the same as input. </param>
    /// <param name="textureType"> The file type and format to convert the data to. </param>
    /// <param name="mipMaps"> Whether to add mip maps or not. Ignored for .png. </param>
    /// <returns> A task for when the conversion is finished or has failed. </returns>
    public Task ConvertTextureFile(string inputFile, string outputFile, TextureType textureType, bool mipMaps);

    /// <summary>
    /// Convert the given RGBA32 texture data into a different type or format and potentially add mip maps.
    /// </summary>
    /// <param name="rgbaData"> The input byte data for a picture given in RGBA32 format. </param>
    /// <param name="width"> The width of the input picture. The height is computed from the size of <paramref name="rgbaData"/> and this. </param>
    /// <param name="outputFile"> The desired output path. Can be the same as input. </param>
    /// <param name="textureType"> The file type and format to convert the data to. </param>
    /// <param name="mipMaps"> Whether to add mip maps or not. Ignored for .png. </param>
    /// <returns> A task for when the conversion is finished or has failed. </returns>
    public Task ConvertTextureData(byte[] rgbaData, int width, string outputFile, TextureType textureType, bool mipMaps);

    #endregion

    #region Temporary

    /// <summary>
    /// Create a temporary collection without actual settings but with a cache and assign it to a specific character by name only.
    /// </summary>
    /// <remarks>This function is outdated, prefer to use <see cref="CreateNamedTemporaryCollection"/> and <see cref="AssignTemporaryCollection"/>.</remarks>
    /// <param name="tag">A custom tag for your collections.</param>
    /// <param name="character">The full, case-sensitive character name this collection should apply to.</param>
    /// <param name="forceOverwriteCharacter">Whether to overwrite an existing character collection.</param>
    /// <returns>Success, CharacterCollectionExists or NothingChanged and the name of the new temporary collection on success.</returns>
    public (PenumbraApiEc, string) CreateTemporaryCollection(string tag, string character, bool forceOverwriteCharacter);

    /// <summary>
    /// Create a temporary collection of the given <paramref name="name"/>.
    /// </summary>
    /// <param name="name">The intended name. It may not be empty or contain symbols invalid in a path used by XIV.</param>
    /// <returns>Success, InvalidArgument if name is not valid for a collection, or TemporaryCollectionExists.</returns>
    public PenumbraApiEc CreateNamedTemporaryCollection(string name);

    /// <summary>
    /// Assign an existing temporary collection to an actor that currently occupies a specific slot.
    /// </summary>
    /// <param name="collectionName">The chosen collection assigned to the actor.</param>
    /// <param name="actorIndex">The current object table index of the actor.</param>
    /// <param name="forceAssignment">Whether to assign even if the actor is already assigned either a temporary or a permanent collection.</param>
    /// <returns>Success, InvalidArgument if the actor can not be identified, CollectionMissing if the collection does not exist, CharacterCollectionExists if <paramref name="forceAssignment"/> is false and the actor is already assigned a collection, and AssignmentDeletionFailed if <paramref name="forceAssignment"/> is true and the existing temporary assignment could not be deleted. </returns>
    public PenumbraApiEc AssignTemporaryCollection(string collectionName, int actorIndex, bool forceAssignment);

    /// <summary>
    /// Remove the temporary collection assigned to characterName if it exists.
    /// </summary>
    /// <remarks>This function is outdated, prefer to use <see cref="RemoveTemporaryCollectionByName" />.</remarks>
    /// <returns>NothingChanged or Success.</returns>
    public PenumbraApiEc RemoveTemporaryCollection(string characterName);

    /// <summary>
    /// Remove the temporary collection of the given name.
    /// </summary>
    /// <param name="collectionName">The chosen temporary collection to remove.</param>
    /// <returns>NothingChanged or Success.</returns>
    public PenumbraApiEc RemoveTemporaryCollectionByName(string collectionName);

    /// <summary>
    /// Set a temporary mod with the given paths, manipulations and priority and the name tag to all regular and temporary collections.
    /// </summary>
    /// <param name="tag">Custom name for the temporary mod.</param>
    /// <param name="paths">List of redirections (can be swaps or redirections).</param>
    /// <param name="manipString">Zipped Base64 string of meta manipulations.</param>
    /// <param name="priority">Desired priority.</param>
    /// <returns>InvalidGamePath, InvalidManipulation or Success.</returns>
    public PenumbraApiEc AddTemporaryModAll(string tag, Dictionary<string, string> paths, string manipString, int priority);

    /// <summary>
    /// Set a temporary mod with the given paths, manipulations and priority and the name tag to a specific collection.
    /// </summary>
    /// <param name="tag">Custom name for the temporary mod.</param>
    /// <param name="collectionName">Name of the collection the mod should apply to. Can be a temporary collection name.</param>
    /// <param name="paths">List of redirections (can be swaps or redirections).</param>
    /// <param name="manipString">Zipped Base64 string of meta manipulations.</param>
    /// <param name="priority">Desired priority.</param>
    /// <returns>CollectionMissing, InvalidGamePath, InvalidManipulation or Success.</returns>
    public PenumbraApiEc AddTemporaryMod(string tag, string collectionName, Dictionary<string, string> paths, string manipString,
        int priority);

    /// <summary>
    /// Remove the temporary mod with the given tag and priority from the temporary mods applying to all collections, if it exists.
    /// </summary>
    /// <param name="tag">The tag to look for.</param>
    /// <param name="priority">The initially provided priority.</param>
    /// <returns>NothingDone or Success.</returns>
    public PenumbraApiEc RemoveTemporaryModAll(string tag, int priority);

    /// <summary>
    /// Remove the temporary mod with the given tag and priority from the temporary mods applying to a specific collection, if it exists.
    /// </summary>
    /// <param name="tag">The tag to look for.</param>
    /// <param name="collectionName">Name of the collection the mod should apply to. Can be a temporary collection name.</param>
    /// <param name="priority">The initially provided priority.</param>
    /// <returns>CollectionMissing, NothingDone or Success.</returns>
    public PenumbraApiEc RemoveTemporaryMod(string tag, string collectionName, int priority);

    #endregion

    #region Resource Tree

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
    /// <param name="withUIData"> Whether to get names and icons along with the paths. </param>
    /// <returns> An array of resource information dictionaries, of the same length and in the same order as the given game object index array. </returns>
    /// <remarks>
    /// It is the caller's responsibility to make sure the returned resource handles are still in use on the game object's draw object before using them. <para />
    /// Also, callers should not use UI data for non-UI purposes.
    /// </remarks>
    public IReadOnlyDictionary<nint, ( string, string, ChangedItemIcon )>?[] GetGameObjectResourcesOfType(ResourceType type, bool withUIData,
        params ushort[] gameObjects);

    /// <summary>
    /// Get the player and player-owned game objects' resources of a given type, as dictionaries of resource handles to actual paths and, optionally, names and icons.
    /// </summary>
    /// <param name="type"> Type of the resources to get, for example <see cref="ResourceType.Mtrl"/> for materials. </param>
    /// <param name="withUIData"> Whether to get names and icons along with the paths. </param>
    /// <returns> A dictionary of game object indices to resource information dictionaries. </returns>
    /// <remarks>
    /// It is the caller's responsibility to make sure the returned resource handles are still in use on the game object's draw object before using them. <para />
    /// Also, callers should not use UI data for non-UI purposes.
    /// </remarks>
    public IReadOnlyDictionary<ushort, IReadOnlyDictionary<nint, ( string, string, ChangedItemIcon )>> GetPlayerResourcesOfType(
        ResourceType type, bool withUIData);

    /// <summary>
    /// Get the given game objects' resource tree.
    /// </summary>
    /// <param name="withUIData"> Whether to get names and icons along with the paths. </param>
    /// <param name="gameObjects"> The game object indices for which to get the resources. </param>
    /// <returns> An array of resource trees, of the same length and in the same order as the given game object index array. </returns>
    /// <remarks>
    /// It is the caller's responsibility to make sure the returned resource handles are still in use on the game object's draw object before using them. <para />
    /// Also, callers should not use UI data for non-UI purposes.
    /// </remarks>
    public Ipc.ResourceTree?[] GetGameObjectResourceTrees(bool withUIData, params ushort[] gameObjects);

    /// <summary>
    /// Get the player and player-owned game objects' resource trees.
    /// </summary>
    /// <param name="withUIData"> Whether to get names and icons along with the paths. </param>
    /// <returns> A dictionary of game object indices to resource trees. </returns>
    /// <remarks>
    /// It is the caller's responsibility to make sure the returned resource handles are still in use on the game object's draw object before using them. <para />
    /// Also, callers should not use UI data for non-UI purposes.
    /// </remarks>
    public IReadOnlyDictionary<ushort, Ipc.ResourceTree> GetPlayerResourceTrees(bool withUIData);

    #endregion
}
