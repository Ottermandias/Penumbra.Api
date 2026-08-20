using Dalamud.Plugin.Ipc;
using Luna;

namespace Penumbra.Api.Wrappers;

/// <summary> A wrapper for the mod manager. </summary>
/// <remarks> This is persistent and can generally be kept for the lifetime of either your plugin or Penumbra itself. </remarks>
public sealed class ModManagerWrapper
    : BasicWrapper<ModManagerWrapper, ModManagerWrapper.Method>, IBasicWrapper<ModManagerWrapper>
{
    /// <summary> Get the number of installed mods. </summary>
    public int Count
        => Invoke<int>(Method.Count);

    /// <summary> Get a reference to a mod by its index. </summary>
    /// <remarks> This mod reference should not be kept alive long-term. Use with using. </remarks>
    public ModWrapper? GetByIndex(int modIndex)
        => ModWrapper.Create(Invoke<int, IIdDataShareAdapter>(Method.GetByIndex, modIndex));

    /// <summary> Get a mod by its name or identifier. </summary>
    /// <remarks> This mod reference should not be kept alive long-term. Use with using. </remarks>
    public ModWrapper? GetByName(ModIdentifier mod)
        => ModWrapper.Create(Invoke<ModIdentifier, IIdDataShareAdapter>(Method.GetByName, mod));

    /// <summary> Enumerate all available mods as their identifiers without creating wrapper objects for them. </summary>
    public IEnumerable<ModIdentifier> EnumerateNames()
        => Invoke<IEnumerable<ModIdentifier>>(Method.EnumerateNames) ?? [];

    /// <summary> The methods available for a mod manager adapter. </summary>
    public enum Method
    {
        /// <inheritdoc cref="ModManagerWrapper.GetByIndex"/>
        GetByIndex,

        /// <inheritdoc cref="ModManagerWrapper.GetByName"/>
        GetByName,

        /// <inheritdoc cref="ModManagerWrapper.EnumerateNames"/>
        EnumerateNames,

        /// <inheritdoc cref="ModManagerWrapper.Count"/>
        Count,
    }

    private ModManagerWrapper(IIdDataShareAdapter adapter)
        : base(adapter)
    { }

    /// <inheritdoc/>
    public static ModManagerWrapper? Create(IIdDataShareAdapter? adapter)
        => adapter is null ? null : new ModManagerWrapper(adapter);
}
