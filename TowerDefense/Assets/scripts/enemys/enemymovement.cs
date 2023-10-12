using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class enemymovement : MonoBehaviour
{
    [Header("references")]
    [SerializeField] private Rigidbody2D rb;
    private Health playerhealth;

    [Header("attributes")]
    [SerializeField] private float movespeed = 2f;

    private Transform target;
    private int pathIndex = 0;

    private void Start()
    {
        target = enemyPoints.main.path[pathIndex];
        pathIndex = 0;
        playerhealth = FindAnyObjectByType<Health>();
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            if (pathIndex < enemyPoints.main.path.Length - 1)
            {
                pathIndex++;
                target = enemyPoints.main.path[pathIndex];
            }
            else
            {
                enemyspawner.onEnemyDestroy.Invoke();
                playerhealth.TakeDamage(10);
                // Call TakeDamage to reduce health when the enemy is destroyed
                Destroy(gameObject);
            }
        }
    }



    public void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * movespeed;
    }
}

    
    

