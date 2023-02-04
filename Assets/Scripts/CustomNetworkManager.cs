using Mirror;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
    public System.Action ClientConnectedEvent;
    public System.Action<NetworkConnectionToClient, NetworkMessage> CreateCharacterEvent;

    [SerializeField] private GameSystem _gameSystem;

    private bool playerSpawned;
    private bool playerConnected;

    public GameSystem GameSystem =>_gameSystem;

}

