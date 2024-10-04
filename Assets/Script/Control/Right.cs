using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Right : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerControl.dir = 1;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlayerControl.dir = 0;
    }
}
