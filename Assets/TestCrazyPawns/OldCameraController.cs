using System;
using System.Collections.Generic;
using UnityEngine;

public class OldCameraController : MonoBehaviour
{
    [SerializeField] private Camera _gameCamera;
    [SerializeField] private float _moveSpeed = 20f;
    [SerializeField] private float borderThreshold = 5f;

    private Vector3 _lastCursorPosition;
    private List<BorderIntersectHandler> _intersectHandler = new List<BorderIntersectHandler>();


    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (CheckBorders(out var crossingDirection))
        {
            Debug.Log(crossingDirection);
            MoveCamera(crossingDirection);
        }
    }

    public void Init()
    {
        _intersectHandler.Add(new BorderIntersectHandler((v) => Screen.width - v.x < borderThreshold,
            Vector3.right));
        _intersectHandler.Add(new BorderIntersectHandler((v) => v.x < borderThreshold,
            Vector3.left));
        _intersectHandler.Add(new BorderIntersectHandler((v) => Screen.height - v.y < borderThreshold,
            Vector3.up));
        _intersectHandler.Add(new BorderIntersectHandler((v) => v.y < borderThreshold,
            Vector3.down));
    }

    private bool CheckBorders(out Vector3 borderCrossingDirection)
    {
        var cursorPosition = Input.mousePosition;
        var mouseMoveDirection = (cursorPosition - _lastCursorPosition).normalized;

        foreach (var handler in _intersectHandler)
        {
            if (handler.IsIntersectBorderInDirection(cursorPosition, mouseMoveDirection, out var totalDir))
            {
                borderCrossingDirection = totalDir;
                return true;
            }
        }

        borderCrossingDirection = new Vector3();
        return false;
    }

    private bool IsIntersectBorderInDirection(BorderIntersectHandler handler, out Vector3 intersectDirection)
    {
        if (handler.Condition(handler.BorderDir))
        {
            intersectDirection = handler.BorderDir;
            return true;
        }
        else
        {
            intersectDirection = new Vector3();
            return false;
        }
    }

    public void MoveCamera(Vector3 moveDirection)
    {
        _gameCamera.transform.position += moveDirection * _moveSpeed * Time.deltaTime;
    }
}

public class BorderIntersectHandler
{
    public Func<Vector3, bool> Condition;
    public Vector3 BorderDir;

    public BorderIntersectHandler(Func<Vector3, bool> condition, Vector3 borderDir)
    {
        Condition = condition;
        BorderDir = borderDir;
    }

    public bool IsIntersectBorderInDirection(Vector3 inputPos, Vector3 inputDir, out Vector3 intersectDirection)
    {
        if (Condition(inputPos))
        {
            var dirDot = Vector3.Dot(inputDir, BorderDir);

            if (dirDot > 0.5f)
            {
                intersectDirection = BorderDir;
                return true;
            }
        }

        intersectDirection = new Vector3();
        return false;
    }
}