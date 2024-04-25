using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.UI;  
  
public class MissionShow : MonoBehaviour  
{  
    // 不再需要这个变量，如果MissionBar就是当前GameObject的话  
    // public GameObject MissionBar;  
  
    private float showTimer = 3;  
  
    // Start is called before the first frame update  
    void Awake()  
    {  
        // 如果MissionBar是当前GameObject，直接激活它  
        gameObject.SetActive(true);  
    }  
  
    // Update is called once per frame  
    void Update()  
    {  
        showTimer -= Time.deltaTime;  
  
        if (showTimer < 0)  
        {  
            // 如果MissionBar是当前GameObject，直接隐藏它  
            gameObject.SetActive(false);  
        }  
    }  
}