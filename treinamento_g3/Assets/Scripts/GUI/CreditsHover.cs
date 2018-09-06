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
    Image nameText; //name image
    public Sprite mouseOff;
    public Sprite mouseOn;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();

        //get name
        nameText = transform.GetChild(0).GetComponent<Image>();
        //hide name
        nameText.enabled = false;
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.sprite = mouseOn;
        nameText.enabled = true;
      
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = mouseOff;
        nameText.enabled = false;
       
    }
}
