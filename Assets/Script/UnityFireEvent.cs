using MEventCenter;
using MEventCenter.Example;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MyAssets.EventCenter.Example
{
    public class UnityFireEvent : MonoBehaviour
    {
        public Button mHitPlayerBtn;
        public Button mHitMonsterBtn;
        private void Start()
        {
            //Since GameStart event is not same as gameplay events and it doesnt need parameter, we can use SimpleEventCenter which use System.Action as delegate Type.
            //GameStart事件不需要参数,从分类上讲也不应该和受伤事件使用同一类事件中心,所以这里使用SimpleEventCenter,SimpleEventCenter使用System.Action作为DelegateType,已经足够使用
            //Of cource, you are also not supposed to put GameStart and GetHurt event in the same file.
            //当然,你也不应该将GameStart事件和GetHurt事件放在一起
            SimpleEventCenter.Instance.Fire(CustomEventID.GameStart.ToString());

            mHitPlayerBtn.onClick.AddListener(HitPlayer);
            mHitMonsterBtn.onClick.AddListener(HitMonster);
        }
        public void HitPlayer()
        {
            //We need send damage to the taget function, so we use our CustomEventCenter which use FunctionWithAnInt as delegate type.
            //因为需要传参,所以使用CustomEventCenter,这个事件中心的DelegateType委托类型是只有一个int参数的方法
            CustomEventCenter.Instance.Fire(CustomEventID.PlayerGetHurt, (int)Random.Range(1, 999));
        }
        public void HitMonster()
        {
            CustomEventCenter.Instance.Fire(CustomEventID.MonsterGetHurt, (int)Random.Range(1, 999));
        }
        private void OnDestroy()
        {
            //Same usage as GameStart
            SimpleEventCenter.Instance.Fire(CustomEventID.GameEnd.ToString());
        }
    }
}