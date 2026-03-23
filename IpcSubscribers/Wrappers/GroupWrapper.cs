using Dalamud.Plugin.Ipc;

namespace Penumbra.Api.IpcSubscribers;

/// <summary> A wrapper around option groups. </summary>
public readonly ref struct GroupWrapper(IIdDataShareAdapter? group) : IDisposable
{
    internal enum Method
    {
        GetName,
        GetDefaultSettings,
        GetOptionCount,
        GetOptionName,
    }

    /// <summary> Get the name of the group. </summary>
    public string Name
        => group!.TryInvoke((int)Method.GetName, out string? name) ? name! : throw new ObjectDisposedException(nameof(group));

    /// <summary> Get whether the mod wrapper is valid. </summary>
    public bool IsValid
        => group is not null;

    /// <inheritdoc />
    public void Dispose()
        => group?.Dispose();

    /// <summary> Get the number of option groups in this mod. </summary>
    public int OptionCount
        => group!.TryInvoke((int)Method.GetOptionCount, out int count) ? count : throw new ObjectDisposedException(nameof(group));

    /// <summary> Get the default settings for this group. </summary>
    public uint DefaultSettings
        => group!.TryInvoke((int)Method.GetDefaultSettings, out uint settings) ? settings : throw new ObjectDisposedException(nameof(group));

    /// <summary> Get the name of an option. </summary>
    /// <param name="index"> The index of the option. </param>
    /// <returns> The name of the option. </returns>
    public string Option(int index)
        => group!.TryInvoke((int)Method.GetOptionName, out string? option) ? option! : throw new ObjectDisposedException(nameof(group));
}
