using Dalamud.Plugin;
using Luna;
using Penumbra.Api.Enums;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Penumbra.Api.IpcSubscribers.Legacy;

public sealed class GetMods(IDalamudPluginInterface pi)
    : FuncSubscriber<IList<(string, string)>>(pi, Label)
{
    public const string Label = $"Penumbra.{nameof(GetMods)}";

    public new IList<(string, string)> Invoke()
        => base.Invoke();
}

public sealed class ReloadMod(IDalamudPluginInterface pi)
    : FuncSubscriber<string, string, PenumbraApiEc>(pi, Label)
{
    public const string Label = $"Penumbra.{nameof(ReloadMod)}";

    public PenumbraApiEc Invoke(string modDirectory, string modName = "")
        => base.Invoke(modDirectory, modName);
}

public sealed class InstallMod(IDalamudPluginInterface pi)
    : FuncSubscriber<string, PenumbraApiEc>(pi, Label)
{
    public const string Label = $"Penumbra.{nameof(InstallMod)}";

    public PenumbraApiEc Invoke(string modDirectory)
        => base.Invoke(modDirectory);
}

public sealed class AddMod(IDalamudPluginInterface pi)
    : FuncSubscriber<string, PenumbraApiEc>(pi, Label)
{
    public const string Label = $"Penumbra.{nameof(AddMod)}";

    public PenumbraApiEc Invoke(string modDirectory)
        => base.Invoke(modDirectory);
}

public sealed class DeleteMod(IDalamudPluginInterface pi)
    : FuncSubscriber<string, string, PenumbraApiEc>(pi, Label)
{
    public const string Label = $"Penumbra.{nameof(DeleteMod)}";

    public PenumbraApiEc Invoke(string modDirectory, string modName = "")
        => base.Invoke(modDirectory, modName);
}

public sealed class GetModPath(IDalamudPluginInterface pi)
    : FuncSubscriber<string, string, (PenumbraApiEc, string, bool)>(pi, Label)
{
    public const string Label = $"Penumbra.{nameof(GetModPath)}";

    public (PenumbraApiEc ErrorCode, string Path, bool IsDefault) Invoke(string modDirectory, string modName = "")
        => base.Invoke(modDirectory, modName);
}

public sealed class SetModPath(IDalamudPluginInterface pi)
    : FuncSubscriber<string, string, string, PenumbraApiEc>(pi, Label)
{
    public const string Label = $"Penumbra.{nameof(SetModPath)}";

    public PenumbraApiEc Invoke(string modDirectory, string newPath, string modName = "")
        => base.Invoke(modDirectory, modName, newPath);
}
