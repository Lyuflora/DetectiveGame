using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dec
{
    public class SelectionManager : MonoBehaviour
    {
        public static SelectionManager m_Instance;
        public Camera playerCamera;
        public string selectableTag = "Selectable";
        public Material highlightMaterial;
        public Material defaultMaterial;
        [SerializeField] private Ground ground;

        private IClickable _previousClickable;  // from last frame
        private void Awake()
        {
            // Only one instance of SaveMenu may exist
            if (m_Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            m_Instance = this;
        }
        // select, compare with the last select, change mat, save as last select, change defaul
        private void Update()
        {
            // On enter scope
            {
                RaycastHit hit;
                Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
                if (IsPointerOverUIObject())
                {
                    //Check if is Iclickable on UI
                    IClickable clickable = null;
                    foreach (RaycastResult result in UIObjectsUnderPointer())
                    {
                        if (result.gameObject.GetComponent<IClickable>() != null)
                        {
                            clickable = result.gameObject.GetComponent<IClickable>();
                            break;
                        }
                    }
                    if (clickable != _previousClickable)
                    {
                        if (clickable != null) clickable.OnHoverEnter();
                        if (_previousClickable != null) _previousClickable.OnHoverExit();
                        _previousClickable = clickable;
                    }
                }
                else if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    // Check if is IClickable
                    IClickable clickable = hit.transform.GetComponent<IClickable>();
                    if (clickable != _previousClickable)
                    {
                        if (clickable != null)
                        {
                            clickable.OnHoverEnter();
                        }
                        if (_previousClickable != null) _previousClickable.OnHoverExit();
                        _previousClickable = clickable;
                    }
                    else if (Input.GetButton("Fire1"))
                    {
                        if (clickable != null)
                            clickable.OnLeftClick();
                    }else if (Input.GetButtonDown("Fire2"))
                    {
                        if (clickable != null)
                            clickable.OnRightClickDown();
                    }

                    if (hit.transform.CompareTag("Ground"))
                    {
                        Debug.Log("on Ground");
                        if (Input.GetButton("Fire1"))
                        {
                            ground = hit.transform.GetComponent<Ground>();
                            ground.OnLeftClick();
                        }
                        if (Input.GetButtonUp("Fire1"))
                        {
                            ground.OnLeftClickUp();
                        }
                        // SC_TopDownController.m_Instance.UpdateTargetTrans(); // character movement
                    }
                }


            }

        }

        public void HitGround()
        {

        }

        public bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }

        private List<RaycastResult> UIObjectsUnderPointer()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results;
        }
    }
}