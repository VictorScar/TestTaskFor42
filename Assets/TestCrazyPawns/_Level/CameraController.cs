using TestCrazyPawns.Data;
using UnityEngine;

namespace TestCrazyPawns._Level
{
    public class CameraController : MonoBehaviour
    {
        private GameCamera _gameCamera;
        private float _moveCameraSpeed = 25f;
        private float _minDragThreshold = 2f;
        private float _scaleSpeed = 25f;

        private Vector3 _lastMovePosition;
        private bool _isDragging;

        private void Update()
        {
            if (_isDragging)
            {
                var mousePosition = Input.mousePosition;
                var cursorMoveVector = _lastMovePosition - mousePosition;
                var moveDistance = cursorMoveVector.sqrMagnitude;
          
                if (moveDistance >= _minDragThreshold)
                {
                    var moveDir = cursorMoveVector.normalized;
                    MoveCamera(moveDir);
                }

                _lastMovePosition = mousePosition;
            }
        }
    
        public void Init(GameCamera gameCamera, CameraControllerParams controllerParams)
        {
            _gameCamera = gameCamera;
            _moveCameraSpeed = controllerParams.MoveCameraSpeed;
            _minDragThreshold = controllerParams.MinDragThreshold;
            _scaleSpeed = controllerParams.ScaleSpeed;
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
            _gameCamera.Position += _gameCamera.Forward * _scaleSpeed * scrollValue * Time.deltaTime;
        }

        private void MoveCamera(Vector3 moveDir)
        {
            _gameCamera.Position += new Vector3(moveDir.x, 0, moveDir.y) * _moveCameraSpeed * Time.deltaTime;
        }


  
    }
}