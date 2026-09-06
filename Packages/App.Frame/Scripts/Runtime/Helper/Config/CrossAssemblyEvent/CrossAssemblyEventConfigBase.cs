/* *
 * ===============================================
 * author      : Junzi@macbook
 * e-mail      : shijun_z@163.com
 * create time : 2026年9月5 13:23
 * function    :
 * ===============================================
 * */
using UnityEngine;
using UnityEngine.Events;

public class CrossAssemblyEventConfigBase<T> : ScriptableObject
{
    private readonly UnityEvent<T> _crossAssemblyEvent = new ();

    public void AddListener(UnityAction<T> action)
    {
        _crossAssemblyEvent.AddListener(action);
    }

    public void RemoveListener(UnityAction<T> action)
    {
        _crossAssemblyEvent.RemoveListener(action);
    }

    public void RemoveAllListeners()
    {
        _crossAssemblyEvent.RemoveAllListeners();
    }

    public void Execute(T arg)
    {
        _crossAssemblyEvent?.Invoke(arg);
    }
}