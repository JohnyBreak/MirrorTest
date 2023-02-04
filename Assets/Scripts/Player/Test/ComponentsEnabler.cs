using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class ComponentsEnabler : NetworkBehaviour
{
    [SerializeField] private Transform _camTarget;
    [SerializeField] private List<MonoBehaviour> _componentsToEnable;


    private void Start()
    {
        if (!isOwned) return;

        var camCtrl = Camera.main.GetComponent<TestPlayerCamera>();

        camCtrl.SetTarget(_camTarget);
        camCtrl.enabled = true;
        GetComponent<PlayerStateMachine>().SetCameraTransform(camCtrl.transform);

        foreach (var component in _componentsToEnable)
        {
            component.enabled = true;
        }
    }
}
