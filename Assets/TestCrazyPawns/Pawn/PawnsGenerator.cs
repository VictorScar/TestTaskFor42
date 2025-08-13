using System.Collections.Generic;
using TestCrazyPawns.Pawn;
using UnityEngine;

namespace TestCrazyPawns.Pawn
{
    public class PawnsGenerator
    {
        private int _maxAttemptNumber;
        private float _spawnRadius;

        public PawnsGenerator(int maxAttemptNumber)
        {
            _maxAttemptNumber = maxAttemptNumber;
        }

        public List<ChessFigure> GeneratePawns(PawnGeneratorData data)
        {
            _spawnRadius = data.InitialSpawnRadius;

            var pawns = new List<ChessFigure>();

            for (int i = 0; i < data.SpawnPawnCount; i++)
            {
                var pawn = AddFigure(data);

                if (pawn)
                {
                    pawns.Add(pawn);
                }
            }

            return pawns;
        }

        private ChessFigure AddFigure(PawnGeneratorData data)
        {
            var prefab = data.Prefab;


            if (TryGetNewPosition(_spawnRadius, out var figurePos))
            {
                var pawn = Object.Instantiate(prefab, data.PawnRoot);
                pawn.Init(data.PawnData);
                pawn.Position = GetRandomPositionInRadius(data.InitialSpawnRadius);
                return pawn;
            }

            return null;
        }

        private bool TryGetNewPosition(float spawnRadius, out Vector2 newPos)
        {
            for (int i = 0; i < _maxAttemptNumber; i++)
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
}