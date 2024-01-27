using System.Collections;
using System.Collections.Generic;
using Interfaces;
using TMPro.EditorUtilities;
using UnityEngine;

public class BlueMagic : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public LayerMask whatIsSolid;
    public int damageAmount = 10; // 撞击敌人时减少的Health值

    private bool isMovingLeft; // 标记当前是否正在向左移动

    private void Start()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.gameObject.GetComponent<IDamageable>().OnHit(damageAmount, Vector2.zero);
            }
        }
        Invoke("DestroyProjectile", lifeTime);
        isMovingLeft = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().isFacingLeft;
    }

    private void Update()
    {
        // 根据移动方向设置对象朝向
        if (isMovingLeft)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            transform.localScale = new Vector3(-1f, 1f, 1f); // 反转朝向
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            transform.localScale = new Vector3(1f, 1f, 1f); // 恢复朝向
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    // Method to set the direction when the prefab is instantiated
    public void SetDirection(bool movingLeft)
    {
        isMovingLeft = movingLeft;
    }

    // 处理碰撞事件
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查是否碰撞到带有"敌人"标签的对象
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("01");
            // 获取敌人的IDamageable组件（假设敌人有一个实现了IDamageable接口的脚本来处理减体力行为）
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable != null)
            {
                // 减少敌人的体力值
                damageable.OnHit(damageAmount, Vector2.zero); // 飞行物击中敌人时没有击退效果，所以传递 Vector2.zero
            }

            // 销毁飞行物体（蓝色魔法特效）
            Debug.Log("02");
        }
    }

}
