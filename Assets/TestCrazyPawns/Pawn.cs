using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour, IDraggableObject
{
    public Transform Transform
    {
        get => transform;
        
    }
    public Vector3 Position
    {
        get => Position;
        set => Position = value;
    }

    public void Activate()
    {
       Debug.Log("Activate!");
    }

    
}