using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dec
{
    public class Item : MonoBehaviour, IClickable
    {
        public float outlineWidth = 0.1f;

        [Header("Clues")]
        public List<Clue> clueList;

        public void OnHoverEnter()
        {
            Debug.Log("enter item");
            if (GetComponent<Renderer>().material)
            {
                //Debug.Log(GetComponent<Renderer>().material.GetFloat("_OutlineWidth"));
                GetComponent<Renderer>().material.SetFloat("_OutlineWidth", outlineWidth);
            }
                
        }

        public void OnHoverExit()
        {
            Debug.Log("exit item");
            //Debug.Log(GetComponent<Renderer>().material.GetFloat("_OutlineWidth"));
            if(GetComponent<Renderer>().material)
                GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 0.0f);
        }

        public void OnLeftClick()
        {
            Debug.Log("Left Click Item");
            //SC_TopDownController.m_Instance.AssignNewDesti();

        }
        public void OnLeftClickUp()
        {
            //SC_TopDownController.m_Instance.AssignNewDesti();
        }

        public void OnRightClickDown()
        {
            Debug.Log("Right Click Item");
            int id = Random.Range(0, 3);
            UnlockClue(id);


        }

        // 解锁物体上第x个线索
        public void UnlockClue(int index)
        {
            
            if (index < clueList.Count && clueList[index] != null)
            {
                clueList[index].available = true;
                ClueManager.m_Instance.AddClue(clueList[index].clueId);
                Debug.Log("Get clue "+ index);
            }
                
        }
    }
}