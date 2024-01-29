using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 100f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    
    bool isEnergyZero ;
    
    
    Vector2 movementInput;

    SpriteRenderer spriteRenderer;

    Rigidbody2D rb;
    
    Animator animator;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    public Boolean isFacingLeft = false;
    public Boolean isFacingRight = false;

    private float moveX, moveY;

    private Vector2 moveDirection;
    
    // Start is called before the first frame update
    
    public bool GetPlayerFlipX()
    {
        return spriteRenderer.flipX;
    }
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void FixedUpdate()
    {
        inputSection();
        PlayerMove();
        PlayerAnim();
    }


    private void inputSection()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    private void PlayerMove()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void PlayerAnim()
    {
        if (moveDirection != Vector2.zero)
        {
            animator.SetBool("Walking",true);
        }
        else
        {
            animator.SetBool("Walking",false);
        }
        
        animator.SetFloat("moveX",moveX);
        animator.SetFloat("moveY",moveY);
    }
    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset);
        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            if (direction.y > 0)
            {
            }
            else if (direction.y < 0)
            {
            }
            return false;
        }
    }
    
}
