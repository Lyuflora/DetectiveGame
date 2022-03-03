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
        public List<NodeClue> coupleIdList; //  能与之触发新节点的节点
        public List<NodeInfo> linkableList; // 允许玩家连线的节点

    }

[Serializable]
public class NodeClue
    {
        public int nodeId;
        public NodeInfo clueNodeInfo;
    }
}