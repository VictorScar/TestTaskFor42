using UnityEngine;

[CreateAssetMenu(menuName = "GameConfigs/PawnConfig", fileName = "PawnConfig")]
public class PawnConfig : ScriptableObject
{
    [SerializeField] private Pawn pawnPrefab;
    [SerializeField] private Material selectedConnectorConnectorMaterial;
    [SerializeField] private Material defaultMaterial;
    
    public Pawn PawnPrefab => pawnPrefab;
    public Material SelectedConnectorMaterial => selectedConnectorConnectorMaterial;
    public Material DefaultMaterial => defaultMaterial;
}