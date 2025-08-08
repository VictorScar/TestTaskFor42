using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour, IDraggableObject
{
    [SerializeField] private PawnConnector[] connectors;
    public Transform Transform
    {
        get => transform;
        
    }
    public Vector3 Position
    {
        get => transform.position;
        set => transform.position = new Vector3(value.x, 0, value.y);
    }

    public void Activate()
    {
       Debug.Log("Activate!");
    }

    public void Deactivate()
    {
        Debug.Log("Deactivate!");
    }
}