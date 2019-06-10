using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdinaryBullet : BulletManage
{
    /*普通子弹（挂到飞机和普通炮塔上）*/
    protected override void SonTriggerStay(Collider col)
    {
     
        if (col.gameObject.gameObject.GetComponent<NPCjinzhan>())
        {
            Debug.Log("zhongdan!!!!!!!!!!!!!");
            col.gameObject.gameObject.GetComponent<NPCjinzhan>().hurt = damage;
            col.gameObject.gameObject.GetComponent<NPCjinzhan>().SetTransition(Transition.GetHurt);
            //Destroy(this.gameObject);
            //回收
            BulletResources.GetInstance().RecyleBullet(this);
        }
    }
}
