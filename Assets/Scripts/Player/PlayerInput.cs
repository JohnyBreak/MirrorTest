using Mirror;
using UnityEngine;

public class PlayerInput : NetworkBehaviour
{
    public System.Action LMBPressEvent;

    private Vector3 _moveVector;

    public Vector3 MoveVector
    {
        get { return _moveVector.normalized; }
        private set { _moveVector = value; }
    }

    public Vector3 NormalizedMoveVector
    {
        get { return _moveVector.normalized; }
    }

    void Update()
    {
        if (!isOwned) return;

        _moveVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            LMBPressEvent?.Invoke();
        }
    }
}
