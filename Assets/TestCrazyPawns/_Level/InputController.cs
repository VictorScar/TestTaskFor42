using UnityEngine;

namespace TestCrazyPawns._Level
{
    public class InputController : MonoBehaviour
    {
        private IPlayerInput _input;
        private DragController _dragController;
        private CameraController _cameraController;
    
        public void Init(IPlayerInput input, DragController dragController, CameraController cameraController)
        {
            _input = input;
            _dragController = dragController;
            _cameraController = cameraController;
            
            _input.onStartDrag += _dragController.OnStartDragElement;
            _input.onEndDrag += _dragController.OnEndDragElement;
            _input.onClick += _dragController.OnSelectConnector;
            _input.onScroll += _cameraController.ScalingCamera;
        }

        private void OnDisable()
        {
            _input.onStartDrag -= _dragController.OnStartDragElement;
            _input.onEndDrag -= _dragController.OnEndDragElement;
            _input.onClick -= _dragController.OnSelectConnector;
            _input.onScroll -= _cameraController.ScalingCamera;
        }
    }
}
