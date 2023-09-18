using Penumbra.Api.Enums;

namespace Penumbra.Api;

/// Delegates used by different events.
/// <summary>Used when hovering over a changed item.</summary>
/// <returns>The hovered <paramref name="item"/> if it is of a known type, null otherwise.</returns>
public delegate void ChangedItemHover(object? item);

/// <summary>Used when changed item is clicked.</summary>
/// <returns>The mouse <paramref name="button" /> used and the clicked <paramref name="item"/> if it is of a known type, null otherwise.</returns>
public delegate void ChangedItemClick(MouseButton button, object? item);

/// <summary>Used when a game object is redrawn by Penumbra.</summary>
/// <returns>The <paramref name="objectPtr" /> to the redrawn object and its <paramref name="objectTableIndex" />.</returns>
public delegate void GameObjectRedrawnDelegate(IntPtr objectPtr, int objectTableIndex);

/// <summary>
/// Used when the setting of a mod is changed in any way.
/// </summary>
/// <returns>The <paramref name="type" /> of change, <para />
/// the <paramref name="collectionName" /> in which the setting is changed, <para />
/// the <paramref name="modDirectory" /> name of the mod, <para />
/// and whether the change was <paramref name="inherited" /> or not.</returns>
public delegate void ModSettingChangedDelegate(ModSettingChange type, string collectionName, string modDirectory, bool inherited);

/// <summary>
/// Used before a new character base draw object is created from a <paramref name="gameObject" />.
/// </summary>
/// <returns>A pointer to the source <paramref name="gameObject" />, <para />
/// the <paramref name="collectionName" /> used for the object, <para />
/// a pointer to the used <paramref name="modelId" /> (of type <c>ushort*</c>), <para />
/// a pointer to the <paramref name="customize" /> array, <para />
/// and a pointer to the <paramref name="equipData" /> array.</returns>
public delegate void CreatingCharacterBaseDelegate(IntPtr gameObject, string collectionName, IntPtr modelId, IntPtr customize,
    IntPtr equipData);

/// <summary>
/// Used after a character base <paramref name="drawObject" /> has been created from a <paramref name="gameObject" />.
/// </summary>
/// <returns>A pointer to the source <paramref name="gameObject" />, <para />
/// the <paramref name="collectionName" /> used for the object, <para />
/// a pointer to newly created <paramref name="drawObject" />.</returns>
public delegate void CreatedCharacterBaseDelegate(IntPtr gameObject, string collectionName, IntPtr drawObject);

/// <summary>
/// Used when a specific game object has resolved a path to a non-default path.
/// </summary>
/// <returns>A pointer to the source <paramref name="gameObject" />, <para />
/// the original <paramref name="gamePath" /> that was resolved by Penumbra, <para />
/// the resulting <paramref name="localPath" /> returned by Penumbra.</returns>
public delegate void GameObjectResourceResolvedDelegate(IntPtr gameObject, string gamePath, string localPath);
