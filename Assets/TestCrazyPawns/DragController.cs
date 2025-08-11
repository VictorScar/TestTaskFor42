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
    private Pawn _draggableObject;
    private PawnConnector _settingConnector;
    private ConnectionsController _connectionController;
    private PawnsController _pawnsController;

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
            var checkPointPosition = _draggableObject.Position + _draggableObject.Transform.up * 2f;
            var deskRay = new Ray(checkPointPosition,
                checkPointPosition - _draggableObject.Transform.up * scanDistance);
            _draggableObject.IsMustDeleted = !Physics.Raycast(deskRay, scanDistance, deskMask);
        }

        if (IsSettingConnector)
        {
            
            /*foreach (var pawn in _pawnsController.Pawns)
            {
                foreach (var connector in pawn.Connectors)
                {
                   
                    //connector.Color  = pawn.Color
                }
              
            }*/
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
                if (scanResult.collider.TryGetComponent<Pawn>(out var draggableObject))
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

    private void RemovePawn(Pawn pawn)
    {
        _connectionController.RemoveConnectionsByPawn(pawn.Connectors);
        _pawnsController.RemovePawn(pawn);
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
                    _pawnsController.UpdateConnectorsState(_settingConnector);
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
                    if (_pawnsController.GetPawnByConnector(connector, out var connectorPawn))
                    {
                        if (!connectorPawn.IsContainConnector(_settingConnector))
                        {
                            _connectionController.AddConnection(_settingConnector, connector);
                        }
                    }
                  
                    _settingConnector = null;
                    _pawnsController.UpdateConnectorsState(_settingConnector);
                }
            }
        }
    }

    public void Init(ConnectionsController connectionsController, PawnsController pawnsController)
    {
        _connectionController = connectionsController;
        _pawnsController = pawnsController;
    }
}