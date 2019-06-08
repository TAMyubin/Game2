using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletResources
{
    private List<BulletManage> Deaths = new List<BulletManage>();
    public List<BulletManage> Lives = new List<BulletManage>();
    private GameObject[] Bullets = new GameObject[4];//3种类型的敌机
    private static BulletResources _instance;

    public static BulletResources GetInstance()
    {
        if (_instance == null)
        {
            _instance = new BulletResources();
        }
        return _instance;
    }
    private BulletResources()
    {
        for (int i = 0; i < Bullets.Length; i++)
        {
            string path = "Bullet/Bullet" + i;//资源的路径
            Bullets[i] = Resources.Load(path) as GameObject;
        }
    }
    /// <summary>
    /// 生成炮塔
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public BulletManage CreatBullet(int type)
    {
        BulletManage Bullet = null;
        if (Deaths.Count != 0)
        {
            foreach (var e in Deaths)//遍历死亡列表
            {
                if (e.type == type)//如果死亡列表中敌机的类型与需要生产的敌机类型相同
                {
                    Bullet = e;
                    Deaths.Remove(Bullet);
                    Bullet.gameObject.SetActive(true);
                    break;
                }
            }
        }
        if (Bullet == null)
        {
            GameObject go = GameObject.Instantiate(Bullets[type-1]);
            Bullet = go.GetComponent<BulletManage>();
        }
        Bullet.type = type;
        Lives.Add(Bullet);
        return Bullet;
    }
    public void RecyleBullet(BulletManage Bullet)
    {
        Bullet.gameObject.SetActive(false);
        Lives.Remove(Bullet);
        Deaths.Add(Bullet);
    }
}
