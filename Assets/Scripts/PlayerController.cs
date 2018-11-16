using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public float paddleSpeed = 1f;
    public GameObject ball;
    public GameObject spawnBall;
    public GameObject bricks;
    public GameObject spawnBricks;
    [SyncVar] public bool isSpawnHaut = false;
    [SyncVar] public bool isReady = false;
    [SyncVar] public bool allReady = false;

    public GameObject cloneBall;
    private Rigidbody rbBall;
    private Ball ScriptBall;

    private PlayerController[] ListPlayer;

    private Vector3 playerPos;
    private float xPos;
    private bool ballInPlay = false;

    void Update() {
        if (!isLocalPlayer) {
            return;
        }

        ListPlayer = FindObjectsOfType<PlayerController>();

        if (ListPlayer[0].isReady && ListPlayer[1].isReady) {
            allReady = true;
        }

#if (UNITY_EDITOR || UNITY_STANDALONE)
        if (isSpawnHaut) {
            xPos = transform.position.x + (Input.GetAxis("Horizontal") * -paddleSpeed);
        } else {
            xPos = transform.position.x + (Input.GetAxis("Horizontal") * paddleSpeed);
        }
            
        playerPos = new Vector3(Mathf.Clamp(xPos, -2.1f, 2.1f), transform.position.y, 0f);
        transform.position = playerPos;

        if(Input.GetKey(KeyCode.Space) && isReady == false) {
            CmdSpawnBall();
        }

        if (Input.GetButtonDown("Fire1") && allReady == true && ballInPlay == false) {
            CmdShootBall();
        }
#else
        Touch touch = Input.GetTouch(0);

        if (Input.touchCount == 1) {
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) {
                Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
                xPos = transform.position.x + (touchedPos.x * paddleSpeed);
                playerPos = new Vector3(Mathf.Clamp(xPos, -2.1f, 2.1f), transform.position.y, 0f);
                transform.position = playerPos;
            }
        }

        if (Input.touchCount == 2 && ballInPlay == false) {
            CmdShootBall();
        }
#endif
    }

    [Command]
    public void CmdSpawnBall() {
        isReady = true;

        if ((!isSpawnHaut && isReady) && (isSpawnHaut && isReady)) {
            allReady = true;
        }

        cloneBall = Instantiate(ball, spawnBall.transform.position, Quaternion.identity) as GameObject;
        cloneBall.transform.SetParent(transform);
        NetworkServer.Spawn(cloneBall);
    }

    [Command]
    void CmdShootBall() {
        cloneBall.transform.parent = null;
        ballInPlay = true;
        rbBall = cloneBall.GetComponent<Rigidbody>();
        ScriptBall = cloneBall.GetComponent<Ball>();
        rbBall.isKinematic = false;
        rbBall.AddForce(new Vector3(ScriptBall.ballInitialVelocity, ScriptBall.ballInitialVelocity, 0));
    }

    public override void OnStartLocalPlayer() {
        GetComponent<MeshRenderer>().material.color = Color.blue;

        if (transform.position.y == GameObject.Find("SpawnBas").transform.position.y) {
            //Player Bas
            Debug.Log("Player Bas");
            isSpawnHaut = false;
            Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (transform.position.y == GameObject.Find("SpawnHaut").transform.position.y) {
            //Player Bas
            Debug.Log("Player Haut");
            isSpawnHaut = true;
            Camera.main.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
    }
}
