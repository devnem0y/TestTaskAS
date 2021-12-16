using System;

public class Dispatcher
{
    #region Events
    
    public static event Action<object> OnElementClick;
    public static event Action<object> OnGenerationDone;
    
    public static event Action OnStart;
    public static event Action OnRestart;
    public static event Action OnLevelCompleted;
    public static event Action OnWin;
    public static event Action OnFailed;

    #endregion

    #region ActionsEvent

    private static Action GetEvent(Event e)
    {
        switch (e)
        {
            case Event.ON_START: return OnStart;
            case Event.ON_RESTART: return OnRestart;
            case Event.ON_LEVEL_COMPLETED: return OnLevelCompleted;
            case Event.ON_WIN: return OnWin;
            case Event.ON_FAILED: return OnFailed;
            
            default: throw new ArgumentOutOfRangeException(nameof(e), e, null);
        }
    }

    #endregion

    #region ActionsEventHasParam

    private static Action<object> GetEventHasParam(Event e)
    {
        switch (e)
        {
            case Event.ON_ELEMENT_CLICK: return OnElementClick;
            case Event.ON_GENERATION_DONE: return OnGenerationDone;

            default: throw new ArgumentOutOfRangeException(nameof(e), e, null);
        }
    }

    #endregion

    #region Send

    /// <summary>
    /// Отправка события без параметров
    /// </summary>
    /// <param name="e">Событие</param>
    public static void Send(Event e)
    {
        Invoker(GetEvent(e));
    }
    
    /// <summary>
    /// Отправка события с одним параметром любого типа
    /// </summary>
    /// <param name="e">Событие</param>
    /// <param name="arg">Параметр</param>
    public static void Send(Event e, object arg)
    {
        Invoker(GetEventHasParam(e), arg);
    }
    
    /// <summary>
    /// Отправка события с массивом параметров любого типа
    /// </summary>
    /// <param name="e">Событие</param>
    /// <param name="args">Массив параметров</param>
    public static void Send(Event e, params object[] args)
    {
        Invoker(GetEventHasParam(e), args);
    }
    
    private static void Invoker(Action action)
    {
        action?.Invoke();
    }

    private static void Invoker(Action<object> action, object arg)
    {
        action?.Invoke(arg);
    }
    
    private static void Invoker(Action<object> action, params object[] args)
    {
        action?.Invoke(args);
    }

    #endregion
}

public enum Event
{
    ON_ELEMENT_CLICK,

    ON_START,
    ON_RESTART,
    ON_LEVEL_COMPLETED,
    ON_WIN,
    ON_FAILED,

    ON_GENERATION_DONE,
}