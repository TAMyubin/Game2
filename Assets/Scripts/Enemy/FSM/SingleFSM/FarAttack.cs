using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FarAttack : MonoBehaviour
{
   
    protected Transform PlayerTransform;//玩家的位置

    public Vector3 destPos;//当前位置
    public enum SBState
    {
        None,
        Chase,
        Attack,
        Hurt,
        Dead,
    }
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
                       /*攻击状态需要的变量*/
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
    private CapsuleCollider cap;

    // Start is called before the first frame update
    void Start()
    {
        jiafen = GameObject.FindGameObjectWithTag("炮塔").GetComponent<炮塔Manager>();
        curState = SBState.Chase;//当前状态为追逐
        bDead = false;
        elsapeedTime = shootRate;
        objPlayer = GameObject.FindGameObjectWithTag("realPlayer");
        PlayerTransform = objPlayer.transform;//玩家位置
        EnermyAgent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        cap = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (objPlayer.GetComponent<PlayHP>().HP > 0)//在主角大于0的前提下
        {
            switch (curState)
            {
                case SBState.Chase: UpdateChaseState(); break;
                case SBState.Attack: UpdateAttackState(); break;
                case SBState.Hurt: UpdateHurtState(hurt); break;
                case SBState.Dead: UpdateDeadState(); break;
            }
        }
    }
    /// <summary>
    /// 追逐状态
    /// </summary>
    private void UpdateChaseState()
    {
        destPos = PlayerTransform.position;
        float dist = Vector3.Distance(transform.position, destPos);
        if (objPlayer.GetComponent<PlayHP>().HP > 0)//在主角大于0的前提下
        {
            destPos.y = 0;
            //进入攻击范围
            if (dist < 10)
            {
                ani.SetBool("Move", false);
                EnermyAgent.Stop();//停止寻路
                curState = SBState.Attack;
            }
            else//脱离攻击范围，继续追击
            {
                EnermyAgent.isStopped = false;//开启寻路
                EnermyAgent.SetDestination(destPos);//设置寻路的终点为主角的当前位置
                transform.LookAt(Player);
                ani.SetBool("Move", true);
                curState = SBState.Chase;
            }
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
            ani.SetBool("Attack", true);
        }
        /*距离拉开后变为追逐模式*/
        destPos = PlayerTransform.position;
        float dist = Vector3.Distance(transform.position, destPos);
        if (dist > 10)
        {
            curState = SBState.Chase;
            ani.SetBool("Attack", false);
        }
    }
    /*攻击那一刹*/
    public void AttackStart()
    {
        bAttack = true;
        GameObject go = Instantiate(bullet, transform.position, transform.rotation);
        go.GetComponent<EnemyBullet>().SetPos(PlayerTransform.position);
    }
    /*怪物攻击后摇*/
    public void AttackEnd()
    {
        ani.SetBool("Attack", false);
        bAttack = false;
        elsapeedTime = 0;
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
       
        //transform.Translate(Vector3.down * Time.deltaTime*3);//怪物的尸体往下沉
        time += Time.deltaTime;
        if (time >= 3)
        {
            Destroy(this.gameObject);
        }
        if (bDead)
        {
            jiafen.jia = true;
            jiafen.num = num;
            ani.SetTrigger("Death");
            cap.enabled = false;
            bDead = false;
        }

    }
}
