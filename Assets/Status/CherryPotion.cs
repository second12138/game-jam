using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryPotion : MonoBehaviour
{
    public void OnPotionButtonClicked()
    {
        // 在此处执行回血逻辑
        // 假设有一个名为"Player"的游戏对象，持有HealthManaBar脚本来管理血量和蓝量
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Status playerHealth = player.GetComponent<Status>();
            playerHealth.Usecherry();
            Destroy(gameObject);
        }
    }
}
