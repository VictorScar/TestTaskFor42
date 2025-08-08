using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    [SerializeField] private DragController dragController;
    
    private void Start()
    {
        input.onDrag += dragController.StartDragging;
        input.onEndDrag += dragController.EndDragging;
        input.onClick += dragController.StartSetConnection;
        input.onClick += dragController.EndSetConnection;
    }

    private void OnDestroy()
    {
        input.onClick -= dragController.StartSetConnection;
        input.onClick -= dragController.EndSetConnection;
        input.onDrag -= dragController.StartDragging;
        input.onEndDrag -= dragController.EndDragging;
    }
}
