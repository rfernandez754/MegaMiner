using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public string playerName;

    public int money;

    public int power;
    public int pickaxePower;
    public int powerFromLevel;

    public int playerLevel;
    public float currentXP;
    public float xpToLevelUp;

    public int currentPickaxeID;



    // Start is called before the first frame update
    void Start()
    {
        playerName = "Player";
        playerLevel = 1;
        power = 1;
        currentXP = 0;
        powerFromLevel = 0;
        xpToLevelUp = Mathf.Pow(playerLevel, 2) * 10;
        //DisplayStats.displayStats.UpdateText();
    }

    void Update()
    {
        
    }


    //Money Methods
    public void AddMoney(int amount)
    {
        money += amount;
        DisplayStats.displayStats.UpdateText();
    }

    public void ReduceMoney(int amount)
    {
        money -= amount;
        DisplayStats.displayStats.UpdateText();
    }


    //Level Methods
    public void LevelUp()
    {
        playerLevel++;
        powerFromLevel = (playerLevel-1) * 2;
        UpdatePower();
        xpToLevelUp = Mathf.Pow(playerLevel, 2) * 10;
        DisplayStats.displayStats.UpdateText();
    }

    public void AddXP(float xp)
    {
        currentXP += xp;
        CheckXP();
    }

    private void CheckXP()
    {
        if(currentXP >= xpToLevelUp)
        {
            float leftOverXP = currentXP - xpToLevelUp;
            LevelUp();
            currentXP = leftOverXP;
            CheckXP();
        }
        DisplayStats.displayStats.UpdateText();
    }

    public void UpdatePower()
    {
        power = pickaxePower + powerFromLevel;
        DisplayStats.displayStats.UpdateText();
    }
}
