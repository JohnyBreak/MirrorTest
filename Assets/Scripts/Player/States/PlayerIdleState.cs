using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : UnitBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory stateFactory)
        : base(currentContext, stateFactory)
    { }
    

    public override void EnterState()
    {
        _ctx.AppliedMovementX = 0;
        _ctx.AppliedMovementZ = 0;
    }

    public override void ExitState()
    {
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void CheckSwitchStates()
    {
        if (_ctx.CurrentMovement != Vector3.zero)
        {
            SwitchState(_factory.Run());
        }
    }
}
