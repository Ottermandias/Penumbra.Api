using Penumbra.Api.Helpers;

namespace Penumbra.Api.Enums;

/// <summary> The available properties for the mod adapter and wrapper. </summary>
public enum ModProperty
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
