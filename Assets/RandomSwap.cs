using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject enemyPrefab; // 小怪的预制体
    public float spawnRadius = 5f; // 生成范围的半径
    public int maxEnemies = 10; // 最大小怪数量
    public float spawnDelay = 1f; // 生成间隔时间

    private int currentEnemyCount = 0;

    // 新增一个变量来接收生成小怪的中心位置
    public Vector2 centerPosition;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (currentEnemyCount < maxEnemies)
        {
            // 在指定中心位置附近生成随机位置
            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
            Vector2 randomPosition = centerPosition + randomOffset;

            // 检测是否有碰撞体在生成位置
            Collider2D[] colliders = Physics2D.OverlapCircleAll(randomPosition, 0.1f);
            bool hasColliders = colliders.Length > 0;

            // 如果有碰撞体，则重新生成随机位置
            while (hasColliders)
            {
                randomOffset = Random.insideUnitCircle * spawnRadius;
                randomPosition = centerPosition + randomOffset;
                colliders = Physics2D.OverlapCircleAll(randomPosition, 0.1f);
                hasColliders = colliders.Length > 0;
            }

            // 实例化小怪
            GameObject enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);

            currentEnemyCount++;

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void OnEnemyDestroyed()
    {
        currentEnemyCount--;
    }
}