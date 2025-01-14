using Penumbra.Api.Enums;

namespace Penumbra.Api.Api;

/// <summary> API methods pertaining to the management of temporary collections and mods. </summary>
public interface IPenumbraApiTemporary
{
    /// <summary> Temporarily set the settings of a mod in a collection to given values. </summary>
    /// <param name="collectionId"> The collection to manipulate. </param>
    /// <param name="modDirectory"> Specify the mod via its directory name. </param>
    /// <param name="modName"> Specify the mod via its (non-unique) display name. </param>
    /// <param name="inherit"> Whether the mod should be forced to inherit from parent collections (if this is true, the other settings do not matter). </param>
    /// <param name="enabled"> Whether the mod should be enabled or disabled. </param>
    /// <param name="priority"> The desired priority for the mod. </param>
    /// <param name="options"> The new settings for the mod, as a map of Group Name -> All enabled Options (should be only one for single select groups).</param>
    /// <param name="source"> A string to describe the source of those temporary settings. This is displayed to the user. </param>
    /// <param name="key"> An optional lock to prevent other plugins and the user from changing these settings. Changes in mod structure will still remove the settings. Use 0 for no lock, or negative numbers for an identification lock that does not prevent the user from editing the temporary settings, but allows you to use <seealso cref="RemoveAllTemporaryModSettings"/> with the same key to only remove your settings. </param>
    /// <returns> Success, CollectionMissing if the collection does not exist, TemporarySettingImpossible if the collection can not have settings, ModMissing if the mod can not be identified, TemporarySettingDisallowed if there is already a temporary setting with a different key, OptionGroupMissing if a group can not be found, OptionMissing if an option can not be found. </returns>
    /// <remarks> If not all groups are set in <paramref name="options"/>, they will be set to their default settings. </remarks>
    public PenumbraApiEc SetTemporaryModSettings(Guid collectionId, string modDirectory, string modName, bool inherit, bool enabled, int priority,
        IReadOnlyDictionary<string, IReadOnlyList<string>> options, string source, int key);

    /// <summary> Temporarily set the settings of a mod in a collection to given values. </summary>
    /// <param name="objectIndex"> The game object index of the object whose collection you want to change. </param>
    /// <param name="modDirectory"> Specify the mod via its directory name. </param>
    /// <param name="modName"> Specify the mod via its (non-unique) display name. </param>
    /// <param name="inherit"> Whether the mod should be forced to inherit from parent collections (if this is true, the other settings do not matter). </param>
    /// <param name="enabled"> Whether the mod should be enabled or disabled. </param>
    /// <param name="priority"> The desired priority for the mod. </param>
    /// <param name="options"> The new settings for the mod, as a map of Group Name -> All enabled Options (should be only one for single select groups).</param>
    /// <param name="source"> A string to describe the source of those temporary settings. This is displayed to the user. </param>
    /// <param name="key"> An optional lock to prevent other plugins and the user from changing these settings. Changes in mod structure will still remove the settings. Use 0 for no lock. </param>
    /// <returns> Success, InvalidArgument if the game object does not exist, TemporarySettingImpossible if the collection can not have settings, ModMissing if the mod can not be identified, TemporarySettingDisallowed if there is already a temporary setting with a different key, OptionGroupMissing if a group can not be found, OptionMissing if an option can not be found. </returns>
    /// <remarks> If not all groups are set in <paramref name="options"/>, they will be set to their default settings. </remarks>
    public PenumbraApiEc SetTemporaryModSettingsPlayer(int objectIndex, string modDirectory, string modName, bool inherit, bool enabled, int priority,
        IReadOnlyDictionary<string, IReadOnlyList<string>> options, string source, int key);

    /// <summary> Temporarily set the settings of a mod in a collection to given values. </summary>
    /// <param name="collectionId"> The collection to manipulate. </param>
    /// <param name="modDirectory"> Specify the mod via its directory name. </param>
    /// <param name="modName"> Specify the mod via its (non-unique) display name. </param>
    /// <param name="key"> An optional key to a potential lock applied to those settings. </param>
    /// <returns> Success, NothingDone if no temporary settings could be removed with this key, CollectionMissing if the collection does not exist, TemporarySettingDisallowed if the key did not correspond to the lock. </returns>
    public PenumbraApiEc RemoveTemporaryModSettings(Guid collectionId, string modDirectory, string modName, int key);

    /// <summary> Temporarily set the settings of a mod in a collection to given values. </summary>
    /// <param name="objectIndex"> The game object index of the object whose collection you want to change. </param>
    /// <param name="modDirectory"> Specify the mod via its directory name. </param>
    /// <param name="modName"> Specify the mod via its (non-unique) display name. </param>
    /// <param name="key"> An optional key to a potential lock applied to those settings. </param>
    /// <returns> Success, NothingDone if the mod did not have temporary settings in this collection, InvalidArgument if the game object does not exist, TemporarySettingDisallowed if the key did not correspond to the lock. </returns>
    public PenumbraApiEc RemoveTemporaryModSettingsPlayer(int objectIndex, string modDirectory, string modName, int key);

    /// <summary> Temporarily set the settings of a mod in a collection to given values. </summary>
    /// <param name="collectionId"> The collection to manipulate. </param>
    /// <param name="key"> An optional key to a lock applied to those settings. All settings that use this key will be removed, all others ignored. </param>
    /// <returns> Success, NothingDone if no temporary settings could be removed with this key, CollectionMissing if the collection does not exist. </returns>
    public PenumbraApiEc RemoveAllTemporaryModSettings(Guid collectionId, int key);

