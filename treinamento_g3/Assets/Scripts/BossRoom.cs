using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour {

    public GameObject Boss;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(Boss != null)
                Boss.GetComponent<BossController>().PlayerInZone = true;
        }
    }
}
