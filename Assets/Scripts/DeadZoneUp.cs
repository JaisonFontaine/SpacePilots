using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeadZoneUp : NetworkBehaviour {

    private GM scriptGM;

    void Start() {
        scriptGM = FindObjectOfType<GM>();
    }

    void OnTriggerEnter(Collider col) {
        scriptGM.CmdLoseLife2(col.GetComponent<Ball>().playerUp);
    }
}
