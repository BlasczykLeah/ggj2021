using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject myImage;

    void Start()
    {
        myImage.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GetComponent<Button>().interactable) myImage.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        myImage.SetActive(false);
    }

    private void OnDisable()
    {
        myImage.SetActive(false);
    }
}
