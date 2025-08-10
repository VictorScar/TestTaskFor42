using UnityEngine;
using UnityEngine.Serialization;

public class DragController : MonoBehaviour
{
    [SerializeField] private Camera gameCamera;
    [SerializeField] private Transform dragContainer;

    [SerializeField] private float scanDistance = 500f;
    [SerializeField] private LayerMask pawnsMask;
    [SerializeField] private LayerMask deskMask;

    private Plane plane = new Plane(Vector3.up, 0);
    private IDraggableObject _draggableObject;
    private PawnConnector _settingConnector;
    private ConnectionsController _connectionController;

    private bool IsDragging => _draggableObject != null;
    private bool IsSettingConnector => _settingConnector != null;

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
        var ray = gameCamera.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out var distance))
        {
            dragContainer.position = ray.GetPoint(distance);
        }

        if (IsDragging)
        {
            var checkPointPosition = _draggableObject.Position + _draggableObject.Transform.up * 2f;
            var deskRay = new Ray(checkPointPosition, checkPointPosition - _draggableObject.Transform.up * scanDistance);
            _draggableObject.IsMustDeleted = !Physics.Raycast(deskRay, scanDistance, deskMask);

            Debug.DrawRay(checkPointPosition, checkPointPosition - _draggableObject.Transform.up * scanDistance, Color.magenta);
            Debug.Log("MustBeDeleted: " + _draggableObject.IsMustDeleted);
        }
    }

    public void StartDragging()
    {
        if (!IsDragging)
        {
            var ray = gameCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var scanResult, scanDistance,
                    pawnsMask))
            {
                if (scanResult.collider.TryGetComponent<IDraggableObject>(out var draggableObject))
                {
                    DraggableObject = draggableObject;
                    DraggableObject.Activate();
                }
            }
        }
    }

    public void EndDragging()
    {
        if (IsDragging)
        {
            DraggableObject.Deactivate();

            if (DraggableObject.IsMustDeleted)
            {
                RemovePawn(DraggableObject);
                
            }
            DraggableObject = null;
        }
    }

    private void RemovePawn(IDraggableObject draggableObject)
    {
        _connectionController.RemoveConnectionsByPawn(DraggableObject.Connectors);
        DraggableObject.Remove();
    }

    public void StartSetConnection()
    {
        if (!IsSettingConnector)
        {
            var ray = gameCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var scanResult, scanDistance,
                    pawnsMask))
            {
                if (scanResult.collider.TryGetComponent<PawnConnector>(out var connector))
                {
                    _settingConnector = connector;
                }
            }
        }
        else
        {
            EndSetConnection();
        }
    }

    public void EndSetConnection()
    {
        if (IsSettingConnector)
        {
            var ray = gameCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var scanResult, scanDistance,
                    pawnsMask))
            {
                if (scanResult.collider.TryGetComponent<PawnConnector>(out var connector))
                {
                    if (connector != _settingConnector)
                    {
                        _connectionController.AddConnection(_settingConnector, connector);
                        _settingConnector = null;
                    }
                }
            }
        }
    }

    public void Init(ConnectionsController connectionsController)
    {
        _connectionController = connectionsController;
    }
}