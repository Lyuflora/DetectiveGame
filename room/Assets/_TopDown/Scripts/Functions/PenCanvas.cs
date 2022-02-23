using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PenCanvas : MonoBehaviour, IPointerClickHandler
{
    public Action OnPenCanvasLeftClickEvent;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerId == -1) // -1, -2 and -3 = left, right and center mouse buttons
        {
            //Left Click
            Debug.Log("Canvas Click");
            OnPenCanvasLeftClickEvent?.Invoke();    // null conditional

        }

    }
}
