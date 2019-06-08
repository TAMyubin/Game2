using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private NPCjinzhan enemy;
    public Transform[] createPosition;
    void Start()
    {

        InvokeRepeating("Create", 1.5f, 3f);//延迟重复调用
    }
    void Create()
    {
        int a = Random.Range(0, createPosition.Length);//随机选取位置数组的下标
        int b = Random.Range(0, 4);
        enemy = EnemyResource.GetInstance().CreateEnemy(b);
        enemy.transform.position = createPosition[a].position;
 
    }
}
