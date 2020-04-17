using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MineTile : MonoBehaviour
{

    public GameObject tile;

    private bool pointerDown;
    public bool clickingTile;
    private float pointerDownTimer;

    public float requiredHoldTime;

    public UnityEvent onLongClick;

    [SerializeField]
    private Image fillImage;

    public Image backroundImage;

    public Text tileName;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
        requiredHoldTime = 3;
        tile = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && tile)
        {
            tileName.text = tile.GetComponent<TileStats>().tileName;
            Debug.Log("mouse click on " + tileName);
            pointerDown = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("mouse click done " + tileName);
            Reset();
        }

        if (pointerDown && clickingTile)
        {
            backroundImage.gameObject.SetActive(true);
            tileName.gameObject.SetActive(true);
            pointerDownTimer += Time.deltaTime;
            if(pointerDownTimer >= requiredHoldTime)
            {
                //if (onLongClick != null)
                //    onLongClick.Invoke();

                DestroyTile();
                Reset();
            }

            fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
        }
    }

    private void Reset()
    {
        pointerDown = false;
        clickingTile = false;
        pointerDownTimer = 0;
        fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
        backroundImage.gameObject.SetActive(false);
        tileName.gameObject.SetActive(false);
    }

    public void DestroyTile()
    {
        this.gameObject.GetComponent<PlayerStats>().AddMoney(tile.GetComponent<TileStats>().worth);
        this.gameObject.GetComponent<PlayerStats>().AddXP(tile.GetComponent<TileStats>().xp);
        this.gameObject.GetComponent<PlayerController>().AddToInventory(tile.GetComponent<TileStats>().id);
        Destroy(tile);
    }
}
