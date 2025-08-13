using TestCrazyPawns.Pawn;
using UnityEngine;

namespace TestCrazyPawns.Pawn
{
    public struct PawnConfigData
    {
        public Material ActiveConnectorMaterial;
        public Material SelectedConnectorMaterial;
        public Material DefaultConnectorMaterial;
        public Material DeleteMaterial;
        public ChessFigure ChessFigurePrefab;
        public int SpawnPawnCount;
        public float InitialSpawnRadius;
        public int MaxAttemptGeneratePawn;
    }
}