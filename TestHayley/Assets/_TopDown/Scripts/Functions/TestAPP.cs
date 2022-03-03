using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dec
{
    public class TestAPP : MonoBehaviour
    {
        public static TestAPP m_Instance;

        public Manifest m_Manifest;
        public ClueManager m_ClueManager;

        [HideInInspector]
        public Action OnGraphChangeEvent;
        [HideInInspector] public GameObject m_NodeSpherePrefab;
        [HideInInspector] public List<NodeSphere> m_NodeSphereList;
        [HideInInspector] public AdjacencyList<int> m_Graph;

        private void Awake()
        {
            m_Instance = this;
        }
        // ----------------------------
        // Mind map Graph
        public void CheckGraphFromStart()
        {
            List<NodeInfo> list = m_Manifest.m_NodeInfoList;
            for (int i = 0; i < list.Count; i++)
            {
                for (int j=0;j< list[i].coupleIdList.Count; j++)
                {
                    if(TestAPP.m_Instance.m_Graph.TestEdge(i, list[i].coupleIdList[j].nodeId))
                    {
                        Debug.Log("Link and get New Node");
                    }
                }
                
            }
            
        }
        // b点是否在a点的cp数组中
        public void CheckGraphFromNewNode(int start, int end)
        {
            NodeInfo node = m_Manifest.m_NodeInfoList[start];
            if (node.coupleIdList.Count > 0)
            {
                for (int i = 0; i < node.coupleIdList.Count; i++)
                {
                    if (node.coupleIdList[i].nodeId == end)
                    {
                        Debug.Log("New Node");
                        m_Graph.AddVertex(node.coupleIdList[i].clueNodeInfo.nodeId);

                        // AddNodeSphereToGraph
                        PenTool.m_Instance.AddNodeSphereToScreen(start, end, node.coupleIdList[i].clueNodeInfo);
                    }
                }
            }
            
        }

        public void Start()
        {
            
            OnGraphChangeEvent += () => CheckGraphFromNewNode(PenTool.m_Instance.start_id,PenTool.m_Instance.end_id);
            OnGraphChangeEvent += () => CheckGraphFromNewNode(PenTool.m_Instance.end_id,PenTool.m_Instance.start_id);
            //Invoke(() => CheckGraph(start: PenTool.m_Instance.start_id, end: PenTool.m_Instance.end_id));
            //m_Graph = m_Manifest.m_InitGraph; // 初始化
            m_Graph = new AdjacencyList<int>(10);
            Debug.Log("Add original vertex");
            InitGraph();
            PrintGraph();
        }
        public void PrintGraph()
        {
            Debug.Log(m_Graph.ToString());

        }
        public void InitGraph()
        {

            for(int i = 0; i < m_Manifest.m_NodeInfoList.Count; i++)
            {
                AddNodeToGraph(i);
            }
        }
        public void AddNodeToGraph(int index)
        {
            m_Graph.AddVertex(index);
            int i = m_Manifest.m_NodeInfoList[index].nodeId;
            if (m_NodeSphereList[index].GetComponent<Billboard>())
            {
                m_NodeSphereList[index].GetComponent<Billboard>().SetIdText(index);
            }
                
        }

    }
}