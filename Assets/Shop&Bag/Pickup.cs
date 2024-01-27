using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pickup : MonoBehaviour
{
    private Bag bag;
    public int dropChance;

    public GameObject itemButton;
    // Start is called before the first frame update
    void Start()
    {
        bag = GameObject.FindGameObjectWithTag("Player").GetComponent<Bag>();
    }

    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < bag.slots.Length; i++)
            {
                if (bag.isFull[i] == false)
                {
                    bag.isFull[i] = true;
                    Instantiate(itemButton, bag.slots[i].transform,false);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
