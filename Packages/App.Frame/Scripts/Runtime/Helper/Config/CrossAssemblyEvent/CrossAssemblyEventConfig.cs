/* *
 * ===============================================
 * author      : Junzi@macbook
 * e-mail      : shijun_z@163.com
 * create time : 2026年9月5 13:22
 * function    :
 * ===============================================
 * */
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CrossAssemblyEventConfig", menuName = "App/CrossAssemblyEventConfig")]
public class CrossAssemblyEventConfig : ScriptableObject
{
    private readonly UnityEvent _crossAssemblyEvent = new ();

    public void AddListener(UnityAction action)
    {
        _crossAssemblyEvent.AddListener(action);
    }

    public void RemoveListener(UnityAction action)
    {
        _crossAssemblyEvent.RemoveListener(action);
    }

    public void RemoveAllListeners()
    {
        _crossAssemblyEvent.RemoveAllListeners();
    }

    public void Execute()
    {
        _crossAssemblyEvent?.Invoke();
    }
}