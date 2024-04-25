using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @Author: wangzixuan31 2916492020@qq.com
///<summary>
///草莓 碰撞
//</summary>
public class Collectible : MonoBehaviour
{
    public ParticleSystem collectEffect;//拾取特效

    public AudioClip collectClip;//拾取音效


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)//另一个碰撞体（other）进入当前对象的触发器区域时，该方法会被调用
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc != null) //这是一个检查，确保我们确实从other对象上获取了PlayerController脚本
        {
            if(pc.MyCurrentHealth < pc.MyMaxHealth)
            {
                pc.ChangeHealth(1);
                Instantiate(collectEffect, transform.position, Quaternion.identity);//生成特效 位置：草莓 角度：改变

                AudioManager.instance.AudioPlay(collectClip);

                Destroy(this.gameObject);//碰撞后消失
                
                //Debug.Log("玩家碰到了草莓！");
            }

            //pc.ChangeHealth(1);
            //Debug.Log("玩家碰到了草莓！"); 内测代码
        }
       
    }


}
