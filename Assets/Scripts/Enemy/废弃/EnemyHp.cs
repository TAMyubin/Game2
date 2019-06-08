using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHp : MonoBehaviour {
    public int hp = 100;
    private Animator ani;
    private NavMeshAgent agent;//寻路
    private EnermyFollow enermy;
    private CapsuleCollider cap;

    public 炮塔Manager manager;

	// Use this for initialization
	void Start () {
        ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        enermy = GetComponent<EnermyFollow>();
        cap = GetComponent<CapsuleCollider>();

        manager = GameObject.Find("炮塔管理").GetComponent<炮塔Manager>();//单纯为了加分
	}
    public void TakeDamage(int damage)//受伤
    {
        if (hp <= 0)
        {
            return;//跳出受伤函数
        }
        hp -= damage;//掉血

        if (hp <= 0)
        {
         
            Dead();
       
    
        }
    }
    ///<summary>
    ///敌人死亡
    /// </summary>
    /// 
    void Dead()
    {
        transform.Translate(Vector3.down * Time.deltaTime);//怪物的尸体往下沉
        if (transform.position.y < -2.2f)
        {
            Destroy(this.gameObject);

        }
        agent.enabled = false;
        enermy.enabled = false;
        ani.SetTrigger("Death");
        cap.enabled = false;
    }
}
