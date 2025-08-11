using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float minDragTime;

    private bool _lMouseIsPressed;
    public float _pressedTime;
    private bool hasBeenPressedOnThisFrame;
    private bool _isDragging;

    public event Action onStartDrag;
    public event Action onEndDrag;
    public event Action onDrag;
    public event Action onClick;

    void Update()
    {
        _lMouseIsPressed = Input.GetMouseButton(0);

        if (_lMouseIsPressed)
        {
            _pressedTime += Time.deltaTime;
            hasBeenPressedOnThisFrame = true;

            if (!_isDragging && _pressedTime > minDragTime)
            {
                onStartDrag?.Invoke();
                Debug.Log("OnStartDrag");
            }
        }
        else
        {
            _pressedTime = 0f;

            if (hasBeenPressedOnThisFrame && _pressedTime < minDragTime && !_isDragging)
            {
                onClick?.Invoke();
                Debug.Log("OnClick");
                hasBeenPressedOnThisFrame = false;
            }

            if (_isDragging)
            {
                onEndDrag?.Invoke();
                Debug.Log("OnEndDrag)");
                _isDragging = false;
            }

            hasBeenPressedOnThisFrame = false;
        }

        if (_lMouseIsPressed && _pressedTime > minDragTime)
        {
            onDrag?.Invoke();
            // Debug.Log("OnDrag");
            _isDragging = true;
        }
    }
}