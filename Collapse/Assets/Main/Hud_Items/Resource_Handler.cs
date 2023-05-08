using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Resource_Handler : MonoBehaviour
{
    int Fuel = 100;
    int Survival = 100;
    int Minerals = 100;
    int currentResource;
    int futureResource;
    int FuelFuture;
    int SurvivalFuture;
    int MineralsFuture;
    string Type;
    bool updateResource;

    bool updateFuel;
    bool updateSurvival;
    bool updateMinerals;

    public int FuelTextAmount = 0;
    public int SurvivalTextAmount = 0;
    public int MineralsTextAmount = 0;

    int currentTime;
    float scrollingTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //updateResource = checkToUpdate();

        if (updateResource) 
        {
            if (scrollingTime > 0.05)
            {

                UpdateResourceEffect();
                scrollingTime = 0;
            }
            else
                scrollingTime += Time.deltaTime;
        }
    }

    //bool checkToUpdate() 
    //{
    //    return (updateFuel || updateMinerals || updateSurvival);
    //}
    void UpdateResourceEffect()
    {
        //if (futureResource > 0)
        //{
        //    switch (Type)
        //    {
        //        case "Fuel":
        //            Fuel++;
        //            break;
        //        case "Mineral":
        //            Minerals++;
        //            break;
        //        case "Survival":
        //            Survival++;
        //            break;

        //    }
        //    futureResource--;
        //}
        checkUpdateResources();
        if (FuelFuture > 0)
        {
            Fuel++;
            FuelFuture--;
        }
        else
            updateFuel= false;

        if (MineralsFuture > 0)
        { 
            Minerals++;
            MineralsFuture--;
        }
        else
            updateMinerals= false;
        if (SurvivalFuture > 0)
        {
            Survival++;
            SurvivalFuture--;
        }
        else
            updateSurvival= false;

        if (!updateFuel && !updateMinerals && !updateSurvival)
            updateResource = false;
    }
    public void SetUpdateResource(bool updateRes) { updateResource = updateRes; }
    public void SetFutureResource(int amount, int currentAmount, string type) { futureResource = amount; currentResource = currentAmount;  Type = type; }
    public void SetFuelResource(int amount) { FuelFuture += amount; checkUpdateResources(); /*updateFuel = true;*/ }
    public void SetSurvivalResource(int amount) { SurvivalFuture += amount; checkUpdateResources(); /*updateSurvival = true;*/ }
    public void SetMineralsResource(int amount) { MineralsFuture += amount; checkUpdateResources();/*updateMinerals = true;*/ }

    
    public void resetFuture() 
    {
        FuelFuture = 0;
        SurvivalFuture = 0;
        MineralsFuture = 0;
        FuelTextAmount= 0;
        SurvivalTextAmount= 0;
        MineralsTextAmount= 0;
    }
    public void checkUpdateResources() 
    {
        if (SurvivalFuture > 0)
            updateSurvival = true;

        if(FuelFuture > 0)
            updateFuel = true;

        if(MineralsFuture > 0)
            updateMinerals = true;
    }

    public int GetFuel() { return Fuel; }
    public int GetSurvival() { return Survival; }

    public int GetMinerals() { return Minerals; }

    public int GetFuelFuture() { return FuelFuture; }
    public int GetSurvivalFuture() { return SurvivalFuture; }

    public int GetMineralsFuture() { return MineralsFuture; }


    public void SetFuel(int fuel) { Fuel += fuel; }

    public void SetSurvival(int survival) { Survival += survival; }

    public void SetMinerals(int minerals) { Minerals += minerals; }

    public void SetFuelTextAmount(int fuelTextAmount) { FuelTextAmount += fuelTextAmount;}
    public void SetSurvialTextAmount(int survialTextAmount) { SurvivalTextAmount += survialTextAmount;}
    public void SetMineralTextAmount(int mineralTextAmount) { MineralsTextAmount += mineralTextAmount; }
}
