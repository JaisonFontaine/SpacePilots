using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bricks : NetworkBehaviour {

    public GameObject brickParticle;

    private GM[] ListGM;

    void OnCollisionEnter(Collision other)
    {
        Instantiate(brickParticle, transform.position, Quaternion.identity);

        ListGM = FindObjectsOfType<GM>();

        foreach (GM gm in ListGM)
        {
            gm.CmdDestroyBrick();
        }

        NetworkServer.UnSpawn(gameObject);
        Destroy(gameObject);
    }
}
