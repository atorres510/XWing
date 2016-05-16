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

    private float shipSpeed = 3.0f;
    public Transform target;
    float timeCounter = 0;
    float degreeCounter = 0;
    bool inCircularManeuver = false;

    // Use this for initialization
    void Start()
    {
        target.position = transform.position;

    }
	
	// Update is called once per frame
	void Update()
    {
        MoveCommand();
        CircularManeuver(1, 1f, 0, 45f, true);
    }

    /* Radius:
     * Xoffset:
     * Yoffset:
     * Degree:
     */
    void CircularManeuver(float radius, float xoffset, float yoffset, float degree, bool isLeft)
    {
        if(isLeft)
        {
            if (inCircularManeuver)
            {
                timeCounter += Time.deltaTime;
                degreeCounter += 1f;
                target.Rotate(0, 0, 1);
                float x = Mathf.Cos(timeCounter) * radius - xoffset;
                float y = Mathf.Sin(timeCounter) * radius + yoffset;
                float z = 0;
                target.position = new Vector3(x, y, z);
            }
        }
        else
        {
            if (inCircularManeuver)
            {
                timeCounter += Time.deltaTime;
                degreeCounter += 1f;
                target.Rotate(0, 0, -1);
                float x = -Mathf.Cos(timeCounter) * radius + xoffset;
                float y = Mathf.Sin(timeCounter) * radius + yoffset;
                float z = 0;
                target.position = new Vector3(x, y, z);
            }
        }
        
        if(degree <= degreeCounter)
        {
            inCircularManeuver = false;
            degreeCounter = 0;
        }
    }

    void MoveCommand()
    {
        if(Input.GetKey(KeyCode.W))
        {
            inCircularManeuver = true;
        }
        transform.rotation = target.rotation;
        transform.position = Vector3.MoveTowards(transform.position, target.position, shipSpeed * Time.deltaTime);
        
    }

    void StraightManeuver(float distance)
    {
        target.position = new Vector3(transform.position.x, transform.position.y + distance, transform.position.z);
    }

    void AttackCommand()
    {

    }
}
