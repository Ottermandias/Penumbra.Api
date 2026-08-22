using System.IO;
using Dalamud.Plugin;
using Dalamud.Plugin.Ipc;
using Luna;
using Penumbra.Api.IpcSubscribers;

namespace Penumbra.Api.Wrappers;

/// <summary> A wrapper for the mod manager. </summary>
/// <remarks> This is persistent and can generally be kept for the lifetime of either your plugin or Penumbra itself. </remarks>
public sealed class ModManagerWrapper
    : BasicWrapper<ModManagerWrapper, ModManagerWrapper.Method>, IBasicWrapper<ModManagerWrapper>
{
    /// <summary> Request the corresponding adapter from Penumbra and create a wrapper. </summary>
    /// <param name="pluginInterface"> The plugin interface. </param>
    /// <returns> A mod manager wrapper. </returns>
    public static ModManagerWrapper Request(IDalamudPluginInterface pluginInterface)
        => new GetModManagerAdapter(pluginInterface).Invoke();

    /// <summary> Get the number of installed mods. </summary>
    public int Count
        => Invoke<int>(Method.Count);

    /// <summary> Get the current mod root directory, if any is set. </summary>
    public DirectoryInfo? ModDirectory
        => Invoke<DirectoryInfo>(Method.ModDirectory);

    /// <summary> Get a reference to a mod by its index. </summary>
    /// <remarks> This mod reference should not be kept alive long-term. Use with using. </remarks>
    public ModWrapper? GetByIndex(int modIndex)
        => BasicWrapper.Create<ModWrapper>(Invoke<int, IIdDataShareAdapter>(Method.GetByIndex, modIndex));

    /// <summary> Get a mod by its name or identifier. </summary>
    /// <remarks> This mod reference should not be kept alive long-term. Use with using. </remarks>
    public ModWrapper? GetByName(ModIdentifier mod)
        => BasicWrapper.Create<ModWrapper>(Invoke<ModIdentifier, IIdDataShareAdapter>(Method.GetByName, mod));

    /// <summary> Enumerate all available mods as their identifiers without creating wrapper objects for them. </summary>
    public IEnumerable<ModIdentifier> EnumerateNames()
        => Invoke<IEnumerable<ModIdentifier>>(Method.EnumerateNames) ?? [];

    /// <summary> The methods available for a mod manager adapter. </summary>
    public enum Method
    {
        /// <inheritdoc cref="Version"/>
        Version = BasicWrapper.VersionMethod,

        /// <inheritdoc cref="ModManagerWrapper.GetByIndex"/>
        GetByIndex,

        /// <inheritdoc cref="ModManagerWrapper.GetByName"/>
        GetByName,

        /// <inheritdoc cref="ModManagerWrapper.EnumerateNames"/>
        EnumerateNames,

        /// <inheritdoc cref="ModManagerWrapper.Count"/>
        Count,

        /// <inheritdoc cref="ModManagerWrapper.ModDirectory"/>
        ModDirectory,
    }

    private ModManagerWrapper(IIdDataShareAdapter adapter)
        : base(adapter)
    { }

    /// <inheritdoc/>
    static ModManagerWrapper? IBasicWrapper<ModManagerWrapper>.CreateWrapper(IIdDataShareAdapter? adapter)
        => adapter is null ? null : new ModManagerWrapper(adapter);
}
