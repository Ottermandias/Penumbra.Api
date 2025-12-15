using Dalamud.Plugin;
using Penumbra.Api.Api;
using Penumbra.Api.Enums;
using Penumbra.Api.Helpers;

namespace Penumbra.Api.IpcSubscribers;

/// <inheritdoc cref="IPenumbraApiResolve.ResolveDefaultPath"/>
public sealed class ResolveDefaultPath(IDalamudPluginInterface pi)
    : FuncSubscriber<string, string>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(ResolveDefaultPath)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.ResolveDefaultPath"u8;

    /// <inheritdoc cref="IPenumbraApiResolve.ResolveDefaultPath"/>
    public new string Invoke(string gamePath)
        => base.Invoke(gamePath);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<string, string> Provider(IDalamudPluginInterface pi, IPenumbraApiResolve api)
        => new(pi, Label, api.ResolveDefaultPath);
}

/// <inheritdoc cref="IPenumbraApiResolve.ResolveInterfacePath"/>
public sealed class ResolveInterfacePath(IDalamudPluginInterface pi)
    : FuncSubscriber<string, string>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(ResolveInterfacePath)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.ResolveInterfacePath"u8;

    /// <inheritdoc cref="IPenumbraApiResolve.ResolveInterfacePath"/>
    public new string Invoke(string gamePath)
        => base.Invoke(gamePath);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<string, string> Provider(IDalamudPluginInterface pi, IPenumbraApiResolve api)
        => new(pi, Label, api.ResolveInterfacePath);
}

/// <inheritdoc cref="IPenumbraApiResolve.ResolveGameObjectPath"/>
public sealed class ResolveGameObjectPath(IDalamudPluginInterface pi)
    : FuncSubscriber<string, int, string>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(ResolveGameObjectPath)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.ResolveGameObjectPath"u8;

    /// <inheritdoc cref="IPenumbraApiResolve.ResolveGameObjectPath"/>
    public new string Invoke(string gamePath, int gameObjectIdx)
        => base.Invoke(gamePath, gameObjectIdx);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<string, int, string> Provider(IDalamudPluginInterface pi, IPenumbraApiResolve api)
        => new(pi, Label, api.ResolveGameObjectPath);
}

/// <inheritdoc cref="IPenumbraApiResolve.ResolvePath"/>
public sealed class ResolvePath(IDalamudPluginInterface pi)
    : FuncSubscriber<Guid, string, (int, string?)>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(ResolvePath)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.ResolvePath"u8;

    /// <inheritdoc cref="IPenumbraApiResolve.ResolvePath"/>
    public PenumbraApiEc Invoke(Guid collection, string gamePath, out string? resolvedPath)
    {
        (var ret, resolvedPath) = base.Invoke(collection, gamePath);
        return (PenumbraApiEc)ret;
    }

    /// <summary> Create a provider. </summary>
    public static FuncProvider<Guid, string, (int, string?)> Provider(IDalamudPluginInterface pi, IPenumbraApiResolve api)
        => new(pi, Label, (a, b) => ((int)api.ResolvePath(a, b, out var c), c));
}

/// <inheritdoc cref="IPenumbraApiResolve.ResolvePlayerPath"/>
public sealed class ResolvePlayerPath(IDalamudPluginInterface pi)
    : FuncSubscriber<string, string>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(ResolvePlayerPath)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.ResolvePlayerPath"u8;

    /// <inheritdoc cref="IPenumbraApiResolve.ResolvePlayerPath"/>
    public new string Invoke(string gamePath)
        => base.Invoke(gamePath);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<string, string> Provider(IDalamudPluginInterface pi, IPenumbraApiResolve api)
        => new(pi, Label, api.ResolvePlayerPath);
}

/// <inheritdoc cref="IPenumbraApiResolve.ReverseResolveGameObjectPath"/>
public sealed class ReverseResolveGameObjectPath(IDalamudPluginInterface pi)
    : FuncSubscriber<string, int, string[]>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(ReverseResolveGameObjectPath)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.ReverseResolveGameObjectPath"u8;

    /// <inheritdoc cref="IPenumbraApiResolve.ReverseResolveGameObjectPath"/>
    public new string[] Invoke(string gamePath, int gameObjectIdx)
        => base.Invoke(gamePath, gameObjectIdx);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<string, int, string[]> Provider(IDalamudPluginInterface pi, IPenumbraApiResolve api)
        => new(pi, Label, api.ReverseResolveGameObjectPath);
}

