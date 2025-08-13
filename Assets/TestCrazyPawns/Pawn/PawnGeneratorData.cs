using TestCrazyPawns.Pawn;
using UnityEngine;

namespace TestCrazyPawns.Pawn
{
    public struct PawnGeneratorData
    {
        public float InitialSpawnRadius;
        public int SpawnPawnCount;
        public ChessFigure Prefab;
        public Transform PawnRoot;

        public PawnData PawnData;
    }
}