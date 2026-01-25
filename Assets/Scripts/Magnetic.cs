using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Magnetic : MonoBehaviour
{
    public float pullSpeed = .5f;
    private Transform player;
    private bool isAttracted = false;
    private bool attractable = false;

    void Start()
    {
        StartCoroutine(EnableMagnetismAfterDelay(1f));
    }

    void Update()
    {
        if (isAttracted && player != null && attractable)
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
        }
    }

    IEnumerator EnableMagnetismAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        attractable = true;
        Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();
        if (rb)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}
