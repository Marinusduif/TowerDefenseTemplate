using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plot : MonoBehaviour
{
    [Header("refs")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    
    private GameObject turret;
    private Color StartColor;

    private void Start()
    {
        StartColor = sr.color;
    }
    private void OnMouseOver()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = StartColor;
    }

    private void OnMouseDown()
    {
        if (turret != null) return;

        GameObject turretToBuild = TurretBuilder.main.GetSelectedTurret();
        turret = Instantiate(turretToBuild, transform.position, Quaternion.identity);
    }
}
