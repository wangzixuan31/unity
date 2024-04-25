using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @Author: wangzixuan31 2916492020@qq.com
/// <summary>
/// 子弹包 
/// </summary>
public class BulletBag : MonoBehaviour
{
    public int bulletCount = 10;//子弹

    public ParticleSystem collectEffect;//拾取特效

    public AudioClip collectClip;//拾取音效

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();//检测是否为玩家

        if (pc != null) //这是一个检查，确保我们确实从other对象上获取了PlayerController脚本
        {
            if (pc.MyCurBulletCount < pc.MyMaxBulletCount)
            {
                pc.ChangeBulletCount(bulletCount);//增加子弹数

                Instantiate(collectEffect, transform.position, Quaternion.identity);//生成特效 位置：弹药包 角度：改变

                AudioManager.instance.AudioPlay(collectClip);//音效

                Destroy(this.gameObject);//碰撞后消失

                Debug.Log("玩家碰到子弹包！");
            }


            //Debug.Log("玩家碰到了草莓！"); 内测代码
        }
    }
}
