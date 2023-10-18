using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class enemymovement : MonoBehaviour
{
    [Header("references")]
    [SerializeField] private Rigidbody2D rb;  // The Rigidbody2D component of the enemy.
    private Health playerhealth;  // A reference to the player's health.

    [Header("attributes")]
    [SerializeField] private float movespeed = 2f;  // The speed at which the enemy moves.

    private Transform target;  // The current target of the enemy.
    private int pathIndex = 0;  // The current path index the enemy is following.

    private void Start()
    {
        target = enemyPoints.main.path[pathIndex];  // Set the target as the starting point of the path.
        pathIndex = 0;  // Initialize the path index.
        playerhealth = FindAnyObjectByType<Health>();  // Find an object of type 'Health' and link it to 'playerhealth'.
    }

    private void Update()
    {
        Mover();  // Call the movement function.
    }

    public void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * movespeed;  // Adjust the Rigidbody2D velocity to move towards the target.
    }

    public void Mover()
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
                // Call TakeDamage to reduce health when the enemy is destroyed.
                Destroy(gameObject);
            }
        }
    }
}








