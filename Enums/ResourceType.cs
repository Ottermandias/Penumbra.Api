using System.IO;
using ImSharp;

namespace Penumbra.Api.Enums;

/// <summary> An enum representing known resource types by the unsigned value of their up-to-four bytes. </summary>
public enum ResourceType : uint
{
    /// <summary> No known extension. </summary>
    Unknown = 0,

    /// <summary> .aet files. </summary>
    Aet = 0x00616574,

    /// <summary> Ambient data files. </summary>
    Amb = 0x00616D62,

    /// <summary> Attachment point data files. </summary>
    Atch = 0x61746368,

    /// <summary> Animation texture files (same format as <see cref="Tex"/>). </summary>
    Atex = 0x61746578,

    /// <summary> Animation visual effect files. </summary>
    Avfx = 0x61766678,

    /// <summary> .awt files. </summary>
    Awt = 0x00617774,

    /// <summary> Bonamik skeleton files. </summary>
    Bklb = 0x626B6C62,

    /// <summary> Character Make Parameter files. </summary>
    Cmp = 0x00636D70,

    /// <summary> Binary cutscene files. </summary>
    Cutb = 0x63757462,

    /// <summary> Dictionary files. </summary>
    Dic = 0x00646963,

    /// <summary> .eanb files. </summary>
    Eanb = 0x65616E62,

    /// <summary> .eid files. </summary>
    Eid = 0x00656964,

    /// <summary> Binary environment files. </summary>
    Envb = 0x656E7662,

    /// <summary> Equipment Deformer Parameter files. </summary>
    Eqdp = 0x65716470,

    /// <summary> Equipment Parameter files. </summary>
    Eqp = 0x00657170,

    /// <summary> .eslb files. </summary>
    Eslb = 0x65736C63,

    /// <summary> .essb files. </summary>
    Essb = 0x65737362,

    /// <summary> Extra Skeleton Template files. </summary>
    Est = 0x00657374,

    /// <summary> Equipment VFX Parameter files. </summary>
    Evp = 0x00657670,

    /// <summary> Excel Data files. </summary>
    Exd = 0x00657864,

    /// <summary> Excel Header files. </summary>
    Exh = 0x00657868,

    /// <summary> .exl files. </summary>
    Exl = 0x0065786C,

    /// <summary> .fdt files. </summary>
    Fdt = 0x00666474,

    /// <summary> .fpeb files. </summary>
    Fpeb = 0x66706562,

    /// <summary> .gfd files. </summary>
    Gfd = 0x00676664,

    /// <summary> .ggd files. </summary>
    Ggd = 0x00676764,

    /// <summary> Gimmick Parameter files. </summary>
    Gmp = 0x00676D70,

    /// <summary> .gzd files. </summary>
    Gzd = 0x00677A64,

    /// <summary> Image Change files. </summary>
    Imc = 0x00696D63,

    /// <summary> Kinedriver Bone files. </summary>
    Kdb = 0x006B6462,

    /// <summary> .kdlb files. </summary>
    Kdlb = 0x6B646C62,

    /// <summary> .lcb files. </summary>
    Lcb = 0x006C6362,

    /// <summary> .lgb files. </summary>
    Lgb = 0x006C6762,

    /// <summary> Binary lua files. </summary>
    Luab = 0x6C756162,

    /// <summary> .lvb files. </summary>
    Lvb = 0x006C7662,

    /// <summary> Model files. </summary>
    Mdl = 0x006D646C,

    /// <summary> .mlt files. </summary>
    Mlt = 0x006D6C74,

    /// <summary> Material files. </summary>
    Mtrl = 0x6D74726C,

    /// <summary> .obsb files. </summary>
    Obsb = 0x6F627362,

    /// <summary> Partial Animation Pack files. </summary>
    Pap = 0x00706170,

    /// <summary> Physical Bone Deformer files. </summary>
    Pbd = 0x00706264,

    /// <summary> .pcb files. </summary>
    Pcb = 0x00706362,

    /// <summary> Binary physics files. </summary>
    Phyb = 0x70687962,

    /// <summary> .plt files. </summary>
    Plt = 0x00706C74,

