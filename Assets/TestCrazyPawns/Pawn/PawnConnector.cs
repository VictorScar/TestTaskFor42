using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnConnector : MonoBehaviour
{
    [SerializeField] private MeshRenderer renderer;
    private ConnectorData _data;

    public Vector3 Position
    {
        get => transform.position;
    }

    public void Init(ConnectorData connectorData)
    {
        _data = connectorData;
    }

    public void SetState(ConnectorState state)
    {
        switch (state)
        {
            case ConnectorState.Default:
                renderer.material = _data.DefaultConnectorMaterial;
                break;
            case ConnectorState.Selected:
                renderer.material = _data.SelectedConnectorMaterial;
                break;
            case ConnectorState.Active:
                renderer.material = _data.ActiveConnectorMaterial;
                break;
        }
    }
}