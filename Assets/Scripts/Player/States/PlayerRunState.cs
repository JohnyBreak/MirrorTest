using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : UnitBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory stateFactory)
        : base(currentContext, stateFactory)
    { }


    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
        HandleRotatation();
        HandleMove();

        CheckSwitchStates();
    }

    public override void CheckSwitchStates()
    {
        if (_ctx.CurrentMovement == Vector3.zero)
        {
            SwitchState(_factory.Idle());
        }

        if (_ctx.IsDashPressed && _ctx.CanDash && _ctx.DashReloaded)
        {
            SwitchState(_factory.Dash());
        }
    }

    private void HandleRotatation()
    {
        if (_ctx.CurrentMovement.x == 0 && _ctx.CurrentMovement.z == 0) return;
        float turnSmoothVelocity = 0;
        float targetAngle = Mathf.Atan2(_ctx.CurrentMovement.x, _ctx.CurrentMovement.z) * Mathf.Rad2Deg + _ctx.CameraTransform.eulerAngles.y;

        float angle = Mathf.SmoothDampAngle(_ctx.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, _ctx.TurnSmoothTime);

        _ctx.transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    private void HandleMove()
    {
        _ctx.AppliedMovementZ = _ctx.CameraRelativeMovement.z * _ctx.MovementSpeed;
        _ctx.AppliedMovementX = _ctx.CameraRelativeMovement.x * _ctx.MovementSpeed;
    }
}
