using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnsGenerator : MonoBehaviour
{
    [SerializeField] private Pawn pawnPrefab;

    public void GeneratePawns(PawnGeneratorData data)
    {
        var prefab = data.Prefab;

        for (int i = 0; i < data.SpawnPawnCount; i++)
        {
            
        }
    }

    private Vector3 GetFreePosition()
    {
        return new Vector3();
    }
}

