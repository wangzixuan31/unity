using System.Collections;  
using UnityEngine;  
using UnityEngine.UI;  
  
public class VictoryUIController : MonoBehaviour  
{  
    public GameObject victoryUIPanel;  
    private EnemyRepairManager repairManager;  
    private bool isVictoryShown = false;  
    private bool isHideVictoryUICoroutineRunning = false; // 新增的标志位  
  
    void Start()  
    {  
        victoryUIPanel.SetActive(false);  
        repairManager = EnemyRepairManager.Instance;  
        if (repairManager == null)  
        {  
            Debug.LogError("EnemyRepairManager not found in scene!");  
            return;  
        }  
        StartCoroutine(CheckForVictory());  
    }  
  
    IEnumerator CheckForVictory()  
    {  
        while (true)  
        {  
            yield return new WaitForSeconds(1);  
            int fixedCount = repairManager.GetFixedCount();  
            if (fixedCount > 2 && !isVictoryShown && !isHideVictoryUICoroutineRunning)  
            {  
                ShowVictoryUI();  
            }  
        }  
    }  
  
    void ShowVictoryUI()  
    {  
        if (!isVictoryShown && !isHideVictoryUICoroutineRunning)  
        {  
            victoryUIPanel.SetActive(true);  
            StartCoroutine(HideVictoryUIAfterDelay(4f));  
            isHideVictoryUICoroutineRunning = true; // 标记协程正在运行  
        }  
    }  
  
    IEnumerator HideVictoryUIAfterDelay(float delay)  
    {  
        yield return new WaitForSeconds(delay);  
        HideVictoryUI();  
    }  
  
    void HideVictoryUI()  
    {  
        victoryUIPanel.SetActive(false);  
        // 不要在这里重置 isVictoryShown，因为它应该在协程完成后重置  
    }  
  
    // 当HideVictoryUIAfterDelay协程完成时调用此方法以重置标志位  
    void OnHideVictoryUICoroutineComplete()  
    {  
        isHideVictoryUICoroutineRunning = false; // 协程完成，重置标志位  
        isVictoryShown = false; // 现在可以安全地重置isVictoryShown了  
    }  
}