using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFWSEnemy : NPCjinzhan
{
   
    protected override void SonAttackStart()
    {
        GameObject go = Instantiate(bullet, transform.position, transform.rotation);
        go.GetComponent<OrdinaryEnemyBullet>().SetPos(target(temp).position);
        //BulletManage bullet;
        //bullet = BulletResources.GetInstance().CreatBullet(4);
        //bullet.GetComponent<OrdinaryEnemyBullet>().SetPos((temp).position);
        //bullet.transform.position = this.transform.position;
        //bullet.transform.rotation = this.transform.rotation;

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
