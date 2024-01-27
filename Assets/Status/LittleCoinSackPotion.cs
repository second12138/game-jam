using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleCoinSackPotion : MonoBehaviour
{
    public void OnPotionButtonClicked()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Status playerHealth = player.GetComponent<Status>();
            playerHealth.UseLittleSack();
            Destroy(gameObject);
        }
    }
}
