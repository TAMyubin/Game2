using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : FSMState
{
  public HurtState()
    {
        stateID = FSMStateID.Hurt;
    }
    public override void Act(Transform PlayerTransform, Transform BabyTransform, Transform Turret, Transform self)
    {
       if (self.GetComponent<NPCjinzhan>().HP <= 0)
        {
            self.GetComponent<NPCjinzhan>().bDead = true;
          self.GetComponent<NPCjinzhan>().SetTransition(Transition.NoHp);
        }
        else 
        {
            self.GetComponent<NPCjinzhan>().SetTransition(Transition.disshort);
        }
    
    }
    public override void Reason(Transform PlayerTransform, Transform BabyTransform, Transform Turret, Transform self)
    {
        self.GetComponent<NPCjinzhan>().HP -= self.GetComponent<NPCjinzhan>().hurt;
        Debug.Log("中弹了！！！！！！！！！！！！！！！！！！");
    }
}
