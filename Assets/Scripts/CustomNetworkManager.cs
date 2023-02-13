using Mirror;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
    public System.Action ClientConnectedEvent;
    public System.Action<NetworkConnectionToClient, NetworkMessage> CreateCharacterEvent;

    [SerializeField] private GameSystem _gameSystem;

    public GameSystem GameSystem =>_gameSystem;

}

