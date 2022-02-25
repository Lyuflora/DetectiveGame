using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dec
{
    public class ClueManager : MonoBehaviour
    {
        // Only for Debug
        [Header("Debug")]
        public List<Clue> m_ViewCurrentClues;
        public List<Item> m_ViewCurrentItems;


        private void Start()
        {
            m_ViewCurrentClues = new List<Clue>();
            m_ViewCurrentItems = new List<Item>();
            InitClues();
        }

        public void InitClues()
        {
            // set all the clues not found if needed
            for(int i=0;i<TestAPP.m_Instance.m_Manifest.m_AllClues.Count; i++)
            {
                TestAPP.m_Instance.m_Manifest.m_AllClues[i].isFound = false;
            }
        }

        // pass the clue from ClueTrigger to ClueManager-CLueList
        public void AddClue(Clue r_Clue)
        {
            // Find item in the list
            //m_ItemList.Find((x) => x == r_Clue.m_Owner);
            int itemIndex = TestAPP.m_Instance.m_Manifest.m_AllItems.IndexOf(r_Clue.m_Owner);
            Item targetItem = TestAPP.m_Instance.m_Manifest.m_AllItems[itemIndex];


            // 直接加入附属物体的线索List
            if (r_Clue.isFound == true)
            {
                return;
            }

            r_Clue.isFound = true;
            targetItem.clueList.Add(r_Clue);
            targetItem.isFound = true;
            Debug.Log("Add clue");

            // For previewing
            PreviewClue(r_Clue, targetItem);
        }

        public void PreviewClue(Clue r_Clue, Item r_Item)
        {
            Debug.Log("Add clue" + r_Clue.name);
            
            if (!m_ViewCurrentClues.Contains(r_Clue))
            {
                m_ViewCurrentClues.Add(r_Clue);
            }
            if (!m_ViewCurrentItems.Contains(r_Item))
            {
                m_ViewCurrentItems.Add(r_Item);                
            }
            return;
        }

        public void AddClue(int id)
        {
            // 每条线索有一个唯一的id
            m_ViewCurrentClues.Add(Manifest.m_Instance.m_AllClues[id]);
            Clue currentClue = Manifest.m_Instance.m_AllClues[id];

        }

        // obslete
        public bool AttemptLink(NodeInfo start, NodeInfo end)
        {
            for(int i = 0; i < start.linkableList.Count; i++)
            {
                if (start.linkableList[i].nodeId == end.nodeId)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
