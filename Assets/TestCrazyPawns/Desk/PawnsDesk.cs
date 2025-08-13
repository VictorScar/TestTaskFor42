using UnityEngine;

namespace TestCrazyPawns.Desk
{
    public class PawnsDesk : MonoBehaviour
    {
        [SerializeField] private Transform root;
    
        public Transform Root => root.transform;
    }
}
