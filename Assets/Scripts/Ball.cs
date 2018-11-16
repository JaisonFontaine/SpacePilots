using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ball : NetworkBehaviour {

    public float ballInitialVelocity = 600f;

    private Rigidbody rb;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        
    }
}
