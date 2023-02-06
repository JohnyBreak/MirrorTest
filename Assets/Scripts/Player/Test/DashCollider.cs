using Mirror;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class DashCollider : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;
    [SerializeField]  private Transform _parentTransform;

    private CapsuleCollider _collider;
    private PlayerInfo _playerInfo;
    //private HitManager _hitManager;

    void Start()
    {
        //var nm = (CustomNetworkManager)NetworkManager.singleton;
        //_hitManager = nm.GameSystem.HitManager;
            
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
                //_hitManager.CmdChangeTargetColor(colorChange);
                _playerInfo.TargetChangeColor(colorChange);
                //if (colorChange.ChangeColor()) 
                //{
                //    _playerInfo.IncreaseScore();
                //}
            }
        }
    }
}
