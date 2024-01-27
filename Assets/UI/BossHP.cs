using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHP : MonoBehaviour
{
    public UnityEngine.UI.Image bossHP;
    public float maxHP = 1000f; 
    public float currentHP = 1000f;

    void Update()
    {
        float healthPercentage = currentHP / maxHP;
        bossHP.fillAmount = healthPercentage;
    }
    
}
