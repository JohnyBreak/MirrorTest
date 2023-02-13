using Mirror;
using UnityEngine;

public class HitManager : NetworkBehaviour
{
    public void ChangeTargetColor(ColorChange colorChange, string name)
    {
        RpcChangeTargetColor(colorChange, name);
    }

    [ClientRpc]
    public void RpcChangeTargetColor(ColorChange colorChange, string name)
    {
        colorChange.ChangeColor(name);
    }
}
