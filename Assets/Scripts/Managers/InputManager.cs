using Mirror;
using UnityEngine;

public class InputManager : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!isClient) return; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
