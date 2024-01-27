using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private Bag bag;
    public int slotID;

    private void Start()
    {
        bag = GameObject.FindGameObjectWithTag("Player").GetComponent<Bag>();
    }

    private void Update()
    {
        if (transform.childCount<=0)
        {
            bag.isFull[slotID] = false;
        }
    }

    public void Drop()
    {
        foreach (Transform i in transform)
        {
           GameObject.Destroy(i.gameObject); 
        }
    }
}
