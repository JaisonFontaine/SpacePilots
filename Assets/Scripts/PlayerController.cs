using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public float paddleSpeed = 1f;
    public GameObject ball;
    public GameObject spawnBall;
    public bool isSpawnHaut = false;

    private Vector3 playerPos;
    private float xPos;

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
#if (UNITY_EDITOR || UNITY_STANDALONE)
            if (isSpawnHaut)
            {
                xPos = transform.position.x + (Input.GetAxis("Horizontal") * -paddleSpeed);
            } else {
                xPos = transform.position.x + (Input.GetAxis("Horizontal") * paddleSpeed);
            }
            
            playerPos = new Vector3(Mathf.Clamp(xPos, -2.1f, 2.1f), transform.position.y, 0f);
            transform.position = playerPos;

            if(Input.GetKey(KeyCode.Space) && !GameObject.FindGameObjectWithTag("Ball")){
                CmdSpawnBall();
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
#endif
        }
    }

    [Command]
    void CmdSpawnBall() {
        GameObject Ball = Instantiate(ball, spawnBall.transform.position, Quaternion.identity) as GameObject;
        Ball.transform.SetParent(transform);
        NetworkServer.Spawn(Ball);
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;

        if (transform.position.y == GameObject.Find("SpawnBas").transform.position.y)
        {
            //Player Bas
            Debug.Log("Player Bas");
            isSpawnHaut = false;
            Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (transform.position.y == GameObject.Find("SpawnHaut").transform.position.y)
        {
            //Player Bas
            Debug.Log("Player Haut");
            isSpawnHaut = true;
            Camera.main.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
    }
}
