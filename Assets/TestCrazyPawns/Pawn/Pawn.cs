using UnityEngine;

public class Pawn : MonoBehaviour
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

        if (Connectors != null)
        {
            foreach (var connector in Connectors)
            {
                connector.Init(data.ConnectorData);
            }
        }
    }

    public void Activate()
    {
        
    }

    public void Deactivate()
    {
       
    }

    public bool IsContainConnector(PawnConnector connector)
    {
        foreach (var c in connectors)
        {
            if (connector == c)
            {
                return true;
            }
        }

        return false;
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