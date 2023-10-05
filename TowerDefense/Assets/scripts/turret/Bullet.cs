using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    internal Transform Target;

    [Header("refs")]
    [SerializeField] private Rigidbody2D Rb;

    [Header("attributes")]
    [SerializeField] private float BulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;

    public void SetTarget(Transform _target)
    {
        Target = _target;
    }

    private void FixedUpdate()
    {
        if (Target != null)
        {
            Vector2 direction = (Target.position - transform.position).normalized;
            Rb.velocity = direction * BulletSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        HealtEnemy healthEnemy = other.gameObject.GetComponent<HealtEnemy>();
        if (healthEnemy != null)
        {
            healthEnemy.TakeDamage(bulletDamage);
        }

        Destroy(gameObject);
    }
}
