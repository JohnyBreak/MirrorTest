using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerInput : MonoBehaviour
{
    public System.Action LMBPressEvent;
    public System.Action LMBReleaseEvent;
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
        _moveVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            LMBPressEvent?.Invoke();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            LMBReleaseEvent?.Invoke();
        }
    }
}
