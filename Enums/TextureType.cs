namespace Penumbra.Api.Enums;

/// <summary>
/// The different types of textures a given texture can be converted to.
/// </summary>
public enum TextureType
{
    /// <summary> Convert the texture to .png. </summary>
    Png = 0,

    /// <summary> Keep the texture format as it is but save as .tex. </summary>
    AsIsTex = 1,

    /// <summary> Keep the texture format as it is but save as .dds. </summary>
    AsIsDds = 2,

    /// <summary> Convert the texture to RGBA32 and save as .tex. </summary>
    RgbaTex = 3,

    /// <summary> Convert the texture to RGBA32 and save as .dds. </summary>
    RgbaDds = 4,

    /// <summary> Convert the texture to BC3 and save as .tex. </summary>
    Bc3Tex = 5,

    /// <summary> Convert the texture to BC3 and save as .dds. </summary>
    Bc3Dds = 6,

    /// <summary> Convert the texture to BC7 and save as .tex. </summary>
    Bc7Tex = 7,

    /// <summary> Convert the texture to BC7 and save as .dds. </summary>
    Bc7Dds = 8,

    /// <summary> Convert the texture to .tga. </summary>
    Targa = 9,

    /// <summary> Convert the texture to BC1 and save as .tex. </summary>
    Bc1Tex = 10,

    /// <summary> Convert the texture to BC1 and save as .dds. </summary>
    Bc1Dds = 11,

    /// <summary> Convert the texture to BC4 and save as .tex. </summary>
    Bc4Tex = 12,

    /// <summary> Convert the texture to BC4 and save as .dds. </summary>
    Bc4Dds = 13,

    /// <summary> Convert the texture to BC5 and save as .tex. </summary>
    Bc5Tex = 14,

    /// <summary> Convert the texture to BC5 and save as .dds. </summary>
    Bc5Dds = 15,
}
