using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public LayerMask layermask;

    public Canvas shopCanvas;
    public Canvas statsCanvas;
    public Canvas questCanvas;
    private bool shopOpen;

    public Camera playerCamera;

    Rigidbody rb;

    private bool toolEquipped;

    private GameObject player;
    private GameObject shop;
    private GameObject questSpot;

    public Text UIMessageText;

    public int[] inventory = new int[100];

    // Start is called before the first frame update
    void Start()
    {
        shopOpen = false;
        toolEquipped = false;
        rb = this.gameObject.GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        shop = GameObject.Find("Shop");
        questSpot = GameObject.Find("QuestSpot");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x < shop.transform.position.x + 4 && player.transform.position.x > shop.transform.position.x - 4 && player.transform.position.y > 0)
        {
            UIMessageText.text = "Press I to open shop";
            UIMessageText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (shopOpen)
                {
                    MakeShopInactive();
                    UIMessageText.gameObject.SetActive(true);
                }
                else
                {
                    MakeShopActive();
                    UIMessageText.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            UIMessageText.gameObject.SetActive(false);
            if (shopOpen)
            {
                MakeShopInactive();
            }
        }

        if (player.transform.position.x < questSpot.transform.position.x + 4 && player.transform.position.x > questSpot.transform.position.x - 4 && player.transform.position.y > 0)
        {
            UIMessageText.text = "Press E to talk to Dave";
            UIMessageText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (shopOpen)
                {
                    MakeQuestUIInactive();
                    UIMessageText.gameObject.SetActive(true);
                }
                else
                {
                    MakeQuestUIActive();
                    UIMessageText.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            UIMessageText.gameObject.SetActive(false);
            if (shopOpen)
            {
                MakeQuestUIInactive();
            }
        }

        transform.Translate(-1 * speed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, 0f);

        playerCamera.GetComponent<Transform>().position = new Vector3(this.transform.position.x, this.transform.position.y + 1.0F, this.transform.position.z - 6.1F);

        if (Input.GetMouseButtonDown(0) && !toolEquipped)// && !EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("0");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100))//,layermask))
            {
                Debug.Log("1");
                if (!(hit.transform.gameObject.name == "Player") && !(hit.transform.gameObject.GetComponent<Renderer>().material.name == "Dirt (Instance)")&& !(hit.transform.gameObject.tag == "NonMineable"))
                {
                    Debug.Log("2");
                    this.GetComponent<MineTile>().tile = hit.transform.gameObject;
                    this.GetComponent<MineTile>().requiredHoldTime = (1.0F*hit.transform.gameObject.GetComponent<TileStats>().toughness)/GameObject.Find("Player").GetComponent<PlayerStats>().power;
                    this.GetComponent<MineTile>().clickingTile = true;               
                }
            }
        }



        if (Input.GetKeyDown("space") && shopOpen == false)
        {
            //rb.AddForce(transform.up * 125);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject blaster = GameObject.Find("Blaster");
            if (blaster.GetComponent<Blaster>().isEquipped == true)
            {
                toolEquipped = false;
                this.GetComponentInChildren<Camera>().transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 6.1F);
                blaster.GetComponent<Blaster>().isEquipped = false;
            }
            else
            {
                toolEquipped = true;
                this.GetComponentInChildren<Camera>().transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 25.0F);
                blaster.GetComponent<Blaster>().isEquipped = true;
            }


        }
    }

    public void MakeShopInactive()
    {
        shopCanvas.gameObject.SetActive(false);
        shopOpen = false;
        statsCanvas.gameObject.SetActive(true);
    }

    public void MakeShopActive()
    {
        statsCanvas.gameObject.SetActive(false);
        shopCanvas.gameObject.SetActive(true);
        shopOpen = true;
    }

    public void MakeQuestUIInactive()
    {
        questCanvas.gameObject.SetActive(false);
        statsCanvas.gameObject.SetActive(true);
    }

    public void MakeQuestUIActive()
    {
        statsCanvas.gameObject.SetActive(false);
        questCanvas.gameObject.SetActive(true);
    }

    public void AddToInventory(int id)
    {
        inventory[id] = inventory[id] + 1;
    }

    public void RemoveFromInventory(int id, int amount)
    {
        inventory[id] = inventory[id] - amount;
    }
}
