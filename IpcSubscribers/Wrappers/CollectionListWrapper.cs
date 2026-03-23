using Dalamud.Plugin.Ipc;
using Penumbra.Api.Enums;

namespace Penumbra.Api.IpcSubscribers;

public sealed class CollectionListWrapper(IIdDataShareAdapter collectionShare) : IDisposable
{
    public enum Method
    {
        GetById,
        GetByName,
        GetByIdentifier,
        GetEnumerator,
        Count,
        GetCurrent,
        GetDefault,
        GetInterface,
        TryGetForObject,
        GetForType,
    }

    public string? GetNameById(Guid id)
        => collectionShare.TryInvoke<Guid, string>((int)0, id, out var name) ? name : null;

    public CollectionWrapper Current
        => collectionShare.TryInvoke((int)Method.GetCurrent, out IIdDataShareAdapter? coll)
            ? new CollectionWrapper(coll)
            : new CollectionWrapper(null);

    public CollectionWrapper Default
        => collectionShare.TryInvoke((int)Method.GetDefault, out IIdDataShareAdapter? coll)
            ? new CollectionWrapper(coll)
            : new CollectionWrapper(null);

    public CollectionWrapper Interface
        => collectionShare.TryInvoke((int)Method.GetInterface, out IIdDataShareAdapter? coll)
            ? new CollectionWrapper(coll)
            : new CollectionWrapper(null);

    public PenumbraApiEc TryGetForObject(int objectIndex, out CollectionWrapper wrapper)
    {
        if (!collectionShare.TryInvoke((int)Method.TryGetForObject, objectIndex, out (int, IIdDataShareAdapter?) coll))
        {
            wrapper = new CollectionWrapper(null);
            return PenumbraApiEc.UnknownError;
        }

        wrapper = new CollectionWrapper(coll.Item2);
        return (PenumbraApiEc)coll.Item1;
    }

    public CollectionWrapper GetForType(ApiCollectionType type)
        => collectionShare.TryInvoke((int)Method.GetForType, (byte)type, out IIdDataShareAdapter? coll)
            ? new CollectionWrapper(coll)
            : new CollectionWrapper(null);


    public void Dispose()
        => collectionShare.Dispose();
}
