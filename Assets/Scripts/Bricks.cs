using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bricks : NetworkBehaviour {

    public GameObject brickParticle;

    void OnCollisionEnter(Collision other)
    {
        /*Instantiate(brickParticle, transform.position, Quaternion.identity);
        GM.instance.DestroyBrick();
        Destroy(gameObject);*/
    }
}
