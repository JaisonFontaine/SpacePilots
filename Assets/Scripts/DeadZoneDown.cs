using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeadZoneDown : NetworkBehaviour {

    private PlayerController[] ListPlayer;

    private PlayerController playerDown;

    void Update()
    {
        ListPlayer = FindObjectsOfType<PlayerController>();

        foreach (PlayerController pc in ListPlayer) {
            if (!pc.isSpawnHaut) {
                playerDown = pc;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        playerDown.GetComponent<GM>().CmdLoseLife1();
    }
}
