using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowBownBullet :BulletManage
{
    /*减速子弹*/
    protected override void SonTriggerStay(Collider col)
    {
        NPCjinzhan npc = col.gameObject.gameObject.GetComponent<NPCjinzhan>();
        if (npc)
        {
            npc.hurt = damage;
            npc.SetTransition(Transition.GetHurt);
            npc.isCtrl = true;
            //Destroy(this.gameObject);
            BulletResources.GetInstance().RecyleBullet(this);

        }
    }
    }
