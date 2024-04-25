using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @Author: wangzixuan31 2916492020@qq.com
/// <summary>
/// 敌人相关
/// </summary>
public class EnemyContreller : MonoBehaviour
{
    public float speed = 3;

    public float changeDirectionTime = 2f;//2秒后折返

    public bool isVertical;//可选择是否垂直水平：为角色添加脚本后可选：Is Vertical

    public ParticleSystem brokenEffect;//损坏特效

    public AudioClip fixedClip;//被修复的音效

    private float changeTimer;//changeTimer用于跟踪自上次改变方向以来经过的时间，以便在适当的时候再次改变方向

    

    private bool isFixed;//修复状态

    private Vector2 moveDirection;//移动方向

    private Rigidbody2D rbody;//获取组件

    private Animator anim;//获取动画组件

    private EnemyRepairManager repairManager;


    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();//获取组件

        moveDirection = isVertical ? Vector2.up : Vector2.right;//移动 = 垂直 ？是则朝上：否则朝右

        changeTimer = changeDirectionTime;

        isFixed = false;

        repairManager = EnemyRepairManager.Instance;  
        if (repairManager != null)  
        {  
            // 如果机器人一开始就是修复状态，增加修复计数  
            if (isFixed)  
            {  
                repairManager.IncrementFixedCount();  
            }  
        }  
    }

    // Update is called once per frame
    void Update()
    {
        if (isFixed) return;//被修复就不执行


        changeTimer -= Time.deltaTime;
        if (changeTimer < 0)
        {
            moveDirection *= -1;//方向折返
            changeTimer = changeDirectionTime;//重新计时
        }


        Vector2 position = rbody.position;
        position.x += moveDirection.x * speed * Time.deltaTime;
        position.y += moveDirection.y * speed * Time.deltaTime;
        rbody.MovePosition(position);

        anim.SetFloat("moveX",moveDirection.x);
        anim.SetFloat("moveY",moveDirection.y);

    }
    /// <summary>
    /// 对玩家的碰撞攻击
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter2D(Collision2D other)//真实物理碰撞 只有在甲乙都有碰撞rigidbody 2d才行
    {
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        if(pc != null)//如果碰到的是玩家
        {
            pc.ChangeHealth(-1);
        }
    }
    /// <summary>
    /// 暴动机器人被修复
    /// </summary>
    public void Fixed()
    {

        isFixed = true;//修好了

        if (brokenEffect.isPlaying == true)
        {
            brokenEffect.Stop();
        }
        AudioManager.instance.AudioPlay(fixedClip);//修复音效

        if (repairManager != null)  
        {  
            // 增加修复状态的敌人数量  
            repairManager.IncrementFixedCount();  
        }  

        rbody.simulated = false;//禁用物理
        anim.SetTrigger("fix");//播放被修复动画
    }

    public void EndRepair()  
    {  
        isFixed = false;  
        if (repairManager != null)  
        {  
            // 减少修复状态的敌人数量  
            repairManager.DecrementFixedCount();  
        }  
        // ... 结束修复逻辑 ...  
    }  
  
    // 你可以添加一个方法来获取当前修复状态的敌人数量  
    public int GetFixedEnemiesCount()  
    {  
        if (repairManager != null)  
        {  
            return repairManager.GetFixedCount();  
        }  
        return 0; // 如果没有找到repairManager，返回0  
    } 
}
