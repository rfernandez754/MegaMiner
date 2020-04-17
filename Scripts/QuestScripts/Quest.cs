using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public Text questText;
    public Canvas questCanvas;
    public Button questButton;

    private int currentQuestItemIndex;
    private int currentAmount;

    private bool questActive;
    private bool talkedBefore;
    private bool endOfConversation;
    private bool questComplete;
    private bool timeToEndConversation;

    private string[] options = { "stone", "coal", "hard stone", "iron", "silver", "diamond" };
    private int[] ids = {1,2,4,5,6,7};

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        currentQuestItemIndex = -1;
        questActive = false;
        talkedBefore = false;
        timeToEndConversation = false;
        questButton.onClick.AddListener(ButtonPress);
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(questActive && player.GetComponent<PlayerController>().inventory[ids[currentQuestItemIndex]] >= currentAmount)
        {
            questComplete = true;
        }
        else
        {
            questComplete = false;
        }

        if(endOfConversation)
        {
            player.GetComponent<PlayerController>().MakeQuestUIInactive();
            endOfConversation = false;
        }
    }

    void DisplayDialogue(string str)
    {
        questText.text = str;
    }

    void ButtonPress()
    {
        if (!talkedBefore)
        {
            System.Random num = new System.Random();

            //currentQuestItemIndex = num.Next(0, options.Length);
            //currentAmount = num.Next(1, 5);
            currentQuestItemIndex = 0;
            currentAmount = 1;

            string str = "Bring me " + currentAmount + " " + options[currentQuestItemIndex] + ".";
            DisplayDialogue(str);
            questActive = true;
            talkedBefore = true;
        }
        else if (questComplete)
        {
            System.Random num = new System.Random();
            int reward = num.Next(10, 50);
            DisplayDialogue("Thank you for the resource! Here is " + reward + " coins!");
            player.GetComponent<PlayerStats>().AddMoney(reward);
            player.GetComponent<PlayerController>().RemoveFromInventory(ids[currentQuestItemIndex], currentAmount);
            questActive = false;
            questComplete = false;
        }
        else if (timeToEndConversation)
        {
            endOfConversation = true;
            timeToEndConversation = false;
        }
        else if (!questActive)
        {
            System.Random num = new System.Random();

            //currentQuestItemIndex = num.Next(0, options.Length);
            //currentAmount = num.Next(1, 5);
            currentQuestItemIndex = 0;
            currentAmount = 1;

            string str = "Bring me " + currentAmount + " " + options[currentQuestItemIndex] + ".";
            DisplayDialogue(str);
            questActive = true;
        }
        else
        {
            endOfConversation = true;
        }
    }
    
}
