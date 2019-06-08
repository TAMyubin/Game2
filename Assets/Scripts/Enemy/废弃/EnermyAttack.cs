using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyAttack : MonoBehaviour {
    public float attackTime = 2;
    private float timer = 0;
    public int damage = 10;
	// Use this for initialization
	void Start () {
        timer = attackTime;
	}
    void OnCollisionStay(Collision collision)
    {
        timer += Time.deltaTime;
        if(collision.gameObject.tag == "realPlayer")
        {
            timer += Time.deltaTime;
            if(timer>=attackTime)//达到攻击的时间就开始掉血
            {
                timer -= attackTime;//掉血
                collision.gameObject.GetComponent<PlayHP>().TackDamage(damage);
            }
        }
    }
}
