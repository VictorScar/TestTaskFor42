using UnityEngine;

namespace TestCrazyPawns._Level
{
  public class GameCamera : MonoBehaviour
  {
    [SerializeField] private Camera cam;

    public Camera Camera => cam;
  
    public Vector3 Position
    {
      get => transform.position;
      set => transform.position = value;
    }

    public Vector3 Forward => transform.forward;
  }
}
