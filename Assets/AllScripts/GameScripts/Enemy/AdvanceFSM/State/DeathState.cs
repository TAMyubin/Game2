using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : FSMState
{
    public DeathState()
    {
        stateID = FSMStateID.Dead;
    }
    public override void Act(Transform PlayerTransform, Transform BabyTransform, Transform Turret, Transform self)
    {
      
    }

    public override void Reason(Transform PlayerTransform, Transform BabyTransform, Transform Turret, Transform self)
    {
        self.GetComponent<NPCjinzhan>().Death();
    }
}
