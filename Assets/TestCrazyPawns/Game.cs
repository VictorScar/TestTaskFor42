using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private DeskGenerator deskGenerator;
    [SerializeField] private GameConfig gameConfig;
    [SerializeField] private ConnectionsController connectionsController;
    [SerializeField] private DragController dragController;
    [SerializeField] private PawnsController pawnsController;

    private PawnsDesk _desk;

    public PawnsDesk Desk => _desk;

    private void Start()
    {
        var generatorData = new DeskGeneratorData
        {
            DeskSize = new Vector2(gameConfig.CrazyPawnSettings.CheckerboardSize,
                gameConfig.CrazyPawnSettings.CheckerboardSize),
            CellSize = gameConfig.CellSize,
            BlackCelColor = gameConfig.CrazyPawnSettings.BlackCellColor,
            WhiteCelColor = gameConfig.CrazyPawnSettings.WhiteCellColor
        };

        var pawnConfigData = new PawnConfigData
        {
            PawnPrefab = gameConfig.PawnConfig.PawnPrefab,
            ActiveConnectorMaterial = gameConfig.CrazyPawnSettings.ActiveConnectorMaterial,
            SelectedConnectorMaterial = gameConfig.PawnConfig.SelectedConnectorMaterial,
            DefaultConnectorMaterial = gameConfig.PawnConfig.DefaultMaterial,
            DeleteMaterial = gameConfig.CrazyPawnSettings.DeleteMaterial,
            InitialSpawnRadius = gameConfig.CrazyPawnSettings.InitialZoneRadius,
            SpawnPawnCount = gameConfig.CrazyPawnSettings.InitialPawnCount,
        };

        _desk = deskGenerator.Generate(generatorData);
        pawnsController.Init(pawnConfigData);
        dragController.Init(connectionsController, pawnsController);
    }
}