using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBuilder : MonoBehaviour
{
   public static TurretBuilder main;

    [Header("Refs")]
    [SerializeField] private GameObject[] turretPrefap;

    private int SelectedTurret = 0;
    private void Awake()
    {
        main = this;
    }

    public GameObject GetSelectedTurret()
    {
        return turretPrefap[SelectedTurret];
    }
}
