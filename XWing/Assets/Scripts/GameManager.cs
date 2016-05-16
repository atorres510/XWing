using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    Player myPlayer;
    List<Player> playerList = new List<Player>();


    PhotonView photonView;

    //called in network manager when online, called in Start() when offline
    public void SetMyPlayer(Player p) {

        myPlayer = p;

    }


    public void AddPlayertoPlayerList(Player p) {
        
        playerList.Add(p);

    }


    public void SetUp() {

        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject playerObject in playerObjects) {

            AddPlayertoPlayerList(playerObject.GetComponent<Player>());


        }

        foreach (Player p in playerList)
        {

            Debug.Log("Player " + p.GetPlayerID());

        }


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


    void Start() {

        photonView = gameObject.GetComponent<PhotonView>();

        if (PhotonNetwork.offlineMode) {

            //do singleplayer stuff


        }

    }




    // Update is called once per frame
    void Update () {
	
	}
}
