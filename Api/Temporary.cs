using Penumbra.Api.Enums;

namespace Penumbra.Api.Api;

public partial interface IPenumbraApi
{
    /// <summary>
    /// Create a temporary collection without actual settings but with a cache and assign it to a specific character by name only.
    /// </summary>
    /// <remarks>This function is outdated, prefer to use <see cref="CreateNamedTemporaryCollection"/> and <see cref="AssignTemporaryCollection"/>.</remarks>
    /// <param name="tag">A custom tag for your collections.</param>
    /// <param name="character">The full, case-sensitive character name this collection should apply to.</param>
    /// <param name="forceOverwriteCharacter">Whether to overwrite an existing character collection.</param>
    /// <returns>Success, CharacterCollectionExists or NothingChanged and the GUID of the new temporary collection on success.</returns>
    public (PenumbraApiEc, Guid) CreateTemporaryCollection(string tag, string character, bool forceOverwriteCharacter);

    /// <summary>
    /// Create a temporary collection of the given <paramref name="name"/>.
    /// </summary>
    /// <param name="name">The intended name. It may not be empty or contain symbols invalid in a path used by XIV.</param>
    /// <returns>Success, InvalidArgument if name is not valid for a collection, or TemporaryCollectionExists and the GUID of the created collection on success.</returns>
    public (PenumbraApiEc, Guid) CreateNamedTemporaryCollection(string name);

    /// <summary>
    /// Assign an existing temporary collection to an actor that currently occupies a specific slot.
    /// </summary>
    /// <param name="collectionId">The chosen collection assigned to the actor.</param>
    /// <param name="actorIndex">The current object table index of the actor.</param>
    /// <param name="forceAssignment">Whether to assign even if the actor is already assigned either a temporary or a permanent collection.</param>
    /// <returns>Success, InvalidArgument if the actor can not be identified, CollectionMissing if the collection does not exist, CharacterCollectionExists if <paramref name="forceAssignment"/> is false and the actor is already assigned a collection, and AssignmentDeletionFailed if <paramref name="forceAssignment"/> is true and the existing temporary assignment could not be deleted. </returns>
    public PenumbraApiEc AssignTemporaryCollection(Guid collectionId, int actorIndex, bool forceAssignment);

    /// <summary>
    /// Remove the temporary collection assigned to characterName if it exists.
    /// </summary>
    /// <remarks>This function is outdated, prefer to use <see cref="RemoveTemporaryCollectionByName" />.</remarks>
    /// <returns>NothingChanged or Success.</returns>
    public PenumbraApiEc RemoveTemporaryCollection(string characterName);

    /// <summary>
    /// Remove the temporary collection of the given name.
    /// </summary>
    /// <param name="collectionId">The chosen temporary collection to remove.</param>
    /// <returns>NothingChanged or Success.</returns>
    public PenumbraApiEc RemoveTemporaryCollectionByName(Guid collectionId);

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
    /// <param name="collectionId">GUID of the collection the mod should apply to. Can be a temporary collection.</param>
    /// <param name="paths">List of redirections (can be swaps or redirections).</param>
    /// <param name="manipString">Zipped Base64 string of meta manipulations.</param>
    /// <param name="priority">Desired priority.</param>
    /// <returns>CollectionMissing, InvalidGamePath, InvalidManipulation or Success.</returns>
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
}
