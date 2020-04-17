using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeShop : MonoBehaviour
{

    private List<GameObject> pickaxeInfoList = new List<GameObject>();
    private List<GameObject> buyButtonList = new List<GameObject>();

    public static PickaxeShop pickaxeShop;

    public GameObject pickaxeInfoPrefab;
    public Transform grid;

    public List<Pickaxe> pickaxeList = new List<Pickaxe>();

    // Start is called before the first frame update
    void Start()
    {
        pickaxeShop = this;
        GenerateList();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void GenerateList()
    {
        for(int i = 0; i < pickaxeList.Count; i++)
        {
            GameObject info = Instantiate(pickaxeInfoPrefab, grid, false);
            PickaxeInfo infoScript = info.GetComponent<PickaxeInfo>();

            Debug.Log("ID: " + pickaxeList[i].pickaxeID);

            infoScript.pickaxePower.text = pickaxeList[i].pickaxePower.ToString();
            infoScript.pickaxeName.text = pickaxeList[i].pickaxeName;
            infoScript.pickaxeID = pickaxeList[i].pickaxeID;
            infoScript.pickaxePrice.text = pickaxeList[i].pickaxePrice.ToString();

            Debug.Log("Name: " + pickaxeList[i].pickaxeName);

            infoScript.buyButton.GetComponent<BuyPickaxe>().pickaxeID = pickaxeList[i].pickaxeID;

            pickaxeInfoList.Add(info);
            buyButtonList.Add(infoScript.buyButton);

            if (pickaxeList[i].bought)
                infoScript.pickaxeImage.sprite = Resources.Load<Sprite>("Sprites/" + pickaxeList[i].boughtImageName);
            else
                infoScript.pickaxeImage.sprite = Resources.Load<Sprite>("Sprites/" + pickaxeList[i].unboughtImageName);

        }
    }

    public void UpdateImage(int pickaxeID)
    {
        for(int i = 0; i < pickaxeInfoList.Count; i++)
        {
            PickaxeInfo infoScript = pickaxeInfoList[i].GetComponent<PickaxeInfo>();
            if (infoScript.pickaxeID == pickaxeID)
            {
                for (int j = 0; j < pickaxeList.Count; j++)
                {
                    if (pickaxeList[j].pickaxeID == pickaxeID)
                    {
                        if (pickaxeList[j].bought)
                        {
                            infoScript.pickaxeImage.sprite = Resources.Load<Sprite>("Sprites/" + pickaxeList[i].boughtImageName);
                            infoScript.pickaxePrice.text = "Owned";
                        }
                        else
                            infoScript.pickaxeImage.sprite = Resources.Load<Sprite>("Sprites/" + pickaxeList[i].unboughtImageName);
                    }
                }
            }
        }
    }

    public void UpdateText(int pickaxeID)
    {
        for (int i = 0; i < buyButtonList.Count; i++)
        {
            BuyPickaxe buyPickaxeScript = buyButtonList[i].GetComponent<BuyPickaxe>();
            if (buyPickaxeScript.pickaxeID != pickaxeID && buyPickaxeScript.buttonText.text == "Using")
            {
                buyPickaxeScript.buttonText.text = "Equip";
            }
        }
    }
}
