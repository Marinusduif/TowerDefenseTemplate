using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.Mathematics;

public class turret : MonoBehaviour
{
    [Header("references")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefap;
    [SerializeField] private Transform shotingPoint;

    [Header("attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float bulletRecharge = 1f;

    private Transform target;
    private float timeToFireBullet;

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;

        }

        rotateToTarget();

        if (!CheckTargetRange())
        {
            target = null;

        }else
        {
            timeToFireBullet += Time.deltaTime;

            if (timeToFireBullet >= 1f / bulletRecharge)
            {
                Shoot();
                timeToFireBullet = 0f;
            }
        }


        if (target != null)
        {
            rotateToTarget();
        }
    }

    private bool CheckTargetRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange,
            (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            // Find the nearest target
            float closestDistance = Mathf.Infinity;
            foreach (var hit in hits)
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    target = hit.transform;
                }
            }
        }
    }

    private void Shoot(){
        GameObject bulletObj = Instantiate(bulletPrefap, shotingPoint.position, quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }   

    
    private void rotateToTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - 
            transform.position.x) * Mathf.Rad2Deg - 90f;

        quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation,
            targetRotation, rotationSpeed * Time.deltaTime);
    }

  
}
