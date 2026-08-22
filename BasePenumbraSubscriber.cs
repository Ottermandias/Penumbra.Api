using Dalamud.Plugin;
using Luna;
using Penumbra.Api.IpcSubscribers;

namespace Penumbra.Api;

/// <summary> A base <see cref="PluginSubscriber"/> with Penumbras data pre-filled. </summary>
/// <param name="log"><inheritdoc/></param>
/// <param name="pluginInterface"><inheritdoc/></param>
/// <param name="requiredMajor"><inheritdoc/></param>
/// <param name="requiredMinor"><inheritdoc/></param>
public class BasePenumbraSubscriber(LunaLogger log, IDalamudPluginInterface pluginInterface, int requiredMajor, int requiredMinor)
    : PluginSubscriber(log, pluginInterface, IpcSubscribers.Initialized.Subscriber(pluginInterface),
        IpcSubscribers.Disposed.Subscriber(pluginInterface), requiredMajor, requiredMinor, "Penumbra")
{
    /// <inheritdoc/>
    protected override void PluginInitialized()
    { }

    /// <inheritdoc/>
    protected override void PluginDisposed()
    { }

    /// <inheritdoc/>
    protected override (int Major, int Minor) GetVersionInfo()
        => new ApiVersion(PluginInterface).Invoke();
}
