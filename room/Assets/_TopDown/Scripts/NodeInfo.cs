using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dec
{
    [CreateAssetMenu(fileName = "NewNodeInfo", menuName = "Dec/NodeInfo", order = 1)]
    public class NodeInfo : ScriptableObject
    {
        public string title;
        public int nodeId;
        public Sprite icon;
        public List<NodeClue> coupleIdList; //  ����֮�����½ڵ�Ľڵ�
        public List<NodeInfo> linkableList; // ����������ߵĽڵ�

    }

[Serializable]
public class NodeClue
    {
        public int nodeId;
        public NodeInfo clueNodeInfo;
    }
}