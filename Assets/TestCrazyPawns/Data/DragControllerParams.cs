using System;
using UnityEngine;

namespace TestCrazyPawns.Data
{
    [Serializable]
    public struct DragControllerParams
    {
        public float DragScanDistance;
        public float CheckingVerticalOffset;
        public LayerMask PawnsMask;
        public LayerMask DeskMask;
        public LayerMask InteractableMask;
    }
}
