using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionMarkController : MonoBehaviour {

    public GameObject pickupPrefab;
    public Material usedMat;

    void HitZone()
    {
        Instantiate(pickupPrefab, transform.position + Vector3.up, transform.rotation);
        transform.parent.GetComponent<Renderer>().material = usedMat;
        GetComponent<BoxCollider>().enabled = false;
    }
    void OnTriggerEnter(Collider other)
    {
      if(other.tag=="Player")
        {
            HitZone();
        }
    }
}
