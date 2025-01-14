using Dalamud.Plugin;
using Penumbra.Api.Api;
using Penumbra.Api.Enums;
using Penumbra.Api.Helpers;
using PseudoModSetting =
    System.ValueTuple<bool, bool, int,
        System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IReadOnlyList<string>>>;

namespace Penumbra.Api.IpcSubscribers;

/// <inheritdoc cref="IPenumbraApiTemporary.CreateTemporaryCollection"/>
public sealed class CreateTemporaryCollection(IDalamudPluginInterface pi)
    : FuncSubscriber<string, Guid>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(CreateTemporaryCollection)}.V5";

    /// <inheritdoc cref="IPenumbraApiTemporary.CreateTemporaryCollection"/>
    public new Guid Invoke(string name = "")
        => base.Invoke(name);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<string, Guid> Provider(IDalamudPluginInterface pi, IPenumbraApiTemporary api)
        => new(pi, Label, api.CreateTemporaryCollection);
}

/// <inheritdoc cref="IPenumbraApiTemporary.DeleteTemporaryCollection"/>
public sealed class DeleteTemporaryCollection(IDalamudPluginInterface pi)
    : FuncSubscriber<Guid, int>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(DeleteTemporaryCollection)}.V5";

    /// <inheritdoc cref="IPenumbraApiTemporary.DeleteTemporaryCollection"/>
    public new PenumbraApiEc Invoke(Guid collectionId)
        => (PenumbraApiEc)base.Invoke(collectionId);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<Guid, int> Provider(IDalamudPluginInterface pi, IPenumbraApiTemporary api)
        => new(pi, Label, g => (int)api.DeleteTemporaryCollection(g));
}

/// <inheritdoc cref="IPenumbraApiTemporary.AssignTemporaryCollection"/>
public sealed class AssignTemporaryCollection(IDalamudPluginInterface pi)
    : FuncSubscriber<Guid, int, bool, int>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(AssignTemporaryCollection)}.V5";

    /// <inheritdoc cref="IPenumbraApiTemporary.AssignTemporaryCollection"/>
    public new PenumbraApiEc Invoke(Guid collectionId, int actorIndex, bool forceAssignment = true)
        => (PenumbraApiEc)base.Invoke(collectionId, actorIndex, forceAssignment);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<Guid, int, bool, int> Provider(IDalamudPluginInterface pi, IPenumbraApiTemporary api)
        => new(pi, Label, (a, b, c) => (int)api.AssignTemporaryCollection(a, b, c));
}

/// <inheritdoc cref="IPenumbraApiTemporary.AddTemporaryModAll"/>
public sealed class AddTemporaryModAll(IDalamudPluginInterface pi)
    : FuncSubscriber<string, Dictionary<string, string>, string, int, int>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(AddTemporaryModAll)}.V5";

    /// <inheritdoc cref="IPenumbraApiTemporary.AddTemporaryModAll"/>
    public new PenumbraApiEc Invoke(string tag, Dictionary<string, string> paths, string manipString, int priority)
        => (PenumbraApiEc)base.Invoke(tag, paths, manipString, priority);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<string, Dictionary<string, string>, string, int, int> Provider(IDalamudPluginInterface pi,
        IPenumbraApiTemporary api)
        => new(pi, Label, (a, b, c, d) => (int)api.AddTemporaryModAll(a, b, c, d));
}

/// <inheritdoc cref="IPenumbraApiTemporary.AddTemporaryMod"/>
public sealed class AddTemporaryMod(IDalamudPluginInterface pi)
    : FuncSubscriber<string, Guid, Dictionary<string, string>, string, int, int>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(AddTemporaryMod)}.V5";

    /// <inheritdoc cref="IPenumbraApiTemporary.AddTemporaryMod"/>
    public new PenumbraApiEc Invoke(string tag, Guid collectionId, Dictionary<string, string> paths, string manipString, int priority)
        => (PenumbraApiEc)base.Invoke(tag, collectionId, paths, manipString, priority);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<string, Guid, Dictionary<string, string>, string, int, int> Provider(IDalamudPluginInterface pi,
        IPenumbraApiTemporary api)
        => new(pi, Label, (a, b, c, d, e) => (int)api.AddTemporaryMod(a, b, c, d, e));
}

/// <inheritdoc cref="IPenumbraApiTemporary.RemoveTemporaryModAll"/>
public sealed class RemoveTemporaryModAll(IDalamudPluginInterface pi)
    : FuncSubscriber<string, int, int>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(RemoveTemporaryModAll)}.V5";

    /// <inheritdoc cref="IPenumbraApiTemporary.RemoveTemporaryModAll"/>
    public new PenumbraApiEc Invoke(string tag, int priority)
        => (PenumbraApiEc)base.Invoke(tag, priority);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<string, int, int> Provider(IDalamudPluginInterface pi,
        IPenumbraApiTemporary api)
        => new(pi, Label, (a, b) => (int)api.RemoveTemporaryModAll(a, b));
}

