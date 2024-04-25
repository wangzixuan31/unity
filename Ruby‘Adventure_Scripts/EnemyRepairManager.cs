using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
  
public class EnemyRepairManager : MonoBehaviour  
{  
    // 单例引用  
    public static EnemyRepairManager Instance { get; private set; }  
  
    // 修复状态的敌人数量  
    public int enemiesFixedCount { get; private set; }  
  
    private void Awake()  
    {  
        // 确保只有一个实例存在  
        if (Instance == null)  
        {  
            Instance = this;  
            DontDestroyOnLoad(gameObject); // 如果你希望在加载新场景时保留这个实例  
        }  
        else  
        {  
            Destroy(gameObject); // 如果存在另一个实例，销毁这个重复的实例  
        }  
    }  
  
    // 增加修复状态的敌人数量  
    public void IncrementFixedCount()  
    {  
        enemiesFixedCount++;  
    }  
  
    // 减少修复状态的敌人数量  
    public void DecrementFixedCount()  
    {  
        enemiesFixedCount = Mathf.Max(0, enemiesFixedCount - 1);  
    }  
  
    // 获取修复状态的敌人数量  
    public int GetFixedCount()  
    {  
        return enemiesFixedCount;  
    }  
}