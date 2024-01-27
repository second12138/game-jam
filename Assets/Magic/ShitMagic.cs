using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitMagic : MonoBehaviour
{
    public float slowdownFactor = 0.5f; // 减速因子，设置为0.5表示减速一半

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("3");
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.moveSpeed*=slowdownFactor; // 进入圆形范围时减速
            }
            
        }
        if (other.CompareTag("Enemy"))
        {
            AStar astar = other.GetComponent<AStar>();
            if (astar != null)
            {
                astar.speed *= slowdownFactor;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.moveSpeed/=slowdownFactor; // 离开圆形范围时恢复正常速度
            }
        }

        if (other.CompareTag("Enemy"))
        {
            AStar astar = other.GetComponent<AStar>();
            if (astar != null)
            {
                astar.speed /= slowdownFactor;
            }
        }
    }
}
