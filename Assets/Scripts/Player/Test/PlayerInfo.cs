using Mirror;
using System;
using UnityEngine;

public class PlayerInfo : NetworkBehaviour
{
    [SyncVar] private string _name;
    private GameSystem _gameSystem;
    private HitManager _hitManager;

    //void Start()
    //{
    //    var nm = (CustomNetworkManager)NetworkManager.singleton;
    //    _gameSystem = nm.GameSystem;
    //    _hitManager = nm.GameSystem.HitManager;
    //    _name = name;
    //    _gameSystem.AddPlayer(_name);
    //}

    public override void OnStartLocalPlayer()
    {
        if (!isOwned) return;

        CmdAddPlayerToDictionary();
    }

    //public override void OnStopLocalPlayer()
    //{
    //    if (!isOwned) return;

    //    CmdRemovePlayerFromDictionary();
    //}

    [Command]
    private void CmdAddPlayerToDictionary() 
    {
        var nm = (CustomNetworkManager)NetworkManager.singleton;
        _gameSystem = nm.GameSystem;
        _hitManager = nm.GameSystem.HitManager;
        _name = name;
        _gameSystem.AddPlayer(_name);
    }

    [Command]
    private void CmdRemovePlayerFromDictionary()
    {
        _gameSystem.RemovePlayer(_name);
    }

    public void TargetChangeColor(ColorChange colorChange)
    {
        Debug.Log(isServer);
        if (isServer)
            _hitManager.RpcChangeTargetColor(colorChange, _name);
        else
            CmdChangeColor(colorChange, _name);
    }

    [Command]
    public void CmdChangeColor(ColorChange colorChange, string name) 
    {
        _hitManager.ChangeTargetColor(colorChange, name);
    }


}