    /// <summary> Temporarily set the settings of a mod in a collection to given values. </summary>
    /// <param name="objectIndex"> The game object index of the object whose collection you want to change. </param>
    /// <param name="key"> An optional key to a lock applied to those settings. All settings that can be removed with this key will be removed, all others ignored. </param>
    /// <returns> Success, NothingDone if no temporary settings could be removed with this key, InvalidArgument if the game object does not exist. </returns>
    public PenumbraApiEc RemoveAllTemporaryModSettingsPlayer(int objectIndex, int key);

    /// <summary> Create a temporary collection. </summary>
    /// <param name="name"> The name for the collection. Arbitrary and only used internally for debugging. </param>
    /// <returns> The GUID of the created temporary collection. </returns>
    public Guid CreateTemporaryCollection(string name);

    /// <summary> Remove the temporary collection of the given name. </summary>
    /// <param name="collectionId"> The chosen temporary collection to remove. </param>
    /// <returns> NothingChanged or Success. </returns>
    public PenumbraApiEc DeleteTemporaryCollection(Guid collectionId);

    /// <summary>
    /// Assign an existing temporary collection to an actor that currently occupies a specific slot.
    /// </summary>
    /// <param name="collectionId">The chosen collection assigned to the actor.</param>
    /// <param name="actorIndex">The current object table index of the actor.</param>
    /// <param name="forceAssignment">Whether to assign even if the actor is already assigned either a temporary or a permanent collection.</param>
    /// <returns>Success, InvalidArgument if the actor can not be identified, CollectionMissing if the collection does not exist, CharacterCollectionExists if <paramref name="forceAssignment"/> is false and the actor is already assigned a collection, and AssignmentDeletionFailed if <paramref name="forceAssignment"/> is true and the existing temporary assignment could not be deleted. </returns>
    public PenumbraApiEc AssignTemporaryCollection(Guid collectionId, int actorIndex, bool forceAssignment);

    /// <summary>
    /// Set a temporary mod with the given paths, manipulations and priority and the name tag to all regular and temporary collections.
    /// </summary>
    /// <param name="tag">Custom name for the temporary mod.</param>
    /// <param name="paths">List of redirections (can be swaps or redirections).</param>
    /// <param name="manipString">Zipped Base64 string of meta manipulations.</param>
    /// <param name="priority">Desired priority.</param>
    /// <returns>InvalidGamePath, InvalidManipulation or Success.</returns>
    public PenumbraApiEc AddTemporaryModAll(string tag, Dictionary<string, string> paths, string manipString, int priority);

    /// <summary> Set a temporary mod with the given paths, manipulations and priority and the name tag to a specific collection.
    /// </summary>
    /// <param name="tag">Custom name for the temporary mod.</param>
    /// <param name="collectionId">GUID of the collection the mod should apply to. Can be a temporary collection.</param>
    /// <param name="paths">List of redirections (can be swaps or redirections).</param>
    /// <param name="manipString">Zipped Base64 string of meta manipulations.</param>
    /// <param name="priority">Desired priority.</param>
    /// <returns>CollectionMissing, InvalidGamePath, InvalidManipulation, InvalidArgument (GUID is nil) or Success.</returns>
    public PenumbraApiEc AddTemporaryMod(string tag, Guid collectionId, Dictionary<string, string> paths, string manipString,
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
    /// <param name="collectionId">GUID of the collection the mod should apply to. Can be a temporary collection.</param>
    /// <param name="priority">The initially provided priority.</param>
    /// <returns>CollectionMissing, NothingDone or Success.</returns>
    public PenumbraApiEc RemoveTemporaryMod(string tag, Guid collectionId, int priority);

    /// <summary> Get the current temporary settings of a mod in the given collection. </summary>
    /// <param name="collectionId"> The collection to query. </param>
    /// <param name="modDirectory"> Specify the mod via its directory name. </param>
    /// <param name="modName"> Specify the mod via its (non-unique) display name. </param>
    /// <param name="key"> The key for the settings lock.</param>
    /// <returns>
    /// The settings as (ForceInherit, Enabled, Priority, Settings) or null if none are registered,
    /// the registered source for the temporary settings or empty,
    /// and Success, CollectionMissing, ModMissing or TemporarySettingDisallowed if the used key was > 0 and different from the provided key.
    /// </returns>
    public (PenumbraApiEc ErrorCode, (bool, bool, int, Dictionary<string, List<string>>)?, string) QueryTemporaryModSettings(Guid collectionId, string modDirectory,
        string modName, int key);

    /// <summary> Get the current temporary settings of a mod in the collection assigned to a given game object. </summary>
    /// <param name="objectIndex"> The game object index of the object whose collection you want to change. </param>
    /// <param name="modDirectory"> Specify the mod via its directory name. </param>
    /// <param name="modName"> Specify the mod via its (non-unique) display name. </param>
    /// <param name="key"> The key for the settings lock.</param>
    /// <returns>
    /// The settings as (ForceInherit, Enabled, Priority, Settings) or null if none are registered,
    /// the registered source for the temporary settings or empty,
    /// and Success, InvalidArgument if the game object does not exist, ModMissing, or TemporarySettingDisallowed if the used key was > 0 and different from the provided key.
    /// </returns>
    public (PenumbraApiEc ErrorCode, (bool, bool, int, Dictionary<string, List<string>>)? Settings, string Source) QueryTemporaryModSettingsPlayer(int objectIndex, string modDirectory, string modName, int key);
}
