using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayerState : FSMState
{

    public ChasePlayerState()
    {
        stateID = FSMStateID.Chasing;
    }

    public override void Act(Transform PlayerTransform, Transform BabyTransform, Transform Turret, Transform self)
    {
        float dast = Vector3.Distance(self.position, Turret.position);
        if (self.GetComponent<NPCjinzhan>().Turrets.Count > 0 && dast < 10)
        {
            self.GetComponent<NPCjinzhan>().StopNav();
            self.GetComponent<NPCjinzhan>().SetTransition(Transition.FineTurret);
        }
        float dist = Vector3.Distance(self.position, BabyTransform.position);
        float dost = Vector3.Distance(self.position, PlayerTransform.position);
        if (dist < dost&& self.GetComponent<NPCjinzhan>().Turrets.Count == 0)//如果怪物距离baby较近
        {
            Debug.Log("转换到追逐baby");
            self.GetComponent<NPCjinzhan>().StopNav();
            self.GetComponent<NPCjinzhan>().SetTransition(Transition.dislong);
        }
        if(dost < self.GetComponent<NPCjinzhan>().dis&&self.GetComponent<NPCjinzhan>().Turrets.Count== 0)//玩家进入怪物的攻击范围
        {
            self.GetComponent<NPCjinzhan>().StopNav();
            self.GetComponent<NPCjinzhan>().SetTransition(Transition.CanAttack);
        }
    }

    public override void Reason(Transform PlayerTransform, Transform BabyTransform, Transform Turret, Transform self)
    {
        if (Vector3.Distance(self.position, PlayerTransform.position) < 2.5f)
        {
            self.GetComponent<NPCjinzhan>().ani.SetBool("Attack", false);
            self.GetComponent<NPCjinzhan>().StopNav();
        }
        else
        {
            self.GetComponent<NPCjinzhan>().StartNav(PlayerTransform);
            Debug.Log(1);

        }
    }
}
