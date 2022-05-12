using System;
using System.Collections.Generic;

namespace MEventCenter
{
    /// <summary>
    /// <para>EventCenter Class.Use a dictionary to store events.Must extend this class to use it.</para>
    /// <para>可以继承此类以单独分类各种类的事件中心</para>
    /// </summary>
    /// <typeparam name="InstanceType">
    /// <para>Instance class name.Usually it's the name of the class that extend this class.</para>
    /// <para>单例类,一般情况下是继承EventCenter的类的类名</para>
    /// </typeparam>
    /// <typeparam name="KeyType">
    /// <para>Keys to search an event.It can be string or enums you define.</para>
    /// <para>索引类型,可以是string或者你自定的枚举</para>
    /// </typeparam>
    /// <typeparam name="DelegateType">
    /// <para>Methods' type.If there is no need for parameters, it can be <see cref="System.Action"/></para>
    /// <para>事件方法类型.如果不需要参数,可以直接使用<see cref="System.Action"/></para>
    /// </typeparam>
    public abstract class EventCenter<InstanceType, KeyType, DelegateType> where DelegateType : Delegate where InstanceType : EventCenter<InstanceType, KeyType, DelegateType>
    {
        /// <summary>
        /// <para>Private instance field.</para>
        /// <para>私有单例字段</para>
        /// </summary>
        private static InstanceType _instance;

        /// <summary>
        /// <para>Use ClassName.Instance to access the instance instead of new().</para>
        /// <para>使用ClassName.Instance访问全局唯一单例,不要使用new实例化</para>
        /// </summary>
        public static InstanceType Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Activator.CreateInstance<InstanceType>();
                    _instance.Init();
                }
                return _instance;
            }
        }

        /// <summary>
        /// <para>EventCenter uses a dictionary to store events.</para>
        /// <para>事件中心使用一个字典存储所有的事件</para>
        /// </summary>
        private Dictionary<KeyType, DelegateType> mEvents;
        /// <summary>
        /// <para>Init Method.</para>
        /// <para>初始化方法</para>
        /// </summary>
        public virtual void Init()
        {
            mEvents = new Dictionary<KeyType, DelegateType>();
        }
        /// <summary>
        /// <para>Trigger an event via a key</para>
        /// <para>通过key触发一个或多个事件</para>
        /// </summary>
        /// <param name="key">
        /// <para>Event name you define.</para>
        /// <para>事件名称</para>
        /// </param>
        /// <param name="args">
        /// <para>Parameters if your <see cref="DelegateType"/> needs.</para>
        /// <para>如果你的<see cref="DelegateType"/>需要参数,则在这里填写它</para>
        /// </param>
        /// <returns>
        /// <para>A boolean if this evnet has been defined.</para>
        /// <para>如果该事件被定义且触发,返回true,否则返回false</para>
        /// </returns>
        public virtual bool Fire(KeyType key, params object[] args)
        {
            if (mEvents.TryGetValue(key, out DelegateType _delegate))
            {
                _delegate.DynamicInvoke(args);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// <para>Register an event <paramref name="key"/> with a handle <paramref name="dele"/>.</para>
        /// <para>注册一个事件,当事件<paramref name="key"/>抛出时由<paramref name="dele"/>处理</para>
        /// </summary>
        /// <param name="key">
        /// <para>Event idtity.</para>
        /// <para>事件的唯一标识</para>
        /// </param>
        /// <param name="dele">
        /// <para>Handler when event <paramref name="key"/> fired out.</para>
        /// <para>当事件<paramref name="key"/>抛出时的处理</para>
        /// </param>
        public virtual void Register(KeyType key, DelegateType dele)
        {
            if (mEvents.TryGetValue(key, out DelegateType outDele))
            {
                mEvents[key] = (DelegateType)Delegate.Combine(outDele, dele);
            }
            else
            {
                mEvents.Add(key, dele);
            }
        }
        /// <summary>
        /// <para>Deregister a handler of <paramref name="key"/></para>
        /// <para>在事件<paramref name="key"/>中注销处理方法</para>
        /// </summary>
        /// <param name="key">
        /// <para>Event idtity.</para>
        /// <para>事件的唯一标识</para>
        /// </param>
        /// <param name="dele">
        /// <para>Handler when event <paramref name="key"/> fired out.</para>
        /// <para>当事件<paramref name="key"/>抛出时的处理</para>
        /// </param>
        /// <returns>
        /// <para>A Boolean if <paramref name="dele"/> has been registered.</para>
        /// <para>返回处理方法<paramref name="dele"/>是否被注册过</para>
        /// </returns>
        public virtual bool Deregister(KeyType key, DelegateType dele)
        {
            if (mEvents.TryGetValue(key, out DelegateType outDele))
            {
                mEvents[key] = (DelegateType)Delegate.Remove(outDele, dele);
                if (outDele.GetInvocationList().Length == 0) mEvents.Remove(key);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// <para>Get a delegate that contains all the handler registered.Usually it's used for highly customized.</para>
        /// <para>取某个事件的委托,该委托包含了该事件的所有处理.一般用以高度自定义委托</para>
        /// </summary>
        /// <param name="key">
        /// <para>Event idtity.</para>
        /// <para>事件的唯一标识</para>
        /// </param>
        /// <returns>
        /// <typeparamref name="DelegateType"/>
        /// </returns>
        public virtual DelegateType GetHandlers(KeyType key)
        {
            if (!mEvents.ContainsKey(key)) mEvents.Add(key, null);
            return mEvents[key];
        }
        /// <summary>
        /// <para>Search a method in <paramref name="key"/>'s handlers</para>
        /// <para>在对应事件中搜索是否存在某个方法</para>
        /// </summary>
        /// <param name="key">
        /// <para>Event idtity.</para>
        /// <para>事件的唯一标识</para>
        /// </param>
        /// <param name="dele">
        /// <para>The method you want to search.</para>
        /// <para>需要搜索的方法</para>
        /// </param>
        /// <returns>
        /// <para>A Boolean if <paramref name="dele"/> is inside the event handler</para>
        /// <para>如果事件的所有处理方法中存在该方法则返回true</para>
        /// </returns>
        public virtual bool CheckIfEventExist(KeyType key, DelegateType dele)
        {
            if (mEvents.TryGetValue(key, out DelegateType outDele))
            {
                Delegate[] delegates = outDele.GetInvocationList();
                foreach (var item in delegates)
                {
                    if (item.Equals(dele)) return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// <para>Access the dictionary that store all the event keys and handlers.It's used for highly customized.Dont use it unless you know what you are doing.</para>
        /// <para>访问存储所有事件和对应处理的字典.此方法用以高度自定义,一般不推荐直接调用.</para>
        /// </summary>
        /// <returns>
        /// <see cref="mEvents"/>
        /// </returns>
        public virtual Dictionary<KeyType, DelegateType> GetEventsDictionary()
        {
            return mEvents;
        }
    }
}