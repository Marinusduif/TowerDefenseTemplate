using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtEnemy : MonoBehaviour
{
    [Header("Artibuts")]
    [SerializeField] private int HitPoints = 2;
    [SerializeField] private int FtokensPerEnemy = 20;

    public void TakeDamage(int dmg)
    {
        HitPoints -= dmg;
        
        if (HitPoints <= 0)
        {
            enemyPoints.main.GetFTokens(FtokensPerEnemy);
            enemyspawner.onEnemyDestroy.Invoke();
            Destroy(gameObject);
        }

        

    }
}
