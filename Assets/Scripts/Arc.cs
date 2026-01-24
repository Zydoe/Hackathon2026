using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arc : MonoBehaviour
{

    //Explosion
    [SerializeField] private float explosionRadius = 2.5f;
    [SerializeField] private int damage = 2;
    [SerializeField] private LayerMask damageableLayers;
    private bool hasExploded = false;
    public IEnumerator TravelArc(Vector3 destination, float duration)
    {
        var startPosition = transform.position;
        var percentComplete = 0.0f;
        while (percentComplete < 1.0f)
        {
            percentComplete += Time.deltaTime / duration;
            var currentHeight = Mathf.Sin(Mathf.PI *
            percentComplete); transform.position =
            Vector3.Lerp(startPosition,
            destination, percentComplete) + Vector3.up * currentHeight; yield
            return null;
        }

        gameObject.SetActive(false);
    }
    public void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;


        // Find everything in radius
        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            explosionRadius,
            damageableLayers
        );

        foreach (Collider hit in hits)
        {
            Entity damageable = hit.GetComponentInParent<Entity>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
