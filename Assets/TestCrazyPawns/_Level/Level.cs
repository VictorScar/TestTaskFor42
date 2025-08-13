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

        private GameConfigData _gameConfigData;
        private PawnsDesk _desk;

        public PawnsDesk Desk => _desk;

        private void Start()
        {
            _gameConfigData = DIContainer.I.GetInstance<Game>().GameConfigData;
           
            _desk = deskGenerator.Generate(_gameConfigData.DeskConfigData);
            pawnsController.Init(_gameConfigData.PawnConfigData);
            dragController.Init(connectionsController, pawnsController, cameraController);
        }
    }
}