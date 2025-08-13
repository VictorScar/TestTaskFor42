using System;
using UnityEngine;

namespace TestCrazyPawns.Pawn
{
    [Serializable]
    public struct PawnData
    {
        public Material DeleteMaterial;
  
        public ConnectorData ConnectorData { get; set; }
    }
}