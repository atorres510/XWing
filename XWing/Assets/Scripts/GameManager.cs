using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    Player myPlayer;
    List<Player> playerList = new List<Player>();

    bool gameOn = false;
    int previousPhase = 0;

    public int currentPhase;
    int correctedCurrentPhase;




    //holds local photonview component
    PhotonView photonView;

    //holds parent UI objects that hold other relevent UI objects.  used to activate/deactivate groups of elements more easily
    public GameObject[] UIParentObjects; //0 - static elements, 1 - setup, 2 - planning, 3 - activation, 4 - combat, 5 - resolution

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

        if (gameOn) {

            ChangePhases();
            PhotonViewUpdate();

        }




    }

    #region SetUpMethods
    //holds methods that set up the board, GM member variables, and players
   
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

        StartGame();


    }

    #endregion

    #region UI Methods

    public void EnableParentUIObject(int phase) {

        UIParentObjects[phase].gameObject.SetActive(true);


    }

    public void DisableParentUIObject(int phase)
    {

        UIParentObjects[phase].gameObject.SetActive(false);


    }



    public void ReadyToStartButton() {

        myPlayer.isCommitted = true;

        if (AreAllOtherClientsCommitted()) {
            Debug.Log("Changing from Phase " + currentPhase + " to Phase " + (currentPhase + 1));
            currentPhase++;

        }

    }






    #endregion

    #region Client to Client Methods
    //used for communication between players, networked or local
    bool AreAllOtherClientsCommitted() {

        foreach (Player p in playerList) {

            if (!p.isCommitted) {

                return false;

            }



        }

        return true;
        
    }


    void ChangePhases() {

        if (currentPhase != previousPhase)
        {
            DisableParentUIObject(previousPhase);
            EnableParentUIObject(currentPhase);
            previousPhase = currentPhase;
        }

        else {

            

        }



    }





    #endregion

    #region GameMethods
    //hold phases of the game, such as planning, activation, and combat along with rules and other game data

    void StartGame() {

        //Phase 1 - SetUp
        currentPhase = 1;
        previousPhase = 1;
        EnableParentUIObject(currentPhase);
        gameOn = true;


    }



    #endregion

    #region PUNMethods
    //Holds any method directly using PUN source code

    //used by photon view to stream data between watched objects
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.isWriting)
        {

            stream.SendNext(currentPhase);

        }

        else {

            this.correctedCurrentPhase = (int)stream.ReceiveNext();

        }

    }

    //used to stream info through photonserialize view.  called in Update()
    void PhotonViewUpdate()
    {

        if (!photonView.isMine)
        {

            currentPhase = correctedCurrentPhase;


        }




    }


    #endregion



}
