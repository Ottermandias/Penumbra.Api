using System.Linq;
using Dalamud.Plugin.Ipc;
using Luna;
using Penumbra.Api.Enums;

namespace Penumbra.Api.Wrappers;

public sealed class CollectionManagerWrapper : BasicWrapper<CollectionManagerWrapper, CollectionManagerWrapper.Method>,
    IBasicWrapper<CollectionManagerWrapper>
{
    public CollectionWrapper? GetByIndex(int index)
        => CollectionWrapper.Create(Invoke<int, IIdDataShareAdapter>(Method.GetByIndex, index));

    public CollectionWrapper? GetById(Guid id)
        => CollectionWrapper.Create(Invoke<Guid, IIdDataShareAdapter>(Method.GetById, id));

    public CollectionWrapper? GetByName(string name)
        => CollectionWrapper.Create(Invoke<string, IIdDataShareAdapter>(Method.GetByName, name));

    public CollectionWrapper? GetByIdentifier(string identifier)
        => CollectionWrapper.Create(Invoke<string, IIdDataShareAdapter>(Method.GetByIdentifier, identifier));

    public int Count
        => Invoke<int>(Method.Count);

    public IEnumerable<CollectionWrapper> Enumerate()
        => Invoke<IEnumerable<IIdDataShareAdapter>>(Method.GetEnumerable)?.Select(CollectionWrapper.Create).OfType<CollectionWrapper>() ?? [];

    public IEnumerable<(Guid Identifier, string Name, int Index)> GetNames()
        => Invoke<IEnumerable<(Guid Identifier, string Name, int Index)>>(Method.GetNames) ?? [];

    public CollectionWrapper? Current
        => CollectionWrapper.Create(Invoke<IIdDataShareAdapter>(Method.GetCurrent));

    public CollectionWrapper? Default
        => CollectionWrapper.Create(Invoke<IIdDataShareAdapter>(Method.GetDefault));

    public CollectionWrapper? Interface
        => CollectionWrapper.Create(Invoke<IIdDataShareAdapter>(Method.GetInterface));

    public CollectionWrapper? ByType(ApiCollectionType type)
        => CollectionWrapper.Create(Invoke<int, IIdDataShareAdapter>(Method.GetForType, (int)type));

    public enum Method
    {
        GetById,
        GetByName,
        GetByIdentifier,
        GetEnumerable,
        Count,
        GetCurrent,
        GetDefault,
        GetInterface,
        TryGetForObject,
        GetForType,
        GetByIndex,
        GetNames,
    }

    /// <inheritdoc/>
    public static CollectionManagerWrapper? Create(IIdDataShareAdapter? adapter)
        => adapter is null ? null : new CollectionManagerWrapper(adapter);

    private CollectionManagerWrapper(IIdDataShareAdapter adapter)
        : base(adapter)
    { }
}
