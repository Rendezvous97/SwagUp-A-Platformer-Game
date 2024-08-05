using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Chicken : Enemy
{
    [Header("Chicken Details")]
    [SerializeField] private float aggroDuration;
    [SerializeField] private float detectionRange;

    private float aggroTimer;
    private bool playerDetected;
    private bool canFlip = true;
    
    

    protected override void Update()
    {

        base.Update();

        anim.SetFloat("xVelocity", rb.velocity.x);
        aggroTimer -= Time.deltaTime;

        if(isDead)
            return;

        if(playerDetected)
        {
            canMove = true;
            aggroTimer = aggroDuration;
        }

        if(aggroTimer < 0)
            canMove = false;

        HandleMovement();
        handleCollisions();

        if(isGrounded)
            HandleTurnAround();
    }

    private void HandleTurnAround()
    {
        if (!isGroundInFrontDetected || isWallDetected)
        {
            Flip();
            canMove = false;
            rb.velocity = Vector2.zero;
        }
    }

    private void HandleMovement()
    {
        if(canMove == false)
            return;
        
        handleFlip(player.transform.position.x);

        rb.velocity = new Vector2(moveSpeed * facingDir, rb.velocity.y);
 
    }

    protected override void Flip()
    {
        base.Flip();
        canFlip = true;
    }

    protected override void handleFlip(float xValue)
    {
        if(canMove == false)
            return;
            
        if ((xValue < transform.position.x && facingRight) || (xValue > transform.position.x && !facingRight))
        {
            if(canFlip)
            {
                canFlip = false;
                Invoke(nameof(Flip), .3f);
            }
        }
    }

    protected override void handleCollisions()
    {
        base.handleCollisions();
        playerDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDir, detectionRange, whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + detectionRange * facingDir, transform.position.y));
    }
}
