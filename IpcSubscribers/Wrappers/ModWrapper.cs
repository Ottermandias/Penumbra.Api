using System.IO;
using Dalamud.Plugin.Ipc;

namespace Penumbra.Api.IpcSubscribers;

/// <summary> A wrapper for a single mod returned by the synchronized mod list. </summary>
/// <param name="mod"> The type-erased mod. </param>
/// <remarks>
///   If the Penumbra instance this was built for is disposed, or the mod itself is not alive anymore, this will throw on any query.
///   Prefer not to store ModWrappers and instead only use them during a single iteration. Disposing them is not necessary, but may be beneficial.
/// </remarks>
public readonly ref struct ModWrapper(IIdDataShareAdapter? mod) : IDisposable
{
    internal enum Method
    {
        GetModPath,
        GetIndex,
        GetName,
        GetIdentifier,
        GetAuthor,
        GetDescription,
        GetVersion,
        GetWebsite,
        GetImage,
        GetSortName,
        GetFolder,
        GetFullPath,
        GetImportDate,
        GetLastConfigEdit,
        GetFavorite,
        GetModTags,
        GetLocalTags,
        GetRequiredFeatures,
        GetGroupCount,
        GetGroup,
        GetChangedItems,
    }

    /// <summary> Get whether the mod wrapper is valid. </summary>
    public bool IsValid
        => mod is not null;

    /// <summary> The full path of the mod directory. </summary>
    public DirectoryInfo ModPath
        => mod!.TryInvoke((int)Method.GetModPath, out DirectoryInfo? ret) ? ret! : throw new ObjectDisposedException(nameof(mod));

    /// <summary> The internal index of the mod. </summary>
    public int Index
        => mod!.TryInvoke((int)Method.GetIndex, out int ret) ? ret : throw new ObjectDisposedException(nameof(mod));

    /// <summary> The display name of the mod. </summary>
    public string Name
        => mod!.TryInvoke((int)Method.GetName, out string? ret) ? ret! : throw new ObjectDisposedException(nameof(mod));

    /// <summary> The unique identifier (directory name) of the mod. </summary>
    public string Identifier
        => mod!.TryInvoke((int)Method.GetIdentifier, out string? ret) ? ret! : throw new ObjectDisposedException(nameof(mod));

    /// <summary> The author of the mod. </summary>
    public string Author
        => mod!.TryInvoke((int)Method.GetAuthor, out string? ret) ? ret! : throw new ObjectDisposedException(nameof(mod));

    /// <summary> The description of the mod. </summary>
    public string Description
        => mod!.TryInvoke((int)Method.GetDescription, out string? ret) ? ret! : throw new ObjectDisposedException(nameof(mod));

    /// <summary> The version of the mod. </summary>
    public string Version
        => mod!.TryInvoke((int)Method.GetVersion, out string? ret) ? ret! : throw new ObjectDisposedException(nameof(mod));

    /// <summary> The website of the mod. </summary>
    public string Website
        => mod!.TryInvoke((int)Method.GetWebsite, out string? ret) ? ret! : throw new ObjectDisposedException(nameof(mod));

    /// <summary> The relative image path of the mod. </summary>
    public string Image
        => mod!.TryInvoke((int)Method.GetImage, out string? ret) ? ret! : throw new ObjectDisposedException(nameof(mod));

    /// <summary> The filesystem sort name of the mod if non-default, null otherwise. </summary>
    public string? SortName
        => mod!.TryInvoke((int)Method.GetSortName, out string? ret) ? ret : throw new ObjectDisposedException(nameof(mod));

    /// <summary> The filesystem folder containing this mod, or empty if it is directly in the root. </summary>
    public string Folder
        => mod!.TryInvoke((int)Method.GetFolder, out string? ret) ? ret! : throw new ObjectDisposedException(nameof(mod));

    /// <summary> The full filesystem path as currently in effect, including duplicate modifiers and resolved sort name. </summary>
    public string FullPath
        => mod!.TryInvoke((int)Method.GetFullPath, out string? ret) ? ret! : throw new ObjectDisposedException(nameof(mod));

    /// <summary> The import date of the mod. </summary>
    public DateTimeOffset ImportDate
        => mod!.TryInvoke((int)Method.GetImportDate, out DateTimeOffset ret) ? ret : throw new ObjectDisposedException(nameof(mod));

    /// <summary> The last time the mod's configuration was edited in any collection. </summary>
    public DateTimeOffset LastConfigEdit
        => mod!.TryInvoke((int)Method.GetLastConfigEdit, out DateTimeOffset ret) ? ret : throw new ObjectDisposedException(nameof(mod));

    /// <summary> Whether the mod is a favorite or not. </summary>
    public bool Favorite
        => mod!.TryInvoke((int)Method.GetFavorite, out bool ret) ? ret : throw new ObjectDisposedException(nameof(mod));

    /// <summary> The tags the mod creator has set for this mod. </summary>
    public IReadOnlyList<string> ModTags
        => mod!.TryInvoke((int)Method.GetModTags, out IReadOnlyList<string>? ret) ? ret! : throw new ObjectDisposedException(nameof(mod));

    /// <summary> The tags the user has set for this mod. </summary>
    public IReadOnlyList<string> LocalTags
        => mod!.TryInvoke((int)Method.GetLocalTags, out IReadOnlyList<string>? ret) ? ret! : throw new ObjectDisposedException(nameof(mod));

    /// <summary> The mask of required features for this mod. </summary>
    public ulong RequiredFeatures
        => mod!.TryInvoke((int)Method.GetRequiredFeatures, out ulong ret) ? ret : throw new ObjectDisposedException(nameof(mod));

    /// <summary> Get the number of option groups in this mod. </summary>
    public int GroupCount
        => mod!.TryInvoke((int)Method.GetGroupCount, out int count) ? count : throw new ObjectDisposedException(nameof(mod));

    /// <summary> Get the specified group. </summary>
    /// <param name="index"> The index of the group. </param>
    /// <returns> The group. </returns>
    public GroupWrapper this[int index]
        => mod!.TryInvoke((int)Method.GetGroup, index, out IIdDataShareAdapter? group)
            ? new GroupWrapper(group)
            : throw new ObjectDisposedException(nameof(mod));

    /// <inheritdoc />
    public void Dispose()
        => mod?.Dispose();
}
