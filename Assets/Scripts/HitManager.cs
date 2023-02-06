using Mirror;
using UnityEngine;

public class HitManager : NetworkBehaviour
{
    [Server]
    public void ServerChangeTargetColor(ColorChange colorChange)
    {
        ChangeTargetColor(colorChange);
    }

    [ClientRpc]
    public void ChangeTargetColor(ColorChange colorChange) 
    {
        if (!isServer) return;

        colorChange.ChangeColor(colorChange.GetComponent<NetworkIdentity>().connectionToClient);
    }
}
