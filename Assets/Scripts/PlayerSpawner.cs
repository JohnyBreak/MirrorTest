using Mirror;
using UnityEngine;

public class PlayerSpawner : NetworkBehaviour
{
    //    [SerializeField] private CustomNetworkManager _netManager;
    //    [SerializeField] private GameObject _playerPrefab;
    //    [SerializeField] private GameObject _camPrefab;

    //    [SerializeField] private Transform _spawnPoint;

    //    void Start()
    //    {
    //        _netManager = (CustomNetworkManager)CustomNetworkManager.singleton;
    //        _netManager.CreateCharacterEvent += OnCreateCharacter;
    //    }

    //    //private void OnClientConnected() 
    //    //{
    //    //    OnCreateCharacter();
    //    //}

    //    [Command]
    //    private void SpawnPlayerCamera()
    //    {
    //        var camera = Instantiate(_camPrefab);//SpawnPlayerCamera();

    //        GameObject go = Instantiate(_playerPrefab, _spawnPoint.position, Quaternion.identity);

    //        camera.GetComponent<PlayerCameraController>().SetTarget(go.GetComponent<Player>().CameraTarget);

    //        go.GetComponent<PlayerMovement>().SetCameraTransform(camera.transform);

    //        //GameObject go = Instantiate(_camPrefab);
    //        //Debug.Log("SpawnPlayerCamera");
    //        ////NetworkServer.Spawn(go);
    //        //return go;
    //    }

    //    public void OnCreateCharacter(NetworkConnectionToClient conn, NetworkMessage message)
    //    {
    //        SpawnPlayerCamera();
    //        //var camera = Instantiate(_camPrefab);//SpawnPlayerCamera();

    //        //GameObject go = Instantiate(_playerPrefab, _spawnPoint.position, Quaternion.identity);

    //        //camera.GetComponent<PlayerCameraController>().SetTarget(go.GetComponent<Player>().CameraTarget);

    //        //go.GetComponent<PlayerMovement>().SetCameraTransform(camera.transform);

    //        //NetworkServer.AddPlayerForConnection(conn, go);
    //        //NetworkServer.AddPlayerForConnection(conn, camera);
    //        Debug.LogError("OnCreateCharacter");
    //    }
    //    private void OnDestroy()
    //    {
    //        _netManager.CreateCharacterEvent -= OnCreateCharacter;
    //    }
}
