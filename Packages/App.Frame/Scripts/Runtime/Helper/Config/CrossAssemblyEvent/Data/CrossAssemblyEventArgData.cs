/* *
 * ===============================================
 * author      : Junzi@macbook
 * e-mail      : shijun_z@163.com
 * create time : 2026年9月25 11:32
 * function    : 
 * ===============================================
 * */

using System;

[Serializable]
public class CrossAssemblyEventArgData<T> : CrossAssemblyEventData
{
    public T Arg;
    public CrossAssemblyEventArgData(T arg)
    {
        this.Arg = arg;
    }
}