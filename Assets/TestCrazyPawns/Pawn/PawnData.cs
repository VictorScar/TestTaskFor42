using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PawnData
{
    public Material DeleteMaterial;
  
    public ConnectorData ConnectorData { get; set; }
}