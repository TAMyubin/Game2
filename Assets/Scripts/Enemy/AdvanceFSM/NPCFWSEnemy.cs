using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFWSEnemy : NPCjinzhan
{
    private new BulletManage bullet;
    protected override void SonAttackStart()
    {
        bullet = BulletResources.GetInstance().CreatBullet(4);
          //  GameObject go = Instantiate(bullet, transform.position, transform.rotation);
            bullet.GetComponent<OrdinaryEnemyBullet>().SetPos(target(temp).position);
        bullet.transform.position = this.transform.position;
        bullet.transform.rotation = this.transform.rotation;
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
