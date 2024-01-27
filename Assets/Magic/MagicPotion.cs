using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPotion : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject magic;
    public float cost;
    public float maintainTime;
    public void OnPotionButtonClicked()
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Status playersStatus = player.GetComponent<Status>();
            if (playersStatus.currentMana >= cost)
            {
                playersStatus.currentMana -= cost;
                Debug.Log("magic on");
                GameObject magicEffects =  Instantiate(magic, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
                Debug.Log("off");
                Destroy(magicEffects,maintainTime);
            }
        }
    }
}
