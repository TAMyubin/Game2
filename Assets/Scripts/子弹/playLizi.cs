using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playLizi : MonoBehaviour
{
    public ParticleSystem zuojiao;
    public ParticleSystem youjiao;
    // Start is called before the first frame update
        public void playstar()
    {
        zuojiao.startRotation =  this.gameObject.transform.rotation.y*Mathf.PI;  
        zuojiao.Play();
    }
    public void youjiaoplaystar()
    {
        youjiao.startRotation = this.gameObject.transform.rotation.y * Mathf.PI;
        youjiao.Play();
    }
    public void playend()
    {
        youjiao.Stop();
        zuojiao.Stop();
    }
}
