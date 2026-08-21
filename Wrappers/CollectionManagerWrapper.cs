using System.Linq;
using Dalamud.Plugin;
using Dalamud.Plugin.Ipc;
using Luna;
using Penumbra.Api.Enums;
using Penumbra.Api.IpcSubscribers;

namespace Penumbra.Api.Wrappers;

/// <summary> A wrapper for the collection manager. </summary>
/// <remarks> This is persistent and can generally be kept for the lifetime of either your plugin or Penumbra itself. </remarks>
public sealed class CollectionManagerWrapper : BasicWrapper<CollectionManagerWrapper, CollectionManagerWrapper.Method>,
    IBasicWrapper<CollectionManagerWrapper>
{
    /// <summary> Request the corresponding adapter from Penumbra and create a wrapper. </summary>
    /// <param name="pluginInterface"> The plugin interface. </param>
    /// <returns> A collection manager wrapper. </returns>
    public static CollectionManagerWrapper Request(IDalamudPluginInterface pluginInterface)
        => new GetCollectionManagerAdapter(pluginInterface).Invoke();

    /// <summary> Get a reference to a collection by its current internal index. </summary>
    /// <remarks> This collection reference should not be kept alive long-term. Use with using. </remarks>
    public CollectionWrapper? GetByIndex(int index)
        => BasicWrapper.Create<CollectionWrapper>(Invoke<int, IIdDataShareAdapter>(Method.GetByIndex, index));

    /// <summary> Get a reference to a collection by its persistent identifier. </summary>
    /// <remarks> This collection reference should not be kept alive long-term. Use with using. </remarks>
    public CollectionWrapper? GetById(Guid id)
        => BasicWrapper.Create<CollectionWrapper>(Invoke<Guid, IIdDataShareAdapter>(Method.GetById, id));

    /// <summary> Get a reference to a collection by its name. </summary>
    /// <remarks> This collection reference should not be kept alive long-term. Use with using. </remarks>
    public CollectionWrapper? GetByName(string name)
        => BasicWrapper.Create<CollectionWrapper>(Invoke<string, IIdDataShareAdapter>(Method.GetByName, name));

    /// <summary> Get a reference to a collection by its string identifier, which can be its name or a long enough part of its GUID. </summary>
    /// <remarks> This collection reference should not be kept alive long-term. Use with using. </remarks>
    public CollectionWrapper? GetByIdentifier(string identifier)
        => BasicWrapper.Create<CollectionWrapper>(Invoke<string, IIdDataShareAdapter>(Method.GetByIdentifier, identifier));

    /// <summary> Get the number of persistent collections available. </summary>
    public int Count
        => Invoke<int>(Method.Count);

    /// <summary> Enumerate all collections. </summary>
    /// <returns> An enumeration of collection references. </returns>
    /// <remarks> These collection references should not be kept alive long-term. Dispose after use. </remarks>
    public IEnumerable<CollectionWrapper> Enumerate()
        => Invoke<IEnumerable<IIdDataShareAdapter>>(Method.GetEnumerable)?.Select(BasicWrapper.Create<CollectionWrapper>).OfType<CollectionWrapper>()
         ?? [];

    /// <summary> Get the identifying information for all available collections without creating reference wrappers. </summary>
    /// <returns> An enumeration of the persistent identifiers, non-anonymized names and indices of the collections. </returns>
    public IEnumerable<(Guid Identifier, string Name, int Index)> GetNames()
        => Invoke<IEnumerable<(Guid Identifier, string Name, int Index)>>(Method.GetNames) ?? [];

    /// <summary> Get a reference to the currently selected collection. </summary>
    /// <remarks> This collection reference should not be kept alive long-term. Use with using. </remarks>
    public CollectionWrapper? Current
        => BasicWrapper.Create<CollectionWrapper>(Invoke<IIdDataShareAdapter>(Method.GetCurrent));

    /// <summary> Get a reference to the collection assigned as default collection. </summary>
    /// <remarks> This collection reference should not be kept alive long-term. Use with using. </remarks>
    public CollectionWrapper? Default
        => BasicWrapper.Create<CollectionWrapper>(Invoke<IIdDataShareAdapter>(Method.GetDefault));

    /// <summary> Get a reference to the collection assigned as interface collection. </summary>
    /// <remarks> This collection reference should not be kept alive long-term. Use with using. </remarks>
    public CollectionWrapper? Interface
        => BasicWrapper.Create<CollectionWrapper>(Invoke<IIdDataShareAdapter>(Method.GetInterface));

    /// <summary> Get a reference to a collection by its type. </summary>
    /// <param name="type"> The type of collection assignment to query. </param>
    /// <remarks> This collection reference should not be kept alive long-term. Use with using. </remarks>
    public CollectionWrapper? ByType(ApiCollectionType type)
        => BasicWrapper.Create<CollectionWrapper>(Invoke<int, IIdDataShareAdapter>(Method.GetForType, (int)type));

    /// <summary> Get a reference to the collection affecting an object. </summary>
    /// <param name="objectIndex"> The index of the object to query. </param>
    /// <param name="onlyIndividual"> Whether to only accept actual individual assignments for the object, or the actual collection affecting it. </param>
    /// <remarks> This collection reference should not be kept alive long-term. Use with using. </remarks>
    public CollectionWrapper? TryGetForObject(int objectIndex, bool onlyIndividual = false)
        => BasicWrapper.Create<CollectionWrapper>(Invoke<int, bool, IIdDataShareAdapter>(Method.TryGetForObject, objectIndex, onlyIndividual));

    /// <summary> Get only the ID of the collection currently affecting a game object by its index. </summary>
    /// <param name="objectIndex"> The index of the object to query. </param>
    /// <returns> The GUID of the collection affecting the object, which is the default-assigned collection if the object does not exist. </returns>
    public Guid ObjectCollectionId(int objectIndex)
        => Invoke<int, Guid>(Method.GetPlayerCollectionId, objectIndex);

    /// <summary> Get a reference to the collection currently affecting the player character. </summary>
    /// <remarks> This collection reference should not be kept alive long-term. Use with using. </remarks>
    public CollectionWrapper? PlayerCollection
        => BasicWrapper.Create<CollectionWrapper>(Invoke<IIdDataShareAdapter>(Method.GetPlayerCollection));

    /// <summary> Get only the ID of the collection currently affecting the player character, which is the default-assigned collection if the player does not exist. </summary>
    public Guid PlayerCollectionId
        => Invoke<Guid>(Method.GetPlayerCollectionId);

    /// <summary> The methods available for a collection manager adapter. </summary>
    public enum Method
    {
        /// <inheritdoc cref="CollectionManagerWrapper.GetById"/>
        GetById,

        /// <inheritdoc cref="CollectionManagerWrapper.GetByName"/>
        GetByName,

        /// <inheritdoc cref="CollectionManagerWrapper.GetByIdentifier"/>
        GetByIdentifier,

        /// <inheritdoc cref="CollectionManagerWrapper.Enumerate"/>
        GetEnumerable,

        /// <inheritdoc cref="CollectionManagerWrapper.Count"/>
        Count,

        /// <inheritdoc cref="CollectionManagerWrapper.Current"/>
        GetCurrent,

        /// <inheritdoc cref="CollectionManagerWrapper.Default"/>
        GetDefault,

        /// <inheritdoc cref="CollectionManagerWrapper.Interface"/>
        GetInterface,

        /// <inheritdoc cref="CollectionManagerWrapper.TryGetForObject"/>
        TryGetForObject,

        /// <inheritdoc cref="CollectionManagerWrapper.ByType"/>
        GetForType,

        /// <inheritdoc cref="CollectionManagerWrapper.GetByIndex"/>
        GetByIndex,

        /// <inheritdoc cref="CollectionManagerWrapper.GetNames"/>
        GetNames,

        /// <inheritdoc cref="CollectionManagerWrapper.ObjectCollectionId"/>
        GetObjectCollectionId,

        /// <inheritdoc cref="CollectionManagerWrapper.PlayerCollectionId"/>
        GetPlayerCollectionId,

        /// <inheritdoc cref="CollectionManagerWrapper.PlayerCollection"/>
        GetPlayerCollection,
    }

    /// <inheritdoc/>
    static CollectionManagerWrapper? IBasicWrapper<CollectionManagerWrapper>.CreateWrapper(IIdDataShareAdapter? adapter)
        => adapter is null ? null : new CollectionManagerWrapper(adapter);

    private CollectionManagerWrapper(IIdDataShareAdapter adapter)
        : base(adapter)
    { }
}
