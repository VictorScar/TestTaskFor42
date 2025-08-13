using TestCrazyPawns._GameServices;
using TestCrazyPawns._Level;
using TestCrazyPawns.Connections;
using TestCrazyPawns.Desk;
using TestCrazyPawns.Pawn;
using TestCrazyPawns.Configs;
using TestCrazyPawns.Data;
using UnityEngine;

namespace TestCrazyPawns.Core
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private DeskGenerator deskGenerator;
        [SerializeField] private ConnectionsController connectionsController;
        [SerializeField] private DragController dragController;
        [SerializeField] private PawnsController pawnsController;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private GameCamera gameCamera;
        [SerializeField] private InputController inputController;
        
        private GameConfigData _gameConfigData;
        private PawnsDesk _desk;

        public PawnsDesk Desk => _desk;

        private void Start()
        {
            var game = DIContainer.I.GetInstance<Game>();
            _gameConfigData = game.GameConfigData;

            _desk = deskGenerator.Generate(_gameConfigData.DeskConfigData);
            pawnsController.Init(_gameConfigData.PawnConfigData);

            var dragControllerData = new DragControllerData
            {
                ConnectionController = connectionsController,
                PawnController = pawnsController,
                CameraController = cameraController,
                DragControllerParams = _gameConfigData.DragControllerParams,
                GameCamera = gameCamera
            };

            dragController.Init(dragControllerData);
            cameraController.Init(gameCamera, _gameConfigData.CameraControllerParams);
            inputController.Init(game.GameServices.Input, dragController, cameraController);
        }
    }
}