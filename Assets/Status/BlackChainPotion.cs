using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackChainPotion : MonoBehaviour
{
    public void OnPotionButtonClicked()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Status playerHealth = player.GetComponent<Status>();
            playerHealth.BlackChainBUff();
        }
    }

    private void OnDestroy()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Status playerHealth = player.GetComponent<Status>();
            playerHealth.BlackChainStop();
        }
    }
}
