using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameCamera gameCamera;
    [SerializeField] private float moveCameraSpeed = 10f;
    [SerializeField] private float minDragThreshold = 0.25f;
    [SerializeField] private float scaleSpeed = 10f;

    private Vector3 _lastMovePosition;
    private bool _isDragging;

    private void Update()
    {
        if (_isDragging)
        {
            var mousePosition = Input.mousePosition;
            var cursorMoveVector = _lastMovePosition - mousePosition;
            var moveDistance = cursorMoveVector.sqrMagnitude;
          
            if (moveDistance >= minDragThreshold)
            {
                var moveDir = cursorMoveVector.normalized;
                MoveCamera(moveDir);
            }

            _lastMovePosition = mousePosition;
        }
    }

    public void StartDragCamera(Vector3 cursorPosition)
    {
        _isDragging = true;
    }

    public void EndDragCamera()
    {
        _isDragging = false;
    }

    public void ScalingCamera(float scrollValue)
    {
        gameCamera.Position += gameCamera.Forward * scaleSpeed * scrollValue * Time.deltaTime;
    }

    private void MoveCamera(Vector3 moveDir)
    {
        gameCamera.Position += new Vector3(moveDir.x, 0, moveDir.y) * moveCameraSpeed * Time.deltaTime;
    }
    
    
}