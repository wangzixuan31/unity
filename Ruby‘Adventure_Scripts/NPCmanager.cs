using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// @Author: wangzixuan31 2916492020@qq.com
/// <summary>
/// NPC 交流
/// </summary>
public class NPCmanager : MonoBehaviour
{
    public GameObject tipImage;//提示

    public GameObject dialoImage;//对话；

    public float showTime = 4;//对话框显示时间

    private float showTimer;//对话框显示计时器

    // Start is called before the first frame update
    void Start()
    {
        tipImage.SetActive(true);//初识提示
        dialoImage.SetActive(false);//初始隐藏默认对话框
        showTimer = -1;
    }

    // Update is called once per frame
    void Update()
    {
        showTimer -= Time.deltaTime;//showTimer的值减少了一个Time.deltaTime的量

        if (showTimer < 0) //计时器大于0就显示
        {
            dialoImage.SetActive(false);

            if (showTimer < 0)
            {
                tipImage.SetActive(true);
                dialoImage.SetActive(false);
            }
        }
    }
    ///显示对话框
    public void ShowDialog()
    {
        showTimer = showTime;
        tipImage.SetActive(false);
        dialoImage.SetActive(true);
    }
   


    
}
