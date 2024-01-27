using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnToPlayer : MonoBehaviour
{
    public GameObject prefabToSpawn; 

    public void SpawnPrefabAtPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); 
        if (player != null)
        {
            Vector3 spawnPosition = player.transform.position - new Vector3(0, 0.1f, 0);
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
