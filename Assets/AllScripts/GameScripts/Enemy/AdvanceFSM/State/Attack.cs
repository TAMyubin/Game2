using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : FSMState
{
    public Attack()
    {
        stateID = FSMStateID.Attack;
    }
    public override void Act(Transform PlayerTransform, Transform BabyTransform,Transform Turret, Transform self)
    {
        destPos = PlayerTransform.position;
        float dist = Vector3.Distance(self.position, PlayerTransform.position);
        if (dist > self.GetComponent<NPCjinzhan>().dis)
        {
            self.GetComponent<NPCjinzhan>().bAttack = false;
            self.GetComponent<NPCjinzhan>().ani.SetBool("Attack", false);
            self.GetComponent<NPCjinzhan>().SetTransition(Transition.disshort);
        }
    }
    public override void Reason(Transform PlayerTransform, Transform BabyTransform, Transform Turret, Transform self)
    {
 
        self.LookAt(PlayerTransform);
        self.GetComponent<NPCjinzhan>().Attack();
    
    }
}
