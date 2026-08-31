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
    public string ModVersion
        => Invoke<string>(Method.ModVersion) ?? string.Empty;

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
        /// <inheritdoc cref="BasicWrapper{TSelf,TEnum}.Disposed"/>
        DisposedEvent = BasicWrapper.DisposedEventMethod,

        /// <inheritdoc cref="BasicWrapper{TSelf,TEnum}.Alive"/>
        Alive = BasicWrapper.AliveMethod,

        /// <inheritdoc cref="BasicWrapper{TSelf,TEnum}.Version"/>
        Version = BasicWrapper.VersionMethod,

        /// <inheritdoc cref="ModWrapper.ModPath"/>
        ModPath,

        /// <inheritdoc cref="ModWrapper.Index"/>
        Index,

        /// <inheritdoc cref="ModWrapper.Name"/>
        Name,

        /// <inheritdoc cref="ModWrapper.Identifier"/>
        Identifier,

        /// <inheritdoc cref="ModWrapper.Author"/>
        Author,

        /// <inheritdoc cref="ModWrapper.Description"/>
        Description,

        /// <inheritdoc cref="ModWrapper.ModVersion"/>
        ModVersion,

        /// <inheritdoc cref="ModWrapper.Website"/>
        Website,

        /// <inheritdoc cref="ModWrapper.Image"/>
        Image,

        /// <inheritdoc cref="ModWrapper.SortName"/>
        SortName,

        /// <inheritdoc cref="ModWrapper.Folder"/>
        Folder,

        /// <inheritdoc cref="ModWrapper.FullPath"/>
        FullPath,

        /// <inheritdoc cref="ModWrapper.ImportDate"/>
        ImportDate,

        /// <inheritdoc cref="ModWrapper.LastConfigEdit"/>
        LastConfigEdit,

        /// <inheritdoc cref="ModWrapper.Favorite"/>
        Favorite,

        /// <inheritdoc cref="ModWrapper.ModTags"/>
        ModTags,

        /// <inheritdoc cref="ModWrapper.LocalTags"/>
        LocalTags,

        /// <inheritdoc cref="ModWrapper.RequiredFeatures"/>
        RequiredFeatures,
    }

    private ModWrapper(IIdDataShareAdapter adapter)
        : base(adapter)
    { }

    /// <inheritdoc/>
    static ModWrapper? IBasicWrapper<ModWrapper>.CreateWrapper(IIdDataShareAdapter? adapter)
        => adapter is null ? null : new ModWrapper(adapter);

    /// <inheritdoc />
    protected override string IpcLabel
        => string.Empty;
}
