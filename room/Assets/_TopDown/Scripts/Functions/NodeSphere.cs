using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dec
{
    public class NodeSphere : MonoBehaviour
    {
        public Action<NodeSphere> OnNodeSphereLeftClickEvent;
        public int m_tempId;
        public NodeInfo m_nodeInfo;

        public void OnMouseDown()
        {
            Debug.Log("OnmouseDown");
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

            OnNodeSphereLeftClickEvent?.Invoke(this);

        }

        private void Start()
        {

        }

    }
}
