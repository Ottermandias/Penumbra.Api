using Dalamud.Plugin.Ipc;
using Penumbra.Api.Enums;

namespace Penumbra.Api.IpcSubscribers;

public readonly ref struct CollectionWrapper(IIdDataShareAdapter? collection) : IDisposable
{
    public enum Method
    {
        GetId,
        GetName,
        GetAnonymousName,
        GetChangedItems,
        HasCache,
        GetActualSettingsByIndex,
        GetActualSettingsByName,
        GetTemporarySettingsByIndex,
        GetTemporarySettingsByName,
        GetOwnSettingsByIndex,
        GetOwnSettingsByName,
    }

    public bool Valid
        => collection is not null;

    public Guid Id
        => collection!.TryInvoke((int)Method.GetId, out Guid id) ? id : throw new ArgumentNullException(nameof(collection));

    public string Name
        => collection!.TryInvoke((int)Method.GetName, out string? name) ? name! : throw new ArgumentNullException(nameof(collection));

    public string AnonymousName
        => collection!.TryInvoke((int)Method.GetAnonymousName, out string? name) ? name! : throw new ArgumentNullException(nameof(collection));

    public void Dispose()
        => collection?.Dispose();
}
