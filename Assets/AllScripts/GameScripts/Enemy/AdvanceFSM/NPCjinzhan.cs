using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPCjinzhan : AdvancedFSM
{
    protected virtual void SonStart() { }//子函数
    protected virtual void SonUpdate() { }//子函数
    protected virtual void SonAttackStart() { }//子函数
    protected virtual void SonAttackEnd() { }//子函数
    /*具体的实现类*/
    GameObject objPlayer;
    GameObject Baby;
    Transform tr;
    public int type;
    public List<GameObject> Turrets = new List<GameObject>(); //保存攻击范围内的炮塔
    private NavMeshAgent EnermyAgent;
    public Animator ani;
    [Header("移动速度")]
    public float curSpeed;//移动的速度
    /*攻击状态需要的变量*/
    [Header("近战攻击力")]
    public int damage;
    [Header("攻击CD（频率）")]
    public float shootRate;//限制多少秒后才能攻击
    public float elsapeedTime;
    private float time = 0;
     public bool bAttack;//怪物是否进入攻击状态
    [Header("攻击距离")]
    public float dis;
    [Header("远战小兵的攻击子弹")]
    public GameObject bullet;

    /*受伤状态及死亡需要的变量*/
    [Header("被控制时长")]
    public float bCtrl;
    public bool isCtrl;
    private float bctrltime;
    [Header("血量值")]
    public int HP;
    private int hp;
  
    private 炮塔Manager manage;
    [Header("得到的分数（金钱）")]
    public int num;

    [Header("怪物受到的伤害（传参用 下面的不用改）")]
    public int hurt;
    public bool bDead;//死了没
    public Transform temp ;


    protected override void Initialize() {
        hp = HP;
        objPlayer = GameObject.FindGameObjectWithTag("realPlayer");//获取玩家

        Baby = GameObject.FindGameObjectWithTag("Baby");//获取baby



        PlayerTransform = objPlayer.transform;//玩家位置
        BabyTransform = Baby.transform;//baby位置
        temp = PlayerTransform;
        EnermyAgent = GetComponent<NavMeshAgent>();//获取寻路组件
        ani = GetComponent<Animator>();//获取动画状态机

        manage = GameObject.FindGameObjectWithTag("炮塔").GetComponent<炮塔Manager>();

        EnermyAgent.speed = curSpeed;

        ConstructFSM();
       
    }
    /// <summary>
    /// 把所有的状态以及所有的状态转换条件创建出来
    /// </summary>
    protected void ConstructFSM()
    {
        ChaseBabyState chasebaby = new ChaseBabyState();//创建追逐baby状态
        chasebaby.AddTransition(Transition.disshort, FSMStateID.Chasing);
        chasebaby.AddTransition(Transition.GetHurt, FSMStateID.Hurt);
        chasebaby.AddTransition(Transition.FineTurret, FSMStateID.ChaseTurret);


        ChasePlayerState chasePlayerState = new ChasePlayerState();
        chasePlayerState.AddTransition(Transition.dislong, FSMStateID.ChaseBaby);
        chasePlayerState.AddTransition(Transition.CanAttack, FSMStateID.Attack);
        chasePlayerState.AddTransition(Transition.GetHurt, FSMStateID.Hurt);
        chasePlayerState.AddTransition(Transition.FineTurret, FSMStateID.ChaseTurret);


        Attack attackState = new Attack();
        attackState.AddTransition(Transition.disshort, FSMStateID.Chasing);
        attackState.AddTransition(Transition.GetHurt, FSMStateID.Hurt);


        HurtState hurtState = new HurtState();
        hurtState.AddTransition(Transition.disshort, FSMStateID.Chasing);
        hurtState.AddTransition(Transition.NoHp, FSMStateID.Dead);

        DeathState deathState = new DeathState();


        ChaseTurretState chaseTurretState = new ChaseTurretState();
        chaseTurretState.AddTransition(Transition.CanAttackTurret, FSMStateID.AttackTurret);
        chaseTurretState.AddTransition(Transition.disshort, FSMStateID.Chasing);
        chaseTurretState.AddTransition(Transition.GetHurt, FSMStateID.Hurt);


        AttackTurret attackTurret = new AttackTurret();
        attackTurret.AddTransition(Transition.FineTurret, FSMStateID.ChaseTurret);
        attackTurret.AddTransition(Transition.GetHurt, FSMStateID.Hurt);


        AddFSMState(chasebaby);//并添加到list集合中
        AddFSMState(chasePlayerState);
        AddFSMState(attackState);
        AddFSMState(hurtState);
        AddFSMState(deathState);
        AddFSMState(chaseTurretState);
        AddFSMState(attackTurret);
        SonStart();
    }

    protected override void FSMUpdate() {

        if (Turrets.Count > 0)
        {for(int i = 0;i<Turrets.Count;i++)
            if (Turrets[i].GetComponent<TurretAttack>().Hp <= 0)
            {
                Turrets.Remove(Turrets[i]);
                if (Turrets.Count <= 0)
                {
                    return;
                }
            }
            CurrentState.Reason(PlayerTransform, BabyTransform, Turrets[0].transform, transform);
            CurrentState.Act(PlayerTransform, BabyTransform, Turrets[0].transform, transform);
        }
        else
        {
            CurrentState.Reason(PlayerTransform, BabyTransform, PlayerTransform, transform);
            CurrentState.Act(PlayerTransform, BabyTransform, PlayerTransform, transform);
        }

        SonUpdate();
  
        recovery();
    }

   public void SetTransition(Transition t)//进行状态转换
    {
        PerformTransition(t);
    }


    #region 追逐状态进行寻路的方法
    public void StartNav(Transform target)//开启寻路
    {
        EnermyAgent.isStopped = false;//开启寻路
        EnermyAgent.SetDestination(target.position);//设置寻路的终点为主角的当前位置
       // transform.LookAt(target);
        ani.SetBool("Move", true);
    }
    public void StopNav()//停止寻路
    {
        EnermyAgent.isStopped = true ;//停止寻路
        ani.SetBool("Move", false);
    }
    #endregion

    #region 攻击状态进行攻击的方法
    /// <summary>
    /// 攻击具体方法
    /// </summary>
    public void Attack()
    {
        elsapeedTime += Time.deltaTime;
        if (elsapeedTime >= shootRate)
        {
            ani.SetBool("Attack", true);
            elsapeedTime = 0;
        }
    }

    /// <summary>
    /// 攻击瞬间
    /// </summary>
    public void AttackStart()
    {
        bAttack = true;
        SonAttackStart();
        //GameObject go = Instantiate(bullet, transform.position, transform.rotation);
        //go.GetComponent<EnemyBullet>().SetPos(PlayerTransform.position);
    }
    /// <summary>
    /// 攻击后摇
    /// </summary>
    public void AttackEnd()
    {
        ani.SetBool("Attack", false);
        bAttack = false;
        SonAttackEnd();
    }

    /// <summary>
    /// 碰撞
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Baby")//碰撞到Baby
        {
            Destroy(gameObject);
            //回收
            //EnemyResource.GetInstance().RecyleEnemy(this);
            Baby.GetComponent<BabyHp>().iskouxue = true;
        }
        if (bAttack)//近战攻击玩家
        {
            if (col.gameObject.gameObject.tag == "realPlayer")
            {
                col.gameObject.GetComponent<PlayHP>().TackDamage(damage);
                bAttack = false;
                ani.SetBool("Attack", false);
            }
            if (bAttack)//近战攻击炮塔
            {
                if (temp.tag == "Turret")
                {
                    temp.gameObject.GetComponent<TurretAttack>().TackDamage(damage);
                    ani.SetBool("Attack", false);
                    bAttack = false;
                }
            }
        }
    }
    #endregion
    /// <summary>
    /// 被控制
    /// </summary>
    void recovery()
    {
        if (isCtrl)
        {
            bctrltime += Time.deltaTime;
            if (bctrltime >= bCtrl)
            {
               EnermyAgent.speed = curSpeed;
                bctrltime = 0;
                isCtrl = false;

            }
            else
            {
                EnermyAgent.speed = 0;
            }
        }
    }

    public void Death()
    {
        if (bDead)
        {
            ani.SetTrigger("Death");
            manage.jia = true;
            manage.num = num;
            bDead = false;
   
        }
        time += Time.deltaTime;
        if (time >= 2)
        {

              Destroy(this.gameObject);
            //回收
           // EnemyResource.GetInstance().RecyleEnemy(this);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        
        if (col.tag == "Turret")
        {
            Turrets.Add(col.gameObject);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Turret")
        {
            Turrets.Remove(col.gameObject);
        }
    }
}
