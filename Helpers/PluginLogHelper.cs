using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace Penumbra.Api.Helpers;

internal class PluginLogHelper
{
    [PluginService]
    private static IPluginLog? _log { get; set; }

    private PluginLogHelper(DalamudPluginInterface pi)
        => pi.Inject(this);

    public static void WriteError(DalamudPluginInterface pi, string errorMessage)
        => GetLog(pi).Error(errorMessage);

    public static IPluginLog GetLog(DalamudPluginInterface pi)
    {
        if (_log != null)
            return _log;

        _ = new PluginLogHelper(pi);
        return _log!;
    }
}
