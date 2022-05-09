using UnityEngine;

/// <summary>
/// <para>Delete this file if you wish.It's just an example.</para>
/// <para>此文件只是个示范,可以删除这个文件.</para>
/// </summary>
namespace MEventCenter.Example
{
    /// <summary>
    /// <para>Extend EventCenter. Fire a <see cref="CustomEventID"/> and handle with <see cref="FunctionWithAnInt"/>.</para>
    /// <para>继承EventCenter.使用CustomID作为事件名，使用FunctionWhitParam委托作为事件触发时所执行的方法类型</para>
    /// </summary>
    public class CustomEventCenter : EventCenter<CustomEventCenter, CustomEventID, FunctionWithAnInt>
    {
        /// <summary>
        /// <para>Override Fire method as we dont need so much more parameters.Override it or not is OK anyway.</para>
        /// <para>重写Fire方法,我们不需要那么多的参数.不重写也行.</para>
        /// <para>I didnt find a way to restrict parammeters or just hide this method while overriding it.If you got a resolution, welcome to create a pull request!</para>
        /// <para>我目前没有找到如何重写并限制参数类型或直接隐藏的方法,如果你有解决办法,欢迎提交pull request!</para>
        /// </summary>
        public override bool Fire(CustomEventID eventID, params object[] args)
        {
            Debug.Log($"Fire Custom Event:{eventID}");
            if (args.Length > 0 && args[0].GetType().Equals(typeof(int)))
            {
                return base.Fire(eventID, args[0]);
            }
            else
            {
                Debug.LogError($"{GetType().Name}: Parameters are not fit");
                return false;
            }
        }

        public bool Fire(CustomEventID eventID, int number)
        {
            return base.Fire(eventID, number);
        }

        public override void Register(CustomEventID key, FunctionWithAnInt dele)
        {
            Debug.Log($"Register event: {key}. With method {dele.Method.Name}()");
            base.Register(key, dele);
        }
    }
}