    /// <summary> Sound files. </summary>
    Scd = 0x00736364,

    /// <summary> .sgb files. </summary>
    Sgb = 0x00736762,

    /// <summary> .shcd files. </summary>
    Shcd = 0x73686364,

    /// <summary> Shader Package files. </summary>
    Shpk = 0x7368706B,

    /// <summary> Binary Skeleton files. </summary>
    Sklb = 0x736B6C62,

    /// <summary> .skp files. </summary>
    Skp = 0x00736B70,

    /// <summary> Shader Parameter files. </summary>
    Spm = 0x0073706D,

    /// <summary> Staining Template files. </summary>
    Stm = 0x0073746D,

    /// <summary> .svb files. </summary>
    Svb = 0x00737662,

    /// <summary> .tera files. </summary>
    Tera = 0x74657261,

    /// <summary> Texture files. </summary>
    Tex = 0x00746578,

    /// <summary> Binary Timeline files. </summary>
    Tmb = 0x00746D62,

    /// <summary> .ugd files. </summary>
    Ugd = 0x00756764,

    /// <summary> UI Layout Data files. </summary>
    Uld = 0x00756C64,

    /// <summary> .waoe files. </summary>
    Waoe = 0x77616F65,

    /// <summary> .wtd files. </summary>
    Wtd = 0x00777464,
}

/// <summary> Extension methods for the resource type enum. </summary>
public static class ResourceTypeExtensions
{
    /// <summary> Extension methods for the resource type enum. </summary>
    extension(ResourceType type)
    {
        /// <summary> Whether the extension type is defined in the enum. </summary>
        public bool Known
            => type is not ResourceType.Unknown && type.Defined;

        /// <summary> Get a resource type enum from a given extension. </summary>
        public static ResourceType FromExtension(ReadOnlySpan<byte> ext)
            => ext.Length switch
            {
                0 => ResourceType.Unknown,
                1 => (ResourceType)(ext[0] | 32),
                2 => (ResourceType)(ext[1] | 32 | ((ext[0] | 32) << 8)),
                3 => (ResourceType)(ext[2] | 32 | ((ext[1] | 32) << 8) | ((ext[0] | 32) << 16)),
                4 => (ResourceType)(ext[3] | 32 | ((ext[2] | 32) << 8) | ((ext[1] | 32) << 16) | ((ext[0] | 32) << 24)),
                _ => ResourceType.Unknown,
            };

        /// <summary> Get a resource type enum from a given extension. </summary>
        public static ResourceType FromExtension(ReadOnlySpan<char> ext)
            => ext.Length switch
            {
                0 => ResourceType.Unknown,
                1 => (ResourceType)((byte)ext[0] | 32),
                2 => (ResourceType)((byte)ext[1] | 32 | (((byte)ext[0] | 32) << 8)),
                3 => (ResourceType)((byte)ext[2] | 32 | (((byte)ext[1] | 32) << 8) | (((byte)ext[0] | 32) << 16)),
                4 => (ResourceType)((byte)ext[3] | 32 | (((byte)ext[2] | 32) << 8) | (((byte)ext[1] | 32) << 16) | (((byte)ext[0] | 32) << 24)),
                _ => ResourceType.Unknown,
            };


        /// <summary> Get a resource type enum from a given path. </summary>
        public static ResourceType FromPath(ReadOnlySpan<byte> path)
        {
            // This is mostly an adaptation of Path.GetExtension to ROS<byte>.
            var length = path.Length;
            for (var index = length - 1; index >= 0; --index)
            {
                var c = path[index];
                if (c is (byte)'.')
                    return index != length - 1 ? FromExtension(path[(index + 1)..]) : ResourceType.Unknown;

                if (c is (byte)'/' or (byte)'\\')
                    break;
            }

            return ResourceType.Unknown;
        }

        /// <summary> Get a resource type enum from a given path. </summary>
        public static ResourceType FromPath(ReadOnlySpan<char> path)
        {
            var extension = Path.GetExtension(path);
            return extension.IsEmpty
                ? ResourceType.Unknown
                : FromExtension(extension[1..]);
        }
    }
}
