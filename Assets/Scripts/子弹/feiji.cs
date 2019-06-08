using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feiji : MonoBehaviour
{
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
            //掉血
            Destroy(this.gameObject);
        }
    }
    void OnTriggerStay(Collider col)
    {
        //SimpleFSM fsm;
        if (col.gameObject.gameObject.GetComponent<SimpleFSM>())
        {
            //怪物受伤
             col.gameObject.GetComponent<SimpleFSM>().hurt = damage;
            col.gameObject.GetComponent<SimpleFSM>().curState = SimpleFSM.SBState.Hurt;
            Destroy(this.gameObject);
        }
       
    }
}
