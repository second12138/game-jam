using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static int[,] shopItems = new int[2, 9];
    public Status playerStatus; 
    public Text coinText;
    public GameObject [] dropItems;
    public GameObject[] uniqueItems;

    // Start is called before the first frame update
    void Start()
    {
        // 获取Player的Status脚本
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<Status>();

        coinText.text = "Coins: " + playerStatus.coins;

        // Item ID
        // 请注意，数组索引从0开始，所以这里将第一个索引行去掉了
        shopItems[0, 1] = 1;
        shopItems[0, 2] = 2;
        shopItems[0, 3] = 3;
        shopItems[0, 4] = 4;
        shopItems[0, 5] = 5;
        shopItems[0, 6] = 6;
        shopItems[0, 7] = 7;
        shopItems[0, 8] = 8;

        // Price
        shopItems[1, 1] = 20;
        shopItems[1, 2] = 30;
        shopItems[1, 3] = 5;
        shopItems[1, 4] = 10;
        shopItems[1, 5] = 5;
        shopItems[1, 6] = 3;
        shopItems[1, 7] = 300;
        shopItems[1, 8] = 301;
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = "Coins: " + playerStatus.coins;
    }

    public void Buy()
    {
        GameObject buttonRef = EventSystem.current.currentSelectedGameObject;
        int itemId = buttonRef.GetComponent<ShoppingItem>().itemId;

        if (playerStatus.coins >= shopItems[1, itemId])
        {
            playerStatus.coins -= shopItems[1, itemId];
            dropItems[itemId].GetComponent<SpawnToPlayer>().SpawnPrefabAtPlayerPosition();
            if (uniqueItems.Contains(dropItems[itemId]))
            {
                Destroy(buttonRef);
            }
        }
    }
}
