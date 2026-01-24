using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Camera playerCam;
    public Rigidbody2D rb;
    public GameObject playerSprite;

    Vector2 movement;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        //Flip sprite based on movement direction
        if (movement.x > 0)
        {
            playerSprite.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (movement.x < 0)
        {
            playerSprite.transform.localScale = new Vector3(1, 1, 1);
        }
        //CamFollowPlayer();
    }

    void CamFollowPlayer()
    {
        Vector3 newCamPos = new Vector3(transform.position.x, transform.position.y, playerCam.transform.position.z);
        playerCam.transform.position = newCamPos;
    }
}
