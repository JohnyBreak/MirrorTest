using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private GameSystem _gameSystem;

    void Start()
    {
        _name = name;
        _gameSystem.AddPlayer(_name);
    }

    public void IncreaseScore() 
    {
        _gameSystem.UpdateScoreInfo(_name);
    }

}
