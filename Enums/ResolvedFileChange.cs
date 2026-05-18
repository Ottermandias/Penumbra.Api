namespace Penumbra.Api.Enums;

/// <summary>
/// Describes the way a resolved file change was effected in a collection.
/// </summary>
public enum ResolvedFileChange
{
    Added,
    Removed,
    Replaced,
    FullRecomputeStart,
    FullRecomputeFinished,
}
