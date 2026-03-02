using System.IO;
using Penumbra.Api.Enums;

namespace Penumbra.Api.Helpers;

/// <summary> A wrapper for a single mod returned by the synchronized mod list. </summary>
/// <param name="mod"> The type-erased mod. </param>
/// <remarks>
///   If the Penumbra instance this was built for is disposed, or the mod itself is not alive anymore, this will throw on any query.
///   Prefer not to store ModWrappers and instead only use them during a single iteration. Disposing them is not necessary, but may be beneficial.
/// </remarks>
public readonly struct ModWrapper(IDisposable mod) : IDisposable
{
    /// <summary> Get the adapter as a dictionary of type-erased properties. </summary>
    private IReadOnlyList<object?> Adapter
        => (IReadOnlyList<object?>)mod;

    /// <summary> The full path of the mod directory. </summary>
    public DirectoryInfo ModPath
        => (DirectoryInfo)Adapter[(int)ModProperty.ModPath]!;

    /// <summary> The internal index of the mod. </summary>
    public int Index
        => (int)Adapter[(int)ModProperty.Index]!;

    /// <summary> The display name of the mod. </summary>
    public string Name
        => (string)Adapter[(int)ModProperty.Name]!;

    /// <summary> The unique identifier (directory name) of the mod. </summary>
    public string Identifier
        => (string)Adapter[(int)ModProperty.Identifier]!;

    /// <summary> The author of the mod. </summary>
    public string Author
        => (string)Adapter[(int)ModProperty.Author]!;

    /// <summary> The description of the mod. </summary>
    public string Description
        => (string)Adapter[(int)ModProperty.Description]!;

    /// <summary> The version of the mod. </summary>
    public string Version
        => (string)Adapter[(int)ModProperty.Version]!;

    /// <summary> The website of the mod. </summary>
    public string Website
        => (string)Adapter[(int)ModProperty.Website]!;

    /// <summary> The relative image path of the mod. </summary>
    public string Image
        => (string)Adapter[(int)ModProperty.Image]!;

    /// <summary> The filesystem sort name of the mod if non-default, null otherwise. </summary>
    public string? SortName
        => (string?)Adapter[(int)ModProperty.SortName];

    /// <summary> The filesystem folder containing this mod, or empty if it is directly in the root. </summary>
    public string Folder
        => (string)Adapter[(int)ModProperty.Folder]!;

    /// <summary> The full filesystem path as currently in effect, including duplicate modifiers and resolved sort name. </summary>
    public string FullPath
        => (string)Adapter[(int)ModProperty.FullPath]!;

    /// <summary> The import date of the mod. </summary>
    public DateTimeOffset ImportDate
        => (DateTimeOffset)Adapter[(int)ModProperty.ImportDate]!;

    /// <summary> The last time the mod's configuration was edited in any collection. </summary>
    public DateTimeOffset LastConfigEdit
        => (DateTimeOffset)Adapter[(int)ModProperty.LastConfigEdit]!;

    /// <summary> Whether the mod is a favorite or not. </summary>
    public bool Favorite
        => (bool)Adapter[(int)ModProperty.Favorite]!;

    /// <summary> The tags the mod creator has set for this mod. </summary>
    public IReadOnlyList<string> ModTags
        => (IReadOnlyList<string>)Adapter[(int)ModProperty.ModTags]!;

    /// <summary> The tags the user has set for this mod. </summary>
    public IReadOnlyList<string> LocalTags
        => (IReadOnlyList<string>)Adapter[(int)ModProperty.LocalTags]!;

    /// <summary> The mask of required features for this mod. </summary>
    public ulong RequiredFeatures
        => (ulong)Adapter[(int)ModProperty.RequiredFeatures]!;

    /// <inheritdoc />
    public void Dispose()
        => mod.Dispose();
}
