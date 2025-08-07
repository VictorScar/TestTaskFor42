using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DragController : MonoBehaviour
{
    [SerializeField] private Camera scanningSource;
    [SerializeField] private Transform dragContainer;

    [SerializeField] private float scanDistance = 500f;
    [SerializeField] private LayerMask scanningMask;

    private IDraggableObject _draggableObject;
    private bool isDragging => _draggableObject != null;
    private Plane plane = new Plane(Vector3.up, 0);

    public IDraggableObject DraggableObject
    {
        get => _draggableObject;
        set
        {
            _draggableObject = value;

            if (value!= null)
            {
                _draggableObject.Transform.SetParent(dragContainer);
            }
        }
    }

    private void Update()
    {
        var ray = scanningSource.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out var distance))
        {
            dragContainer.position = ray.GetPoint(distance);
        }

        if (!isDragging && Input.GetMouseButton(0))
        {
            StartDragging();
        }
    }

    private void StartDragging()
    {
        var ray = scanningSource.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var scanResult, scanDistance,
                scanningMask))
        {
            if (scanResult.collider.TryGetComponent<IDraggableObject>(out var draggableObject))
            {
                DraggableObject = draggableObject;
                _draggableObject.Activate();
            }
        }
    }
}