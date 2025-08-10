using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PawnGeneratorData
{
    public float InitialSpawnRadius;
    public int SpawnPawnCount;
    public Pawn Prefab;
    public Transform PawnRoot;

    public PawnData PawnData;
}