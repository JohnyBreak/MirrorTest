using Mirror;
using System;
using UnityEngine;

public class PlayerInfo : NetworkBehaviour
{
    private string _name;
    private GameSystem _gameSystem;
    private HitManager _hitManager;

    void Start()
    {
        var nm = (CustomNetworkManager)NetworkManager.singleton;
        _gameSystem = nm.GameSystem;
        _hitManager = nm.GameSystem.HitManager;
        _name = name;
        _gameSystem.AddPlayer(_name);
    }

    public void IncreaseScore()
    {
        _gameSystem.UpdateScoreInfo(_name);
    }

    internal void TargetChangeColor(ColorChange colorChange)
    {
        Debug.Log(isServer);
        if (isServer)
            _hitManager.RpcChangeTargetColor(colorChange);
        else
            CmdChangeColor(colorChange);


    }

    [Command]
    public void CmdChangeColor(ColorChange colorChange) 
    {
        _hitManager.ChangeTargetColor(colorChange);
    }


}
