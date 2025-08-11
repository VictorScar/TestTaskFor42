using UnityEngine;

public class DragController : MonoBehaviour
{
    [SerializeField] private Camera gameCamera;
    [SerializeField] private Transform dragContainer;

    [SerializeField] private float scanDistance = 500f;
    [SerializeField] private LayerMask pawnsMask;
    [SerializeField] private LayerMask deskMask;
    [SerializeField] private LayerMask interactableMask;

    private Plane plane = new Plane(Vector3.up, 0);
    private Pawn _draggableObject;
    private PawnConnector _settingConnector;
    private ConnectionsController _connectionController;
    private PawnsController _pawnsController;
    private float _checkingVerticalOffset = 2f;

    private bool IsDragging => _draggableObject != null;
    private bool IsSettingConnector => _settingConnector != null;

    public Pawn DraggableObject
    {
        get => _draggableObject;
        set
        {
            if (_draggableObject != null)
            {
                _draggableObject.Transform.SetParent(_pawnsController.transform);
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
            var checkPointPosition =
                _draggableObject.Position + _draggableObject.Transform.up * _checkingVerticalOffset;
            var deskRay = new Ray(checkPointPosition,
                checkPointPosition - _draggableObject.Transform.up * scanDistance);
            _draggableObject.IsMustDeleted = !Physics.Raycast(deskRay, scanDistance, deskMask);
        }
    }

    public void Init(ConnectionsController connectionsController, PawnsController pawnsController)
    {
        _connectionController = connectionsController;
        _pawnsController = pawnsController;
    }

    public void OnStartDragElement()
    {
        var ray = gameCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var scanResult, scanDistance,
                interactableMask))
        {
            if (scanResult.collider.TryGetComponent<Pawn>(out var draggableObject))
            {
                if (!IsDragging)
                {
                    StartDragging(draggableObject);
                }
            }
            else if (scanResult.collider.TryGetComponent<PawnConnector>(out var connector))
            {
                StartSetConnection(connector);
            }
        }
    }

    public void OnEndDraggElement()
    {
        if (IsDragging)
        {
            EndDragging();
        }
        else if (IsSettingConnector)
        {
            EndSetConnection();
        }
    }

    public void OnSelectConnector()
    {
        if (!IsSettingConnector)
        {
            var ray = gameCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var scanResult, scanDistance,
                    pawnsMask))
            {
                if (scanResult.collider.TryGetComponent<PawnConnector>(out var connector))
                {
                    StartSetConnection(connector);
                }
            }
        }
        else
        {
            EndSetConnection();
        }
    }

    private void StartDragging(Pawn draggableObject)
    {
        DraggableObject = draggableObject;
        DraggableObject.Activate();
    }

    private void EndDragging()
    {
        DraggableObject.Deactivate();

        if (DraggableObject.IsMustDeleted)
        {
            RemovePawn(DraggableObject);
        }

        DraggableObject = null;
    }

    private void RemovePawn(Pawn pawn)
    {
        _connectionController.RemoveConnectionsByPawn(pawn.Connectors);
        _pawnsController.RemovePawn(pawn);
    }

    private void StartSetConnection(PawnConnector connector)
    {
        _settingConnector = connector;
        _pawnsController.UpdateConnectorsState(_settingConnector);
    }

    private void EndSetConnection()
    {
        var ray = gameCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var scanResult, scanDistance,
                pawnsMask))
        {
            if (scanResult.collider.TryGetComponent<PawnConnector>(out var connector))
            {
                if (_pawnsController.GetPawnByConnector(connector, out var connectorPawn))
                {
                    if (!connectorPawn.IsContainConnector(_settingConnector))
                    {
                        _connectionController.AddConnection(_settingConnector, connector);
                    }
                }
            }
        }

        _settingConnector = null;
        _pawnsController.UpdateConnectorsState(_settingConnector);
    }
}