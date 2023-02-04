
using UnityEngine;

public class PlayerGroundedState : UnitBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory stateFactory) 
        : base(currentContext, stateFactory)
    {
        _isRootState = true;
    }
    
    public override void EnterState()
    {
        _ctx.AppliedMovementZ = 0;
        _ctx.AppliedMovementX = 0;
        InitializeSubState();
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
    }

    public override void InitializeSubState()
    {
        if (_ctx.CurrentMovement == Vector3.zero)
        {
            SetSubState(_factory.Idle());
        }
        else
        {
            SetSubState(_factory.Run());
        }
    }

    public override void CheckSwitchStates()
    {
    }
}
