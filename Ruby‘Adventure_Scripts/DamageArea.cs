using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @Author: wangzixuan31 2916492020@qq.com
/// <summary>
/// 陷阱:地刺 伤害
/// </summary>
public class DamageArea : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.ChangeHealth(-1);
        }

    }
}