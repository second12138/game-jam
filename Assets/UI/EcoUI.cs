using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EcoUI : MonoBehaviour
{
    public Text coins;

    private void Update()
    {
        coins.text = GameObject.FindGameObjectWithTag("Player").GetComponent<Status>().coins.ToString();
    }
}
