using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TestCrazyPawns._GameServices
{
    public class LocalConfigProvider : ConfigProviderBase
    {
        [SerializeField] private GameConfig gameConfig;

        public override UniTask<GameConfigData> LoadConfigData(CancellationToken token)
        {
            var gameConfigData = new GameConfigData();

            if (gameConfig)
            {
                gameConfigData = new GameConfigData
                {
                    DeskConfigData = new DeskConfigData
                    {
                        DeskSize = new Vector2(gameConfig.CrazyPawnSettings.CheckerboardSize,
                            gameConfig.CrazyPawnSettings.CheckerboardSize),
                        CellSize = gameConfig.CellSize,
                        BlackCelColor = gameConfig.CrazyPawnSettings.BlackCellColor,
                        WhiteCelColor = gameConfig.CrazyPawnSettings.WhiteCellColor
                    },

                    PawnConfigData = new PawnConfigData
                    {
                        PawnPrefab = gameConfig.PawnConfig.PawnPrefab,
                        ActiveConnectorMaterial = gameConfig.CrazyPawnSettings.ActiveConnectorMaterial,
                        SelectedConnectorMaterial = gameConfig.PawnConfig.SelectedConnectorMaterial,
                        DefaultConnectorMaterial = gameConfig.PawnConfig.DefaultMaterial,
                        DeleteMaterial = gameConfig.CrazyPawnSettings.DeleteMaterial,
                        InitialSpawnRadius = gameConfig.CrazyPawnSettings.InitialZoneRadius,
                        SpawnPawnCount = gameConfig.CrazyPawnSettings.InitialPawnCount,
                    }
                };
            }

            return UniTask.FromResult(gameConfigData);
        }
    }
}