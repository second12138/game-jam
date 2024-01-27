using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMagic : MonoBehaviour
{
    private float slowdownFactor = 0.00001f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Tree Magic On Enemy");
        if (other.CompareTag("Enemy"))
        {
            AStar astar = other.GetComponent<AStar>();
            if (astar != null)
            {
                astar.speed *=slowdownFactor;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.CompareTag("Enemy"))
        {
            AStar astar = other.GetComponent<AStar>();
            if (astar != null)
            {
                astar.speed /= slowdownFactor;
            }
        }

        Debug.Log("Tree Magic OFF Enemy");
    }
}
