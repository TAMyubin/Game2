using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManage : MonoBehaviour
{

    protected virtual void SonTriggerStay(Collider col) { }

    public int type;
    public float speed = 10;
    public int damage = 20;
    private Vector3 temp;
    public void SetPos(Vector3 target)
    {
        temp = target;
    }
    // Update is called once per frame
    void Update()
    {
        if (temp == null)
        {
            return;
        }
        transform.LookAt(temp);//子弹朝向目标

        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (Vector3.Distance(transform.position, temp) < 0.5f)
        {

          // Destroy(this.gameObject);
            //回收
            BulletResources.GetInstance().RecyleBullet(this);
        }
    }
    void OnTriggerStay(Collider col)
    {
        SonTriggerStay(col);
    }
}
