using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnConnector : MonoBehaviour
{
    public void GetConnector()
    {
        
    }

    public void SetConnector(PawnConnector connector)
    {
        Debug.Log("Set joint!!");
    }

    public Vector3 Position 
    { 
        get => transform.position;
       // set => transform.position = value;
    }
}
