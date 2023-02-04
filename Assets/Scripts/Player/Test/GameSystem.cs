using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    public Action<string> WinGameEvent;

    [SerializeField] private int _requiredScore = 3;
    [SerializeField] private float _restartTime = 5f;

    private Dictionary<string, int> _PlayersDictionary;
    private GameStateManager _stateManager;

    public GameStateManager GameStateManager => _stateManager;

    private void Awake()
    {
        _PlayersDictionary = new Dictionary<string, int>();
        _stateManager = GetComponent<GameStateManager>();
        _stateManager.SetState(GameStateManager.GameState.GamePlay);
    }

    public void AddPlayer(string name) 
    {
        _PlayersDictionary.Add(name, 0);
    }

    public void UpdateScoreInfo(string name) 
    {
        _PlayersDictionary[name]++;
        if (_PlayersDictionary[name] >= _requiredScore) 
        {
            Win(name);
        }
    }
    private void Win(string name)
    {
        Debug.Log($"{name} is winner");
        WinGameEvent?.Invoke(name);

        _stateManager.SetState(GameStateManager.GameState.Paused);
        StartCoroutine(RestartGameRoutine());
    }

    private IEnumerator RestartGameRoutine() 
    {
        yield return new WaitForSeconds(_restartTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
