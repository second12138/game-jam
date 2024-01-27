using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingItem : MonoBehaviour
{
    // Start is called before the first frame update
    public int itemId;
    public Text priseText;
    public GameObject shopManager;

    // Update is called once per frame
    void Update()
    {
        priseText.text = "Price: " + ShopManager.shopItems[1,itemId];
    }
}


