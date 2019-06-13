using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTurretState : FSMState
{
    public ChaseTurretState ()
    {
        stateID = FSMStateID.ChaseTurret;
    }
    public override void Act(Transform PlayerTransform, Transform BabyTransform, Transform Turret, Transform self)
    {
        float dost = Vector3.Distance(self.position, Turret.position);
        if (self.GetComponent<NPCjinzhan>().Turrets.Count > 0&&dost < self.GetComponent<NPCjinzhan>().dis)
        {
            self.GetComponent<NPCjinzhan>().StopNav();
            self.GetComponent<NPCjinzhan>().SetTransition(Transition.CanAttackTurret);
        }
       else if (self.GetComponent<NPCjinzhan>().Turrets.Count == 0 )
        {
            self.GetComponent<NPCjinzhan>().StopNav();
            self.GetComponent<NPCjinzhan>().SetTransition(Transition.disshort);
        }


    }
    public override void Reason(Transform PlayerTransform, Transform BabyTransform, Transform Turret, Transform self)
    {
        if (Vector3.Distance(self.position, Turret.position) < 2.5f)//怪物与炮塔距离小于怪物攻击的攻击范围
        {
            self.GetComponent<NPCjinzhan>().StopNav();
        }
        else
        {
            self.GetComponent<NPCjinzhan>().StartNav(Turret);
        }
        
    
    }
}
