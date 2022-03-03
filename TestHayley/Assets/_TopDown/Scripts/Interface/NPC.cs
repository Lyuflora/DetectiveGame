using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dec
{
    public class NPC : MonoBehaviour, IClickable
    {
        public float outlineWidth;

        public void OnHoverEnter()
        {
            Debug.Log("enter npc");
            GetComponentInChildren<Renderer>().material.SetFloat("_OutlineWidth", outlineWidth);
        }

        public void OnHoverExit()
        {
            Debug.Log("exit npc");
            GetComponentInChildren<Renderer>().material.SetFloat("_OutlineWidth", 0.0f);

        }

        public void OnLeftClick()
        {
            Debug.Log("LClick npc");
            SC_TopDownController.m_Instance.AssignNewDesti();
        }
        public void OnLeftClickUp()
        {
            SC_TopDownController.m_Instance.AssignNewDesti();
        }
        public void OnRightClickDown()
        {
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}