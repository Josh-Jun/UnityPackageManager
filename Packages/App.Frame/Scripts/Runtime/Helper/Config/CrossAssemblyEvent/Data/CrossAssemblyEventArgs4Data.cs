/* *
 * ===============================================
 * author      : Junzi@macbook
 * e-mail      : shijun_z@163.com
 * create time : 2026年9月25 12:37
 * function    : 
 * ===============================================
 * */

using System;

[Serializable]
public class CrossAssemblyEventArgs4Data<T1, T2, T3, T4> : CrossAssemblyEventData
{
    public T1 Arg1;
    public T2 Arg2;
    public T3 Arg3;
    public T4 Arg4;
    public CrossAssemblyEventArgs4Data(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        this.Arg1 = arg1;
        this.Arg2 = arg2;
        this.Arg3 = arg3;
        this.Arg4 = arg4;
    }
}
