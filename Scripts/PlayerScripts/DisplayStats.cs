using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStats : MonoBehaviour
{

    public static DisplayStats displayStats;

    public Text moneyText;
    public Text powerText;
    public Text levelText;
    public Text nameText;

    void Start()
    {
        displayStats = this;
    }

    public void UpdateText()
    {
        moneyText.text = "Money: " + GameObject.Find("Player").GetComponent<PlayerStats>().money.ToString();
        powerText.text = "Power: " + GameObject.Find("Player").GetComponent<PlayerStats>().power.ToString();
        levelText.text = "Level: " + GameObject.Find("Player").GetComponent<PlayerStats>().playerLevel.ToString() + " (" + GameObject.Find("Player").GetComponent<PlayerStats>().currentXP.ToString() + "/" + GameObject.Find("Player").GetComponent<PlayerStats>().xpToLevelUp.ToString() + ")";
        nameText.text = GameObject.Find("Player").GetComponent<PlayerStats>().playerName;
    }
}
