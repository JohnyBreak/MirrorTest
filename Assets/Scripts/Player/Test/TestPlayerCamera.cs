using Mirror;
using UnityEngine;

public class TestPlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _mouseSensitivity = 3f;
    [SerializeField] private float _distanceFromTarget = 6f;
    [SerializeField] private float _smoothTime = 0.2f;
    [SerializeField] private Vector2 _rotationMinMax = new Vector2(-45, 60);
    [SerializeField] private LayerMask _obstacles;

    private GameStateManager _gameStateManager;
    private float _rotationX;
    private float _rotationY;
    private Vector3 _currentRotation;
    private Vector3 _smoothVelocity;

    private void Start()
    {
        var nm = (CustomNetworkManager)NetworkManager.singleton;
        _gameStateManager = nm.GameSystem.GameStateManager;
    }

    void LateUpdate()
    {
        if (_gameStateManager.CurrentGameState != GameStateManager.GameState.GamePlay) return;
        Rotate();
        CheckObstacles();
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * _mouseSensitivity;

        _rotationX += mouseX;
        _rotationY -= mouseY;

        _rotationY = Mathf.Clamp(_rotationY, _rotationMinMax.x, _rotationMinMax.y);

        Vector3 nextRotation = new Vector3(_rotationY, _rotationX);

        _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _smoothVelocity, _smoothTime);
        transform.localEulerAngles = _currentRotation;
        transform.position = _target.position - transform.forward * _distanceFromTarget;
    }

    private void CheckObstacles()
    {
        //float distance = Vector3.Distance(transform.position, _target.position);
        RaycastHit hit;

        if (Physics.Raycast(_target.position, transform.position - _target.position, out hit, _distanceFromTarget + 1.5f, _obstacles))
        {
            transform.position = hit.point + transform.forward * .5f;
        }
        else
        {
            transform.position -= transform.forward;
        }
    }

    public void SetTarget(Transform t) 
    {
        _target = t;
    }
}
