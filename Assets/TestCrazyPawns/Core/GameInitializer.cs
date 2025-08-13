using ScarFramework.UI;
using TestCrazyPawns._GameServices;
using UnityEngine;

namespace TestCrazyPawns.Core
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private LocalConfigProvider configProviderPrefab;
        [SerializeField] private PlayerInput inputPrefab;
        [SerializeField] private UISystem uiSystemPrefab;
        [SerializeField] private ScenesLoadController scenesLoadControllerPrefab;
        [SerializeField] private Game gamePrefab;

        private Game _game;
        private GameServices _gameServices;

        public void Init()
        {
            _game = Instantiate(gamePrefab);
            var input = Instantiate(inputPrefab, _game.transform);
            var configProvider = Instantiate(configProviderPrefab, _game.transform);
            var uiSystem = Instantiate(uiSystemPrefab, _game.transform);
            var sceneController = Instantiate(scenesLoadControllerPrefab, _game.transform); 
            _gameServices = new GameServices(input, configProvider, uiSystem, sceneController);
            _game.Init(_gameServices);
        }
    }
}