using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _cameraTarget;
    [SerializeField] private PlayerCameraController _cam;

    public Transform CameraTarget => _cameraTarget;
}
