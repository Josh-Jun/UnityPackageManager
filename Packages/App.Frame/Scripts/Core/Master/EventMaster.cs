/* *
 * ===============================================
 * author      : Josh@book
 * e-mail      : shijun_z@163.com
 * create time : 2024年10月21 13:24
 * function    :
 * ===============================================
 * */

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using App.Core.Helper;
using App.Core.Tools;
using UnityEngine;

namespace App.Core.Master
{
    public struct EventData
    {
        public object obj;
        public MethodInfo method;
    }

    public class EventMaster : SingletonMono<EventMaster>
    {
        private readonly Dictionary<string, List<EventData>> Events = new();
        private const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

        public void AddEventMethods(object obj)
        {
            var type = obj.GetType();
            var methods = type.GetMethods(flags).Where(info => info.GetCustomAttributes(typeof(EventAttribute), false).Any()).ToList();
            foreach (var method in methods)
            {
                var ea = method.GetCustomAttributes(typeof(EventAttribute), false).First() as EventAttribute;
                var data = new EventData
                {
                    method = method,
                    obj = obj
                };
                if (Events.ContainsKey(ea!.Event))
                {
                    Events[ea.Event].Add(data);
                }
                else
                {
                    Events.Add(ea.Event, new List<EventData> { data });
                }
            }
        }

        public void RemoveEventMethods(object obj)
        {
            var type = obj.GetType();
            var methods = type.GetMethods(flags).Where(info => info.GetCustomAttributes(typeof(EventAttribute), false).Any()).ToList();
            foreach (var ea in methods.Select(method => method.GetCustomAttributes(typeof(EventAttribute), false).First() as EventAttribute))
            {
                Events.Remove(ea!.Event);
            }
        }

        public void AddEventMethods(ILogic logic)
        {
            var type = logic.GetType();
            var methods = type.GetMethods(flags).Where(info => info.GetCustomAttributes(typeof(EventAttribute), false).Any()).ToList();
            foreach (var method in methods)
            {
                var ea = method.GetCustomAttributes(typeof(EventAttribute), false).First() as EventAttribute;
                var data = new EventData
                {
                    method = method,
                    obj = logic
                };
                if (Events.ContainsKey(ea!.Event))
                {
                    Events[ea.Event].Add(data);
                }
                else
                {
                    Events.Add(ea.Event, new List<EventData> { data });
                }
            }
        }

        public void RemoveEventMethods(ILogic logic)
        {
            var type = logic.GetType();
            var methods = type.GetMethods(flags).Where(info => info.GetCustomAttributes(typeof(EventAttribute), false).Any()).ToList();
            foreach (var ea in methods.Select(method => method.GetCustomAttributes(typeof(EventAttribute), false).First() as EventAttribute))
            {
                Events.Remove(ea!.Event);
            }
        }

        public void Execute(string eventName, params object[] args)
        {
            if (Events.TryGetValue(eventName, out var datas))
            {
                foreach (var data in datas)
                {
                    var paramsInfo = data.method.GetParameters();
                    if (paramsInfo.Length != args.Length)
                    {
                        Log.W($"类对象{data.obj}中{eventName}事件对应的方法{data.method.Name}参数对应不上！！！");
                        return;
                    }

                    for (var i = 0; i < args.Length; i++)
                    {
                        if (args[i].GetType() == paramsInfo[i].ParameterType) continue;
                        Log.W($"参数类型不正确", ("目标类型", paramsInfo[i].ParameterType.Name), ("来源类型", args[i].GetType().Name));
                        return;
                    }

                    data.method.Invoke(data.obj, args);
                }
            }
            else
            {
                Log.W($"{eventName} 事件未找到！");
            }
        }

        public bool Has(string eventName)
        {
            return Events.ContainsKey(eventName);
        }

        public void Remove(string eventName)
        {
            Events.Remove(eventName);
        }

        public void RemoveAll()
        {
            Events.Clear();
        }
    }
}