using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Com.JaisonFontaine.SpacePilots
{
    public class Ball : MonoBehaviourPunCallbacks, IPunObservable {

        #region Public Fields

        public float ballInitialVelocity = 400f;
        public int idPlayerBall;

        #endregion


        #region IPunObservable implementation

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(idPlayerBall);
            }
            else
            {
                // Network player, receive data
                this.idPlayerBall = (int)stream.ReceiveNext();
            }
        }

        #endregion


        #region MonoBehaviour CallBacks

        void Awake() {

        }

        void Update() {
        
        }

        #endregion
    }
}
