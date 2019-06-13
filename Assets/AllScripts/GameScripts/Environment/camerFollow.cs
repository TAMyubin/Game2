using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerFollow : MonoBehaviour {
    public float carmerSpeed;//摄像机平滑移动的程度
   public Vector3 offset;//初始时候摄像机和主角的相对位置
    public Transform player;//主角的Transform
	// Use this for initialization
	void Start () {
    
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, offset + player.position, carmerSpeed*Time.deltaTime);
	}
}
