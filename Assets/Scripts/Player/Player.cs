using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private Transform _cameraTarget;

    void Start()
    {
        if (!isLocalPlayer) return;

        GetComponent<PlayerMovement>().SetCameraTransform(Camera.main.transform);
        Camera.main.GetComponent<PlayerCameraController>().SetTarget(_cameraTarget);
    }
}
