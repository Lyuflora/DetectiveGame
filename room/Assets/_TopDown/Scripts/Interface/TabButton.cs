using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Dec
{
    [RequireComponent(typeof(Image))]
    public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        public TabGroup tabGroup;
        public Image background;

        public UnityEvent onTabSelected;
        public UnityEvent onTabDeselected;

        public void OnPointerClick(PointerEventData eventData)
        {
            tabGroup.OnTabSelected(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            tabGroup.OnTabEnter(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tabGroup.OnTabExit(this);
        }

        private void Start()
        {
            background = GetComponent<Image>();
            tabGroup.Subscribe(this);
        }

        // call back
        public void Select()
        {
            if (onTabSelected != null)
            {
                onTabSelected.Invoke();
            }
        }

        public void DeSelect()
        {
            if (onTabSelected != null)
            {
                onTabDeselected.Invoke();
            }
        }
    }
}