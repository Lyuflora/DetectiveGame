using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dec
{
    public class ChoiceSlot : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("OnDrop");
            if(eventData.pointerDrag != null)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                if (eventData.pointerDrag.GetComponent<InventorySlot>() != null)
                {
                    // add the choice to the player's current choice list
                    Item item = eventData.pointerDrag.GetComponent<InventorySlot>().item;
                    QuizManager.m_Instance.AddChoice(item);
                }   

            }
        }
    }
}