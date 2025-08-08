using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameConfigs/GameConfig", fileName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    [SerializeField] private DeskConfig deskConfig;
    [SerializeField] private PawnConfig pawnConfig;
    
    public DeskConfig DeskConfig => deskConfig;
    public PawnConfig PawnConfig => pawnConfig;
}
