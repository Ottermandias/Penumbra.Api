using Penumbra.Api.Enums;

namespace Penumbra.Api.Api;

public partial interface IPenumbraApi
{
    /// <returns>A list of the IDs of all currently installed collections.</returns>
    public IList<Guid> GetCollections();

    /// <returns>The GUID of the currently selected collection.</returns>
    public Guid GetCurrentCollection();

    /// <returns>The GUID of the default collection.</returns>
    public Guid GetDefaultCollection();

    /// <returns>The GUID of the interface collection.</returns>
    public Guid GetInterfaceCollection();

    /// <returns>The GUID of the collection associated with <paramref name="characterName"/> and whether it exists as character collection.</returns>
    public (Guid, bool) GetCharacterCollection(string characterName);

    /// <returns>A dictionary of affected items in <paramref name="collectionId"/> via GUID and known objects or null.</returns>
    public IReadOnlyDictionary<string, object?> GetChangedItemsForCollection(Guid collectionId);

    /// <returns>The GUID of the collection assigned to the given <paramref name="type"/> or an empty string if none is assigned or type is invalid.</returns>
    public Guid GetCollectionForType(ApiCollectionType type);

    /// <summary>
    /// Set a collection by GUID for a specific type.
    /// </summary>
    /// <param name="type">The collection type to set.</param>
    /// <param name="collectionId">The GUID of the collection to set it to.</param>
    /// <param name="allowCreateNew">Allow only setting existing types or also creating an unset type.</param>
    /// <param name="allowDelete">Allow deleting existing collections if <paramref name="collectionId"/> is empty.</param>
    /// <returns>InvalidArgument if type is invalid,
    /// NothingChanged if the new collection is the same as the old,<br />
    /// AssignmentDeletionDisallowed if <paramref name="collectionId"/> is empty and <paramref name="allowDelete"/> is false, and the assignment exists,<br />
    /// or if Default, Current or Interface would be deleted.<br />
    /// CollectionMissing if the new collection can not be found,<br />
    /// AssignmentCreationDisallowed if <paramref name="allowCreateNew"/> is false and the assignment does not exist,<br />
    /// or Success, as well as the GUID of the previous collection (empty if no assignment existed).
    /// </returns>
    public (PenumbraApiEc, Guid OldCollection) SetCollectionForType(ApiCollectionType type, Guid collectionId, bool allowCreateNew,
        bool allowDelete);

    /// <returns>Return whether the object at <paramref name="gameObjectIdx" /> produces a valid identifier, if the identifier has a collection assigned, and the collection that affects the object.</returns>
    public (bool ObjectValid, bool IndividualSet, Guid EffectiveCollection) GetCollectionForObject(int gameObjectIdx);

    /// <summary>
    /// Set a collection by GUID for a specific game object.
    /// </summary>
    /// <param name="gameObjectIdx">The index of the desired game object in the object table.</param>
    /// <param name="collectionId">The GUID of the collection to set it to.</param>
    /// <param name="allowCreateNew">Allow only setting existing individuals or also creating a new individual assignment.</param>
    /// <param name="allowDelete">Allow deleting existing individual assignments if <paramref name="collectionId"/> is empty.</param>
    /// <returns>InvalidIdentifier if <paramref name="gameObjectIdx"/> does not produce an existing game object or the object is not identifiable,
    /// NothingChanged if the new collection is the same as the old,<br />
    /// AssignmentDeletionDisallowed if <paramref name="collectionId"/> is empty and <paramref name="allowDelete"/> is false, and the assignment exists,<br />
    /// CollectionMissing if the new collection can not be found,<br />
    /// AssignmentCreationDisallowed if <paramref name="allowCreateNew"/> is false and the assignment does not exist,<br />
    /// or Success, as well as the name of the previous collection (empty if no assignment existed).</returns>
    public (PenumbraApiEc, Guid OldCollection) SetCollectionForObject(int gameObjectIdx, Guid collectionId, bool allowCreateNew,
        bool allowDelete);
}
