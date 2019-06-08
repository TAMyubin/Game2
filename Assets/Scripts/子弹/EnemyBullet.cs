using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10;
    public int damage = 20;
    private Vector3 temp;
    // Start is called before the first frame update

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
        if (col.gameObject.gameObject.tag == "realPlayer")
        {
            col.gameObject.GetComponent<PlayHP>().TackDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
