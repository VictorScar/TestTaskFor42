using System;

namespace TestCrazyPawns.Data
{
    [Serializable]
    public struct CameraControllerParams
    {
        public float MoveCameraSpeed;
        public float MinDragThreshold;
        public float ScaleSpeed;
    }
}
