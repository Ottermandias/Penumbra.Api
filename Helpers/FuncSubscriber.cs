using Dalamud.Plugin;
using Dalamud.Plugin.Ipc;
using Dalamud.Plugin.Ipc.Exceptions;

namespace Penumbra.Api.Helpers;

/// <summary>
/// Specialized subscriber only allowing to invoke functions with a return.
/// </summary>
public readonly struct FuncSubscriber<TRet>
{
    private readonly string                     _label;
    private readonly ICallGateSubscriber<TRet>? _subscriber;

    /// <summary> Whether the subscriber could successfully be created. </summary>
    public bool Valid
        => _subscriber != null;

    public FuncSubscriber(DalamudPluginInterface pi, string label)
    {
        _label = label;
        try
        {
            _subscriber = pi.GetIpcSubscriber<TRet>(label);
        }
        catch (Exception e)
        {
            PluginLogHelper.WriteError(pi, $"Error registering IPC Subscriber for {label}\n{e}");
            _subscriber = null;
        }
    }

    /// <summary> Invoke the function. See the source of the subscriber for details.</summary>
    public TRet Invoke()
        => _subscriber != null ? _subscriber.InvokeFunc() : throw new IpcNotReadyError(_label);
}

/// <inheritdoc cref="FuncSubscriber{TRet}"/>
public readonly struct FuncSubscriber<T1, TRet>
{
    private readonly string                         _label;
    private readonly ICallGateSubscriber<T1, TRet>? _subscriber;

    /// <inheritdoc cref="FuncSubscriber{TRet}.Valid"/>
    public bool Valid
        => _subscriber != null;

    public FuncSubscriber(DalamudPluginInterface pi, string label)
    {
        _label = label;
        try
        {
            _subscriber = pi.GetIpcSubscriber<T1, TRet>(label);
        }
        catch (Exception e)
        {
            PluginLogHelper.WriteError(pi, $"Error registering IPC Subscriber for {label}\n{e}");
            _subscriber = null;
        }
    }

    /// <inheritdoc cref="FuncSubscriber{TRet}.Invoke"/>
    public TRet Invoke(T1 a)
        => _subscriber != null ? _subscriber.InvokeFunc(a) : throw new IpcNotReadyError(_label);
}

/// <inheritdoc cref="FuncSubscriber{TRet}"/>
public readonly struct ParamsFuncSubscriber<T1, TRet>
{
    private readonly string                           _label;
    private readonly ICallGateSubscriber<T1[], TRet>? _subscriber;

    /// <inheritdoc cref="FuncSubscriber{TRet}.Valid"/>
    public bool Valid
        => _subscriber != null;

    public ParamsFuncSubscriber(DalamudPluginInterface pi, string label)
    {
        _label = label;
        try
        {
            _subscriber = pi.GetIpcSubscriber<T1[], TRet>(label);
        }
        catch (Exception e)
        {
            PluginLogHelper.WriteError(pi, $"Error registering IPC Subscriber for {label}\n{e}");
            _subscriber = null;
        }
    }

    /// <inheritdoc cref="FuncSubscriber{TRet}.Invoke"/>
    public TRet Invoke(params T1[] a)
        => _subscriber != null ? _subscriber.InvokeFunc(a) : throw new IpcNotReadyError(_label);
}

/// <inheritdoc cref="FuncSubscriber{TRet}"/>
public readonly struct FuncSubscriber<T1, T2, TRet>
{
    private readonly string                             _label;
    private readonly ICallGateSubscriber<T1, T2, TRet>? _subscriber;

    /// <inheritdoc cref="FuncSubscriber{TRet}.Valid"/>
    public bool Valid
        => _subscriber != null;

    public FuncSubscriber(DalamudPluginInterface pi, string label)
    {
        _label = label;
        try
        {
            _subscriber = pi.GetIpcSubscriber<T1, T2, TRet>(label);
        }
        catch (Exception e)
        {
            PluginLogHelper.WriteError(pi, $"Error registering IPC Subscriber for {label}\n{e}");
            _subscriber = null;
        }
    }

    /// <inheritdoc cref="FuncSubscriber{TRet}.Invoke"/>
    public TRet Invoke(T1 a, T2 b)
        => _subscriber != null ? _subscriber.InvokeFunc(a, b) : throw new IpcNotReadyError(_label);
}

/// <inheritdoc cref="FuncSubscriber{TRet}"/>
public readonly struct ParamsFuncSubscriber<T1, T2, TRet>
{
    private readonly string _label;
    private readonly ICallGateSubscriber<T1, T2[], TRet>? _subscriber;

    /// <inheritdoc cref="FuncSubscriber{TRet}.Valid"/>
    public bool Valid
        => _subscriber != null;

    public ParamsFuncSubscriber(DalamudPluginInterface pi, string label)
    {
        _label = label;
        try
        {
            _subscriber = pi.GetIpcSubscriber<T1, T2[], TRet>(label);
        }
        catch (Exception e)
        {
            PluginLogHelper.WriteError(pi, $"Error registering IPC Subscriber for {label}\n{e}");
            _subscriber = null;
        }
    }

    /// <inheritdoc cref="FuncSubscriber{TRet}.Invoke"/>
    public TRet Invoke(T1 a, params T2[] b)
        => _subscriber != null ? _subscriber.InvokeFunc(a, b) : throw new IpcNotReadyError(_label);
}

