using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Ship : MonoBehaviour {

    int[] engine; //0 = min, 1 = current, 2 = max
    int[] shields;
    int[] weapons;
    int[] reserves;

    int energyIncrement = 10;
    [SerializeField]
    Image EngineBar, ShieldsBar, WeaponsBar, ReserveBar;
   



    public void AddEngine() {

        AddEnergy(engine);
        EngineBar.fillAmount = EnergyLeveltoFloat(engine);
       
    }

    public void SubstractEngine()
    {

        SubtractEnergy(engine);
        EngineBar.fillAmount = EnergyLeveltoFloat(engine);

    }

    public void AddShields()
    {

        AddEnergy(shields);
        ShieldsBar.fillAmount = EnergyLeveltoFloat(shields);

    }

    public void SubstractShields()
    {

        SubtractEnergy(shields);
        ShieldsBar.fillAmount = EnergyLeveltoFloat(shields);

    }

    public void AddWeapons()
    {

        AddEnergy(weapons);
        WeaponsBar.fillAmount = EnergyLeveltoFloat(weapons);

    }

    public void SubstractWeapons()
    {

        SubtractEnergy(weapons);
        WeaponsBar.fillAmount = EnergyLeveltoFloat(weapons);

    }


    void AddEnergy(int[] system) {

        if (system[1] < system[2] && reserves[1] > reserves[0])
        {

            system[1] += energyIncrement;
            reserves[1] -= energyIncrement;
            ReserveBar.fillAmount = EnergyLeveltoFloat(reserves);

        }

        else { }

    }

    void SubtractEnergy(int[] system)
    {

        if (system[1] > system[0])
        {

            system[1] -= energyIncrement;
            reserves[1] += energyIncrement;
            ReserveBar.fillAmount = EnergyLeveltoFloat(reserves);

        }

        else { }

    }

    float EnergyLeveltoFloat(int[] system) {

        float f1 = (float)system[1];
        float f2 = (float)system[2];

        return (f1 / f2);

    }


	void Start () {

        engine = new int[3] {0, 50, 100};
        EngineBar.fillAmount = EnergyLeveltoFloat(engine);

        shields = new int[3] { 0, 50, 100 };
        ShieldsBar.fillAmount = EnergyLeveltoFloat(shields);

        weapons = new int[3] { 0, 50, 100 };
        WeaponsBar.fillAmount = EnergyLeveltoFloat(weapons);

        reserves = new int[3] { 0, 0, 150 };
        ReserveBar.fillAmount = EnergyLeveltoFloat(reserves);

    }


    
}
