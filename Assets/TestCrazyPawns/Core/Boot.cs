using UnityEngine;

namespace TestCrazyPawns.Core
{
    public class Boot : MonoBehaviour
    {
        [SerializeField] private DIContainer di;
        [SerializeField] private GameInitializer initializer;

        void Start()
        {
            di.Init();
            initializer.Init();
        }
    }
}