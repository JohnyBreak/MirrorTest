using Mirror;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private string _name;
    private GameSystem _gameSystem;

    void Start()
    {
        var nm = (CustomNetworkManager)NetworkManager.singleton;
        _gameSystem = nm.GameSystem;

        _name = name;
        _gameSystem.AddPlayer(_name);
    }

    public void IncreaseScore() 
    {
        _gameSystem.UpdateScoreInfo(_name);
    }

}
