using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshRenderer))]
public class ColorChange : MonoBehaviour
{
    [SerializeField] private Color _StandartColor;
    [SerializeField] private Color _HitedColor;
    [SerializeField] private float _changeTime;
    private Material _material;

    private WaitForSeconds _wait;
    private bool _camChangeColor = true;

    private void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
        _material.color = _StandartColor;

        _wait = new WaitForSeconds(_changeTime);
    }

    public bool ChangeColor() 
    {
        if (!_camChangeColor) return false;

        StartCoroutine(ChangeColorRoutine());
        return true;
    }

    private IEnumerator ChangeColorRoutine() 
    {
        _camChangeColor = false;
        _material.color = _HitedColor;

        yield return _wait;

        _material.color = _StandartColor;

        _camChangeColor = true;
    }

}
