using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dec {
    [CreateAssetMenu(fileName = "NewManifest", menuName = "Dec/Manifest", order = 2)]
    public class Manifest : ScriptableObject
    {
        public static Manifest m_Instance;
        public List<ItemInfo> m_ClueBase;
        public List<ItemInfo> m_Clues;
        public AdjacencyList<int> m_InitGraph;
        public List<NodeSphere> m_NodeSphereList;
        public List<NodeInfo> m_NodeInfoList;   // ��������ǰ�˳�����
        public List<NodeInfo> m_SpecialNodeList;    // ���ڲ�ѯ�ºϳɵĵ�

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
