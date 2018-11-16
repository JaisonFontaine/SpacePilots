using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GM : NetworkBehaviour {

    [SyncVar] public int livesPlayer1 = 3;
    [SyncVar] public int livesPlayer2 = 3;
    [SyncVar] public int nbBricks = 20;
    public float resetDelay = 1f;

    public GameObject ball;
    public GameObject spawnBall;
    public GameObject bricks;
    public GameObject spawnBricks;
    public GameObject deathParticles;

    private PlayerController ScriptPlayerController;

    private GameObject cloneBall;

    void Start() {
        ScriptPlayerController = GetComponent<PlayerController>();

        if (!GameObject.FindWithTag("Bricks")) {
            GameObject Bricks = Instantiate(bricks, spawnBricks.transform.position, Quaternion.identity) as GameObject;
            NetworkServer.Spawn(Bricks);
        }
    }

    void CheckGameOver1() {
        /*if (nbBricks < 1)
        {
            GameObject.Find("YouWon").SetActive(true);
            Time.timeScale = .25f;
            Invoke("Reset", resetDelay);
        }*/

        if (livesPlayer1 < 1) {
            livesPlayer1 = 0;
            GameObject.Find("GameOver").SetActive(true);
            Time.timeScale = .25f;
            Invoke("Reset", resetDelay);
        }

        if (livesPlayer2 < 1)
        {
            GameObject.Find("YouWon").SetActive(true);
            Time.timeScale = .25f;
            Invoke("Reset", resetDelay);
        }
    }

    void CheckGameOver2() {
        /*if (nbBricks < 1)
        {
            GameObject.Find("YouWon").SetActive(true);
            Time.timeScale = .25f;
            Invoke("Reset", resetDelay);
        }*/

        if (livesPlayer2 < 1)
        {
            livesPlayer2 = 0;
            GameObject.Find("GameOver").SetActive(true);
            Time.timeScale = .25f;
            Invoke("Reset", resetDelay);
        }

        if (livesPlayer1 < 1)
        {
            GameObject.Find("YouWon").SetActive(true);
            Time.timeScale = .25f;
            Invoke("Reset", resetDelay);
        }
    }

    void Reset() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    [Command]
    public void CmdLoseLife1() {
        livesPlayer1--;
        RpcMajLives1(livesPlayer1);

        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(ScriptPlayerController.cloneBall);
        Invoke("SetupBall", resetDelay);
        CheckGameOver1();
    }

    [Command]
    public void CmdLoseLife2() {
        livesPlayer2--;
        RpcMajLives2(livesPlayer2);

        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(ScriptPlayerController.cloneBall);
        Invoke("SetupBall", resetDelay);
        CheckGameOver2();
    }

    [ClientRpc]
    public void RpcMajLives1(int live1) {
        GameObject.Find("LivesPlayer1").GetComponent<Text>().text = "Lives player 1 : " + live1;
    }

    [ClientRpc]
    public void RpcMajLives2(int live2) {
        GameObject.Find("LivesPlayer2").GetComponent<Text>().text = "Lives player 2 : " + live2;
    }

    void SetupBall() {
        ScriptPlayerController.CmdSpawnBall();
    }

    [Command]
    public void CmdDestroyBrick() {
        nbBricks--;
    }
}
