using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum FSMStateID
{
    None = 0,
    ChaseBaby,//追逐baby
    Chasing,//追逐玩家
    ChaseTurret,//追逐炮塔
    Attack,//攻击
    AttackTurret,//攻击炮塔
    Hurt,//受伤
    Dead,//死亡
}
public enum Transition//状态之间的转换条件
{
    None = 0,
    disshort,//与玩家有距离
    dislong,//与玩家距离大于与baby距离（dislong）
    CanAttack,//与玩家的距离到攻击范围(CanAttack)
    FineTurret,//追逐炮塔
    CanAttackTurret,//可以攻击炮塔
    GetHurt,//受到攻击（GetHurt）
    NoHp,//剩余血量小于0(NoHp)
}
public class AdvancedFSM : FSM
{
    private List<FSMState> fSMStates;//储存所有的状态
    private FSMStateID currentStateID;//记录当前的状态ID
    public FSMStateID CurrentStateID//开放一个当前状态ID的接口
    {
        get
        {
            return currentStateID;
        }
    }
    private FSMState currentState;//记录当前的状态
    public FSMState CurrentState//开放一个当前状态的接口
    {
        get
        {
            return currentState;
        }
    }
    public AdvancedFSM()//构造函数
    {
        fSMStates = new List<FSMState>();
    }
    /// <summary>
    /// 把状态保存到List集合中
    /// </summary>
    /// <param name="fSMState"></param>
    public void AddFSMState(FSMState fSMState)
    {
        if(fSMState == null)//状态为空
        {
           // Debug.LogError("不允许使用空的对象引用");
            return;
        }
        if(fSMStates.Count == 0)
        {
            fSMStates.Add(fSMState);
            currentState = fSMState;//设置默认状态
            currentStateID = fSMState.ID;//并记录默认状态的ID
        }
        foreach(FSMState state in fSMStates)
        {
            if(state.ID == fSMState.ID)//状态已经在集合中
            {
              //  Debug.LogError("该状态已经在集合中");
                return;
            }
        }
        fSMStates.Add(fSMState);
       
    }
    public void DeleteFSMState(FSMStateID fSMState)
    {
        if (fSMState == FSMStateID.None)
        {
            return;
        }
        foreach(FSMState state in fSMStates)
        {
            if (state.ID == fSMState)
            {
                fSMStates.Remove(state);
                return;
            }
        }
    }
    public void PerformTransition(Transition transition)
    {
        if(transition == Transition.None)//转换无效
        {
            return;
        }
       FSMStateID id= currentState.GetOutputState(transition);//当前状态转换到输出状态
        if(id ==FSMStateID.None)//如果输出状态无效、
        {
            return;

        }
        currentStateID = id;
        foreach(FSMState state in fSMStates)
        {
            if(state.ID == currentStateID)
            {
                currentState = state;
                break;
            }
        }
    }
  
}
