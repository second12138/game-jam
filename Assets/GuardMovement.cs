using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour
{
    public Vector2 pointA; // 矩形的第一个点坐标
    public Vector2 pointB; // 矩形的第二个点坐标
    public float speed = 2f; // 移动速度

    private Animator animator;
    private Vector2 targetPosition;
    private int moveDirection = 0; // 0:下，1:右，2:上，3:左

    private void Start()
    {
        animator = GetComponent<Animator>();
        targetPosition = pointA;
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        // 计算当前位置和目标位置之间的差距
        Vector2 direction = targetPosition - (Vector2)transform.position;
        direction.Normalize();

        // 更新游戏对象的位置
        transform.Translate(direction * speed * Time.deltaTime);

        // 设置动画参数，控制播放的方向
        SetAnimationParameters(direction.x, direction.y);

        // 当游戏对象接近目标位置时，切换到下一个目标位置
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            ChangeMoveDirection();
        }
    }

    private void SetAnimationParameters(float x, float y)
    {
        // 设置动画参数，控制播放的方向
        animator.SetFloat("MoveX", x);
        animator.SetFloat("MoveY", y);

        // 将其他的移动方向设置为false
        animator.SetBool("MoveRight", false);
        animator.SetBool("MoveLeft", false);
        animator.SetBool("MoveUp", false);
        animator.SetBool("MoveDown", false);

        // 根据x和y的值来判断游戏对象的移动方向
        if (x > 0)
        {
            animator.SetBool("MoveRight", true);
        }
        else if (x < 0)
        {
            animator.SetBool("MoveLeft", true);
        }

        if (y > 0)
        {
            animator.SetBool("MoveUp", true);
        }
        else if (y < 0)
        {
            animator.SetBool("MoveDown", true);
        }
    }

    // 在下、右、上、左之间循环切换移动方向
    private void ChangeMoveDirection()
    {
        moveDirection = (moveDirection + 1) % 4;

        // 根据移动方向设置下一个目标位置
        switch (moveDirection)
        {
            case 0: // 下
                targetPosition = pointA;
                break;
            case 1: // 右
                targetPosition = new Vector2(pointB.x, pointA.y);
                break;
            case 2: // 上
                targetPosition = pointB;
                break;
            case 3: // 左
                targetPosition = new Vector2(pointA.x, pointB.y);
                break;
        }
    }
}
