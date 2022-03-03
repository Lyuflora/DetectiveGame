using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Dec
{

    public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
    {
        private Canvas canvas;
        [Tooltip("The parent of selected choices")]
        private Transform parent;

        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            if (GameObject.FindWithTag("ChoiceParent") != null)
            {
                parent = GameObject.FindWithTag("ChoiceParent").transform;
            }
            else
            {
                Debug.LogError("Empty Parent for Choice Drag.");
            }

            if (GameObject.FindWithTag("QuizCanvas") != null)
            {
                canvas = GameObject.FindWithTag("QuizCanvas").GetComponent<Canvas>();
            }
            else
            {
                Debug.LogError("Empty Canvas for Choice Drag.");
            }                  
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("On Begin Drag");
            canvasGroup.alpha = .6f;
            canvasGroup.blocksRaycasts = false;
            gameObject.transform.SetParent(parent);
        }

        public void DragAnswerChoice()
        {
            Debug.Log("Pick an answer");
            rectTransform.anchoredPosition = Input.mousePosition;

        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("Dragging");
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("On End Drag");
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            QuizManager.m_Instance.EndDragChoice();
            QuizManager.m_Instance.UpdateChoice();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Pointer Down");
        }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("Dropping");


        }
    }
}