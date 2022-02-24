using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dec
{
    public class ClueManager : MonoBehaviour
    {
        public static ClueManager m_Instance;
        private void Awake()
        {
            m_Instance = this;
        }
        public void AddClue(int id)
        {
            // 每条线索有一个唯一的id
            Manifest.m_Instance.m_Clues.Add(Manifest.m_Instance.m_ClueBase[id]);
            ItemInfo currentClue = Manifest.m_Instance.m_ClueBase[id];
            // 激活节点
            if (currentClue.linkNode)
            {
                ActivateNode(id);
            }
        }

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

        private void ActivateNode(int id)
        {
            
        }
    }
}
