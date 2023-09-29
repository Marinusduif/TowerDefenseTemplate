using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class enemymovement : MonoBehaviour
{
    [Header("references")]
    [SerializeField] private Rigidbody2D rb;

    [Header("attributes")]
    [SerializeField] private float movespeed = 2f;

    private Transform target;
    private int pathIndex = 0;

    private void Start()
    {
        target = enemyPoints.main.path[pathIndex];
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f){
            pathIndex++;


            if(pathIndex == enemyPoints.main.path.Length)
            {
                enemyspawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            } else
            {
                target = enemyPoints.main.path[pathIndex];
            }

        }
    }

    public void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * movespeed;
    }
}

    
    

