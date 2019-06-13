using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTurret : FSMState
{
    public AttackTurret()
    {
        stateID = FSMStateID.AttackTurret;
    }
    public override void Act(Transform PlayerTransform, Transform BabyTransform, Transform Turret, Transform self)
    {
       if (self.GetComponent<NPCjinzhan>().Turrets.Count == 0|| Turret.GetComponent<TurretAttack>().Hp<=0)
        {
          
            self.GetComponent<NPCjinzhan>().SetTransition(Transition.FineTurret);
            Debug.Log("退出攻击炮塔，转为攻击玩家");
        }
    
    }

    public override void Reason(Transform PlayerTransform, Transform BabyTransform, Transform Turret, Transform self)
    {
            self.LookAt(Turret);
            self.GetComponent<NPCjinzhan>().Attack();
            self.GetComponent<NPCjinzhan>().temp = Turret;
      
    }
}
