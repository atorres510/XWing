using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public bool isOnline;
	int numberOfOtherPlayers;


    Player myPlayer;
    GameManager gameManager;

    bool beginSetup = false;

    void Start(){

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (isOnline){

			//number of other players will be atleast 1 in multiplayer
			numberOfOtherPlayers = 1;
			Connect ();

		}

		else{

			PhotonNetwork.offlineMode = true;
			PhotonNetwork.CreateRoom(null);
			numberOfOtherPlayers = 0;

		}

	
	}

	void Update(){

		CheckForSetup ();
	
	}

	//checks if all of the players have connected to the room.  initiates SetUp();
	void CheckForSetup(){

        

        if (PhotonNetwork.otherPlayers.Length == numberOfOtherPlayers && !beginSetup) {

			Debug.Log("setup begins");
            beginSetup = true;
            StartCoroutine("BeginSetup");
		
		}
	
	
	}

	//initiates setup 
	IEnumerator BeginSetup(){


        yield return new WaitForSeconds(0.5f);
        myPlayer.GetComponent<PhotonView>().RPC("SetPlayerID", PhotonTargets.Others, myPlayer.GetPlayerID());
        yield return new WaitForSeconds(0.5f);
        gameManager.SetMyPlayer(myPlayer);
        gameManager.SetUp();

        yield return null;

	}

	//uses photon instantiate to create player
	void CreatePlayer(){

		Vector3 myVector = new Vector3 (8.71f, 6.59f, -11.0f);
		GameObject player = PhotonNetwork.Instantiate ("Player", myVector, Quaternion.identity, 0);
		Player p = player.GetComponent<Player> ();
        myPlayer = p;
		PhotonPlayer[] players = PhotonNetwork.otherPlayers;
	
		Debug.Log("Number of players currently connected: " + players.Length);

        p.SetPlayerID(PhotonNetwork.playerList.Length);
        Debug.Log("Player " + p.GetPlayerID() + " has connected.");
        //CameraController c = p.GetComponent<CameraController>();
        //c.enabled = false;


	}



	void Connect(){

		PhotonNetwork.ConnectUsingSettings ("XWINGv1.0");

	}

	void OnGUI(){

		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());

	}
	
	void OnJoinedLobby(){
		Debug.Log ("OnJoinedLobby()");
		PhotonNetwork.JoinRandomRoom ();

	}

	void OnPhotonRandomJoinFailed(){

		Debug.Log ("OnPhotonRandomJoinFailed()");
		PhotonNetwork.CreateRoom (null);
	
	}



	//joins the room and checks if the player is the first to connect.  if so, that player is player 1(white).
	void OnJoinedRoom(){

		Debug.Log ("OnJoinedRoom()");
		CreatePlayer();


	}

}
