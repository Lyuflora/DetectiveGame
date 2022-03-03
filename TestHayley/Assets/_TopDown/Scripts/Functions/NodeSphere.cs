using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dec
{
    public class NodeSphere : MonoBehaviour, IPointerClickHandler
    {
        public Action OnNodeSphereLeftClickEvent;
        public NodeInfo m_nodeInfo;
        private void Awake()
        {
            OnNodeSphereLeftClickEvent += AssignNode;
        }

        public void AssignNode()
        {
            Debug.Log("Assign Node");
            if (PenTool.m_Instance.start == null)
            {
                PenTool.m_Instance.SetStartPoint(this);
            }
            else if (PenTool.m_Instance.end == null)
            {
                PenTool.m_Instance.SetEndPoint(this);
            }
            else
            {
                Debug.Log("Point unsolved");
            }
        }

        public void OnMouseDown()
        {
            //Debug.Log("OnmouseDown");
            //if (PenTool.m_Instance.start == null)
            //{
            //    PenTool.m_Instance.SetStartPoint(this);
            //}
            //else if (PenTool.m_Instance.end == null)
            //{
            //    PenTool.m_Instance.SetEndPoint(this);
            //}
            //else
            //{
            //    Debug.Log("Point unsolved");
            //}

            //OnNodeSphereLeftClickEvent?.Invoke();

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.pointerId == -1) // -1, -2 and -3 = left, right and center mouse buttons
            {
                //Left Click
                Debug.Log("Node Click");

                OnNodeSphereLeftClickEvent.Invoke();
            }
        }
    }
}
