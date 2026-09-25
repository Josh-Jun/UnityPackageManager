/* *
 * ===============================================
 * author      : Junzi@macbook
 * e-mail      : shijun_z@163.com
 * create time : 2026年9月25 12:35
 * function    : 
 * ===============================================
 * */

using System;

[Serializable]
public class CrossAssemblyEventArgs2Data<T1, T2> : CrossAssemblyEventData
{
    public T1 Arg1;
    public T2 Arg2;
    public CrossAssemblyEventArgs2Data(T1 arg1, T2 arg2)
    {
        this.Arg1 = arg1;
        this.Arg2 = arg2;
    }
}

