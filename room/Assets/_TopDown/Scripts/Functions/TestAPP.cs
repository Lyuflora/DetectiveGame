using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dec
{
    public class TestAPP : MonoBehaviour
    {
        public static TestAPP m_Instance;
        public Action OnGraphChangeEvent;
        
        public GameObject m_NodeSpherePrefab;
        public Transform m_NodeParent;
        public Manifest m_Manifest;
        public List<NodeSphere> m_NodeSphereList;
        public AdjacencyList<int> m_Graph;

        private void Awake()
        {
            m_Instance = this;
        }

        public void CheckGraphFromStart()
        {
            List<NodeInfo> list = m_Manifest.m_NodeInfoList;
            for (int i = 0; i < list.Count; i++)
            {
                for (int j=0;j< list[i].coupleIdList.Count; j++)
                {
                    if(TestAPP.m_Instance.m_Graph.TestEdge(i, list[i].coupleIdList[j].nodeId))
                    {
                        Debug.Log("New Node");
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
                        NodeSphere startSphere = m_NodeSphereList[start];
                        NodeSphere endSphere = m_NodeSphereList[end];
                        Vector3 newNodePos = (startSphere.transform.position + endSphere.transform.position) / 2;
                        NodeSphere dot = Instantiate(m_NodeSpherePrefab, newNodePos, Quaternion.identity, m_NodeParent).GetComponent<NodeSphere>();
                        dot.m_nodeInfo = node.coupleIdList[i].clueNodeInfo;
                        dot.GetComponent<Billboard>().SetIdText(dot.m_nodeInfo.nodeId);
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