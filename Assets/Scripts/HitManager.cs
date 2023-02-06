using Mirror;
using UnityEngine;

public class HitManager : NetworkBehaviour
{
    public void ChangeTargetColor(ColorChange colorChange)
    {
        Debug.Log("CmdChange");
        RpcChangeTargetColor(colorChange);
    }

    [ClientRpc]
    public void RpcChangeTargetColor(ColorChange colorChange)
    {
        Debug.Log("RpcChange");
        colorChange.ChangeColor(colorChange.GetComponent<NetworkIdentity>().connectionToClient);
    }
}
