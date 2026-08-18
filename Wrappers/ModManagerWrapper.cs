using Dalamud.Plugin.Ipc;
using Luna;

namespace Penumbra.Api.Wrappers;

public sealed class ModManagerWrapper
    : BasicWrapper<ModManagerWrapper, ModManagerWrapper.Method>, IBasicWrapper<ModManagerWrapper>
{
    public int Count
        => Invoke<int>(Method.Count);

    public ModWrapper? GetByIndex(int modIndex)
        => ModWrapper.Create(Invoke<int, IIdDataShareAdapter>(Method.GetByIndex, modIndex));

    public ModWrapper? GetByName(ModIdentifier mod)
        => ModWrapper.Create(Invoke<ModIdentifier, IIdDataShareAdapter>(Method.GetByName, mod));

    public IEnumerable<ModIdentifier> EnumerateNames()
        => Invoke<IEnumerable<ModIdentifier>>(Method.EnumerateNames) ?? [];

    public enum Method
    {
        GetByIndex,
        GetByName,
        EnumerateNames,
        Count,
    }

    private ModManagerWrapper(IIdDataShareAdapter adapter)
        : base(adapter)
    { }

    public static ModManagerWrapper? Create(IIdDataShareAdapter? adapter)
        => adapter is null ? null : new ModManagerWrapper(adapter);
}
