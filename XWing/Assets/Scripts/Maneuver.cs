using UnityEngine;
using System.Collections;

public class Maneuver : MonoBehaviour
{
    enum ManeuverDifficulty {Green, Red, White};
    enum ManeuverBarring {Straight, LeftBank, RightBank, LeftTurn, RightTurn, LeftSLoop, RightSLoop, KTurn, LeftTallonRoll, RightTallonRoll, FullStop};

    [SerializeField] int Speed;
    [SerializeField] ManeuverDifficulty Difficulty;
    [SerializeField] ManeuverBarring Barring;

    // Use this for initialization
    void Start()
    {
	
	}
	
	// Update is called once per frame
	void Update()
    {
	
	}
}
