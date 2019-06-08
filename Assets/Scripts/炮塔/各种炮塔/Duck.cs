using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : TurretAttack
{
    private new BulletManage bullet;
    // TurretAttack TurretAttack = new TurretAttack();
    protected override void Attack()
    {
          if (enemys.Count > 0)
        {
            if (enemys[0] != null && enemys[0].GetComponent<NPCjinzhan>().HP > 0)
            {
             ani.SetBool("Attack", true);
                if (bAttack)
                {
                    //GameObject go = Instantiate(bullet, firePos.position, firePos.rotation);
                    //go.GetComponent<BulletManage>().SetPos(enemys[0].transform.position);
                    //bullet = BulletResources.GetInstance().CreatBullet(2);
                    //bullet.GetComponent<OrdinaryBullet>().SetPos(enemys[0].transform.position);
                    //bullet.transform.position = this.transform.position;
                    //bullet.transform.rotation = this.transform.rotation;
                    bAttack = false;
                }
            }
            else if(enemys[0].GetComponent<NPCjinzhan>().HP <= 0)
            {
                ani.SetBool("Attack", false);
                for (int i = 0; i < enemys.Count - 1; i++)
                {
                    if (enemys[i] == null)
                    {
                        if (enemys.Count > 0)
                        {
                            enemys.Remove(enemys[i]);
                        }
                    }
                }
            }
        }
    }
}
