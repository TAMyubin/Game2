using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyResource
{
    private List<NPCjinzhan> Lives = new List<NPCjinzhan>();//生存列表 在使用的资源
    private List<NPCjinzhan> Deaths = new List<NPCjinzhan>();//死亡列表 暂时不用的列表
    private GameObject[] enemyPrefabs = new GameObject[4];//4种怪物
    private static EnemyResource _instance;//单例
    public static EnemyResource GetInstance()
    {
        if (_instance == null)
        {
            _instance = new EnemyResource();
        }
        return _instance;
    }
    private EnemyResource()//构造函数
    {
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            string path = "enemy/enemy" + i;//资源的路径
            enemyPrefabs[i] = Resources.Load(path) as GameObject;
        }
    }
    /// <summary>
    /// 产生怪物
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public NPCjinzhan CreateEnemy(int type)
    {
        NPCjinzhan enemy = null;
        if (Deaths.Count != 0)
        {
            foreach (var e in Deaths)//遍历死亡列表
            {
                if (e.type == type)//如果死亡列表中敌机的类型与需要生产的敌机类型相同
                {
                    enemy = e;
                    Deaths.Remove(enemy);
                    enemy.gameObject.SetActive(true);
                    break;
                }
            }
        }
        if (enemy == null)
        {
            GameObject go = GameObject.Instantiate(enemyPrefabs[type]);
            enemy = go.GetComponent<NPCjinzhan>();
        }
        enemy.type = type;
        Lives.Add(enemy);
        return enemy;
    }
    /// <summary>
    /// 回收怪物
    /// </summary>
    /// <param name="enemy"></param>
    public void RecyleEnemy(NPCjinzhan enemy)
    {
        enemy.gameObject.SetActive(false);
        Lives.Remove(enemy);
        Deaths.Add(enemy);
    }
}
