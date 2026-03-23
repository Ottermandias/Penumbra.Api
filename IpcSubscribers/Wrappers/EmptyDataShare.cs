using Dalamud.Plugin.Ipc;

namespace Penumbra.Api.IpcSubscribers;

internal sealed class EmptyDataShare : IIdDataShareAdapter
{
    public static readonly EmptyDataShare Instance = new();

    private EmptyDataShare()
    { }

    public void Dispose()
    { }
}
