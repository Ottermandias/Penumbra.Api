namespace Penumbra.Api.Enums;

public enum ApiCollectionType : byte
{
    Yourself = 0,

    MalePlayerCharacter,
    FemalePlayerCharacter,
    MaleNonPlayerCharacter,
    FemaleNonPlayerCharacter,
    NonPlayerChild,
    NonPlayerElderly,

    MaleMidlander,
    FemaleMidlander,
    MaleHighlander,
    FemaleHighlander,

    MaleWildwood,
    FemaleWildwood,
    MaleDuskwight,
    FemaleDuskwight,

    MalePlainsfolk,
    FemalePlainsfolk,
    MaleDunesfolk,
    FemaleDunesfolk,

    MaleSeekerOfTheSun,
    FemaleSeekerOfTheSun,
    MaleKeeperOfTheMoon,
    FemaleKeeperOfTheMoon,

    MaleSeawolf,
    FemaleSeawolf,
    MaleHellsguard,
    FemaleHellsguard,

    MaleRaen,
    FemaleRaen,
    MaleXaela,
    FemaleXaela,

    MaleHelion,
    FemaleHelion,
    MaleLost,
    FemaleLost,

    MaleRava,
    FemaleRava,
    MaleVeena,
    FemaleVeena,

    MaleMidlanderNpc,
    FemaleMidlanderNpc,
    MaleHighlanderNpc,
    FemaleHighlanderNpc,

    MaleWildwoodNpc,
    FemaleWildwoodNpc,
    MaleDuskwightNpc,
    FemaleDuskwightNpc,

    MalePlainsfolkNpc,
    FemalePlainsfolkNpc,
    MaleDunesfolkNpc,
    FemaleDunesfolkNpc,

    MaleSeekerOfTheSunNpc,
    FemaleSeekerOfTheSunNpc,
    MaleKeeperOfTheMoonNpc,
    FemaleKeeperOfTheMoonNpc,

    MaleSeawolfNpc,
    FemaleSeawolfNpc,
    MaleHellsguardNpc,
    FemaleHellsguardNpc,

    MaleRaenNpc,
    FemaleRaenNpc,
    MaleXaelaNpc,
    FemaleXaelaNpc,

    MaleHelionNpc,
    FemaleHelionNpc,
    MaleLostNpc,
    FemaleLostNpc,

    MaleRavaNpc,
    FemaleRavaNpc,
    MaleVeenaNpc,
    FemaleVeenaNpc,

    Default   = 0xE0,
    Interface = 0xE1,
    Current   = 0xE2,
}

/// <summary> Further collection types for the non-API version. </summary>
public static class ApiCollectionTypeExtensions
{
    /// <summary> The numeric value for individual collection assignments, not available in the API. </summary>
    public const byte Individual = 0xE3;

    /// <summary> The numeric value for collection deletions or creations, not available in the API. </summary>
    public const byte Inactive = 0xE4;

    /// <summary> The numeric value for temporary collection deletions or creations, not available in the API. </summary>
    public const byte Temporary = 0xE5;
}
