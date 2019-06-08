using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SimpleFSM : MonoBehaviour
{
    protected virtual void SonStart() { }//子函数
    protected virtual void SonUpdate() { }//子函数
    protected virtual void SonAttackStart() { }//子函数
    protected virtual void SonAttackEnd() { }//子函数
    protected virtual void SonAttackState() { }
    public enum SBState
    {
        None,
        ChaseBaby,
        Chase,
        Attack,
        Hurt,
        Dead,
    }
    protected Transform PlayerTransform;//玩家的位置
    private Vector3 destPos;//目标位置
    [Header("奖励分值")]
    public int num;
    private 炮塔Manager jiafen;
    [Header("当前状态")]
    public SBState curState;
    /*追逐状态需要的变量*/
    [Header("移动速度")]
    public float curSpeed;
    private Transform Player;//主角的位置
    private GameObject objPlayer;//获取主角属性
    private NavMeshAgent EnermyAgent;
    private Animator ani;
    private bool bDead;//死了没
    /*追逐Baby的变量*/
    [Header("与baby的距离")]
    public float dist;
    [Header("与玩家的距离")]
    public float dost;
    /*攻击状态需要的变量*/
    [Header("攻击距离")]
    public float dis;
    [Header("近战攻击力")]
    public int damage;
    [Header("攻击子弹")]
    public GameObject bullet;
    [Header("攻击CD（频率）")]
    public float shootRate;//限制多少秒后才能攻击
    private bool bAttack;//怪物是否进入攻击状态
    public float elsapeedTime;
    private float time = 0;
    /*受伤变量*/
    [Header("怪物血量")]
    public int HP;
    [Header("怪物受到的伤害")]
    public int hurt;
    /*死亡变量*/
    private GameObject Baby;
    void Start()
    {
        objPlayer = GameObject.FindGameObjectWithTag("realPlayer");//获取玩家
        Baby = GameObject.FindGameObjectWithTag("Baby");//获取baby
        PlayerTransform = objPlayer.transform;//玩家位置


        jiafen = GameObject.FindGameObjectWithTag("炮塔").GetComponent<炮塔Manager>();
        EnermyAgent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        curState = SBState.ChaseBaby;//当前状态为追逐baby
        bDead = false;
        elsapeedTime = shootRate;

        SonStart();
    }
    // Update is called once per frame
    void Update()
    {
        if (Baby.GetComponent<BabyHp>().end ==false)//游戏未结束前提下
        {
            SonUpdate();
            switch (curState)
            {
                case SBState.ChaseBaby:UpdateBabyChaseState();break;
                case SBState.Chase: UpdateChaseState(); break;
                case SBState.Attack: UpdateAttackState(); break;
                case SBState.Hurt: UpdateHurtState(hurt); break;
                case SBState.Dead: UpdateDeadState(); break;
            }
        }
    }
    /// <summary>
    /// 追逐baby状态
    /// </summary>
    private void UpdateBabyChaseState()
    {
        dist = Vector3.Distance(transform.position, Baby.transform.position);
        dost = Vector3.Distance(transform.position, PlayerTransform.position);
        if (dist < dost)//如果怪物距离baby较近
        {
            destPos = Baby.transform.position;
            destPos.y = 0;
        }
        else if (dist >= dost)
        {
            curState = SBState.Chase;
        }
        //进入攻击范围
        if (dist < 2.5f)
        {
            ani.SetBool("Move", false);
            EnermyAgent.Stop();//停止寻路
            Destroy(this.gameObject);
            Baby.GetComponent<BabyHp>().iskouxue = true;
        }
        else//脱离攻击范围，继续追击
        {
            EnermyAgent.isStopped = false;//开启寻路
            EnermyAgent.SetDestination(destPos);//设置寻路的终点为主角的当前位置
            transform.LookAt(destPos);
            ani.SetBool("Move", true);
        }
    }
    /// <summary>
    /// 追逐玩家状态
    /// </summary>
    private void UpdateChaseState()
    {
        dist = Vector3.Distance(transform.position, Baby.transform.position);
        dost = Vector3.Distance(transform.position, PlayerTransform.position);
        if (dist < dost)//如果怪物距离baby较近
        {
            curState = SBState.ChaseBaby;
        }
        else if (dist >= dost)
        {
            destPos = PlayerTransform.transform.position;
            destPos.y = 0;
        }
        //进入攻击范围
        if (dost < dis)
            {
                ani.SetBool("Move", false);
                EnermyAgent.Stop();//停止寻路
                curState = SBState.Attack;
            }
            else//脱离攻击范围，继续追击
            {
                EnermyAgent.isStopped = false;//开启寻路
                EnermyAgent.SetDestination(destPos);//设置寻路的终点为主角的当前位置
                transform.LookAt(destPos);
                ani.SetBool("Move", true);
            }
    }
    /// <summary>
    /// 攻击状态
    /// </summary>
    private void UpdateAttackState()
    {
        transform.LookAt(PlayerTransform);
        elsapeedTime += Time.deltaTime;
        if (elsapeedTime >= shootRate)
        {
            SonAttackState();
            ani.SetBool("Attack", true);
            elsapeedTime = 0;
        }
        /*距离拉开后变为追逐模式*/
        destPos = PlayerTransform.position;
        float dist = Vector3.Distance(transform.position, destPos);
        if (dist > dis)
        {
            curState = SBState.Chase;
            ani.SetBool("Attack", false);
        }
    }
    /// <summary>
    /// 受伤方法
    /// </summary>
    private void UpdateHurtState(int hurt)
    {
        HP -= hurt;//掉血
        if (HP > 0)
        {
            curState = SBState.Chase;

        }
        else if (HP <= 0)
        {
            bDead = true;
            curState = SBState.Dead;
        }
    }
    /// <summary>
    /// 死亡方法
    /// </summary>
    private void UpdateDeadState()
    {
       // transform.Translate(Vector3.down * Time.deltaTime*3);//怪物的尸体往下沉
        time += Time.deltaTime;
        if (time >= 3)
        {
            jiafen.jia = true;
            jiafen.num = num;
            Destroy(this.gameObject);
        }
        if (bDead)
        {
            ani.SetTrigger("Death");
            bDead = false;
        }
    }
    /*攻击那一刹*/
    public void AttackStart()
    {
        bAttack = true;
        SonAttackStart();

        //GameObject go = Instantiate(bullet, transform.position, transform.rotation);
        //go.GetComponent<EnemyBullet>().SetPos(PlayerTransform.position);
    }
    /*怪物攻击后摇*/
    public void AttackEnd()
    {
        SonAttackEnd();
        ani.SetBool("Attack", false);
        bAttack = false;
    }
    /*近战调用*/
    void OnTriggerStay(Collider col)
    {
        if (bAttack)
        {
            if (col.gameObject.tag == "realPlayer")
            {
                transform.LookAt(PlayerTransform);
                col.gameObject.GetComponent<PlayHP>().TackDamage(damage);
                bAttack = false;
            }
        }
    }
}
