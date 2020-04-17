using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    private float distance;
    public ParticleSystem blasterRay;
    public bool isEquipped;

    // Start is called before the first frame update
    void Start()
    {
        blasterRay.Pause();
        distance = 100;
        isEquipped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isEquipped)
            Fire();
    }

    public void Fire()
    {
        GameObject player = GameObject.Find("Player").gameObject;

        blasterRay.GetComponent<Transform>().position = player.GetComponent<Transform>().position;
        blasterRay.Play();

        RaycastHit[] hits;
        hits = Physics.RaycastAll(player.GetComponent<Transform>().position, -player.GetComponent<Transform>().up, 25.0F);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Transform trans = hit.transform.GetComponent<Transform>();

            if (trans)
            {
                GameObject tile = hit.transform.gameObject;

                GameObject.Find("Player").GetComponent<PlayerStats>().AddMoney(tile.GetComponent<TileStats>().worth);
                GameObject.Find("Player").GetComponent<PlayerStats>().AddXP(tile.GetComponent<TileStats>().xp);
                trans.gameObject.SetActive(false);
            }
        }

        StartCoroutine(Example());

        IEnumerator Example()
        {
            yield return new WaitForSeconds(0.1F);
            blasterRay.Stop();
        }

        

        /*
        Debug.Log("x: " + Input.mousePosition.x + " y: " + Input.mousePosition.y);
        //Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        GameObject player = GameObject.Find("Player").gameObject; 

        Ray ray = new Ray(player.GetComponent<Transform>().position, -player.GetComponent<Transform>().up);
        Debug.DrawRay(player.GetComponent<Transform>().position, -player.GetComponent<Transform>().up, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, distance))
        {
            hit.transform.gameObject.SetActive(false);
        }*/
    }
}
