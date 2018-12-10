using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public float paddleSpeed = 1f;
    public GameObject ball;

    public GameObject spawnBall;
    public GameObject cloneBall;

    [SyncVar] public bool isSpawnUp = false;

    [SyncVar] public bool isReady = false;
    [SyncVar] public bool allReady = false;
    [SyncVar] public bool ballInPlay = false;

    private Vector3 playerPos;
    private float xPos;

    private PlayerController[] listPlayers;
 
    private Rigidbody rbBall;
    private Ball scriptBall;

    void Awake() {
        spawnBall = transform.Find("SpawnBall").gameObject;

        if (transform.position.y == GameObject.Find("SpawnBas").transform.position.y) {
            //Player Bas
            Debug.Log("Player Bas");
            isSpawnUp = false;
        }

        if (transform.position.y == GameObject.Find("SpawnHaut").transform.position.y) {
            //Player Haut
            Debug.Log("Player Haut");
            isSpawnUp = true;
        }
    }

    void FixedUpdate() {
        listPlayers = FindObjectsOfType<PlayerController>();

        if (listPlayers.Length == 2 && listPlayers[0].isReady && listPlayers[1].isReady && allReady == false) {
            allReady = true;
        }
    }

    void Update() {
        if (!isLocalPlayer) {
            return;
        }

#if (UNITY_EDITOR || UNITY_STANDALONE)
        if (isSpawnUp) {
            xPos = transform.position.x + (Input.GetAxis("Horizontal") * -paddleSpeed);
        } else {
            xPos = transform.position.x + (Input.GetAxis("Horizontal") * paddleSpeed);
        }
            
        playerPos = new Vector3(Mathf.Clamp(xPos, -2.1f, 2.1f), transform.position.y, 0f);
        transform.position = playerPos;

        if(Input.GetKey(KeyCode.Space) && isReady == false && listPlayers.Length == 2) {
            CmdSpawnBall();
        }

        if (Input.GetButtonDown("Fire1") && allReady == true && ballInPlay == false) {
            CmdShootBall();
        }
#else
        Touch touch = Input.GetTouch(0);

        if (Input.touchCount == 1)
        {
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
                xPos = transform.position.x + (touchedPos.x * paddleSpeed);
                playerPos = new Vector3(Mathf.Clamp(xPos, -2.1f, 2.1f), transform.position.y, 0f);
                transform.position = playerPos;
            }
        }

        if (Input.touchCount == 2 && ballInPlay == false)
        {
            CmdShootBall();
        }
#endif
    }

    [Command]
    public void CmdSpawnBall() {
        isReady = true;
        ballInPlay = false;
        cloneBall = Instantiate(ball, spawnBall.transform.position, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(cloneBall);
        cloneBall.transform.SetParent(transform);
        scriptBall = cloneBall.GetComponent<Ball>();

        if (isSpawnUp) {
            scriptBall.playerUp = true;
        } else {
            scriptBall.playerUp = false;
        }
    }

    [Command]
    void CmdShootBall() {
        ballInPlay = true;
        cloneBall.transform.parent = null;
        rbBall = cloneBall.GetComponent<Rigidbody>();
        rbBall.isKinematic = false;
        rbBall.AddForce(new Vector3(scriptBall.ballInitialVelocity, scriptBall.ballInitialVelocity, 0));
    }

    public override void OnStartLocalPlayer() {
        GetComponent<MeshRenderer>().material.color = Color.blue;

        if (!isSpawnUp) {
            //Player Bas
            Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (isSpawnUp) {
            //Player Haut
            Camera.main.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
    }
}
