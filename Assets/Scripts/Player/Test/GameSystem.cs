using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class GameSystem : NetworkBehaviour
{
    public Action<string> WinGameEvent;

    [SerializeField] private int _requiredScore = 3;
    [SerializeField] private float _restartTime = 5f;
    public readonly SyncDictionary<string, int> _PlayersDictionary = new SyncDictionary<string, int>();
    //private Dictionary<string, int> _PlayersDictionary;
    private GameStateManager _stateManager;
    private HitManager _hitManager;

    public GameStateManager GameStateManager => _stateManager;
    public HitManager HitManager => _hitManager;

    private void Awake()
    {
        //_PlayersDictionary = new Dictionary<string, int>();
        _stateManager = GetComponent<GameStateManager>();
        _hitManager = GetComponent<HitManager>();
        _stateManager.SetState(GameStateManager.GameState.GamePlay);
    }

    public void AddPlayer(string name) 
    {
        if (!isServer) return;
        _PlayersDictionary.Add(name, 0);
    }

    public void RemovePlayer(string name)
    {
        if (!isServer) return;
        Debug.LogError(name);
        _PlayersDictionary.Remove(name);
    }

    public void UpdateScoreInfo(string name) 
    {
        if (!isServer) return;

        Debug.Log(name);
        _PlayersDictionary[name]++;
        if (_PlayersDictionary[name] >= _requiredScore)
        {
            RpcWin(name);
        }
    }

    [ClientRpc]
    private void RpcWin(string name)
    {
        Debug.Log($"{name} is winner");
        WinGameEvent?.Invoke(name);

        _stateManager.SetState(GameStateManager.GameState.Paused);
        StartCoroutine(RestartGameRoutine());
    }

    private IEnumerator RestartGameRoutine() 
    {
        yield return new WaitForSeconds(_restartTime);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
