using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {



        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 2000))
            {
                // it's better to find the center of the face like this:
                Vector3 position = hit.transform.position + hit.normal;

                // calculate the rotation to create the object aligned with the face normal:
                Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                // create the object at the face center, and perpendicular to it:
                GameObject Placement = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Placement.transform.position = position;
                Placement.transform.rotation = rotation;



                //Instantiate<( PrimitiveType.Cube as GameObject , position , rotation ) as GameObject;
            }
            else
            {
                Debug.Log("nothing");
            }
        }
    }
}
