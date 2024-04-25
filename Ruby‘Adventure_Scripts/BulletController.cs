using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @Author: wangzixuan31 2916492020@qq.com
/// <summary>
/// 控制子弹活动碰撞
/// </summary>
public class BulletController : MonoBehaviour
{
    Rigidbody2D rbody;

    public AudioClip hitClip;//摧毁音效


    // Start is called before the first frame update
    void Awake()//Awake在start前面
    {
        rbody = GetComponent<Rigidbody2D>();//刚创建时未执行移动

        Destroy(this.gameObject, 2f);//生成后多长时间消失
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 子弹检测
    /// </summary>
    /// <param name="moveDirection"></param>
    /// <param name="moveForce"></param>
    public void Move(Vector2 moveDirection,float moveForce)
    {
        rbody.AddForce(moveDirection * moveForce);
    }

    //子弹消失
   void OnCollisionEnter2D(Collision2D other)
    {

        EnemyContreller ec = other.gameObject.GetComponent<EnemyContreller>();
        if (ec != null)
        {

            Debug.Log("击中目标");
            ec.Fixed();//修复敌人
        }
        AudioManager.instance.AudioPlay(hitClip);//命中敌人音效

        Destroy(this.gameObject);//即便不碰撞，子弹也销毁
    }

}
