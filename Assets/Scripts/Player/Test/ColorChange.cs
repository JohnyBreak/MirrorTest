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

    private WaitForSeconds _wait;
    private bool _canChangeColor = true;

    private void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
        _material.color = _StandartColor;

        _wait = new WaitForSeconds(_changeTime);
    }

    [Client]
    public void ChangeColor(NetworkConnection target) 
    {
        Debug.Log("adsasdasd");


        if (!_canChangeColor) return;

        StartCoroutine(ChangeColorRoutine());

        //RpcChangeColor(target);
    }

    public void RpcChangeColor(NetworkConnection target)
    {
        if (!_canChangeColor) return;

        StartCoroutine(ChangeColorRoutine());
        //return true;
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
