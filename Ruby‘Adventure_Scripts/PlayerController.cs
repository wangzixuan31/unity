using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @Author: wangzixuan31 2916492020@qq.com

//<summary>
//控制角色血量移动动画
//</summary>

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;//速度

    private int maxHealth = 5;//最大生命值

    public int currentHealth;//当前生命值
    
    public int MyMaxHealth { get { return maxHealth; } }

    public int MyCurrentHealth { get { return currentHealth; } }

    private float invincibleTime = 2f;//硬直时间2秒

    private float invincibleTimer;//硬直计时器

    private bool isInvincible;//是不是在硬直状态

    public GameObject bulletPrefab;//子弹
    //========玩家音效========

    public AudioClip hitClip;//受伤

    public AudioClip launchClip;//齿轮


    //=========玩家方向========
    private Vector2 lookDirection = new Vector2(1, 0);


    //=========玩家子弹==========
    [SerializeField]
    private int maxBulletCount = 99;//最大子弹数量

    private int curBulletCount;//当前子弹数量

    public int MyCurBulletCount { get { return curBulletCount; } }

    public int MyMaxBulletCount { get { return maxBulletCount; } }

    Rigidbody2D rbody;//刚体组件

    Animator anim;//获得组件


    // Start is called before the first frame update 开局状态
    void Start()
    {
        currentHealth = 5;//生命

        curBulletCount = 2;//当前子弹初识值

        invincibleTimer = 0;//非硬直状态

        rbody = GetComponent<Rigidbody2D>();//从当前游戏对象上获取Rigidbody2D组件的引用，并将其存储在rbody变量中

        anim = GetComponent<Animator>();

        UImanager.instance.UpdateHealthBar(currentHealth,maxHealth);

        UImanager.instance.UpdateBulletCount(curBulletCount,maxBulletCount);
    }

    // Update is called once per frame 每帧调用一次更新
    void Update()
    {
        
        float moveX=Input.GetAxisRaw("Horizontal"); //控制水平方向移动A：-1 D：1 0
        float moveY=Input.GetAxisRaw("Vertical");//控制垂直方向 W：1 S：-1 0

        Vector2 moveVector = new Vector2(moveX, moveY);
        if(moveVector.x != 0 || moveVector.y != 0)
        {
            lookDirection = moveVector;
        }
        anim.SetFloat("Look X",lookDirection.x);
        anim.SetFloat("Look Y",lookDirection.y);//保证移动时有动画可以切
        anim.SetFloat("Speed", moveVector.magnitude);


        //==================移动================

        //Vector2 position = transform.position; //设置开局位置 Vector2是一个表示二维向量的类表示屏幕或2D空间中的位置。transform关于该对象位置、旋转和缩放的信息。
        Vector2 position = rbody.position;
        //方法1
        //position.x += moveX * speed * Time.deltaTime;//移动方向 速度 自上一帧数的时间
        //position.y += moveY * speed * Time.deltaTime;
        //transform.position = position;//更新后的position重新应用到物体的transform
        //方法2 直接将物体移动到新的位置。这样做的好处是可以避免由于物理模拟带来的人物倾斜
        position += moveVector * speed * Time.deltaTime;
        rbody.MovePosition(position);//MovePosition()跳过任何物理模拟步骤（如速度、加速度或碰撞响应），直接将刚体移动到新的位置

        //===============硬直====================
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;

            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
            //倒计时结束以后（2秒），取消无敌状态
        }

        //发射子弹检测 按J
        if(Input.GetKeyDown(KeyCode.J)&& curBulletCount > 0)
        {
            ChangeBulletCount(-1);//消耗子弹

            anim.SetTrigger("Launch");//播放攻击动画

            AudioManager.instance.AudioPlay(launchClip);//发射音效

            GameObject bullet = Instantiate(bulletPrefab, rbody.position, Quaternion.identity);//在哪里发射 在哪个方向发射：默认

            BulletController bc = bullet.GetComponent<BulletController>();

            if(bc != null)
            {
                bc.Move(lookDirection, 300);//子弹速度
            }

        }

        //======按下E键进行交互======

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(rbody.position, lookDirection, 2f, LayerMask.GetMask("NPC"));//发射某物，碰到NPC层级的物品时触发
            if(hit.collider != null)
            {
                NPCmanager npc = hit.collider.GetComponent<NPCmanager>();
                Debug.Log("hit npc");
                npc.ShowDialog();//显示对话框
            }
        }

    }

    /// <summary>
    /// 生命
    /// </summary>
    /// <param name="amount"></param>

     

    public void ChangeHealth(int amount)
    {

        //如果收到伤害
        if (amount < 0)
        {
            if (isInvincible == true)
            {
                return;
            }
                isInvincible = true;

                anim.SetTrigger("Hit");

            AudioManager.instance.AudioPlay(hitClip);//受伤音效

            invincibleTimer = invincibleTime;
        }

        Debug.Log(currentHealth + "/" + maxHealth + "last");//内侧代码：改变前

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        //把玩家的生命值约束在0和最大值之间

        UImanager.instance.UpdateHealthBar(currentHealth,maxHealth);//实时更新血条

        Debug.Log(currentHealth + "/" + maxHealth + "before");//内侧代码：改变后
    }

    public void ChangeBulletCount(int amount)
    {
        curBulletCount = Mathf.Clamp(curBulletCount + amount, 0, maxBulletCount);//子弹数量限制 0-最大值
        UImanager.instance.UpdateBulletCount(curBulletCount, maxBulletCount);
    }

   
     
}
