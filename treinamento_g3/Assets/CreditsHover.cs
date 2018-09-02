using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Display second sprite and text when mouse hovers over image. 
/// Used for credits menu.
/// </summary>
public class CreditsHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    Image image; //main image
    Image name; //name image
    public Sprite mouseOff;
    public Sprite mouseOn;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();

        //get name
        name = transform.GetChild(0).GetComponent<Image>();
        //hide name
        name.enabled = false;
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.sprite = mouseOn;
        name.enabled = true;
      
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = mouseOff;
        name.enabled = false;
       
    }
}
