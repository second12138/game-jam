using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

// Slime 类，实现了 IDamageable 接口
public class Slime : MonoBehaviour, IDamageable
{
    Animator animator;  // 动画控制器

    Rigidbody2D rb;  // 刚体组件

    Collider2D physicsCollider;  // 物理碰撞器

    bool isAlive = true;  // 是否存活

    private LootItem loot;  // 掉落物品
    public float Health  // 生命值
    {
        set
        {
            if (value < _health)  // 如果新的生命值小于当前生命值
            {
                animator.SetTrigger("hit");  // 播放受击动画
            }

            _health = value;  // 更新生命值

            if (_health <= 0)  // 如果生命值小于等于 0
            {
                animator.SetBool("isAlive", false);  // 设置生命状态为死亡
                Targetable = false;  // 设置为不可被攻击
            }
        }
        get
        {
            return _health;  // 返回生命值
        }
    }

    public bool Targetable  // 是否可被攻击
    {
        get
        {
            return _targetable;  // 返回是否可被攻击
        }
        set
        {
            _targetable = value;  // 设置是否可被攻击

            rb.simulated = value;  // 设置刚体是否响应物理效果

            physicsCollider.enabled = value;  // 设置物理碰撞器是否启用
        }
    }

    public float _health = 3;  // 生命值

    public bool _targetable = true;  // 是否可被攻击
    public void Start()  // 在游戏开始时调用
    {
        animator = GetComponent<Animator>();  // 获取动画控制器组件
        animator.SetBool("isAlive", isAlive);  // 设置生命状态
        rb = GetComponent<Rigidbody2D>();  // 获取刚体组件
        physicsCollider = GetComponent<Collider2D>();  // 获取物理碰撞器组件
        loot = GetComponent<LootItem>();  // 获取掉落物品组件
    }

    // 当受到伤害时调用的方法（带击退效果）
    public void OnHit(float damage, Vector2 knockback)
    {
        Health -= damage;  // 扣除生命值
        // 给 Slime 施加力，产生击退效果
        rb.AddForce(knockback);
        Debug.Log("Force" + knockback);  // 打印击退力度
    }

    // 当受到伤害时调用的方法（无击退效果）
    public void OnHit(float damage)
    {
        Health -= damage;  // 扣除生命值
    }

    // 当对象被销毁时调用的方法
    public void OnObjectDestroyed()
    {
        loot.DropItems();  // 掉落物品
        Destroy(gameObject);  // 销毁游戏对象
    }
}