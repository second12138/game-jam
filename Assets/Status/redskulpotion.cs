using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSkullPotion : MonoBehaviour
{
    private bool click;

    public void OnPotionButtonClicked()
    {
        click = !click;
        Debug.Log("Button Clicked. Click status: " + click);

        // 在此处执行回血逻辑
        // 假设有一个名为"Player"的游戏对象，持有Status脚本来管理血量和蓝量
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Status playerHealth = player.GetComponent<Status>();
            if (click)
            {
                playerHealth.UseRedSkull();
                Debug.Log("Buff Applied.");
            }
            else
            {
                playerHealth.StopRedSkull();
                Debug.Log("Buff Removed.");
            }
        }
        else
        {
            Debug.LogWarning("Player not found.");
        }
    }
}






