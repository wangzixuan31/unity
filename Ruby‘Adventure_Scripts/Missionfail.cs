using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.UI;  
  
public class Missionfail : MonoBehaviour  
{  
    public GameObject testUI; // 假设test UI是一个GameObject，并拖入这个public变量  
    public GameObject buttonUI; // 新的UI按钮GameObject  
  
    private PlayerController playerController; // 引用PlayerController脚本  
    private UImanager uiManager; // 引用UImanager脚本  
  
    private void Start()  
    {  
        // 初始化引用  
        playerController = FindObjectOfType<PlayerController>();  
        uiManager = UImanager.instance;  
  
        // 默认隐藏test UI和新的UI按钮  
        testUI.SetActive(false);  
        buttonUI.SetActive(false);  
    }  
  
    private void Update()  
    {  
        // 检查玩家血量是否为0  
        if (playerController != null && playerController.currentHealth <= 0)  
        {  
            // 显示test UI和新的UI按钮  
            testUI.SetActive(true);  
            buttonUI.SetActive(true);  
  
            // 如果需要，你可以在这里禁用玩家移动或其他相关功能  
            playerController.enabled = false;  
  
            // 禁用Missionfail脚本的Update方法，避免反复设置  
            this.enabled = false;  
        }  
    }  
}