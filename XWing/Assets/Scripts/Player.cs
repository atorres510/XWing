using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public int playerID = 0;

    PhotonView photonView;

    public bool isCommitted = false;
    bool correctedIsCommitted = false;



    void Start()
    {

        photonView = gameObject.GetComponent<PhotonView>();

        if (PhotonNetwork.offlineMode)
        {

            //do singleplayer stuff


        }

    }




    // Update is called once per frame
    void Update()
    {

        PhotonViewUpdate();


    }



    #region Set/Get Methods
    [PunRPC]
    public void SetPlayerID (int id){

        playerID = id;

    }

    public int GetPlayerID() {

        return playerID;

    }

    #endregion

    #region PUN Methods
    //used by photon view to stream data between watched objects
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.isWriting)
        {

            stream.SendNext(isCommitted);

        }

        else {

            this.correctedIsCommitted = (bool)stream.ReceiveNext();

        }

    }

    //used to stream info through photonserialize view.  called in Update()
    void PhotonViewUpdate()
    {

        if (!photonView.isMine)
        {

            isCommitted = correctedIsCommitted;


        }




    }


    #endregion




}
