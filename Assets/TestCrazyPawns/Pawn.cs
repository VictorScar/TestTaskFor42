using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Pawn : MonoBehaviour, IDraggableObject
{
    [SerializeField] private PawnConnector[] connectors;
    [SerializeField] private MeshRenderer[] _renderers;
    [SerializeField] private Material _defaultMaterial;
    private Material deleteMaterial;

    private bool _isMustDeleted;

    public bool IsMustDeleted
    {
        get => _isMustDeleted;
        set
        {
            _isMustDeleted = value;

            var pawnMaterial = _defaultMaterial;

            if (value)
            {
                pawnMaterial = deleteMaterial;
            }

            SetMaterial(pawnMaterial);
        }
    }

    public void Remove()
    {
        Destroy(gameObject);
    }

    public PawnConnector[] Connectors => connectors;

    public Transform Transform => transform;

    public Vector3 Position
    {
        get => transform.position;
        set => transform.position = new Vector3(value.x, 0, value.y);
    }

    public void Init(PawnData data)
    {
        deleteMaterial = data.DeleteMaterial;
    }

    public void Activate()
    {
        Debug.Log("Activate!");
    }

    public void Deactivate()
    {
        Debug.Log("Deactivate!");
    }

    private void SetMaterial(Material material)
    {
        if (_renderers != null)
        {
            foreach (var renderer in _renderers)
            {
                renderer.material = material;
            }
        }
    }
}