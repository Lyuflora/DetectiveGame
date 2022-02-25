using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dec {
    [CreateAssetMenu(fileName = "NewManifest", menuName = "Dec/Manifest", order = 2)]
    public class Manifest : ScriptableObject
    {
        public static Manifest m_Instance;
        public List<Clue> m_AllClues;
        public List<Item> m_AllItems;

        [HideInInspector] public AdjacencyList<int> m_InitGraph;
        [HideInInspector] public List<NodeSphere> m_NodeSphereList;
        [HideInInspector] public List<NodeInfo> m_NodeInfoList;   // 理想情况是按顺序放置
        [HideInInspector] public List<NodeInfo> m_SpecialNodeList;    // 用于查询新合成的点

        private void Awake()
        {
            m_Instance = this;
            m_InitGraph = new AdjacencyList<int>();
            m_InitGraph.AddVertex(0);
        }
        
        public void Start()
        {
        }

    }
}
