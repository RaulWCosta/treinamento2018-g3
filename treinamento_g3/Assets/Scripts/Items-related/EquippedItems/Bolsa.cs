using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bolsa : MonoBehaviour {
    
    Image[] icons;

    public void DisableEnable ()
    {
        icons = transform.parent.GetComponentsInChildren<Image>();
        for (int i = 0; i < icons.Length; i++)
            icons[i].enabled = !icons[i].enabled;
        
        return;
    }
}
