using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInput
{
    public event Action onStartDrag;
    public event Action onEndDrag;
    public event Action onDrag;
    public event Action onClick;
    public event Action<float> onScroll;
    
    public bool IsEnabled { get; set; }
}
