using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PawnsGenerator : MonoBehaviour
{
    [SerializeField] private int maxAttemptNumber;

    private float _spawnRadius;
    
    public List<Pawn> GeneratePawns(PawnGeneratorData data)
    {
        _spawnRadius = data.InitialSpawnRadius;
        var pawns = new List<Pawn>();
        
        for (int i = 0; i < data.SpawnPawnCount; i++)
        {
            var pawn = AddFigure(data);
            pawns.Add(pawn);
        }

        return pawns;
    }

    private Pawn AddFigure(PawnGeneratorData data)
    {
        var prefab = data.Prefab;
        var pawn = Instantiate(prefab, data.PawnRoot);

        if (TryGetNewPosition(_spawnRadius, out var figurePos))
        {
            pawn.Position = GetRandomPositionInRadius(data.InitialSpawnRadius);
            return pawn;
        }

        return null;
    }

    private bool TryGetNewPosition(float spawnRadius, out Vector2 newPos)
    {
        for (int i = 0; i < maxAttemptNumber; i++)
        {
            var randomPos = GetRandomPositionInRadius(spawnRadius);
            
            if (VerifyPosition(randomPos, spawnRadius))
            {
                newPos = randomPos;
                return true;
            }
        }

        newPos = new Vector2();
        return false;
    }
    
    private Vector2 GetRandomPositionInRadius(float spawnRadius)
    {
        var newPosition = new Vector2(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius));
        return newPosition;
    }

    private bool VerifyPosition(Vector3 position, float spawnRadius)
    {
        var distance = Vector3.Distance(Vector3.zero, position);

        return spawnRadius <= distance;
    }
}