/// <inheritdoc cref="IPenumbraApiTemporary.RemoveTemporaryMod"/>
public sealed class RemoveTemporaryMod(IDalamudPluginInterface pi)
    : FuncSubscriber<string, Guid, int, int>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(RemoveTemporaryMod)}.V5";

    /// <inheritdoc cref="IPenumbraApiTemporary.RemoveTemporaryMod"/>
    public new PenumbraApiEc Invoke(string tag, Guid collectionId, int priority)
        => (PenumbraApiEc)base.Invoke(tag, collectionId, priority);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<string, Guid, int, int> Provider(IDalamudPluginInterface pi, IPenumbraApiTemporary api)
        => new(pi, Label, (a, b, c) => (int)api.RemoveTemporaryMod(a, b, c));
}

/// <inheritdoc cref="IPenumbraApiTemporary.SetTemporaryModSettings"/>
public sealed class SetTemporaryModSettings(IDalamudPluginInterface pi)
    : FuncSubscriber<Guid, string, string, PseudoModSetting, string, int, int>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(SetTemporaryModSettings)}.V5";

    /// <inheritdoc cref="IPenumbraApiTemporary.SetTemporaryModSettings"/>
    public PenumbraApiEc Invoke(Guid collectionId, string modDirectory, bool inherit, bool enabled, int priority,
        IReadOnlyDictionary<string, IReadOnlyList<string>> settings, string source, int key = 0, string modName = "")
        => (PenumbraApiEc)Invoke(collectionId, modDirectory, modName, (inherit, enabled, priority, settings), source, key);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<Guid, string, string, PseudoModSetting, string, int, int> Provider(IDalamudPluginInterface pi,
        IPenumbraApiTemporary api)
        => new(pi, Label, (a, b, c, d, e, f) => (int)api.SetTemporaryModSettings(a, b, c, d.Item1, d.Item2, d.Item3, d.Item4, e, f));
}

/// <inheritdoc cref="IPenumbraApiTemporary.SetTemporaryModSettingsPlayer"/>
public sealed class SetTemporaryModSettingsPlayer(IDalamudPluginInterface pi)
    : FuncSubscriber<int, string, string, PseudoModSetting, string, int, int>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(SetTemporaryModSettingsPlayer)}.V5";

    /// <inheritdoc cref="IPenumbraApiTemporary.SetTemporaryModSettingsPlayer"/>
    public PenumbraApiEc Invoke(int objectIndex, string modDirectory, bool inherit, bool enabled, int priority,
        IReadOnlyDictionary<string, IReadOnlyList<string>> settings, string source, int key = 0, string modName = "")
        => (PenumbraApiEc)Invoke(objectIndex, modDirectory, modName, (inherit, enabled, priority, settings), source, key);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<int, string, string, PseudoModSetting, string, int, int> Provider(IDalamudPluginInterface pi,
        IPenumbraApiTemporary api)
        => new(pi, Label, (a, b, c, d, e, f) => (int)api.SetTemporaryModSettingsPlayer(a, b, c, d.Item1, d.Item2, d.Item3, d.Item4, e, f));
}

/// <inheritdoc cref="IPenumbraApiTemporary.RemoveTemporaryModSettings"/>
public sealed class RemoveTemporaryModSettings(IDalamudPluginInterface pi)
    : FuncSubscriber<Guid, string, string, int, int>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(RemoveTemporaryModSettings)}.V5";

    /// <inheritdoc cref="IPenumbraApiTemporary.RemoveTemporaryModSettings"/>
    public PenumbraApiEc Invoke(Guid collectionId, string modDirectory, int key = 0, string modName = "")
        => (PenumbraApiEc)base.Invoke(collectionId, modDirectory, modName, key);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<Guid, string, string, int, int> Provider(IDalamudPluginInterface pi, IPenumbraApiTemporary api)
        => new(pi, Label, (a, b, c, d) => (int)api.RemoveTemporaryModSettings(a, b, c, d));
}

/// <inheritdoc cref="IPenumbraApiTemporary.RemoveTemporaryModSettingsPlayer"/>
public sealed class RemoveTemporaryModSettingsPlayer(IDalamudPluginInterface pi)
    : FuncSubscriber<int, string, string, int, int>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(RemoveTemporaryModSettingsPlayer)}.V5";

    /// <inheritdoc cref="IPenumbraApiTemporary.RemoveTemporaryModSettingsPlayer"/>
    public PenumbraApiEc Invoke(int objectIndex, string modDirectory, int key = 0, string modName = "")
        => (PenumbraApiEc)base.Invoke(objectIndex, modDirectory, modName, key);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<int, string, string, int, int> Provider(IDalamudPluginInterface pi, IPenumbraApiTemporary api)
        => new(pi, Label, (a, b, c, d) => (int)api.RemoveTemporaryModSettingsPlayer(a, b, c, d));
}

