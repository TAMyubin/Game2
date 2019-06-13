using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class FSMState
{
    protected Dictionary<Transition, FSMStateID> map = new Dictionary<Transition, FSMStateID>();//用字典存储状态与状态转换条件的映射
    /*状态机的基类*/
    protected FSMStateID stateID;
    public FSMStateID ID
    {
        get
        {
            return stateID;
        }
    }
    protected Vector3 destPos;//目标的位置
  

    public abstract void Reason(Transform PlayerTransform, Transform BabyTransform,Transform Turret, Transform self);//控制一个状态转换到另一个状态
    public abstract void Act(Transform PlayerTransform, Transform BabyTransform, Transform Turret, Transform self);//状态的具体实现方法(处于该状态时做的事情)

    public FSMStateID GetOutputState(Transition transition)
    {
        if(transition == Transition.None)//如果参数是无效的状态
        {
            return FSMStateID.None;
        }
        if (map.ContainsKey(transition)){
            return map[transition];

        }
        return FSMStateID.None;//如果返回的状态没有在列表里
    } 
    public void AddTransition(Transition transition,FSMStateID id)
    {
        if(transition ==Transition.None ||id== FSMStateID.None)//状态无效
        {
            return;
        }
        if (map.ContainsKey(transition))//状态已经在里面（状态重复）
        {
            return;
        }
        map.Add(transition, id);
        Debug.Log("当前状态是:  " + transition + "状态ID为 : " + ID);
    }
    public void DeleteTransition(Transition transition)
    {
        if(transition == Transition.None)//如果条件为空
        {
            return;
        }
        if (map.ContainsKey(transition))
        {
            map.Remove(transition);
            return;
        }

    }
}
