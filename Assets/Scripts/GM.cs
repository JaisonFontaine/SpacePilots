using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GM : NetworkBehaviour {

    [SyncVar] public int lives1 = 3;
    [SyncVar] public int lives2 = 3;
    [SyncVar] public int nbBricks = 20;
    public float resetDelay = 1f;

    public GameObject bricks;
    public GameObject spawnBricks;

    public GameObject deathParticles;

    private PlayerController scriptPlayerController;

    void Start() {
        if (!GameObject.FindWithTag("Bricks")) {
            GameObject cloneBricks = Instantiate(bricks, spawnBricks.transform.position, Quaternion.identity) as GameObject;
            NetworkServer.Spawn(cloneBricks);
        }

        scriptPlayerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
    }

    public void Setup()
    {
        /*clonePaddle = Instantiate(paddle, transform.position, Quaternion.identity) as GameObject;
        Instantiate(bricksPrefab, transform.position, Quaternion.identity);*/
    }

    void Reset()
    {
        /*Time.timeScale = 1f;
        SceneManager.LoadScene(0);*/
    }

    [Command]
    public void CmdLoseLife1() {
        lives1--;
        RpcMajLife1();

        //Instantiate(deathParticles, transform.position, Quaternion.identity);
        //Destroy(scriptPlayerController.cloneBall);

        //scriptPlayerController.CmdSpawnBall();

        //Invoke("SetupPaddle", resetDelay);
        //CmdCheckGameOver1();
    }

    [Command]
    public void CmdLoseLife2() {
        lives2--;
        RpcMajLife2();

        //Instantiate(deathParticles, transform.position, Quaternion.identity);
        //Destroy(scriptPlayerController.cloneBall);

        //scriptPlayerController.CmdSpawnBall();

        //Invoke("SetupPaddle", resetDelay);
        //CmdCheckGameOver2();
    }

    [Command]
    void CmdCheckGameOver1() {
        /*if (nbBricks < 1)
        {
            youWon.SetActive(true);
            Time.timeScale = .25f;
            Invoke("Reset", resetDelay);
        }*/

        if (lives1 < 1) {
            lives1 = 0;
            GameObject.Find("GameOver").SetActive(true);
            //gameOver.SetActive(true);
            Time.timeScale = .25f;
            //Invoke("Reset", resetDelay);
        }

        if (lives2 < 1) {
            lives2 = 0;
            GameObject.Find("YouWon").SetActive(true);
            //gameOver.SetActive(true);
            Time.timeScale = .25f;
            //Invoke("Reset", resetDelay);
        }
    }

    [Command]
    void CmdCheckGameOver2() {
        /*if (nbBricks < 1)
        {
            youWon.SetActive(true);
            Time.timeScale = .25f;
            Invoke("Reset", resetDelay);
        }*/

        if (lives1 < 1)
        {
            lives1 = 0;
            GameObject.Find("YouWon").SetActive(true);
            //gameOver.SetActive(true);
            Time.timeScale = .25f;
            //Invoke("Reset", resetDelay);
        }

        if (lives2 < 1)
        {
            lives2 = 0;
            GameObject.Find("GameOver").SetActive(true);
            //gameOver.SetActive(true);
            Time.timeScale = .25f;
            //Invoke("Reset", resetDelay);
        }
    }

    [ClientRpc]
    public void RpcMajLife1()
    {
        GameObject.Find("LivesPlayer1").GetComponent<Text>().text = "Lives Player 1 : " + lives1;
    }

    [ClientRpc]
    public void RpcMajLife2()
    {
        GameObject.Find("LivesPlayer2").GetComponent<Text>().text = "Lives Player 2 : " + lives2;
    }

    void SetupPaddle()
    {
        //clonePaddle = Instantiate(paddle, transform.position, Quaternion.identity) as GameObject;
    }

    public void DestroyBrick()
    {
        /*nbBricks--;
        CheckGameOver();*/
    }
}
