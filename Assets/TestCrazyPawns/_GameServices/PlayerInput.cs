using System;
using UnityEngine;

namespace TestCrazyPawns._GameServices
{
    public class PlayerInput : MonoBehaviour, IPlayerInput
    {
        [SerializeField] private float minDragTime = 0.2f;

        private float _pressedTime;
        private bool _lMouseIsPressed;
        private bool _hasBeenPressedOnThisFrame;
        private bool _isDragging;

        public event Action onStartDrag;
        public event Action onEndDrag;
        public event Action onDrag;
        public event Action onClick;
        public event Action<float> onScroll;

        public bool IsEnabled { get; set; } = true;

        void Update()
        {
            if (IsEnabled)
            {
                DragAndClickInput();
                MoveInputs();
            }
        }

        private void MoveInputs()
        {
            var scrollDelta = Input.mouseScrollDelta;
            onScroll?.Invoke(scrollDelta.y);
            //  Debug.Log(scrollDelta);
        }

        private void DragAndClickInput()
        {
            _lMouseIsPressed = Input.GetMouseButton(0);

            if (_lMouseIsPressed)
            {
                _pressedTime += Time.deltaTime;
                _hasBeenPressedOnThisFrame = true;

                if (_pressedTime > minDragTime)
                {
                    if (!_isDragging)
                    {
                        onStartDrag?.Invoke();
                        // Debug.Log("OnStartDrag");
                    }

                    onDrag?.Invoke();
                    _isDragging = true;
                }
            }
            else
            {
                _pressedTime = 0f;

                if (_hasBeenPressedOnThisFrame && _pressedTime < minDragTime && !_isDragging)
                {
                    onClick?.Invoke();
                    //Debug.Log("OnClick");
                    _hasBeenPressedOnThisFrame = false;
                }

                if (_isDragging)
                {
                    onEndDrag?.Invoke();
                    // Debug.Log("OnEndDrag)");
                    _isDragging = false;
                }

                _hasBeenPressedOnThisFrame = false;
            }
        }
    }
}