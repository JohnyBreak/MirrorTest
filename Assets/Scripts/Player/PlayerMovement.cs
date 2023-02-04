using Mirror;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _rotationSpeed = 15f;
    [SerializeField] private PlayerInput _playerInput;

    private Transform _cameraTransform;

    private CharacterController _ctrl;
    private float _turnSmoothVelocity;

    private void Awake()
    {
        _ctrl = GetComponent<CharacterController>();
           _playerInput = GetComponent<PlayerInput>();
        _playerInput.LMBPressEvent += OnLMBPress;
    }

    void Update()
    {
        if (!isOwned) return;
        if (!isLocalPlayer) return;

        Move();
    }

    private void Move()
    {
        
        if (_playerInput.MoveVector != Vector3.zero) 
        {
            Vector3 cameraEuler = Quaternion.Euler(0, _cameraTransform.eulerAngles.y, 0) * _playerInput.MoveVector;//new Vector3(_playerInput.MoveVector.x, 0, _playerInput.MoveVector.z);
            Vector3 movementDirection = cameraEuler.normalized;

            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, _rotationSpeed * Time.deltaTime);

            _ctrl.Move(movementDirection * Time.deltaTime * _speed);
        }
        //transform.Translate(movementDirection * Time.deltaTime * _speed);
    }

    private void OnLMBPress() 
    {
        Debug.LogError("OnLMBPress");
    }

    public void SetCameraTransform(Transform camT) 
    {
        _cameraTransform = camT;
    }

    [Client]
    private void OnDestroy()
    {
        if (!isOwned) return;
        if (!isLocalPlayer) return;
        _playerInput.LMBPressEvent -= OnLMBPress;
    }
}
