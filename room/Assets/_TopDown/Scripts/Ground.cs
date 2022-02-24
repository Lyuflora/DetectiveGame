using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dec
{
    public class Ground : MonoBehaviour, IClickable
    {
        public void OnHoverEnter()
        {
        }

        public void OnHoverExit()
        {
        }

        public void OnLeftClick()
        {
            Debug.Log("Left Click Ground");
            //SC_TopDownController.m_Instance.ActivateTarget();

        }

        public void OnLeftClickUp()
        {
            //SC_TopDownController.m_Instance.DeactivateTarget();
        }

        public void OnRightClickDown()
        {
        }
    }
}