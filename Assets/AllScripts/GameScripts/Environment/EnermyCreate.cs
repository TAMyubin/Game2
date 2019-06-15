using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyCreate : MonoBehaviour {
    public Transform[] createPosition;
    public GameObject[] EneryPrefabs;

	void Start () {
  
        InvokeRepeating("Create", 1.5f, 3f);//延迟重复调用
    }
    void Create()
    {
            int a = Random.Range(0, createPosition.Length);//随机选取位置数组的下标
            int b = Random.Range(0, EneryPrefabs.Length);//随机选取预制体数组的下标
            GameObject go = Instantiate(EneryPrefabs[b], createPosition[a].position, createPosition[a].rotation);
            //通过随机选组的数组元素来实例化怪物
        
    }
}
