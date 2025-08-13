using TestCrazyPawns.Pawn;
using UnityEngine;

namespace TestCrazyPawns.Configs
{
    [CreateAssetMenu(menuName = "GameConfigs/PawnConfig", fileName = "PawnConfig")]
    public class PawnConfig : ScriptableObject
    {
        [SerializeField] private ChessFigure chessFigurePrefab;
        [SerializeField] private Material selectedConnectorConnectorMaterial;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private int maxAttemptGeneratePawn = 15;
        public ChessFigure ChessFigurePrefab => chessFigurePrefab;
        public Material SelectedConnectorMaterial => selectedConnectorConnectorMaterial;
        public Material DefaultMaterial => defaultMaterial;
        public int MaxAttemptGeneratePawn => maxAttemptGeneratePawn;
    }
}