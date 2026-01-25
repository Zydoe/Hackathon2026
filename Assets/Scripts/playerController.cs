using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private float moveSpeed = 1;
    public Rigidbody2D rb;
    public Animator anim;
    public Player player;
    public float dashForce = 15f;
    private bool isDashing = false;

    public SpriteRenderer playerSR;

    public TrailRenderer dashTrail;
    Vector2 movement;
    Vector2 lastMoveDir = Vector2.down; // default facing down
    public Transform attackHitbox;





    void Start()
    {
        moveSpeed = player.GetSpeed();
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.sqrMagnitude > 0.1f)
        {
            lastMoveDir = movement.normalized;
        }

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

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isDashing = true;
            StartCoroutine(Dash(0.2f));
        }

        // Attack input
        if (Input.GetMouseButtonUp(0))
        {
            anim.SetTrigger("attack");
            player.Attack();
        }


        UpdateHitboxDirection();
    }

    IEnumerator Dash(float durration)
    {
        dashTrail.emitting = true;
        rb.velocity = movement * dashForce;
        yield return new WaitForSeconds(durration);
        rb.velocity = Vector2.zero;
        dashTrail.emitting = false;
        isDashing = false;
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }


    void UpdateHitboxDirection()
    {
        float angle = Mathf.Atan2(lastMoveDir.y, lastMoveDir.x) * Mathf.Rad2Deg;
        attackHitbox.localRotation = Quaternion.Euler(0, 0, angle - 270f);
    }
}
