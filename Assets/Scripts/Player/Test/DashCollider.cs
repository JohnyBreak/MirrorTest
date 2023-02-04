using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class DashCollider : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;
    private CapsuleCollider _collider;
    [SerializeField]  private Transform _parentTransform;
    [SerializeField] private PlayerInfo _playerInfo;

    void Start()
    {
        //_parentTransform = GetComponentInParent<Transform>();
        _playerInfo = _parentTransform.GetComponent<PlayerInfo>();
           _collider = GetComponent<CapsuleCollider>();
        _collider.enabled = false;
    }

    public void ToggleCollider(bool enabled) 
    {
        _collider.enabled = enabled;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == _parentTransform) return;

        if (((1 << other.gameObject.layer) & _mask) != 0)
        {
            if (other.TryGetComponent<ColorChange>(out var colorChange)) 
            {
                if (colorChange.ChangeColor()) 
                {
                    _playerInfo.IncreaseScore();
                }
            }
        }
    }
}