/// <inheritdoc cref="IPenumbraApiTemporary.RemoveAllTemporaryModSettings"/>
public sealed class RemoveAllTemporaryModSettings(IDalamudPluginInterface pi)
    : FuncSubscriber<Guid, int, int>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(RemoveAllTemporaryModSettings)}.V5";

    /// <inheritdoc cref="IPenumbraApiTemporary.RemoveAllTemporaryModSettings"/>
    public new PenumbraApiEc Invoke(Guid collectionId, int key = 0)
        => (PenumbraApiEc)base.Invoke(collectionId, key);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<Guid, int, int> Provider(IDalamudPluginInterface pi, IPenumbraApiTemporary api)
        => new(pi, Label, (a, b) => (int)api.RemoveAllTemporaryModSettings(a, b));
}

/// <inheritdoc cref="IPenumbraApiTemporary.RemoveAllTemporaryModSettingsPlayer"/>
public sealed class RemoveAllTemporaryModSettingsPlayer(IDalamudPluginInterface pi)
    : FuncSubscriber<int, int, int>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(RemoveAllTemporaryModSettingsPlayer)}.V5";

    /// <inheritdoc cref="IPenumbraApiTemporary.RemoveAllTemporaryModSettingsPlayer"/>
    public new PenumbraApiEc Invoke(int objectIndex, int key = 0)
        => (PenumbraApiEc)base.Invoke(objectIndex, key);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<int, int, int> Provider(IDalamudPluginInterface pi, IPenumbraApiTemporary api)
        => new(pi, Label, (a, b) => (int)api.RemoveAllTemporaryModSettingsPlayer(a, b));
}

/// <inheritdoc cref="IPenumbraApiTemporary.QueryTemporaryModSettings"/>
public sealed class QueryTemporaryModSettings(IDalamudPluginInterface pi)
    : FuncSubscriber<Guid, string, string, int, (int, (bool, bool, int, Dictionary<string, List<string>>)?, string)>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(QueryTemporaryModSettings)}.V5";

    /// <inheritdoc cref="IPenumbraApiTemporary.QueryTemporaryModSettings"/>
    public PenumbraApiEc Invoke(Guid collectionId, string modDirectory,
        out (bool ForceInherit, bool Enabled, int Priority, Dictionary<string, List<string>> Settings)? settings, out string source,
        int key = 0, string modName = "")
    {
        (var ec, settings, source) = Invoke(collectionId, modDirectory, modName, key);
        return (PenumbraApiEc)ec;
    }

    /// <summary> Create a provider. </summary>
    public static FuncProvider<Guid, string, string, int, (int, (bool, bool, int, Dictionary<string, List<string>>)?, string)> Provider(
        IDalamudPluginInterface pi, IPenumbraApiTemporary api)
        => new(pi, Label, (a, b, c, d) =>
        {
            var (ex, settings, source) = api.QueryTemporaryModSettings(a, b, c, d);
            return ((int)ex, settings, source);
        });
}

/// <inheritdoc cref="IPenumbraApiTemporary.QueryTemporaryModSettingsPlayer"/>
public sealed class QueryTemporaryModSettingsPlayer(IDalamudPluginInterface pi)
    : FuncSubscriber<int, string, string, int, (int, (bool, bool, int, Dictionary<string, List<string>>)?, string)>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(QueryTemporaryModSettingsPlayer)}.V5";

    /// <inheritdoc cref="IPenumbraApiTemporary.QueryTemporaryModSettingsPlayer"/>
    public PenumbraApiEc Invoke(int objectIndex, string modDirectory,
        out (bool ForceInherit, bool Enabled, int Priority, Dictionary<string, List<string>> Settings)? settings, out string source,
        int key = 0, string modName = "")
    {
        (var ec, settings, source) = Invoke(objectIndex, modDirectory, modName, key);
        return (PenumbraApiEc)ec;
    }

    /// <summary> Create a provider. </summary>
    public static FuncProvider<int, string, string, int, (int, (bool, bool, int, Dictionary<string, List<string>>)?, string)> Provider(
        IDalamudPluginInterface pi, IPenumbraApiTemporary api)
        => new(pi, Label, (a, b, c, d) =>
        {
            var (ex, settings, source) = api.QueryTemporaryModSettingsPlayer(a, b, c, d);
            return ((int)ex, settings, source);
        });
}
