using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
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
    
    bool CanMove = true;

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
        if (movementInput.x > 0)
        {
            isFacingLeft = false;
            isFacingRight = true;
        }
        else if (movementInput.x < 0)
        {
            isFacingLeft = true;
            isFacingRight = false;
        }
        
        if (CanMove)
        {
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);

                if (!success && movementInput.x != 0)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }

                if (!success && movementInput.y != 0)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
        
                animator.SetBool("isMoving", success);
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (movementInput.y > 0)
        {
            animator.SetBool("MoveUp", true);
            animator.SetBool("MoveDown", false);
            animator.SetBool("IdleUp", false);
            animator.SetBool("IdleDown", false);
        }
        else if (movementInput.y < 0)
        {
            animator.SetBool("MoveUp", false);
            animator.SetBool("MoveDown", true);
            animator.SetBool("IdleUp", false);
            animator.SetBool("IdleDown", false);
        }
        else
        {
            animator.SetBool("MoveUp", false);
            animator.SetBool("MoveDown", false);
        
            if (movementInput.x == 0)
            {
                animator.SetBool("IdleUp", true);
                animator.SetBool("IdleDown", true);
            }
        }

        if (movementInput.x != 0)
        {
            animator.SetBool("IdleUp", false);
            animator.SetBool("IdleDown", false);
        }
        if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        } 
        else if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
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
                animator.SetBool("IdleUp", true);
                animator.SetBool("IdleDown", false);
            }
            else if (direction.y < 0)
            {
                animator.SetBool("IdleUp", false);
                animator.SetBool("IdleDown", true);
            }
            return false;
        }
    }
    
    public void LockMovement()
    {
        CanMove = false;
    }

    public void UnlockMovement()
    {
        CanMove = true;
    }
}
