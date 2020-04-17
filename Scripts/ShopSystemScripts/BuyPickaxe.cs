using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyPickaxe : MonoBehaviour
{

    public int pickaxeID;
    public Text buttonText;
    
    public void PurchasePickaxe()
    {
        if(pickaxeID == 0)
        {
            Debug.Log("No ID set");
            return;
        }

        for(int i = 0; i < PickaxeShop.pickaxeShop.pickaxeList.Count; i++)
        {
            if(pickaxeID == PickaxeShop.pickaxeShop.pickaxeList[i].pickaxeID)
            {
                if(PickaxeShop.pickaxeShop.pickaxeList[i].bought == false)
                {
                    if(PickaxeShop.pickaxeShop.pickaxeList[i].pickaxePrice <= GameObject.Find("Player").GetComponent<PlayerStats>().money)
                    {
                        // Weapon is bought
                        GameObject.Find("Player").GetComponent<PlayerStats>().ReduceMoney(PickaxeShop.pickaxeShop.pickaxeList[i].pickaxePrice);
                        PickaxeShop.pickaxeShop.pickaxeList[i].bought = true;
                        buttonText.text = "Equip";
                    }
                    else
                    {
                        Debug.Log("Not enough money!");
                    }
                }
                else
                {
                    Debug.Log("Has been bough already!");
                    EquipItem(pickaxeID, PickaxeShop.pickaxeShop.pickaxeList[i].pickaxePower);
                }
            }
        }

        PickaxeShop.pickaxeShop.UpdateImage(pickaxeID);
    }

    void EquipItem(int id, int power)
    {
        buttonText.text = "Using";

        GameObject.Find("Player").GetComponent<PlayerStats>().currentPickaxeID = id;
        GameObject.Find("Player").GetComponent<PlayerStats>().pickaxePower = power;
        GameObject.Find("Player").GetComponent<PlayerStats>().UpdatePower();

        PickaxeShop.pickaxeShop.UpdateText(id);

       
    }
}
