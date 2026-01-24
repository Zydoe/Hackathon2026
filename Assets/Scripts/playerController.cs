using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private float moveSpeed = 1;
    public Rigidbody2D rb;
    public Animator anim;
    public Player player;

    public SpriteRenderer playerSR;

    Vector2 movement;

    void Start()
    {
        moveSpeed = player.GetPlayerSpeed();
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.sqrMagnitude > 1)
            movement.Normalize();

        if (movement.x > 0)
        {
            playerSR.flipX = true;
        }
        else if (movement.x < 0)
        {
            playerSR.flipX = false;
        }
        // Update animator parameters
        anim.SetFloat("moveX", movement.x);
        anim.SetFloat("moveY", movement.y);
        anim.SetBool("isMoving", movement.sqrMagnitude > 0.1f);

        // Attack input
        if (Input.GetMouseButtonUp(0))
        {
            anim.SetTrigger("attack");
        }


    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }

}
