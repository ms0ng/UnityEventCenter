using MEventCenter.Example;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.MyAssets.EventCenter.Example
{
    public class UnityEventHandler : MonoBehaviour
    {
        private void Start()
        {
            CustomEventCenter.Instance.Register(CustomEventID.PlayerGetHurt, OnPlayerGetHurt);
            CustomEventCenter.Instance.Register(CustomEventID.MonsterGetHurt, OnMonsterGetHurt);
        }

        private void OnPlayerGetHurt(int number)
        {
            Debug.Log($"Opps, Player Get Damage! Health -{number}!");
        }

        private void OnMonsterGetHurt(int number)
        {
            Debug.Log($"Yeah, Monster Get Damage! Health -{number}!");
        }
    }
}