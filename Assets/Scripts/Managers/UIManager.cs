using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private MainMenuCanvas _mainMenu;
    [SerializeField] private GameObject _mainCamera;

    private CustomNetworkManager _netManager;

    void Start()
    {
        _netManager = (CustomNetworkManager)CustomNetworkManager.singleton;
        _netManager.ClientConnectedEvent += OnClientConnected;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClientConnected()
    {
        _mainMenu.gameObject.SetActive(false);
        _mainCamera.SetActive(false);
    }

    private void OnDestroy()
    {
        _netManager.ClientConnectedEvent -= OnClientConnected;
    }
}
