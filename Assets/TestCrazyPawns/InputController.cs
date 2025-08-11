using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    [SerializeField] private DragController dragController;
    
    private void Start()
    {
        input.onStartDrag += dragController.OnStartDragElement;
        input.onEndDrag += dragController.OnEndDraggElement;
        input.onClick += dragController.OnSelectConnector;
        
    }

    private void OnDestroy()
    {
        input.onStartDrag -= dragController.OnStartDragElement;
        input.onEndDrag -= dragController.OnEndDraggElement;
        input.onClick -= dragController.OnSelectConnector;
    }
}
