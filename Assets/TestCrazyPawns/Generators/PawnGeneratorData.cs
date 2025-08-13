using System.Collections;
using System.Collections.Generic;
using TestCrazyPawns._Pawn;
using UnityEngine;

public struct PawnGeneratorData
{
    public float InitialSpawnRadius;
    public int SpawnPawnCount;
    public ChessFigure Prefab;
    public Transform PawnRoot;

    public PawnData PawnData;
}