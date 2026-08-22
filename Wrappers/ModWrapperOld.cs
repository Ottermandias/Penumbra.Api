using System.IO;

namespace Penumbra.Api.Wrappers;

/// <summary> A wrapper for a single mod returned by the synchronized mod list. </summary>
/// <param name="mod"> The type-erased mod. </param>
/// <remarks>
///   If the Penumbra instance this was built for is disposed, or the mod itself is not alive anymore, this will throw on any query.
///   Prefer not to store ModWrappers and instead only use them during a single iteration. Disposing them is not necessary, but may be beneficial.
/// </remarks>
public readonly struct ModWrapperOld(IDisposable mod) : IDisposable
{
    /// <summary> Get the adapter as a dictionary of type-erased properties. </summary>
    private IReadOnlyList<object?> Adapter
        => (IReadOnlyList<object?>)mod;

    /// <summary> The full path of the mod directory. </summary>
    public DirectoryInfo ModPath
        => (DirectoryInfo)Adapter[(int)ModWrapper.Method.ModPath]!;

    /// <summary> The internal index of the mod. </summary>
    public int Index
        => (int)Adapter[(int)ModWrapper.Method.Index]!;

    /// <summary> The display name of the mod. </summary>
    public string Name
        => (string)Adapter[(int)ModWrapper.Method.Name]!;

    /// <summary> The unique identifier (directory name) of the mod. </summary>
    public string Identifier
        => (string)Adapter[(int)ModWrapper.Method.Identifier]!;

    /// <summary> The author of the mod. </summary>
    public string Author
        => (string)Adapter[(int)ModWrapper.Method.Author]!;

    /// <summary> The description of the mod. </summary>
    public string Description
        => (string)Adapter[(int)ModWrapper.Method.Description]!;

    /// <summary> The version of the mod. </summary>
    public string Version
        => (string)Adapter[(int)ModWrapper.Method.ModVersion]!;

    /// <summary> The website of the mod. </summary>
    public string Website
        => (string)Adapter[(int)ModWrapper.Method.Website]!;

    /// <summary> The relative image path of the mod. </summary>
    public string Image
        => (string)Adapter[(int)ModWrapper.Method.Image]!;

    /// <summary> The filesystem sort name of the mod if non-default, null otherwise. </summary>
    public string? SortName
        => (string?)Adapter[(int)ModWrapper.Method.SortName];

    /// <summary> The filesystem folder containing this mod, or empty if it is directly in the root. </summary>
    public string Folder
        => (string)Adapter[(int)ModWrapper.Method.Folder]!;

    /// <summary> The full filesystem path as currently in effect, including duplicate modifiers and resolved sort name. </summary>
    public string FullPath
        => (string)Adapter[(int)ModWrapper.Method.FullPath]!;

    /// <summary> The import date of the mod. </summary>
    public DateTimeOffset ImportDate
        => (DateTimeOffset)Adapter[(int)ModWrapper.Method.ImportDate]!;

    /// <summary> The last time the mod's configuration was edited in any collection. </summary>
    public DateTimeOffset LastConfigEdit
        => (DateTimeOffset)Adapter[(int)ModWrapper.Method.LastConfigEdit]!;

    /// <summary> Whether the mod is a favorite or not. </summary>
    public bool Favorite
        => (bool)Adapter[(int)ModWrapper.Method.Favorite]!;

    /// <summary> The tags the mod creator has set for this mod. </summary>
    public IReadOnlyList<string> ModTags
        => (IReadOnlyList<string>)Adapter[(int)ModWrapper.Method.ModTags]!;

    /// <summary> The tags the user has set for this mod. </summary>
    public IReadOnlyList<string> LocalTags
        => (IReadOnlyList<string>)Adapter[(int)ModWrapper.Method.LocalTags]!;

    /// <summary> The mask of required features for this mod. </summary>
    public ulong RequiredFeatures
        => (ulong)Adapter[(int)ModWrapper.Method.RequiredFeatures]!;

    /// <inheritdoc />
    public void Dispose()
        => mod.Dispose();
}