/// <inheritdoc cref="IPenumbraApiResolve.ReverseResolvePlayerPath"/>
public sealed class ReverseResolvePlayerPath(IDalamudPluginInterface pi)
    : FuncSubscriber<string, string[]>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(ReverseResolvePlayerPath)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.ReverseResolvePlayerPath"u8;

    /// <inheritdoc cref="IPenumbraApiResolve.ReverseResolvePlayerPath"/>
    public new string[] Invoke(string gamePath)
        => base.Invoke(gamePath);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<string, string[]> Provider(IDalamudPluginInterface pi, IPenumbraApiResolve api)
        => new(pi, Label, api.ReverseResolvePlayerPath);
}

/// <inheritdoc cref="IPenumbraApiResolve.ResolvePlayerPaths"/>
public sealed class ResolvePlayerPaths(IDalamudPluginInterface pi)
    : FuncSubscriber<string[], string[], (string[], string[][])>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(ResolvePlayerPaths)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.ResolvePlayerPaths"u8;

    /// <inheritdoc cref="IPenumbraApiResolve.ResolvePlayerPaths"/>
    public new (string[], string[][]) Invoke(string[] forward, string[] reverse)
        => base.Invoke(forward, reverse);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<string[], string[], (string[], string[][])> Provider(IDalamudPluginInterface pi, IPenumbraApiResolve api)
        => new(pi, Label, api.ResolvePlayerPaths);
}

/// <inheritdoc cref="IPenumbraApiResolve.ResolvePaths"/>
public sealed class ResolvePaths(IDalamudPluginInterface pi)
    : FuncSubscriber<Guid, string[], string[], (int, string[], string[][])>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(ResolvePaths)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.ResolvePaths"u8;

    /// <inheritdoc cref="IPenumbraApiResolve.ResolvePaths"/>
    public PenumbraApiEc Invoke(Guid collection, string[] forward, string[] reverse, out string[] forwardResolved, out string[][] reverseResolved)
    {
        (var ret, forwardResolved, reverseResolved) = base.Invoke(collection, forward, reverse);
        return (PenumbraApiEc)ret;
    }

    /// <inheritdoc cref="IPenumbraApiResolve.ResolvePaths"/>
    public PenumbraApiEc Invoke(Guid collection, string[] forward, out string[]? forwardResolved)
    {
        (var ret, forwardResolved, _) = base.Invoke(collection, forward, []);
        return (PenumbraApiEc)ret;
    }

    /// <summary> Create a provider. </summary>
    public static FuncProvider<Guid, string[], string[], (int, string[], string[][])> Provider(IDalamudPluginInterface pi, IPenumbraApiResolve api)
        => new(pi, Label, (a, b, c) => ((int)api.ResolvePaths(a, b, c, out var f, out var r), f, r));
}

/// <inheritdoc cref="IPenumbraApiResolve.ResolvePlayerPathsAsync"/>
public sealed class ResolvePlayerPathsAsync(IDalamudPluginInterface pi)
    : FuncSubscriber<string[], string[], Task<(string[], string[][])>>(pi, Label)
{
    /// <summary> The label. </summary>
    public const string Label = $"Penumbra.{nameof(ResolvePlayerPathsAsync)}";

    /// <summary> The label as UTF8 string. </summary>
    public static ReadOnlySpan<byte> LabelU8
        => "Penumbra.ResolvePlayerPathsAsync"u8;

    /// <inheritdoc cref="IPenumbraApiResolve.ResolvePlayerPathsAsync"/>
    public new Task<(string[], string[][])> Invoke(string[] forward, string[] reverse)
        => base.Invoke(forward, reverse);

    /// <summary> Create a provider. </summary>
    public static FuncProvider<string[], string[], Task<(string[], string[][])>> Provider(IDalamudPluginInterface pi, IPenumbraApiResolve api)
        => new(pi, Label, api.ResolvePlayerPathsAsync);
}
