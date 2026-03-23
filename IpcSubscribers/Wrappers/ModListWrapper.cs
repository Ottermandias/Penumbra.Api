using System.Collections;
using Dalamud.Plugin.Ipc;

namespace Penumbra.Api.IpcSubscribers;

/// <summary> A wrapper for a synchronized mod list requested via IPC that uses type erasure to standard interfaces and types to cross app context borders. </summary>
/// <param name="modList"> The adapter as requested by IPC. </param>
/// <remarks> If the Penumbra instance this was built for is disposed, this will throw on any query. </remarks>
public sealed class ModListWrapper(IIdDataShareAdapter modList) : IDisposable, IEnumerable<ModWrapper>
{
    internal enum Method
    {
        GetEnumerator,
        Count,
        GetModByIndex,
        GetModByName,
    }

    /// <inheritdoc/>
    public IEnumerator<ModWrapper> GetEnumerator()
    {
        if (!modList.TryInvoke((int)Method.GetEnumerator, out IEnumerator<IIdDataShareAdapter>? enumerator) || enumerator is null)
            yield break;

        try
        {
            while (enumerator!.MoveNext())
                yield return new ModWrapper(enumerator.Current);
        }
        finally
        {
            enumerator!.Dispose();
        }
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    /// <summary> Get the count of installed mods. </summary>
    public int Count
        => modList.TryInvoke((int)Method.Count, out int count) ? count : 0;

    /// <summary> Get a mod by its index. </summary>
    /// <param name="index"> The index. </param>
    /// <returns> The corresponding or invalid ModWrapper. </returns>
    public ModWrapper this[int index]
        => modList.TryInvoke((int)Method.GetModByIndex, index, out IIdDataShareAdapter? mod) ? new ModWrapper(mod) : new ModWrapper(null);

    /// <summary> Get a mod by its unique identifier or its name. </summary>
    /// <param name="identifier"> The unique identifier. If a mod matches this exactly, it will be returned. </param>
    /// <param name="name"> The name of the mod. If an empty identifier is provided or no mod matches the identifier, the first mod matching the name is returned. </param>
    /// <returns> The best matching mod or an invalid ModWrapper. </returns>
    public ModWrapper ByName(string identifier, string name = "")
        => modList.TryInvoke((int)Method.GetModByName, identifier, name, out IIdDataShareAdapter? mod)
            ? new ModWrapper(mod)
            : new ModWrapper(null);

    /// <inheritdoc />
    public void Dispose()
        => modList.Dispose();
}
