
using System.Collections;
using UnityEngine;

public class PlayerDashState : UnitBaseState
{
    public PlayerDashState(PlayerStateMachine currentContext, PlayerStateFactory stateFactory)
    : base(currentContext, stateFactory)
    {
        _isRootState = true;
    }

    public override void CheckSwitchStates()
    {
        SwitchState(_factory.Grounded());
    }

    public override void EnterState()
    {
        _ctx.DashCollider.ToggleCollider(true);
        DoDash();
    }

    public override void ExitState()
    {

        _ctx.DashCollider.ToggleCollider(false);
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
    }

    private void DoDash()
    {
        float targetAngle = Mathf.Atan2(_ctx.CurrentMovement.x, _ctx.CurrentMovement.z) * Mathf.Rad2Deg + _ctx.CameraTransform.eulerAngles.y;
        _ctx.transform.rotation = Quaternion.Euler(0, targetAngle, 0);

        _ctx.AppliedMovementZ = _ctx.CameraRelativeMovement.z * _ctx.DashSpeed;
        _ctx.AppliedMovementX = _ctx.CameraRelativeMovement.x * _ctx.DashSpeed;

        _ctx.DashReloaded = false;
        _ctx.DashStartTime = Time.time;
        _ctx.IsDashing = true;

        _ctx.DashRoutine = DashCoroutine();
        _ctx.StartCoroutine(_ctx.DashRoutine);
    }

    private IEnumerator DashCoroutine()
    {
        while ((Time.time < _ctx.DashStartTime + _ctx.DashTime))
        {
            yield return null;
        }

        _ctx.StartCoroutine(_ctx.DashReloadTimer());
        _ctx.AppliedMovementX = 0;
        _ctx.AppliedMovementZ = 0;
        _ctx.IsDashing = false;

        CheckSwitchStates();
    }
}
