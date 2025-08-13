using CrazyPawn;
using TestCrazyPawns.Data;
using UnityEngine;

[CreateAssetMenu(menuName = "GameConfigs/GameConfig", fileName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    [SerializeField] private DeskConfig deskConfig;
    [SerializeField] private PawnConfig pawnConfig;
    [SerializeField] private CrazyPawnSettings crazyPawnSettings;
    [SerializeField] private Vector2 cellSize = new Vector2(1.5f, 1.5f);
    [SerializeField] private DragControllerParams dragControllerParams;
    [SerializeField] private CameraControllerParams cameraControllerParams;

    public CrazyPawnSettings CrazyPawnSettings => crazyPawnSettings;
    public DragControllerParams DragControllerParams => dragControllerParams;
    public CameraControllerParams CameraControllerParams => cameraControllerParams;
    public PawnConfig PawnConfig => pawnConfig;
    public Vector2 CellSize => cellSize;
   
}
