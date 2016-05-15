using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
    [SerializeField] string ShipName;
    [SerializeField] PilotCard Pilot;
    [SerializeField] int Attack;
    [SerializeField] int Agility;
    [SerializeField] int Hull;
    [SerializeField] int Shield;

    [SerializeField] int NumberOfStress;
    [SerializeField] int NumberOfFocus;

    [SerializeField] Maneuver[] ManeuverDial;
    

    // Use this for initialization
    void Start()
    {
	
	}
	
	// Update is called once per frame
	void Update()
    {
	
	}

    void AttackCommand()
    {

    }
}
