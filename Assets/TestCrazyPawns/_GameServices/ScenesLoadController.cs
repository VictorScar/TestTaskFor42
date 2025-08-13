using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TestCrazyPawns._GameServices
{
    public class ScenesLoadController : MonoBehaviour
    {
        [SerializeField] private int gameplaySceneIndex = 1;
    
        public async UniTask LoadGameScene(CancellationToken token)
        {
            var loading = SceneManager.LoadSceneAsync(gameplaySceneIndex);
            await loading;
        }
    }
}
