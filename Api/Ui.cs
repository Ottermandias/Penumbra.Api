using Penumbra.Api.Enums;

namespace Penumbra.Api.Api;

/// <summary> API methods pertaining to Penumbras UI. </summary>
public interface IPenumbraApiUi
{
    /// <summary>
    ///   Triggered when the user hovers over a listed changed object in a mod tab.<para />
    ///   Can be used to append tooltips.
    /// </summary>
    /// <returns> The type of the changed item and its ID if known. </returns>
    public event Action<ChangedItemType, uint>? ChangedItemTooltip;

    /// <summary> Triggered when the user clicks a listed changed object in a mod tab. </summary>
    /// <returns> The mouse button clicked, the type of the changed item and its ID if known. </returns>
    public event Action<MouseButton, ChangedItemType, uint>? ChangedItemClicked;

    /// <summary> Triggered before the settings tab bar for a mod is drawn, after the title group is drawn. </summary>
    /// <returns> The directory name of the currently selected mod, the total used width of the title bar and the width of the title box. </returns>
    public event Action<string, float, float>? PreSettingsTabBarDraw;

    /// <summary> Triggered before the content of a mod settings panel is drawn. </summary>
    /// <returns> The directory name of the currently selected mod. </returns>
    public event Action<string>? PreSettingsPanelDraw;

    /// <summary> Triggered after the Enabled Checkbox line in settings is drawn, but before options are drawn. </summary>
    /// <returns> The directory name of the currently selected mod. </returns>
    public event Action<string>? PostEnabledDraw;

    /// <summary> Triggered after the content of a mod settings panel is drawn, but still in the child window. </summary>
    /// <returns> The directory name of the currently selected mod. </returns>
    public event Action<string>? PostSettingsPanelDraw;

    /// <summary> Open the Penumbra main config window. </summary>
    /// <param name="tab"> Open the window at a specific tab. Use TabType.None to not change the tab. </param>
    /// <param name="modDirectory"> Select a mod specified via its directory name in the mod tab, empty if none. </param>
    /// <param name="modName"> Select a mod specified via its mod name in the mod tab, empty if none. </param>
    /// <returns>
    ///   InvalidArgument if <paramref name="tab"/> is invalid,
    ///   ModMissing if <paramref name="modDirectory"/> or <paramref name="modName"/> are set non-empty and the mod does not exist,
    ///   Success otherwise.
    /// </returns>
    /// <remarks> If <paramref name="tab"/> is not TabType.Mods, the mod will not be selected regardless of other parameters and ModMissing will not be returned. </remarks>
    public PenumbraApiEc OpenMainWindow(TabType tab, string modDirectory, string modName);

    /// <summary> Close the Penumbra main config window. </summary>
    public void CloseMainWindow();

    /// <summary> Registers a section named "Integration with {Plugin}" in Penumbra's settings. </summary>
    /// <param name="draw"> A function that draws the section's contents using ImGui. </param>
    /// <returns>
    ///   InvalidArgument if <paramref name="draw"/> is invalid,
    ///   NothingChanged if the section was already registered,
    ///   Success otherwise.
    /// </returns>
    /// <remarks> Each plugin can only register one section. Trying to register another will replace the previous one. </remarks>
    public PenumbraApiEc RegisterSettingsSection(Action draw);

    /// <summary> Unregisters a section previously registered with <see cref="RegisterSettingsSection"/>. </summary>
    /// <param name="draw"> The function previously passed to <see cref="RegisterSettingsSection"/>. </param>
    /// <returns>
    ///   NothingChanged if the section was not registered,
    ///   Success otherwise.
    /// </returns>
    /// <remarks> Sections are automatically unregistered if the plugin that owns them is unloaded, but it is still strongly recommended to explicitly call this function. </remarks>
    public PenumbraApiEc UnregisterSettingsSection(Action draw);
}
