/* *
 * ===============================================
 * author      : Josh@win
 * e-mail      : shijun_z@163.com
 * create time : 2026年1月13 13:47
 * function    : 
 * ===============================================
 * */

using System;
using App.Core.Tools;

namespace App.Core.Master
{
    [Serializable]
    public class ItemDataBase
    {
        
    }
    public class ItemBase : EventBaseMono
    {
        protected ItemDataBase ItemData;
        
        [Obsolete("此方法已弃用，请使用InitItem方法", true)]
        protected virtual void Awake()
        {
            InitItem();
            RegisterEvent();
        }
        [Obsolete("此方法已弃用，请使用RegisterEvent方法", true)]
        protected virtual void Start()
        {
        }
        /// <summary>初始化Item</summary>
        protected virtual void InitItem()
        {
        }
        /// <summary>注册消息事件,默认删除此事件</summary>
        protected virtual void RegisterEvent()
        {
        }
        public virtual void RefreshItem(ItemDataBase data)
        {
            ItemData = data;
        }
    }
}