/// <inheritdoc cref="FuncSubscriber{TRet}"/>
public readonly struct FuncSubscriber<T1, T2, T3, TRet>
{
    private readonly string                                 _label;
    private readonly ICallGateSubscriber<T1, T2, T3, TRet>? _subscriber;

    /// <inheritdoc cref="FuncSubscriber{TRet}.Valid"/>
    public bool Valid
        => _subscriber != null;

    public FuncSubscriber(DalamudPluginInterface pi, string label)
    {
        _label = label;
        try
        {
            _subscriber = pi.GetIpcSubscriber<T1, T2, T3, TRet>(label);
        }
        catch (Exception e)
        {
            PluginLogHelper.WriteError(pi, $"Error registering IPC Subscriber for {label}\n{e}");
            _subscriber = null;
        }
    }

    /// <inheritdoc cref="FuncSubscriber{TRet}.Invoke"/>
    public TRet Invoke(T1 a, T2 b, T3 c)
        => _subscriber != null ? _subscriber.InvokeFunc(a, b, c) : throw new IpcNotReadyError(_label);
}

/// <inheritdoc cref="FuncSubscriber{TRet}"/>
public readonly struct ParamsFuncSubscriber<T1, T2, T3, TRet>
{
    private readonly string                                   _label;
    private readonly ICallGateSubscriber<T1, T2, T3[], TRet>? _subscriber;

    /// <inheritdoc cref="FuncSubscriber{TRet}.Valid"/>
    public bool Valid
        => _subscriber != null;

    public ParamsFuncSubscriber(DalamudPluginInterface pi, string label)
    {
        _label = label;
        try
        {
            _subscriber = pi.GetIpcSubscriber<T1, T2, T3[], TRet>(label);
        }
        catch (Exception e)
        {
            PluginLogHelper.WriteError(pi, $"Error registering IPC Subscriber for {label}\n{e}");
            _subscriber = null;
        }
    }

    /// <inheritdoc cref="FuncSubscriber{TRet}.Invoke"/>
    public TRet Invoke(T1 a, T2 b, params T3[] c)
        => _subscriber != null ? _subscriber.InvokeFunc(a, b, c) : throw new IpcNotReadyError(_label);
}

/// <inheritdoc cref="FuncSubscriber{TRet}"/>
public readonly struct FuncSubscriber<T1, T2, T3, T4, TRet>
{
    private readonly string                                     _label;
    private readonly ICallGateSubscriber<T1, T2, T3, T4, TRet>? _subscriber;

    /// <inheritdoc cref="FuncSubscriber{TRet}.Valid"/>
    public bool Valid
        => _subscriber != null;

    public FuncSubscriber(DalamudPluginInterface pi, string label)
    {
        _label = label;
        try
        {
            _subscriber = pi.GetIpcSubscriber<T1, T2, T3, T4, TRet>(label);
        }
        catch (Exception e)
        {
            PluginLogHelper.WriteError(pi, $"Error registering IPC Subscriber for {label}\n{e}");
            _subscriber = null;
        }
    }

    /// <inheritdoc cref="FuncSubscriber{TRet}.Invoke"/>
    public TRet Invoke(T1 a, T2 b, T3 c, T4 d)
        => _subscriber != null ? _subscriber.InvokeFunc(a, b, c, d) : throw new IpcNotReadyError(_label);
}

/// <inheritdoc cref="FuncSubscriber{TRet}"/>
public readonly struct FuncSubscriber<T1, T2, T3, T4, T5, TRet>
{
    private readonly string                                         _label;
    private readonly ICallGateSubscriber<T1, T2, T3, T4, T5, TRet>? _subscriber;

    /// <inheritdoc cref="FuncSubscriber{TRet}.Valid"/>
    public bool Valid
        => _subscriber != null;

    public FuncSubscriber(DalamudPluginInterface pi, string label)
    {
        _label = label;
        try
        {
            _subscriber = pi.GetIpcSubscriber<T1, T2, T3, T4, T5, TRet>(label);
        }
        catch (Exception e)
        {
            PluginLogHelper.WriteError(pi, $"Error registering IPC Subscriber for {label}\n{e}");
            _subscriber = null;
        }
    }

    /// <inheritdoc cref="FuncSubscriber{TRet}.Invoke"/>
    public TRet Invoke(T1 a, T2 b, T3 c, T4 d, T5 e)
        => _subscriber != null ? _subscriber.InvokeFunc(a, b, c, d, e) : throw new IpcNotReadyError(_label);
}
