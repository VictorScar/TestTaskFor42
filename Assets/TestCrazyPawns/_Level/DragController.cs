using TestCrazyPawns.Connections;
using TestCrazyPawns._Pawn;
using TestCrazyPawns.Data;
using UnityEngine;

namespace TestCrazyPawns._Level
{
    public class DragController : MonoBehaviour
    {
        private GameCamera _gameCamera;
        [SerializeField] private Transform dragContainer;

        private float _scanDistance = 500f;
        private float _checkingVerticalOffset = 2f;
        private LayerMask _pawnsMask;
        private LayerMask _deskMask;
        private LayerMask _interactableMask;

        private Plane _plane = new Plane(Vector3.up, 0);
        private ChessFigure _draggableObject;
        private PawnConnector _settingConnector;
        private ConnectionsController _connectionController;
        private PawnsController _pawnsController;
        private CameraController _cameraController;

        private bool IsDragging => _draggableObject != null;
        private bool IsSettingConnector => _settingConnector != null;

        public ChessFigure DraggableObject
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
            var ray = _gameCamera.Camera.ScreenPointToRay(Input.mousePosition);

            if (_plane.Raycast(ray, out var distance))
            {
                dragContainer.position = ray.GetPoint(distance);
            }

            if (IsDragging)
            {
                var checkPointPosition =
                    _draggableObject.Position + _draggableObject.Transform.up * _checkingVerticalOffset;
                var deskRay = new Ray(checkPointPosition,
                    checkPointPosition - _draggableObject.Transform.up * _scanDistance);
                _draggableObject.IsMustDeleted = !Physics.Raycast(deskRay, _scanDistance, _deskMask);
            }
        }

        public void Init(DragControllerData data)
        {
            _connectionController = data.ConnectionController;
            _pawnsController = data.PawnController;
            _cameraController = data.CameraController;
            _gameCamera = data.GameCamera;

            var dragParams = data.DragControllerParams;
            _scanDistance = dragParams.DragScanDistance;
            _checkingVerticalOffset = dragParams.CheckingVerticalOffset;
            _pawnsMask = dragParams.PawnsMask;
            _deskMask = dragParams.DeskMask;
            _interactableMask = dragParams.InteractableMask;
        }

        public void OnStartDragElement()
        {
            var cursorPosition = Input.mousePosition;
            var ray = _gameCamera.Camera.ScreenPointToRay(cursorPosition);

            if (Physics.Raycast(ray, out var scanResult, _scanDistance,
                    _interactableMask))
            {
                if (scanResult.collider.TryGetComponent<global::TestCrazyPawns._Pawn.ChessFigure>(out var draggableObject))
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
                else
                {
                    _cameraController.StartDragCamera(cursorPosition);
                }
            }
        }

        public void OnEndDragElement()
        {
            if (IsDragging)
            {
                EndDragging();
            }
            else if (IsSettingConnector)
            {
                EndSetConnection();
            }
            else
            {
                _cameraController.EndDragCamera();
            }
        }

        public void OnSelectConnector()
        {
            if (!IsSettingConnector)
            {
                var ray = _gameCamera.Camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var scanResult, _scanDistance,
                        _pawnsMask))
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

        private void StartDragging(global::TestCrazyPawns._Pawn.ChessFigure draggableObject)
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

        private void RemovePawn(global::TestCrazyPawns._Pawn.ChessFigure chessFigure)
        {
            _connectionController.RemoveConnectionsByPawn(chessFigure.Connectors);
            _pawnsController.RemovePawn(chessFigure);
        }

        private void StartSetConnection(PawnConnector connector)
        {
            _settingConnector = connector;
            _pawnsController.UpdateConnectorsState(_settingConnector);
        }

        private void EndSetConnection()
        {
            var ray = _gameCamera.Camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var scanResult, _scanDistance,
                    _pawnsMask))
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
}