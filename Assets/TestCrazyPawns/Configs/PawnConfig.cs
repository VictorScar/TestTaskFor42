using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameConfigs/PawnConfig", fileName = "PawnConfig")]
public class PawnConfig : ScriptableObject
{
    [SerializeField] private Pawn pawnPrefab;
    [SerializeField] private float initialSpawnRadius;
    [SerializeField] private int spawnPawnCount;
    [SerializeField] private Material deleteMaterial;
    [SerializeField] private Color activeConnectorColor;
    [SerializeField] private Color inactiveConnectorColor;
    
    public Pawn PawnPrefab => pawnPrefab;
    public float InitialSpawnRadius => initialSpawnRadius;
    public int SpawnPawnCount => spawnPawnCount;
    public Material DeleteMaterial => deleteMaterial;
    public Color ActiveConnectorColor => activeConnectorColor;
    public Color InactiveConnectorColor => inactiveConnectorColor;
}