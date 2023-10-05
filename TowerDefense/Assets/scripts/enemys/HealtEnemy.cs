using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtEnemy : MonoBehaviour
{
    [Header("Artibuts")]
    [SerializeField] private int HitPoints = 2;

    public void TakeDamage(int dmg)
    {
        HitPoints -= dmg;

        if (HitPoints <= 0)
        {
            enemyspawner.onEnemyDestroy.Invoke();
            Destroy(gameObject);
        }



    }
}
