using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class money : MonoBehaviour
{
    public int FTokens;


    private void Start()
    {
        FTokens = 100;

    }

    public void GetFTokens(int amount)
    {
        FTokens += amount;
    }

    public bool SpendFTokens(int amount)
    {
        if (amount <= FTokens)
        {
            FTokens -= amount;
            return true;
        }
        else
        {
            Debug.Log("youBroke");
            return false;
        }
    }

}

