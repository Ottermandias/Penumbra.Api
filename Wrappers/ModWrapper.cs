using Dalamud.Plugin.Ipc;
using Luna;

namespace Penumbra.Api.Wrappers;

/// <summary> A wrapper for a single mod. </summary>
/// <remarks> These should generally only be used for the frame they're created in. </remarks>
public sealed class ModWrapper : BasicWrapper<ModWrapper, ModWrapper.Method>, IBasicWrapper<ModWrapper>
{
    /// <summary> The full path of the mod directory. </summary>
    public string ModPath
        => Invoke<string>(Method.ModPath)!;

    /// <summary> The internal index of the mod. </summary>
    public int Index
        => Invoke<int>(Method.Index);

    /// <summary> The display name of the mod. </summary>
    public string Name
        => Invoke<string>(Method.Name) ?? string.Empty;

    /// <summary> The unique identifier (directory name) of the mod. </summary>
    public string Identifier
        => Invoke<string>(Method.Identifier)!;

    /// <summary> The author of the mod. </summary>
    public string Author
        => Invoke<string>(Method.Author) ?? string.Empty;

    /// <summary> The description of the mod. </summary>
    public string Description
        => Invoke<string>(Method.Description) ?? string.Empty;

    /// <summary> The version of the mod. </summary>
    public string Version
        => Invoke<string>(Method.Version) ?? string.Empty;

    /// <summary> The website of the mod. </summary>
    public string Website
        => Invoke<string>(Method.Website) ?? string.Empty;

    /// <summary> The relative image path of the mod. </summary>
    public string Image
        => Invoke<string>(Method.Image) ?? string.Empty;

    /// <summary> The filesystem sort name of the mod if non-default, null otherwise. </summary>
    public string? SortName
        => Invoke<string>(Method.SortName);

    /// <summary> The filesystem folder containing this mod, or empty if it is directly in the root. </summary>
    public string Folder
        => Invoke<string>(Method.Folder) ?? string.Empty;

    /// <summary> The full filesystem path as currently in effect, including duplicate modifiers and resolved sort name. </summary>
    public string FullPath
        => Invoke<string>(Method.FullPath) ?? string.Empty;

    /// <summary> The import date of the mod. </summary>
    public DateTimeOffset ImportDate
        => Invoke<DateTimeOffset>(Method.ImportDate)!;

    /// <summary> The last time the mod's configuration was edited in any collection. </summary>
    public DateTimeOffset LastConfigEdit
        => Invoke<DateTimeOffset>(Method.LastConfigEdit)!;

    /// <summary> Whether the mod is a favorite or not. </summary>
    public bool Favorite
        => Invoke<bool>(Method.Favorite)!;

    /// <summary> The tags the mod creator has set for this mod. </summary>
    public IReadOnlyList<string> ModTags
        => Invoke<IReadOnlyList<string>>(Method.ModTags)!;

    /// <summary> The tags the user has set for this mod. </summary>
    public IReadOnlyList<string> LocalTags
        => Invoke<IReadOnlyList<string>>(Method.LocalTags)!;

    /// <summary> The mask of required features for this mod. </summary>
    public ulong RequiredFeatures
        => Invoke<ulong>(Method.RequiredFeatures);

    /// <summary> The available properties for the mod adapter and wrapper. </summary>
    public enum Method
    {
        /// <inheritdoc cref="ModWrapper.ModPath"/>
        ModPath = 0,

        /// <inheritdoc cref="ModWrapper.Index"/>
        Index = 1,

        /// <inheritdoc cref="ModWrapper.Name"/>
        Name = 2,

        /// <inheritdoc cref="ModWrapper.Identifier"/>
        Identifier = 3,

        /// <inheritdoc cref="ModWrapper.Author"/>
        Author = 4,

        /// <inheritdoc cref="ModWrapper.Description"/>
        Description = 5,

        /// <inheritdoc cref="ModWrapper.Version"/>
        Version = 6,

        /// <inheritdoc cref="ModWrapper.Website"/>
        Website = 7,

        /// <inheritdoc cref="ModWrapper.Image"/>
        Image = 8,

        /// <inheritdoc cref="ModWrapper.SortName"/>
        SortName = 9,

        /// <inheritdoc cref="ModWrapper.Folder"/>
        Folder = 10,

        /// <inheritdoc cref="ModWrapper.FullPath"/>
        FullPath = 11,

        /// <inheritdoc cref="ModWrapper.ImportDate"/>
        ImportDate = 12,

        /// <inheritdoc cref="ModWrapper.LastConfigEdit"/>
        LastConfigEdit = 13,

        /// <inheritdoc cref="ModWrapper.Favorite"/>
        Favorite = 14,

        /// <inheritdoc cref="ModWrapper.ModTags"/>
        ModTags = 15,

        /// <inheritdoc cref="ModWrapper.LocalTags"/>
        LocalTags = 16,

        /// <inheritdoc cref="ModWrapper.RequiredFeatures"/>
        RequiredFeatures = 17,
    }

    private ModWrapper(IIdDataShareAdapter adapter)
        : base(adapter)
    { }

    /// <inheritdoc/>
    public static ModWrapper? Create(IIdDataShareAdapter? adapter)
        => adapter is null ? null : new ModWrapper(adapter);
}
