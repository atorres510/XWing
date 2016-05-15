using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public bool isOffline = false;
	bool beginSetup = false;
	int numberOfOtherPlayers;

	void Start(){

		if(!isOffline){

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
			StartCoroutine("BeginSetup", 2f);

		
		}
	
	
	}

	//initiates setup for SetUpManager.  Waits for a few seconds to give the setupManagers time to recognize both players.   
	//IEnumerator BeginSetup(float secondsToWait){

		

	//}

	//uses photon instantiate to create player
	void CreatePlayer(){

		Vector3 myVector = new Vector3 (8.71f, 6.59f, -11.0f);
		GameObject myPlayer = PhotonNetwork.Instantiate ("Player", myVector, Quaternion.identity, 0);
		Player p = myPlayer.GetComponent<Player> ();
		PhotonPlayer[] players = PhotonNetwork.otherPlayers;
		
		if (players.Length > 0) {
			
            
			Debug.Log("Number of players currently connected: " + players.Length);
			
			p.playerID = PhotonNetwork.playerList.Length;
            //CameraController c = p.GetComponent<CameraController>();
            //c.enabled = false;

        }

        else {
      
			Debug.Log("No other players currently connected.");
			
			p.playerID = (PhotonNetwork.playerList.Length);
			//CameraController c = p.GetComponent<CameraController>();
			//c.enabled = false;
			
		}

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
