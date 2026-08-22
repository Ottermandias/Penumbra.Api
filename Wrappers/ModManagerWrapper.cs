using System.IO;
using Dalamud.Plugin.Ipc;
using Luna;
using Penumbra.Api.IpcSubscribers;

namespace Penumbra.Api.Wrappers;

/// <summary> A wrapper for the mod manager. </summary>
/// <remarks> This is persistent and can generally be kept for the lifetime of either your plugin or Penumbra itself. </remarks>
public sealed class ModManagerWrapper
    : BasicWrapper<ModManagerWrapper, ModManagerWrapper.Method>, IBasicWrapper<ModManagerWrapper>
{
    /// <summary> Get the number of installed mods. </summary>
    public int Count
        => Invoke<int>(Method.Count);

    /// <summary> Get the current mod root directory, if any is set. </summary>
    public DirectoryInfo? ModDirectory
        => Invoke<DirectoryInfo>(Method.ModDirectory);

    /// <summary> Get the current index of a mod by its name or identifier. </summary>
    /// <param name="mod"> The mod identifier. </param>
    /// <returns> The index of the mod or -1 if none matches. </returns>
    public int IndexByName(ModIdentifier mod)
        => Invoke<ModIdentifier, int>(Method.IndexByName, mod);

    /// <summary> Get the identifier of a mod by its current index. </summary>
    /// <param name="modIndex"> The mod index. </param>
    /// <returns> The identifier for the mod or a pair of empty strings if no mod for this index exists. </returns>
    public ModIdentifier NameByIndex(int modIndex)
        => Invoke<int, ModIdentifier>(Method.NameByIndex, modIndex);

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

    /// <summary> Query whether a specific mod contains a specific changed item. </summary>
    /// <param name="modIndex"> The mod to check. </param>
    /// <param name="changedItem"> The changed item to check. </param>
    /// <returns> True if the mod manipulates the specified item, false otherwise. </returns>
    public bool ContainsChangedItem(int modIndex, string changedItem)
        => Invoke<int, string, bool>(Method.ContainsChangedItem, modIndex, changedItem);

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

        /// <inheritdoc cref="ModManagerWrapper.IndexByName"/>
        IndexByName,

        /// <inheritdoc cref="ModManagerWrapper.NameByIndex"/>
        NameByIndex,

        /// <inheritdoc cref="ModManagerWrapper.ContainsChangedItem"/>
        ContainsChangedItem,
    }

    /// <summary> Create a new mod manager wrapper without a connection to an adapter. </summary>
    public ModManagerWrapper()
    { }

    private ModManagerWrapper(IIdDataShareAdapter adapter)
        : base(adapter)
    { }

    /// <inheritdoc/>
    static ModManagerWrapper? IBasicWrapper<ModManagerWrapper>.CreateWrapper(IIdDataShareAdapter? adapter)
        => adapter is null ? null : new ModManagerWrapper(adapter);

    /// <inheritdoc />
    protected override string IpcLabel
        => GetModManagerAdapter.Label;
}
