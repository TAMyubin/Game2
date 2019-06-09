using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFWSEnemy : NPCjinzhan
{
   
    protected override void SonAttackStart()
    {
         GameObject go = Instantiate(bullet, transform.position, transform.rotation);
        go.GetComponent<OrdinaryEnemyBullet>().SetPos(target(temp).position);

    }
    Transform target(Transform temp)
    {
        if (temp == null)
        {
            return PlayerTransform;
        }
        else
        {
            return temp;
        }
    }
}
