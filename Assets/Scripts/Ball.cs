using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ball : NetworkBehaviour {

    public float ballInitialVelocity = 600f;
    [SyncVar] public bool playerUp = false;
    
    void Update() {
        
    }
}
