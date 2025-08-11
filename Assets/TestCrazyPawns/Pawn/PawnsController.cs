using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnsController : MonoBehaviour
{
    private List<Pawn> _pawns = new List<Pawn>();
    private PawnsGenerator _generator;

    public List<Pawn> Pawns => _pawns;

    public void Init(PawnConfigData pawnConfigData)
    {
        _generator = new PawnsGenerator(15);

        var pawnData = new PawnData()
        {
            ConnectorData = new ConnectorData
            {
                ActiveConnectorMaterial = pawnConfigData.ActiveConnectorMaterial,
                SelectedConnectorMaterial = pawnConfigData.SelectedConnectorMaterial,
                DefaultConnectorMaterial = pawnConfigData.DefaultConnectorMaterial
            },

            DeleteMaterial = pawnConfigData.DeleteMaterial,
        };

        var generatorData = new PawnGeneratorData
        {
            SpawnPawnCount = pawnConfigData.SpawnPawnCount,
            Prefab = pawnConfigData.PawnPrefab,
            InitialSpawnRadius = pawnConfigData.InitialSpawnRadius,
            PawnData = pawnData,
            PawnRoot = transform
        };

        _pawns = _generator.GeneratePawns(generatorData);
    }

    public bool RemovePawn(Pawn pawn)
    {
        if (pawn)
        {
            _pawns.Remove(pawn);

            Destroy(pawn.gameObject);
            return true;
        }

        return false;
    }

    public bool GetPawnByConnector(PawnConnector connector, out Pawn connectorPawn)
    {
        if (connector)
        {
            foreach (var pawn in _pawns)
            {
                if (pawn.IsContainConnector(connector))
                {
                    connectorPawn = pawn;
                    return true;
                }
            }
        }

        connectorPawn = null;
        return false;
    }

    public void UpdateConnectorsState(PawnConnector settingConnector)
    {
        if (settingConnector)
        {
            if (GetPawnByConnector(settingConnector, out var selectedPawn))
            {
                foreach (var pawn in _pawns)
                {
                    foreach (var connector in pawn.Connectors)
                    {
                        if (pawn == selectedPawn)
                        {
                            if (connector == settingConnector)
                            {
                                connector.SetState(ConnectorState.Selected);
                            }
                            else
                            {
                                connector.SetState(ConnectorState.Default);
                            }
                        }
                        else
                        {
                            connector.SetState(ConnectorState.Active);
                        }
                    }
                }
            }
        }
        else
        {
            foreach (var pawn in _pawns)
            {
                foreach (var connector in pawn.Connectors)
                {
                    connector.SetState(ConnectorState.Default);
                }
            }
        }
    }
}