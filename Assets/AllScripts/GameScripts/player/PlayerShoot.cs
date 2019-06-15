using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {
   
    
    [Header("技能指向")]
    public bool ismiaozhun;
    public Vector3 pos;
    [Header("实例飞机")]
    public GameObject feiji;
    private  LineRenderer line;
    private Ray ray;

    // Use this for initialization
    void Start () {
        line = GetComponent<LineRenderer>();
	}
	// Update is called once per frame
	void Update () {

            lujing(ismiaozhun);
    }
  void lujing(bool open)
    {
 
        line.enabled = open;//射线显示
        if (open)
        {
            line.SetPosition(0, transform.position);//设置划线的起点和起点位置 0为起点，1为终点
            ray = new Ray(transform.position, pos);//从枪口向枪口的朝向发射一条射线
            line.SetPosition(1, transform.position + pos * 10);//子弹的射程30米
           
        }       
    }
 public void fly(Vector3 flypos)
    {
        GameObject go = Instantiate(feiji, transform.position, transform.rotation);
        go.GetComponent<OrdinaryBullet>().SetPos(flypos);
        //BulletManage bullet;
        //bullet = BulletResources.GetInstance().CreatBullet(1);
        //bullet.GetComponent<OrdinaryBullet>().SetPos(flypos);
        //bullet.transform.position = this.transform.position;
        //bullet.transform.rotation = this.transform.rotation;
    }
    void clear()
    {
           line.enabled = false;
    }
}
