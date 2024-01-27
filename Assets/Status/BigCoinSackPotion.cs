using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCoinSackPotion : MonoBehaviour
{
    public void OnPotionButtonClicked()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Status playerHealth = player.GetComponent<Status>();
            playerHealth.UseBigSack();
            Destroy(gameObject);
        }
    }
}
