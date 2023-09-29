using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPoints : MonoBehaviour
{   
    public static enemyPoints main;
    public Transform startpoint;
    public Transform[] path;

    private void Awake()
    {
        main = this;
    }
}
