using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Com.JaisonFontaine.SpacePilots {
    public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable {

        #region Private Fields

        private Vector3 playerPos;
        private float xPos;

        //private PlayerController[] listPlayers;

        private Rigidbody rbBall;
        //private Ball scriptBall;

        #endregion


        #region Public Fields

        public float paddleSpeed = 1f;
        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;
        public GameObject ball;

        public GameObject spawnBall;
        public GameObject cloneBall;

        //[SyncVar] public bool isSpawnUp = false;

        public bool isReady = false;
        //[SyncVar] public bool allReady = false;

        public bool ballInPlay = false;

        #endregion


        #region IPunObservable implementation

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
            if (stream.IsWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(ballInPlay);
            }
            else
            {
                // Network player, receive data
                this.ballInPlay = (bool)stream.ReceiveNext();
            }
        }


        #endregion


        #region MonoBehaviour CallBacks

        void Awake() {
            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.IsMine)
            {
                PlayerController.LocalPlayerInstance = this.gameObject;

                GetComponent<MeshRenderer>().material.color = Color.blue;

                if (PhotonNetwork.IsMasterClient) {
                    //Player Bas
                    Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else {
                    //Player Haut
                    Camera.main.transform.rotation = Quaternion.Euler(0, 0, 180);
                }
            }
            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(this.gameObject);
            spawnBall = transform.Find("SpawnBall").gameObject;

            /*if (transform.position.y == GameObject.Find("SpawnBas").transform.position.y) {
                //Player Bas
                Debug.Log("Player Bas");
                isSpawnUp = false;
            }

            if (transform.position.y == GameObject.Find("SpawnHaut").transform.position.y) {
                //Player Haut
                Debug.Log("Player Haut");
                isSpawnUp = true;
            }*/
        }

        void FixedUpdate() {
            /*listPlayers = FindObjectsOfType<PlayerController>();

            if (listPlayers.Length == 2 && listPlayers[0].isReady && listPlayers[1].isReady && allReady == false) {
                allReady = true;
            }*/
        }

        void Update() {
            /*if (!isLocalPlayer) {
                return;
            }*/

            if (!photonView.IsMine && PhotonNetwork.IsConnected) {
                return;
            }

#if (UNITY_EDITOR || UNITY_STANDALONE)
            if (PhotonNetwork.IsMasterClient) {
                //Player Bas
                xPos = transform.position.x + (Input.GetAxis("Horizontal") * paddleSpeed);
            } else {
                //Player Haut
                xPos = transform.position.x + (Input.GetAxis("Horizontal") * -paddleSpeed);
            }

            playerPos = new Vector3(Mathf.Clamp(xPos, -2.1f, 2.1f), transform.position.y, 0f);
            transform.position = playerPos;

            if(Input.GetKey(KeyCode.Space) && isReady == false && PhotonNetwork.CurrentRoom.PlayerCount == 2) {
                SpawnBall();
            }

            /*if (Input.GetButtonDown("Fire1") && allReady == true && ballInPlay == false) {
                CmdShootBall();
            }*/
#else
            /*Touch touch = Input.GetTouch(0);

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
            }*/
#endif
        }

        #endregion


        public void SpawnBall() {
            isReady = true;
            ballInPlay = false;

            cloneBall =  PhotonNetwork.Instantiate(ball.name, spawnBall.transform.position, Quaternion.identity, 0);

            cloneBall.transform.SetParent(transform);
            /*scriptBall = cloneBall.GetComponent<Ball>();

            if (isSpawnUp) {
                scriptBall.playerUp = true;
            } else {
                scriptBall.playerUp = false;
            }*/
        }

        /*[Command]
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
        }*/
    }
}
