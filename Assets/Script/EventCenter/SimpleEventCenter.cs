using System;
using UnityEngine;

namespace MEventCenter
{
    /// <summary>
    /// <para>A simple way to use EvenCenter.Fire a string and handle with Action.</para>
    /// <para>比较简单的事件中心,发射一个字符串作为事件名,使用Action作为事件的处理</para>
    /// </summary>
    public class SimpleEventCenter : EventCenter<SimpleEventCenter, string, Action>
    {
        /// <summary>
        /// <para>I didnt find a way to restrict parammeters or just hide this method while overriding it.If you got a resolution, welcome to create a pull request!</para>
        /// <para>我目前没有找到如何重写并限制参数类型或直接隐藏的方法,如果你有解决办法,欢迎提交pull request!</para>
        /// </summary>
        public override bool Fire(string key, params object[] args)
        {
            if (args.Length > 0)
            {
                Debug.LogWarning("You don't need to fire event with any parameter!");
            }
            Debug.Log($"Fire Event: {key}");
            return base.Fire(key);
        }
    }
}