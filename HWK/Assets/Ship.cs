using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Ship : MonoBehaviour {

    //Systems Members
    int[] engine; //0 = min, 1 = current, 2 = max
    int[] shields;
    int[] weapons;
    int[] reserves;
    int energyIncrement = 10; //energy counted in units (i.e. 10 energy units)

    //UI memberes
    [SerializeField]
    Image EngineBar, ShieldsBar, WeaponsBar, ReserveBar, ShieldPointBar; //for UI
    public Slider[] shieldSliders; //0 = Fore, 1 = Port, 2 = Starboard, 3 = aft
    public Text metrics;

    //Shield Management Members
    int[] shieldPoints; //0 = min, 1 = available, 2 = max, 3 = total;
    int currentlySpentShieldPoints = 0;

    
    public class Shield {

        public enum Section { FORE, PORT, STARBOARD, AFT };

        public Section section;
        public float currentHealth;
        public float maxHealth;
        public float regenerationRate;
        public bool isEnabled;
        public bool isInvulnerable;

        public Image UIElement;

        public Shield(Section sec, float hpCurrent, float hpMax, float regenRate,
            bool enabled, bool invulnerable){

            section =sec;
            currentHealth = hpCurrent;
            maxHealth = hpMax;
            regenerationRate = regenRate;
            isEnabled = enabled;
            isInvulnerable = invulnerable;

        }

        public Shield() {

            section = Shield.Section.FORE;
            currentHealth = 0.0f;
            maxHealth = 100.0f;
            regenerationRate = 1.0f;
            isEnabled = false;
            isInvulnerable = true;
            
        }

        public void IsActive() {




        }
        




    }


    void ManageShieldUpdateFunctions() {






    }











    //called by UI buttons
    #region Add/Subtract Methods 
    public void AddEngine() {

        AddEnergy(engine);
        EngineBar.fillAmount = ConvertEnergyLeveltoFloat(engine);

    }

    public void SubstractEngine()
    {

        SubtractEnergy(engine);
        EngineBar.fillAmount = ConvertEnergyLeveltoFloat(engine);

    }

    public void AddShields()
    {

        AddEnergy(shields);
        ShieldsBar.fillAmount = ConvertEnergyLeveltoFloat(shields);
        ConvertShieldEnergytoPoints();
        ShieldPointBar.fillAmount = ConvertEnergyLeveltoFloat(shieldPoints);
      
    }

    public void SubstractShields()
    {

        SubtractEnergy(shields);
        ShieldsBar.fillAmount = ConvertEnergyLeveltoFloat(shields);
        ConvertShieldEnergytoPoints();
        ShieldPointBar.fillAmount = ConvertEnergyLeveltoFloat(shieldPoints);
       
    }

    public void AddWeapons()
    {

        AddEnergy(weapons);
        WeaponsBar.fillAmount = ConvertEnergyLeveltoFloat(weapons);

    }

    public void SubstractWeapons()
    {

        SubtractEnergy(weapons);
        WeaponsBar.fillAmount = ConvertEnergyLeveltoFloat(weapons);

    }

    public void AddShieldPoints(int value) {

        if (shieldPoints[1] < shieldPoints[2])
        {

            shieldPoints[1] -= value;
            ShieldPointBar.fillAmount = ConvertEnergyLeveltoFloat(shieldPoints);
            ManageShieldValues();
            SliderFailSafe();

        }


    }

    public void SubtractShieldPoints(int value) {

        if (shieldPoints[1] > shieldPoints[0]) {

            shieldPoints[1] -= value;
            ShieldPointBar.fillAmount = ConvertEnergyLeveltoFloat(shieldPoints);
            Debug.Log("ShieldPoints: " + shieldPoints[1]);
            ManageShieldValues();
            SliderFailSafe();

        }

    }

    //adds energy to a particular system in stepwise increments.
    void AddEnergy(int[] system) {

        if (system[1] < system[2] && reserves[1] > reserves[0])
        {

            system[1] += energyIncrement;
            reserves[1] -= energyIncrement;
            ReserveBar.fillAmount = ConvertEnergyLeveltoFloat(reserves);

        }

        else { }

    }
    //removes energy to a particular system in stepwise increments.
    void SubtractEnergy(int[] system)
    {

        if (system[1] > system[0])
        {

            system[1] -= energyIncrement;
            reserves[1] += energyIncrement;
            ReserveBar.fillAmount = ConvertEnergyLeveltoFloat(reserves);

        }

        else { }

    }

    #endregion

    #region Slider Methods

    void EnableSlider(Slider s) {

        s.interactable = true;

    }

    void DisableSlider(Slider s) {

        s.interactable = false;

    }

    //Used to compare current slider values to the last known shield slider value to determine changes
    //called in manage shield values
    float[] lastShieldSliderValue;
    void SetLastShieldSliderValue()
    {

        for (int i = 0; i < shieldSliders.Length; i++){

            lastShieldSliderValue[i] = shieldSliders[i].value;
            

        }


    }

    //ensures that the player cannot spend more points that they have.  takes points away from other shieldSlidervalues if
    //they go over.
    //called in SpendShieldPoints().  
    void SliderFailSafe() {
        Debug.Log("FailSafe");
        if (shieldPoints[1] < 0) {
            Debug.Log("FailSafe Activated");
            for (int i = 0; i < shieldSliders.Length; i++){

                if (shieldSliders[i].value > 0){

                    shieldSliders[i].value--;
                    ManageShieldValues();
                    if (shieldPoints[1] == 0) {

                        break;
                        
                    }

                }

                else {

                    //continue loop

                }
                
            }
            
        }
        
    }

    //when shield points are spent, change available shield points and currently spent points accordingly
    //called in SpendShieldPoints()
    void ManageShieldValues() {

        int tempSpentShieldPoints = 0;
        for (int i = 0; i < shieldSliders.Length; i++){

            tempSpentShieldPoints += (int)shieldSliders[i].value;

        }

        shieldPoints[1] -= tempSpentShieldPoints - currentlySpentShieldPoints;
        currentlySpentShieldPoints = tempSpentShieldPoints;
        //Debug.Log("Spent Shield Points: " + currentlySpentShieldPoints);
        //Debug.Log("Available Shield Points: " + shieldPoints[1]);
        //Debug.Log("Total Shield Points: " + shieldPoints[3]);
        ShieldPointBar.fillAmount = ConvertEnergyLeveltoFloat(shieldPoints);
        SetLastShieldSliderValue();

    }

    //called by shield sliders in the "On Value Changed".  Spend shield points to the desired
    //section of the ship.
    public void SpendShieldPoints() {

        //if there are shield points available to spend, spend them.
        if (shieldPoints[1] > 0){
            Debug.Log("1");
            ManageShieldValues();
            SliderFailSafe();
            

        }

        //if there are no points to spend, be able to move slider down, but not up.
        else {

            for (int i = 0; i < shieldSliders.Length; i++){
                //if trying to move slider up
                if (shieldSliders[i].value >= lastShieldSliderValue[i])
                {

                    shieldSliders[i].value = lastShieldSliderValue[i];
                    

                }
                //if trying to move slider down, move it down by one
                else if ((shieldSliders[i].value < lastShieldSliderValue[i]))
                {
                    Debug.Log("2");
                    ManageShieldValues();
                    SliderFailSafe();

                }

                else { }

            }

        }

    }
    
    #endregion

    
    //converts the energy level on the Shields Bar increase the total Shield Points
    //every 20 energy units. called in Add/Subtract Shields
    void ConvertShieldEnergytoPoints() {
        
        shieldPoints[3] = (shields[1] / 20);

        shieldPoints[1] = shieldPoints[3] - currentlySpentShieldPoints;
        SliderFailSafe();

        /* if ((shieldPoints[3] - currentlySpentShieldPoints) > 0) {

             shieldPoints[1] = shieldPoints[3] - currentlySpentShieldPoints;

         }

         else if ((shieldPoints[3] - currentlySpentShieldPoints) <= 0) {

             shieldPoints[1] = 0;
             currentlySpentShieldPoints = shieldPoints[3];
             //adjust spent shields
         }*/

       // Debug.Log("Spent Shield Points: " + currentlySpentShieldPoints);
       // Debug.Log("Avaliable Shield Points: " + shieldPoints[1]);
       // Debug.Log("Total Shield Points: " + shieldPoints[3]);

    }
    
    //converts ints to floats for use by Image.fillAmount.
    //called in Start and Add/SubtractMethods.
    float ConvertEnergyLeveltoFloat(int[] system) {

        float f1 = (float)system[1];
        float f2 = (float)system[2];

        return (f1 / f2);

    }


    void Start () {

        engine = new int[3] {0, 50, 100};
        EngineBar.fillAmount = ConvertEnergyLeveltoFloat(engine);

        shields = new int[3] { 0, 50, 100 };
        ShieldsBar.fillAmount = ConvertEnergyLeveltoFloat(shields);

        weapons = new int[3] { 0, 50, 100 };
        WeaponsBar.fillAmount = ConvertEnergyLeveltoFloat(weapons);

        reserves = new int[3] { 0, 0, 150 };
        ReserveBar.fillAmount = ConvertEnergyLeveltoFloat(reserves);

        shieldPoints = new int[4] { 0, 2, 5, 2 };
        ShieldPointBar.fillAmount = ConvertEnergyLeveltoFloat(shieldPoints);

        lastShieldSliderValue = new float[4] { 0.0f, 0.0f, 0.0f, 0.0f };
        SetLastShieldSliderValue();

    }

    void Update() {

        // UpdateSliderInteractablility();
        metrics.text = "Shield Energy Units: " + shields[1] + "\nAvailable Shield Points: " + shieldPoints[1] + "\nSpent shield Points: " + currentlySpentShieldPoints + "\nTotal Shield Points: " + shieldPoints[3];

    }
    
}
