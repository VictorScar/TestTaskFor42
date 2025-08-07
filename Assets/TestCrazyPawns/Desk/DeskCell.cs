using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class DeskCell : MonoBehaviour
{
    [SerializeField] private Transform rootTransform;
    [SerializeField] private MeshRenderer viewRenderer;
   
    public Vector2 Position
    {
        set => transform.position = new Vector3(value.x, 0, value.y);
    }

    public Color CellColor
    {
        set => viewRenderer.material.color = value;
    }

    public Vector2 Size
    {
        set => transform.localScale = new Vector3(value.x, 0, value.y);
    }
}
