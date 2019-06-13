using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBabyState :FSMState
{
    /*追逐baby状态*/
  
 public ChaseBabyState( )
    {
        stateID = FSMStateID.ChaseBaby;
    }
    public override void Reason(Transform PlayerTransform, Transform BabyTransform, Transform Turret, Transform self)
    {
        if (self.GetComponent<NPCjinzhan>().Turrets.Count>0)
        {
            self.GetComponent<NPCjinzhan>().StopNav();
            self.GetComponent<NPCjinzhan>().SetTransition(Transition.FineTurret);
        }
        float  dist = Vector3.Distance(self.position, BabyTransform.position);
       float dost = Vector3.Distance(self.position, PlayerTransform.position);
        if (dist >= dost)//如果怪物距离玩家较近
        {
            Debug.Log("转换到追逐baby");
            self.GetComponent<NPCjinzhan>().StopNav();
            self.GetComponent<NPCjinzhan>().SetTransition(Transition.disshort);
        }
        
    }//控制一个状态转换到另一个状态
    public override void Act(Transform PlayerTransform, Transform BabyTransform, Transform Turret, Transform self)
    {
        if (Vector3.Distance(self.position, BabyTransform.position) < 2f)
        {
            self.GetComponent<NPCjinzhan>().StopNav();
        }
        else
        {
            self.GetComponent<NPCjinzhan>().StartNav(BabyTransform);
        }
    }
}
