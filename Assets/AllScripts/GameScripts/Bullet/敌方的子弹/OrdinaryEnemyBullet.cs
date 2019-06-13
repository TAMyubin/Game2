using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdinaryEnemyBullet : BulletManage
{
    /*普通子弹挂到怪物上*/
    protected override void SonTriggerStay(Collider col)
    {
        if (col.gameObject.gameObject.tag == "realPlayer")
        {
            col.gameObject.GetComponent<PlayHP>().TackDamage(damage);
           Destroy(this.gameObject);
            //回收
       
        }
    }
}
