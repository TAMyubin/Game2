using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretResources
{
    public List<TurretAttack> Deaths = new List<TurretAttack>();
    public List<TurretAttack> Lives = new List<TurretAttack>();
    private GameObject[] Turrets = new GameObject[3];//3种类型的敌机
    private static TurretResources _instance;

    public static TurretResources GetInstance()
    {
        if (_instance == null)
        {
            _instance = new TurretResources();
        }
        return _instance;
    }
    private TurretResources()
    {
        for (int i = 0; i < Turrets.Length; i++)
        {
            string path = "Turret/Turret" + i;//资源的路径
            Turrets[i] = Resources.Load(path) as GameObject;
        }
    }
    /// <summary>
    /// 生成炮塔
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public TurretAttack CreatTurret(int type)
    {
        TurretAttack Turret = null;
        if (Deaths.Count != 0)
        {
            foreach (var e in Deaths)//遍历死亡列表
            {
                if (e.type == type)//如果死亡列表中敌机的类型与需要生产的敌机类型相同
                {
                    Turret = e;
                    Deaths.Remove(Turret);
                    Turret.gameObject.SetActive(true);
                    break;
                }
            }
        }
        if (Turret == null)
        {
            GameObject go = GameObject.Instantiate(Turrets[type]);
            Turret = go.GetComponent<TurretAttack>();
        }
        Turret.type = type;
        Lives.Add(Turret);
        return Turret;
    }
    public void RecyleTurret(TurretAttack turret)
    {
        turret.gameObject.SetActive(false);
        Lives.Remove(turret);
        Deaths.Add(turret);
    }
}
