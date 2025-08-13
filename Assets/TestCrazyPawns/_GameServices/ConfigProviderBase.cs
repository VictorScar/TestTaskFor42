using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TestCrazyPawns._GameServices
{
    public abstract class ConfigProviderBase: MonoBehaviour
    {
        public abstract UniTask<GameConfigData> LoadConfigData(CancellationToken token);
    }
}