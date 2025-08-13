using System.Collections.Generic;
using TestCrazyPawns.Pawn;
using UnityEngine;

namespace TestCrazyPawns.Pawn
{
    public class PawnsController : MonoBehaviour
    {
        private List<global::TestCrazyPawns.Pawn.ChessFigure> _pawns = new List<global::TestCrazyPawns.Pawn.ChessFigure>();
        private PawnsGenerator _generator;

        public List<global::TestCrazyPawns.Pawn.ChessFigure> Pawns => _pawns;

        public void Init(PawnConfigData pawnConfigData)
        {
            _generator = new PawnsGenerator(pawnConfigData.MaxAttemptGeneratePawn);

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
                Prefab = pawnConfigData.ChessFigurePrefab,
                InitialSpawnRadius = pawnConfigData.InitialSpawnRadius,
                PawnData = pawnData,
                PawnRoot = transform
            };

            _pawns = _generator.GeneratePawns(generatorData);
        }

        public bool RemovePawn(global::TestCrazyPawns.Pawn.ChessFigure chessFigure)
        {
            if (chessFigure)
            {
                _pawns.Remove(chessFigure);

                Destroy(chessFigure.gameObject);
                return true;
            }

            return false;
        }

        public bool GetPawnByConnector(PawnConnector connector, out global::TestCrazyPawns.Pawn.ChessFigure connectorChessFigure)
        {
            if (connector)
            {
                foreach (var pawn in _pawns)
                {
                    if (pawn.IsContainConnector(connector))
                    {
                        connectorChessFigure = pawn;
                        return true;
                    }
                }
            }

            connectorChessFigure = null;
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
}