namespace Penumbra.Api.Enums;

public enum ResourceType : uint
{
    Unknown = 0,
    Aet     = 0x00616574,
    Amb     = 0x00616D62,
    Atch    = 0x61746368,
    Atex    = 0x61746578,
    Avfx    = 0x61766678,
    Awt     = 0x00617774,
    Bklb    = 0x626B6C62,
    Cmp     = 0x00636D70,
    Cutb    = 0x63757462,
    Dic     = 0x00646963,
    Eanb    = 0x65616E62,
    Eid     = 0x00656964,
    Envb    = 0x656E7662,
    Eqdp    = 0x65716470,
    Eqp     = 0x00657170,
    Eslb    = 0x65736C63,
    Essb    = 0x65737362,
    Est     = 0x00657374,
    Evp     = 0x00657670,
    Exd     = 0x00657864,
    Exh     = 0x00657868,
    Exl     = 0x0065786C,
    Fdt     = 0x00666474,
    Fpeb    = 0x66706562,
    Gfd     = 0x00676664,
    Ggd     = 0x00676764,
    Gmp     = 0x00676D70,
    Gzd     = 0x00677A64,
    Imc     = 0x00696D63,
    Kdb     = 0x006B6462,
    Kdlb    = 0x6B646C62,
    Lcb     = 0x006C6362,
    Lgb     = 0x006C6762,
    Luab    = 0x6C756162,
    Lvb     = 0x006C7662,
    Mdl     = 0x006D646C,
    Mlt     = 0x006D6C74,
    Mtrl    = 0x6D74726C,
    Obsb    = 0x6F627362,
    Pap     = 0x00706170,
    Pbd     = 0x00706264,
    Pcb     = 0x00706362,
    Phyb    = 0x70687962,
    Plt     = 0x00706C74,
    Scd     = 0x00736364,
    Sgb     = 0x00736762,
    Shcd    = 0x73686364,
    Shpk    = 0x7368706B,
    Sklb    = 0x736B6C62,
    Skp     = 0x00736B70,
    Stm     = 0x0073746D,
    Svb     = 0x00737662,
    Tera    = 0x74657261,
    Tex     = 0x00746578,
    Tmb     = 0x00746D62,
    Ugd     = 0x00756764,
    Uld     = 0x00756C64,
    Waoe    = 0x77616F65,
    Wtd     = 0x00777464,
}

public static class ResourceTypeExtensions
{
    public static ResourceType FromExtension(ReadOnlySpan<byte> ext)
        => ext.Length switch
        {
            0 => ResourceType.Unknown,
            1 => (ResourceType)(ext[0] | 32),
            2 => (ResourceType)(ext[0] | 32 | ((ext[1] | 32) << 8)),
            3 => (ResourceType)(ext[0] | 32 | ((ext[1] | 32) << 8) | ((ext[2] | 32) << 16)),
            4 => (ResourceType)(ext[0] | 32 | ((ext[1] | 32) << 8) | ((ext[2] | 32) << 16) | ((ext[2] | 32) << 24)),
            _ => ResourceType.Unknown,
        };

    public static ResourceType FromExtension(ReadOnlySpan<char> ext)
        => ext.Length switch
        {
            0 => ResourceType.Unknown,
            1 => (ResourceType)((byte)ext[0] | 32),
            2 => (ResourceType)((byte)ext[0] | 32 | (((byte)ext[1] | 32) << 8)),
            3 => (ResourceType)((byte)ext[0] | 32 | (((byte)ext[1] | 32) << 8) | (((byte)ext[2] | 32) << 16)),
            4 => (ResourceType)((byte)ext[0] | 32 | (((byte)ext[1] | 32) << 8) | (((byte)ext[2] | 32) << 16) | (((byte)ext[2] | 32) << 24)),
            _ => ResourceType.Unknown,
        };
}
