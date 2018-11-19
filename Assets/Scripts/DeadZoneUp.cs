using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeadZoneUp : NetworkBehaviour {

    private PlayerController[] listPlayers;

    void Update() {
        listPlayers = FindObjectsOfType<PlayerController>();
    }

    void OnTriggerEnter(Collider col) {
        foreach (PlayerController player in listPlayers) {
            if (player.isSpawnHaut) {
                player.GetComponent<GM>().CmdLoseLife2();
            }
        }
    }
}
