#  UnityEventCenter

An event center for Unity(maybe also .Net). Support custom delegate and event key type.

一个较为万用的事件中心，支持自定义委托和事件Key类型

现已并入至[我的轻量框架](https://github.com/ms0ng/MSFrame)中

English Readme is working in progress...

## 快速食用

> 项目已内置有示例，在Example文件夹下

创建一个以string为事件ID，以Action委托为处理的事件中心：

``` C#
public class SimpleEventCenter : EventCenter<SimpleEventCenter, string, Action>
  {
    //是的，这里什么都不用写，继承就好了
  }
```

注册事件“PlayerGetHurt”：

``` C#
SimpleEventCenter.Instance.Register("PlayerGetHurt", OnPlayerGetHurt);

private void OnPlayerGetHurt()
  {
    Debug.Log("Opps, Player Get Damage!");
  }
```

在玩家受击的逻辑里抛出事件：

``` C#
SimpleEventCenter.Instance.Fire("PlayerGetHurt");
```

## 不，我们不要字符串

字符串并不能特别方便地定位“使用该事件的代码”的位置，因此我们应该使用枚举

``` C#
public enum CustomEventID
    {
        MonsterGetHurt,
        PlayerGetHurt,
    }
```

我们修改一下SimpleEventCenter，让它使用CustomEventID作为抛出事件的key，而不是字符串

``` C#
public class SimpleEventCenter : EventCenter<SimpleEventCenter, CustomEventID, Action>
  {
  
  }
```

在注册事件时，我们也稍微修改一下

``` C#
SimpleEventCenter.Instance.Register(CustomEventID.PlayerGetHurt , OnPlayerGetHurt);
```

抛出事件是也是一样
``` C#
SimpleEventCenter.Instance.Fire(CustomEventID.PlayerGetHurt);
```

Nice！我们创造了一个没有字符串的世界！

## 是参数，我加了参数！
既然是造成伤害的事件，那么怎么能没有参数？我们先定义一个带一个int参数的委托。
> 不清楚什么是委托的不要着急，就当我们正在定义一种方法，这种方法是 `带一个int参数的方法`

``` C#
public delegate void WowAMethodWithANumber(int number);
```

然后继续稍微修改我们的SimpleEventCenter

``` C#
public class SimpleEventCenter : EventCenter<SimpleEventCenter, CustomEventID, WowAMethodWithANumber>
  {
  
  }
```

太棒了！现在，我们的SimpleEventCenter事件中心将会抛出`CustomeEventID`事件，并且由长得特别像`WowAMethodWithANumber`的方法来进行处理。这样的话，我们注册的事件就变成了这样：

``` C#
SimpleEventCenter.Instance.Register("PlayerGetHurt", OnPlayerGetHurt);

private void OnPlayerGetHurt(int number)
  {
    Debug.Log($"Opps, Player Get Damage! Health -{number}!");
  }
```

> 如果你不了解委托，那么现在应该明白，我们定义委托 `WowAMethodWithANumber`就是定义了一种要长成这个样子的方法。而我们新的`OnPlayerGetHurt`正好就长这样！因此，它可以被用在我们新的SimpleEventCenter中！

抛出事件时，我们要修改这样

``` C#
SimpleEventCenter.Instance.Fire(CustomEventID.PlayerGetHurt, 233);
```

当我们抛出`CustomEventID.PlayerGetHurt`事件时，Console面板上会打印
``` 
Opps, Player Get Damage! Health -233!
```

---

至此，你花了10分钟已经完全了解了这款事件中心！现在，开始做你的游戏吧
