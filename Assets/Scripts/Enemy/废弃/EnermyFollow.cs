using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnermyFollow : MonoBehaviour {
    private Transform Player;//主角的位置
   private NavMeshAgent EnermyAgent;
    private Animator ani;

    void Start () {
        Player = GameObject.FindWithTag("realPlayer").transform;//通过标签查找player
        EnermyAgent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
	}
	// Update is called once per frame
	void LateUpdate () {
        if (Player.GetComponent<PlayHP>().HP > 0)//在主角大于0的前提下
        {
            Vector3 pos = Player.transform.position;
            pos.y = 0;

            //进入攻击范围
            if (Vector3.Distance(transform.position, Player.position) < 2)
            {
                ani.SetBool("Move", false);
                EnermyAgent.Stop();//停止寻路
            }
            else//脱离攻击范围，继续追击
            {
                EnermyAgent.isStopped = false;//开启寻路
                EnermyAgent.SetDestination(pos);//设置寻路的终点为主角的当前位置
                transform.LookAt(Player);
                ani.SetBool("Move", true);
            }
        }

	}
}
