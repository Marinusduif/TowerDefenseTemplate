using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.Mathematics;

public class turret : MonoBehaviour
{
    [Header("references")]
    [SerializeField] private Transform turretRotationPoint;  // The turret's rotation point reference.
    [SerializeField] private LayerMask enemyMask;            // A layer mask to filter potential enemy targets.
    [SerializeField] private GameObject bulletPrefap;         // The bullet prefab to instantiate.
    [SerializeField] private Transform shotingPoint;         // The point from which bullets are shot.

    [Header("attributes")]
    [SerializeField] private float targetingRange = 5f;      // The range within which the turret can target enemies.
    [SerializeField] private float rotationSpeed = 5f;       // The speed at which the turret rotates.
    [SerializeField] private float bulletRecharge = 1f;      // Time interval between firing bullets.

    private Transform target;                                 // The current target that the turret is aiming at.
    private float timeToFireBullet;                          // Time elapsed since the last bullet was fired.

    private void Update()
    {
        if (target == null)
        {
            FindTarget();  // If no target is currently acquired, find a target and exit the Update function.
            return;
        }

        rotateToTarget();  // Rotate the turret to face the target.

        if (!CheckTargetRange())
        {
            target = null;  // If the target is out of range, clear the target reference.
        }
        else
        {
            timeToFireBullet += Time.deltaTime;  // Increment the time elapsed since the last shot.

            if (timeToFireBullet >= 1f / bulletRecharge)
            {
                Shoot();  // If the recharge time has passed, fire a bullet and reset the timer.
                timeToFireBullet = 0f;
            }
        }
    }

    private bool CheckTargetRange()
    {
        // Check if the distance between the turret and its target is within the targeting range.
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void FindTarget()
    {
        // Cast a circle to detect potential enemy targets within the targeting range.
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange,
            (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            // Find the nearest target among the hits.
            float closestDistance = Mathf.Infinity;
            foreach (var hit in hits)
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    target = hit.transform;  // Set the nearest target as the current target.
                }
            }
        }
    }

    private void Shoot()
    {
        // Instantiate a bullet and set its target to the current acquired target.
        GameObject bulletObj = Instantiate(bulletPrefap, shotingPoint.position, quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    private void rotateToTarget()
    {
        // Calculate the angle between the turret and the target and rotate towards it.
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x -
            transform.position.x) * Mathf.Rad2Deg - 90f;

        quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation,
            targetRotation, rotationSpeed * Time.deltaTime);
    }
}
