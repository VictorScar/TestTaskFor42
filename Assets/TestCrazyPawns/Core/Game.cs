using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TestCrazyPawns.Core
{
    public class Game : MonoBehaviour
    {
        private GameServices _gameServices;
        private GameConfigData _gameConfigData;
        private CancellationTokenSource _gameTokenSource;

        public GameServices GameServices => _gameServices;
        public GameConfigData GameConfigData => _gameConfigData;

        public void Init(GameServices gameServices)
        {
            _gameServices = gameServices;
            DIContainer.I.RegisterComponent(this);
            StartGame();
        }

        private async UniTask StartGame()
        {
            _gameTokenSource = new CancellationTokenSource();

            await LoadConfiguration(_gameTokenSource.Token);
            await _gameServices.ScenesLoadController.LoadGameScene(_gameTokenSource.Token);
        }

        private async UniTask LoadConfiguration(CancellationToken token)
        {
            _gameConfigData = await _gameServices.ConfigProvider.LoadConfigData(token);
        }
    }
}