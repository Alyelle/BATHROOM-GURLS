using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.VFX;

public class ButtonLightUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler   
{
    //public vfx HoverPanel;
    //swifffttt how do you make it so that it becomes bright
    public void OnPointerEnter (PointerEventData eventData)
    {
     //   HoverPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      //  HoverPanel.SetActive(false);
    }
}
