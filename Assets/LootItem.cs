using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LootItem : MonoBehaviour
{
    public List<GameObject> possibleLootList = new List<GameObject>();

    public void DropItems()
    {
        int randomNumber = Random.Range(1, 101);
        List<GameObject> loots = new List<GameObject>();
        foreach (GameObject i in possibleLootList)
        {
            if (randomNumber <= i.GetComponent<Pickup>().dropChance)
            {
                loots.Add(i);
            }
        }

        foreach (GameObject i in loots)
        {
            Instantiate(i, transform.position, Quaternion.identity);
        }
    }
}
