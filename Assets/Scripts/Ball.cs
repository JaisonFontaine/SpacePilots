using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float ballInitialVelocity = 600f;

    private Rigidbody rb;
    private bool ballInPlay;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
#if (UNITY_EDITOR || UNITY_STANDALONE)
        if (Input.GetButtonDown("Fire1") && ballInPlay == false) {
            transform.parent = null;
            ballInPlay = true;
            rb.isKinematic = false;
            rb.AddForce(new Vector3(ballInitialVelocity, ballInitialVelocity, 0));
        }
#else
        Touch touch = Input.GetTouch(0);

        if (Input.touchCount == 2 && ballInPlay == false) {
            transform.parent = null;
            ballInPlay = true;
            rb.isKinematic = false;
            rb.AddForce(new Vector3(ballInitialVelocity, ballInitialVelocity, 0));
        }
#endif
    }
}
