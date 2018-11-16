using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeadZoneUp : NetworkBehaviour {

    private PlayerController[] ListPlayer;

    private PlayerController playerUp;

    void Update() {
        ListPlayer = FindObjectsOfType<PlayerController>();

        foreach (PlayerController pc in ListPlayer) {
            if (pc.isSpawnHaut) {
                playerUp = pc;
            }
        }
    }

    void OnTriggerEnter(Collider col) {
        playerUp.GetComponent<GM>().CmdLoseLife2();
    }
}
