using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Rhino : Enemy
{
    [Header("Rhino Details")]
    [SerializeField] private float detectionRange;
    [SerializeField] private bool playerDetected;

    protected override void Update()
    {
        base.Update();

        anim.SetFloat("xVelocity", rb.velocity.x);

        handleCollisions();
        HandleCharge();
    }

    private void HandleCharge()
    {
        if(canMove == false)
            return;

        rb.velocity = new Vector2(moveSpeed * facingDir, rb.velocity.y);

        if(!isGroundInFrontDetected)
            canMove = false;
    }

    protected override void handleCollisions()
    {
        base.handleCollisions();
        playerDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDir, detectionRange, whatIsPlayer);

        if(playerDetected)
        {
            canMove = true;
            rb.velocity = Vector2.zero;
            Flip();
        }
            

    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + detectionRange * facingDir, transform.position.y));
    }
}
