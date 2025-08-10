using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private DeskGenerator deskGenerator;
    [SerializeField] private PawnsGenerator pawnsGenerator;
    [SerializeField] private GameConfig gameConfig;
    [SerializeField] private Transform pawnsRoot;

    private void Start()
    {
        var generatorData = new DeskGeneratorData
        {
            DeskSize = gameConfig.DeskConfig.DeskSize,
            CellSize = gameConfig.DeskConfig.CellSize,
            BlackCelColor = gameConfig.DeskConfig.BlackCelColor,
            WhiteCelColor = gameConfig.DeskConfig.WhiteCelColor
        };

        deskGenerator.Generate(generatorData);

        var pawnData = new PawnData
        {
            DeleteMaterial = gameConfig.PawnConfig.DeleteMaterial,
            
        };

        var pawnGeneratorData = new PawnGeneratorData
        {
            InitialSpawnRadius = gameConfig.PawnConfig.InitialSpawnRadius,
            Prefab = gameConfig.PawnConfig.PawnPrefab,
            SpawnPawnCount = gameConfig.PawnConfig.SpawnPawnCount,
            PawnRoot = pawnsRoot,
            PawnData = pawnData
        };

        pawnsGenerator.GeneratePawns(pawnGeneratorData);
    }
}