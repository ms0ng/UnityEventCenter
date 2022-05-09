/// <summary>
/// <para>Delete this file if you need.It's just an example.</para>
/// <para>此文件只是个示范,可以删除这个文件.</para>
/// </summary>
namespace MEventCenter.Example
{
    public delegate void SimpleDelegate();//It's the same as using System.Action while the delegate has no return or param.Try use SimpleEventCenter in this case.
    public delegate void FunctionWithAString(string text);
    public delegate void FunctionWithParams(int number, string text);
    public delegate void FunctionWithAnInt(int number);//Only this is used for CustomEventCenter.Delegates above are just examples.
}