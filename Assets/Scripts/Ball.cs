using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ball : NetworkBehaviour {

    public float ballInitialVelocity = 600f;

    private Rigidbody rb;
    private bool ballInPlay;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
#if (UNITY_EDITOR || UNITY_STANDALONE)
        if (Input.GetButtonDown("Fire1") && ballInPlay == false) {
            CmdShootBall();
        }
#else
        Touch touch = Input.GetTouch(0);

        if (Input.touchCount == 2 && ballInPlay == false) {
            CmdShootBall();
        }
#endif
    }

    [Command]
    void CmdShootBall()
    {
        transform.parent = null;
        ballInPlay = true;
        rb.isKinematic = false;
        rb.AddForce(new Vector3(ballInitialVelocity, ballInitialVelocity, 0));
    }
}
