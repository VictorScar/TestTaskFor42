using TestCrazyPawns.Data;
using TestCrazyPawns.Desk;
using TestCrazyPawns.Pawn;
using UnityEngine;

namespace TestCrazyPawns.Configs
{
    public class GameConfigData
    {
        public DeskConfigData DeskConfigData;
        public PawnConfigData PawnConfigData;
        public DragControllerParams DragControllerParams;
        public CameraControllerParams CameraControllerParams;
        public Vector2 CellSize;
    }
}