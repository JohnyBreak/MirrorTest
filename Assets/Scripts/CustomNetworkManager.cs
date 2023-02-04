using Mirror;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
    public System.Action ClientConnectedEvent;
    public System.Action<NetworkConnectionToClient, NetworkMessage> CreateCharacterEvent;

    [SerializeField] private PlayerCameraController _camPrefab;

    private bool playerSpawned;
    private bool playerConnected;


    public override void OnStartServer()
    {
        base.OnStartServer();
        //NetworkServer.RegisterHandler<PosMessage>(OnCreateCharacter);
        Debug.LogError("OnStartServer");
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        playerConnected = true;
        Debug.LogError("OnClientConnect");
        ClientConnectedEvent?.Invoke();

        //NetworkClient.Send(new PosMessage());

    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        // add player at correct spawn position

        

        GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        
        var camera = Instantiate(_camPrefab);

        player.GetComponent<PlayerMovement>().SetCameraTransform(camera.transform);

        camera.GetComponent<PlayerCameraController>().SetTarget(player.GetComponent<Player>().CameraTarget);
        NetworkServer.AddPlayerForConnection(conn, player);

        NetworkServer.Spawn(camera.gameObject);

    }

    public void OnCreateCharacter(NetworkConnectionToClient conn, PosMessage message)
    {
        CreateCharacterEvent?.Invoke(conn, message);

        var camera = Instantiate(_camPrefab);//SpawnPlayerCamera();

        GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        camera.GetComponent<PlayerCameraController>().SetTarget(player.GetComponent<Player>().CameraTarget);

        player.GetComponent<PlayerMovement>().SetCameraTransform(camera.transform);


        //GameObject go = Instantiate(playerPrefab, message.position, Quaternion.identity);
        ////SpawnPlayerCamera(go);
        NetworkServer.AddPlayerForConnection(conn, player);
        //NetworkServer.AddPlayerForConnection(conn, camera.gameObject);
        //Debug.LogError("OnCreateCharacter");
    }
}

public struct PosMessage : NetworkMessage
{
    public Vector3 position;
}

