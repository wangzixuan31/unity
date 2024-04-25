using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// @Author: wangzixuan31 2916492020@qq.com

/// <summary>
/// UI设置
/// </summary>

public class UImanager : MonoBehaviour
{

    public static UImanager instance{ get; private set; }//创建静态属性

    void Awake()
    {
        instance = this;
    }

    public Image healthBar;//血条

    public Text bulletCountText;//子弹数量

    /// <summary>
    /// 血条
    /// </summary>
    /// <param name="curAmount"></param>
    /// <param name="maxAmount"></param>

    public void UpdateHealthBar(int curAmount,int maxAmount)
    {
        healthBar.fillAmount = (float)curAmount / (float)maxAmount;
    }
    /// <summary>
    /// 更新子弹数量文本
    /// </summary>
    /// <param name="curAmount"></param>
    /// <param name="maxAmount"></param>
    public void UpdateBulletCount(int curAmount,int maxAmount) 
    {
        bulletCountText.text = curAmount.ToString() + "/" + maxAmount.ToString();
    }


}
