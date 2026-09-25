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
public class CrossAssemblyEventArgsData<T> : CrossAssemblyEventArgsData
{
    public T Arg;
    public CrossAssemblyEventArgsData(T arg)
    {
        this.Arg = arg;
    }
}

[Serializable]
public class CrossAssemblyEventArgsData<T1, T2> : CrossAssemblyEventArgsData
{
    public T1 Arg1;
    public T2 Arg2;
    public CrossAssemblyEventArgsData(T1 arg1, T2 arg2)
    {
        this.Arg1 = arg1;
        this.Arg2 = arg2;
    }
}

[Serializable]
public class CrossAssemblyEventArgsData<T1, T2, T3> : CrossAssemblyEventArgsData
{
    public T1 Arg1;
    public T2 Arg2;
    public T3 Arg3;
    public CrossAssemblyEventArgsData(T1 arg1, T2 arg2, T3 arg3)
    {
        this.Arg1 = arg1;
        this.Arg2 = arg2;
        this.Arg3 = arg3;
    }
}

[Serializable]
public class CrossAssemblyEventArgsData<T1, T2, T3, T4> : CrossAssemblyEventArgsData
{
    public T1 Arg1;
    public T2 Arg2;
    public T3 Arg3;
    public T4 Arg4;
    public CrossAssemblyEventArgsData(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        this.Arg1 = arg1;
        this.Arg2 = arg2;
        this.Arg3 = arg3;
        this.Arg4 = arg4;
    }
}