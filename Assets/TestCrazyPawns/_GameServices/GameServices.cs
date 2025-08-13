using ScarFramework.UI;
using TestCrazyPawns._GameServices;

namespace TestCrazyPawns.Core
{
    public class GameServices
    {
        private IPlayerInput _playerInput;
        private ConfigProviderBase _configProvider;
        private UISystem _uiSystem;
        private ScenesLoadController _scenesLoadController;
        public IPlayerInput Input => _playerInput;
        public ConfigProviderBase ConfigProvider => _configProvider;
        public UISystem UISystem => _uiSystem;
        public ScenesLoadController ScenesLoadController => _scenesLoadController;
        
        public GameServices(IPlayerInput playerInput, ConfigProviderBase configProvider, UISystem uiSystem, ScenesLoadController scenesLoadController)
        {
            _playerInput = playerInput;
            _configProvider = configProvider;
            _uiSystem = uiSystem;
            _scenesLoadController = scenesLoadController;

            Init();
        }

        private void Init()
        {
            _uiSystem.Init();
        }
    }
}