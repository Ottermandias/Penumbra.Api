using System.Collections.Frozen;

namespace Penumbra.Api.Api;

/// <summary> API methods pertaining to Penumbras own state. </summary>
public interface IPenumbraApiPluginState
{
    /// <returns> The full path of the current penumbra root directory. </returns>
    public string GetModDirectory();

    /// <returns> The entire current penumbra configuration as a json encoded string. </returns>
    public string GetConfiguration();

    /// <summary>
    /// Fired whenever a mod directory change is finished.
    /// </summary>
    /// <returns>The full path of the mod directory and whether Penumbra treats it as valid.</returns>
    public event Action<string, bool>? ModDirectoryChanged;

    /// <returns>True if Penumbra is enabled, false otherwise.</returns>
    public bool GetEnabledState();

    /// <summary>
    /// Fired whenever the enabled state of Penumbra changes.
    /// </summary>
    /// <returns>True if the new state is enabled, false if the new state is disabled</returns>
    public event Action<bool>? EnabledChange;

    /// <summary> Check whether all the given features are supported by this Penumbra version. </summary>
    /// <param name="requiredFeatures"> The features to check for. </param>
    /// <returns> A list of required features that are unsupported by this Penumbra version, which is empty if everything is supported. </returns>
    public string[] CheckSupportedFeatures(IEnumerable<string> requiredFeatures);

    /// <summary> Get the list of specific new features that are currently supported by this Penumbra version. </summary>
    public FrozenSet<string> SupportedFeatures { get; }
}
