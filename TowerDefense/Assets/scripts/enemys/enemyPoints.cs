using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPoints : MonoBehaviour
{   
    public static enemyPoints main;
    public Transform startpoint;
    public Transform[] path;
    public int FTokens;


    private void Awake()
    {
        main = this;
    }

    


}
