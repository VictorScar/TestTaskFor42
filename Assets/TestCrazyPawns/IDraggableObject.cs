using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDraggableObject
{
    Transform Transform { get; }
    Vector3 Position { get; set; }
    void Activate();
    void Deactivate();
    bool IsMustDeleted { get; set; }
    void Destroy();
}