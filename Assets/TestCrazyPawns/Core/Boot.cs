using TestCrazyPawns._GameServices;
using UnityEngine;

namespace TestCrazyPawns.Core
{
    public class Boot : MonoBehaviour
    {
        [SerializeField] private DIContainer di;
        [SerializeField] private GameInitializer initializerPrefab;

        void Start()
        {
            di.Init();
            var initializer = Instantiate(initializerPrefab);
            initializer.Init();
        }
    }
}