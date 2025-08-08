using UnityEngine;

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
            if (_draggableObject != null)
            {
                _draggableObject.Transform.SetParent(null);
            }

            if (value != null)
            {
                value.Transform.SetParent(dragContainer);
            }

            _draggableObject = value;
        }
    }

    private void Update()
    {
        var ray = scanningSource.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out var distance))
        {
            dragContainer.position = ray.GetPoint(distance);
        }
    }

    public void StartDragging()
    {
        if (!isDragging)
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

    public void EndDragging()
    {
        if (isDragging)
        {
            _draggableObject.Deactivate();
            DraggableObject = null;
        }
    }

    public void StartSetConnection()
    {
        Debug.Log("Start set connection");
    }

    public void EndSetConnection()
    {
        Debug.Log("End set connection");
    }
}