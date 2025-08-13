using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    [SerializeField] private DragController dragController;
    [SerializeField] private CameraController cameraController;
    
    private void Start()
    {
        input.onStartDrag += dragController.OnStartDragElement;
        input.onEndDrag += dragController.OnEndDragElement;
        input.onClick += dragController.OnSelectConnector;
        input.onScroll += cameraController.ScalingCamera;

    }

    private void OnDestroy()
    {
        input.onStartDrag -= dragController.OnStartDragElement;
        input.onEndDrag -= dragController.OnEndDragElement;
        input.onClick -= dragController.OnSelectConnector;
        input.onScroll -= cameraController.ScalingCamera;
    }
}
