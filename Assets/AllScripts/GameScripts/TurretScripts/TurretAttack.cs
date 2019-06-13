using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttack : MonoBehaviour {
    protected virtual void Attack() { }

    public int type;

    private GameObject player;
    private 炮塔Manager manager;
    [Header("攻击的目标")]
    public List<GameObject> enemys = new List<GameObject>(); //保存攻击范围内的怪物
    [Header("子弹预制体")]
    public GameObject bullet;
    [Header("枪口位置")]
    public Transform firePos;
    [Header("塔的朝向")]
    public Transform head; 
    private Vector3 pos;  //保存第一个怪物的位置
    public bool bAttack;
    public Animator ani;
    private TurretState turretState;
    [Header("塔的血量")]
    public float Hp = 100;
    private float hp;
    public bool yes;
    public enum TurretState
    {
        idle,
        attack,
        die,
    }
	// Use this for initialization
	void Start () {
        hp = Hp;
        Debug.Log("hp  " + hp + "HP" + Hp);
        manager = GameObject.FindGameObjectWithTag("炮塔").GetComponent<炮塔Manager>();
        ani = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("realPlayer");
        turretState = TurretState.idle;
       transform.position = player.GetComponent<PlayerMove>().nowPos;
    }
    // Update is called once per frame
    void Update () {
        if (Vector3.Distance(this.transform.position, player.transform.position) < 3)
        {
            yes = true;
            //Debug.LogError("附近有炮塔");
        }
        else if(Vector3.Distance(this.transform.position, player.transform.position) >= 3)
        {
            yes = false;
        }
        switch (turretState)
        {
            case TurretState.idle: ani.SetTrigger("Born"); Vector3 lookPos = GameObject.Find("Main Camera").transform.position;lookPos.y  = transform.position.y ; transform.LookAt(lookPos); break;
            case TurretState.attack: lookTargetToAttack(); break;
        }
        Hp -= Time.deltaTime * 2;
        if (Hp <= 0)
        {
            ani.SetBool("Dead", true);
        }
    }
    void lookTargetToAttack()
    {
        if (enemys.Count > 0 && enemys[0] != null)
        {
            pos = enemys[0].transform.position;
            pos.y = head.position.y;
            head.LookAt(pos);
        }
            Attack();
        
    }
    public void CreateShoot()
    {
        bAttack = true;
    }
    //void Attack()
    //{  
    //    if (enemys.Count > 0)
    //    {
    //        if (enemys[0] != null&&enemys[0].GetComponent<NPCjinzhan>().HP>0)
    //        {
    //            ani.SetBool("Attack", true);
    //            if (bAttack)
    //            {
    //                GameObject go = Instantiate(bullet, firePos.position, firePos.rotation);
    //                go.GetComponent<BulletManage>().SetPos(enemys[0].transform.position);
    //                bAttack = false;
    //            }
    //        }
    //        else
    //        {
    //            ani.SetBool("Attack", false);
    //            for (int i= 0; i < enemys.Count-1; i++)
    //            {
    //                if (enemys[i] == null)
    //                {
    //                    if(enemys.Count > 0)
    //                    {
    //                        enemys.Remove(enemys[i]);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
    public void EndBorn()
    {
        turretState = TurretState.attack;
    }
    public void TackDamage(int damage)
    {
        Hp -= damage;
    }
    void Dead()
    {
        Destroy(this.gameObject);
        //回收
     //  TurretResources.GetInstance().RecyleTurret(this);
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "enemy")
        {
            enemys.Add(col.gameObject);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(col.tag == "enemy")
        {
            enemys.Remove(col.gameObject);
        }
    }
}
