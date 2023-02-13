using System.Collections;
using Mirror;
using UnityEngine;


[RequireComponent(typeof(MeshRenderer))]
public class ColorChange : NetworkBehaviour
{
    [SerializeField] private Color _StandartColor;
    [SerializeField] private Color _HitedColor;
    [SerializeField] private float _changeTime;
    private Material _material;

    private GameSystem _gameSystem;
    private WaitForSeconds _wait;
    private bool _canChangeColor = true;

    private void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
        _material.color = _StandartColor;
        var nm = (CustomNetworkManager)NetworkManager.singleton;
        _gameSystem = nm.GameSystem;
        _wait = new WaitForSeconds(_changeTime);
    }

    [Client]
    public void ChangeColor(string name) 
    {
        if (!_canChangeColor) return;

        CmdUpdateScore(name);

        StartCoroutine(ChangeColorRoutine());
    }

    [Command]
    public void CmdUpdateScore(string name) 
    {
        _gameSystem.UpdateScoreInfo(name);
    }

    private IEnumerator ChangeColorRoutine() 
    {
        _canChangeColor = false;
        _material.color = _HitedColor;

        yield return _wait;

        _material.color = _StandartColor;

        _canChangeColor = true;
    }

}
