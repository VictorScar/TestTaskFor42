using TestCrazyPawns._Level;
using TestCrazyPawns.Pawn;
using TestCrazyPawns.Connections;

namespace TestCrazyPawns.Data
{
    public struct DragControllerData
    {
        public ConnectionsController ConnectionController;
        public PawnsController PawnController;
        public CameraController CameraController;
        public DragControllerParams DragControllerParams;
        public GameCamera GameCamera;
    }
}
