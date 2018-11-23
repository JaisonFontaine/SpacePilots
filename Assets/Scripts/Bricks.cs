using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bricks : NetworkBehaviour {

    public GameObject brickParticle;

    private GM scriptGM;

    void Start() {
        scriptGM = FindObjectOfType<GM>();
    }

    void OnCollisionEnter(Collision other) {
        //Instantiate(brickParticle, transform.position, Quaternion.identity);
        scriptGM.CmdDestroyBrick();
        Destroy(gameObject);
    }
}
