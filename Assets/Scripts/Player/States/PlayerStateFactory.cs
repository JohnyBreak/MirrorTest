
using System.Collections.Generic;

public class PlayerStateFactory
{
    public enum PlayerStates 
    {
        idle,
        run,
        grounded,
        dash,
    }

    private PlayerStateMachine _context;
    private Dictionary<PlayerStates, UnitBaseState> _states = new Dictionary<PlayerStates, UnitBaseState>();

    public PlayerStates RootState;
    public PlayerStates SubState;

    public PlayerStateFactory(PlayerStateMachine currentContext) 
    {
        _context = currentContext;
        _states[PlayerStates.idle] = new PlayerIdleState(_context, this);
        _states[PlayerStates.run] = new PlayerRunState(_context, this);
        _states[PlayerStates.grounded] = new PlayerGroundedState(_context, this);
        _states[PlayerStates.dash] = new PlayerDashState(_context, this);
    }

    public UnitBaseState Idle() { SubState = PlayerStates.idle; return _states[PlayerStates.idle]; }
    public UnitBaseState Run() { SubState = PlayerStates.run; return _states[PlayerStates.run]; }
    public UnitBaseState Grounded() { RootState = PlayerStates.grounded; return _states[PlayerStates.grounded]; }
    public UnitBaseState Dash() { RootState = PlayerStates.dash; return _states[PlayerStates.dash]; }
}
