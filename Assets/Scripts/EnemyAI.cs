using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject detectPlayerObject;
    private DetectPlayer detectPlayer;
    public GameObject attackRangeObject;
    private EnemyAttackRange attackRange;
    public Enemy enemy;
    private bool CanPlayDetectSound = true;
    // Start is called before the first frame update
    void Start()
    {
        detectPlayer = detectPlayerObject.GetComponent<DetectPlayer>();
        attackRange = attackRangeObject.GetComponent<EnemyAttackRange>();
    }

    void Update()
    {
        if (attackRange.player != null && detectPlayer.player != null)
        {
            // Attack player
            enemy.Attack(detectPlayer.player.GetComponent<Entity>());
        }
        else if (detectPlayer.player != null)
        {
            if (CanPlayDetectSound)
            {
                enemy.PlayDetectSound();
                CanPlayDetectSound = false;
            }
            // Move towards player
            Vector2 direction = (detectPlayer.player.position - transform.position).normalized;
            transform.position += (Vector3)(direction * enemy.GetSpeed() * Time.deltaTime);
        }
        else
        {
            CanPlayDetectSound = true;
        }
    }


}
