using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private DeskGenerator deskGenerator;
    [SerializeField] private PawnsGenerator pawnsGenerator;
    [SerializeField] private GameConfig gameConfig;
    [SerializeField] private Transform pawnsRoot;
    [SerializeField] private ConnectionsController connectionsController;
    [SerializeField] private DragController dragController;

    private PawnsDesk _desk;
    private List<Pawn> _pawns = new List<Pawn>();

    private void Start()
    {
        var generatorData = new DeskGeneratorData
        {
            DeskSize = gameConfig.DeskConfig.DeskSize,
            CellSize = gameConfig.DeskConfig.CellSize,
            BlackCelColor = gameConfig.DeskConfig.BlackCelColor,
            WhiteCelColor = gameConfig.DeskConfig.WhiteCelColor
        };

        _desk = deskGenerator.Generate(generatorData);

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

        _pawns = pawnsGenerator.GeneratePawns(pawnGeneratorData);

        dragController.Init(connectionsController);
    }
}