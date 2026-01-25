using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Magnetic : MonoBehaviour
{
    public float pullSpeed = 0.11f;
    private Transform player;
    private bool isAttracted = false;

    void Update()
    {
        if (isAttracted && player != null)
        {
            // Move coin toward player
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                pullSpeed * Time.deltaTime
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Magnetic"))
        {
            player = target.transform;
            isAttracted = true;
            Debug.Log("Coin attracted to player");
        }
    }
}
