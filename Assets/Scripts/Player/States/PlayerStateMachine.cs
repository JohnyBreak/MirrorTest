using System.Collections;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    #region Fields

    [SerializeField] private PlayerStateFactory.PlayerStates _currentRootStateName;
    [SerializeField] private PlayerStateFactory.PlayerStates _currentSubStateName;

    [SerializeField] private GameStateManager _gameStateManager;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _movementSpeed = 7.5f;




    private CharacterController _controller;
    private DashCollider _dashCollider;
    private TestPlayerInput _input;
    private float _rotationFactorPerFrame = 15f;
    private Vector3 _currentMovement;
    private Vector3 _appliedMovement;


    private Vector3 _cameraRelativeMovement;
    private float _turnSmoothTime = 0.03f;

    //state
    protected UnitBaseState _currentState;
    protected PlayerStateFactory _states;


    //dash
    [SerializeField] private float _dashSpeed = 20f;
    [SerializeField] private float _dashTime = .33f;
    [SerializeField] private float _dashReloadTime = .44f;

    private bool _isDashPressed = false;
    private bool _canDash = true;
    private bool _dashReloaded = true;
    private float _dashStartTime = 0f;
    private bool _isDashing;
    private IEnumerator _dashRoutine;



    #endregion

    #region Properties
    // getters & setters

    public UnitBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }

    public float AppliedMovementZ { get { return _appliedMovement.z;} set { _appliedMovement.z = value; } }
    public float AppliedMovementX { get { return _appliedMovement.x; } set { _appliedMovement.x = value; } }
    public Vector3 CurrentMovement { get { return  _currentMovement; } set { _currentMovement = value; } }

    //public float MagnitudedMovement => GetMagnitudedMoveVectorForAnimation();
    public float MovementSpeed => _movementSpeed;
    public Transform CameraTransform => _cameraTransform;
    public float TurnSmoothTime => _turnSmoothTime;

    public bool IsDashPressed => _isDashPressed;
    public float DashTime => _dashTime;
    public float DashStartTime { get { return _dashStartTime; } set { _dashStartTime = value; } }
    public float DashSpeed => _dashSpeed;
    public bool IsDashing { get { return _isDashing; } set { _isDashing = value; } }
    public IEnumerator DashRoutine { get { return _dashRoutine; } set { _dashRoutine = value; } }
    public bool CanDash { get { return _canDash; } }
    public bool DashReloaded { get { return _dashReloaded; } set { _dashReloaded = value; } }


    public Vector3 CameraRelativeMovement => _cameraRelativeMovement;
    public DashCollider DashCollider => _dashCollider;
    #endregion

    private void OnEnable()
    {
        _input.LMBPressEvent += OnDashPressed;
        _input.LMBReleaseEvent += OnDashReleased;
    }

    private void Awake()
    {
        Debug.LogError("PlayerStateMachine Awake");
        
        _controller = GetComponent<CharacterController>();
        _dashCollider = GetComponentInChildren<DashCollider>();
        _input = GetComponent<TestPlayerInput>();

        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();

        _currentState.EnterState();
    }

    //private IEnumerator Start()
    //{
    //    yield return new WaitUntil(() => _gameStateManager.CurrentGameState == GameStateManager.GameState.GamePlay);

    //    _currentState.EnterState();
    //}

    private void OnDashPressed()
    {
        _isDashPressed = true;
    }

    private void OnDashReleased()
    {
        _canDash = true;
        _isDashPressed = false;
    }
    
    void Update()
    {
        if (_gameStateManager.CurrentGameState != GameStateManager.GameState.GamePlay) return;
        _cameraRelativeMovement = GetCameraRelativeMoveDirection();

        _currentMovement = _input.NormalizedMoveVector;
        _currentState.UpdateStates();
        _currentRootStateName = _states.RootState;
        _currentSubStateName = _states.SubState;

        _controller.SimpleMove(_appliedMovement);
    }

    //private float GetMagnitudedMoveVectorForAnimation() 
    //{
    //    Vector3 animMoveVectorMagnitude = _currentMovement;
    //    animMoveVectorMagnitude.y = 0;
    //    return animMoveVectorMagnitude.magnitude;
    //}

    public IEnumerator DashReloadTimer()
    {
        //yield return null;
        yield return new WaitForSeconds(_dashReloadTime);
        _dashReloaded = true;
    }

    private Vector3 GetCameraRelativeMoveDirection()
    {
        Vector3 cameraForward = _cameraTransform.forward;
        Vector3 cameraRight = _cameraTransform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        Vector3 forwardCameraRelativeMovement = cameraForward * _currentMovement.z;
        Vector3 rightCameraRelativeMovement = cameraRight * _currentMovement.x;
        Vector3 cameraRelativeMovement = forwardCameraRelativeMovement + rightCameraRelativeMovement;

        Debug.DrawRay(transform.position, cameraRelativeMovement, Color.black);
        return cameraRelativeMovement;
    }

    private void OnDisable()
    {
        _input.LMBPressEvent -= OnDashPressed;
        _input.LMBReleaseEvent -= OnDashReleased;
    }
}
