using System.Collections;
using System.Linq;

namespace Penumbra.Api.Helpers;

/// <summary> A wrapper for a synchronized mod list requested via IPC that uses type erasure to standard interfaces and types to cross app context borders. </summary>
/// <param name="adapter"> The adapter as requested by IPC. </param>
/// <remarks> If the Penumbra instance this was built for is disposed, this will throw on any query. </remarks>
public readonly struct ModListWrapper(IDisposable adapter) : IDisposable, IReadOnlyList<ModWrapper>
{
    /// <summary> Get the adapter as a list of type-erased mods. </summary>
    private IReadOnlyList<IDisposable> Adapter
        => (IReadOnlyList<IDisposable>)adapter;

    /// <inheritdoc />
    public void Dispose()
        => adapter.Dispose();

    /// <inheritdoc/>
    public IEnumerator<ModWrapper> GetEnumerator()
        => Adapter.Select(d => new ModWrapper(d)).GetEnumerator();

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    /// <inheritdoc/>
    public int Count
        => Adapter.Count;

    /// <inheritdoc/>
    public ModWrapper this[int index]
        => new(Adapter[index]);
}
