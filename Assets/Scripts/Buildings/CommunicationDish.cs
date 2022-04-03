using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunicationDish : MonoBehaviour
{

    public Transform castFrom;
    public float range;

    private void detectBody(RaycastHit hit)
    {
        if(hit.transform.tag == "Asteroid")
        {
            Transform prefab = hit.transform.parent;
            hit.transform.gameObject.SetActive(false);

            prefab.Find("Model").gameObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {

        Ray ray = new Ray(castFrom.position, transform.up);
        if(Physics.Raycast(ray, out RaycastHit hit, range))
        {
            detectBody(hit);
        }
    }
}
