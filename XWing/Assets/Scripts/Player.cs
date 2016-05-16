using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public int playerID = 0;




    [PunRPC]
    public void SetPlayerID (int id){

        playerID = id;

    }

    public int GetPlayerID() {

        return playerID;

    }


    //used by photon view to stream data between watched objects
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.isWriting)
        {

         
        }

        else {

          

        }

    }
    
}
