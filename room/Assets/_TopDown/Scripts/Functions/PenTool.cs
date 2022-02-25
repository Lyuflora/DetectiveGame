using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dec
{
public class PenTool : MonoBehaviour
{
    [Header("Pen Canvas")]
    [SerializeField] private PenCanvas penCanvas;

    [Header("Dots")]
    [SerializeField] Transform dotParent;
    [SerializeField] GameObject dotCanvas;
    [SerializeField] public GameObject dotPrefab;
    [SerializeField] List<NodeSphere> NodeList;
    [SerializeField] Color startHighlight;
    [SerializeField] Color endHighlight;
    [SerializeField] Color normalHighlight;
    [SerializeField] Color errorHighlight;

    [Header("Lines")]
    [SerializeField] Transform lineParent;
    [SerializeField] GameObject linePrefab;
    public NodeSphere start;
        public int start_id;
    public NodeSphere end;
        public int end_id;

    private NodeLine currentLine;
    public List<NodeLine> lineList;
    public Camera mainCam;
    public static PenTool m_Instance;
    public bool isMindMapOn = false;
    private bool isLinePermitted = true;
    public Transform newNodePos;

    private void Awake()
    {
        m_Instance = this;
    }

    public void SwitchMindMap()
    {
        dotCanvas.gameObject.SetActive(!isMindMapOn);
        lineParent.gameObject.SetActive(!isMindMapOn);
        penCanvas.gameObject.SetActive(!isMindMapOn);
        isMindMapOn = !isMindMapOn;
    }
    public void SwitchMindMap(bool state)
    {
        dotCanvas.gameObject.SetActive(state);
        lineParent.gameObject.SetActive(state);
        penCanvas.gameObject.SetActive(state);
    }
    public void EnableMindMap()
    {
            isMindMapOn = true;
            SwitchMindMap(isMindMapOn);
    }
    public void DisableMindMap()
    {
            isMindMapOn = false;
            SwitchMindMap(isMindMapOn);
        }

    private void Start()
    {
        //penCanvas.OnPenCanvasLeftClickEvent += AddDot;
        penCanvas.OnPenCanvasLeftClickEvent += TestDot;

        lineList = new List<NodeLine>();
           //EnableMindMap();
        DisableMindMap();
    }

    public void TestDot()
    {
            Debug.Log("!!!!!!!!Test Click Canvas");
    }

    public void AddDot()
    {
        if (currentLine == null)
        {
            currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, lineParent).GetComponent<NodeLine>();
        }
        DotController dot = Instantiate(dotPrefab, GetMousePosition(), Quaternion.identity, dotParent).GetComponent<DotController>();

        dot.OnDragEvent += MoveDot;
        Debug.Log("Added");
        currentLine.AddPoint(dot.transform);
    }
    public void DrawNewLine()
    {
        DrawLine(start, end);
        TestAPP.m_Instance.m_Graph.AddEdge(start_id, end_id);
        TestAPP.m_Instance.PrintGraph();
        TestAPP.m_Instance.OnGraphChangeEvent?.Invoke();
        ResetPenTool();
    }

    public void TestSpecialLink()
    {
        //TestAPP.m_Instance.m_Graph.TestEdge(0, 2);
    }


    public void MoveDot(DotController dot)
    {
        // no use
        Debug.Log("Moving");
        dot.transform.position = GetMousePosition();
    }
    public void SetStartPoint(NodeSphere node)
    {
        start = node;
        start_id = node.m_nodeInfo.nodeId;
        //if (start.GetComponent<MeshRenderer>().material)
        //{
        //    start.GetComponent<MeshRenderer>().material.color = startHighlight;
        //}    
    }

    public void SetEndPoint(NodeSphere node)
    {
        Debug.Log("This is End");
        end = node;
        end_id = node.m_nodeInfo.nodeId;
            // 3D
        //if (end.GetComponent<MeshRenderer>().material)
        //{
        //    end.GetComponent<MeshRenderer>().material.color = endHighlight;
        //}
        
        isLinePermitted =TestAPP.m_Instance.m_ClueManager.AttemptLink(start.m_nodeInfo, end.m_nodeInfo)|| TestAPP.m_Instance.m_ClueManager.AttemptLink(end.m_nodeInfo, start.m_nodeInfo);
        if (true)//isLinePermitted
            {

            //if (start.GetComponent<MeshRenderer>().material)
            //    start.GetComponent<MeshRenderer>().material.color = normalHighlight;
            //if (end.GetComponent<MeshRenderer>().material)
            //    end.GetComponent<MeshRenderer>().material.color = normalHighlight;
            DrawNewLine();
            }
            
        else
        {
            Debug.LogFormat("The two clues cannot be linked: {0}.{1}.", start_id, end_id);
            StartCoroutine(ErrorNode(end));
            if (start.GetComponent<MeshRenderer>().material)
                start.GetComponent<MeshRenderer>().material.color = normalHighlight;
            ResetPenTool();
        }

    }

    IEnumerator ErrorNode(NodeSphere node)
    {
        node.GetComponent<Renderer>().material.color = errorHighlight;
        yield return new WaitForSeconds(0.5f);
        node.GetComponent<Renderer>().material.color = normalHighlight;
    }


    public void DrawLine(NodeSphere r_Start, NodeSphere r_End)
    {
        if (currentLine == null)
        {
            currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, lineParent).GetComponent<NodeLine>();
        }

         Debug.Log("Draw from Start"+ r_Start+ " to End "+ r_End);

        currentLine.AddPoint(r_Start.transform);
        currentLine.AddPoint(r_End.transform);
        lineList.Add(currentLine);

    }
    public void ResetPenTool()
    {

        start = null;
        end = null;
        currentLine = null;
    }
    public void AddNodeSphereToScreen(int start, int end, NodeInfo nodeInfo)
    {
        NodeSphere startSphere = TestAPP.m_Instance.m_NodeSphereList[start];
        NodeSphere endSphere = TestAPP.m_Instance.m_NodeSphereList[end];
        Vector3 newNodePos = (startSphere.transform.position + endSphere.transform.position) / 2;
        NodeSphere dot = Instantiate(PenTool.m_Instance.dotPrefab, newNodePos, Quaternion.identity, dotParent).GetComponent<NodeSphere>();
        dot.m_nodeInfo = nodeInfo;
        if (dot.GetComponent<Billboard>())
        {
            dot.GetComponent<Billboard>().SetIdText(dot.m_nodeInfo.nodeId);
        }
    }

        public void AddNodeSphereToScreen(NodeInfo nodeInfo)
        {

            NodeSphere dot = Instantiate(PenTool.m_Instance.dotPrefab, newNodePos.position, Quaternion.identity, dotParent).GetComponent<NodeSphere>();
            dot.m_nodeInfo = nodeInfo;
            if (dot.GetComponent<Billboard>())
            {
                dot.GetComponent<Billboard>().SetIdText(dot.m_nodeInfo.nodeId);
            }
        }

        // calculate world position of mouse
        private Vector3 GetMousePosition()
    {
        Vector3 worldMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.3f);

        worldMousePosition.z = 10f;
        worldMousePosition = mainCam.ScreenToWorldPoint(worldMousePosition);
        //Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        return worldMousePosition;
    }
}
}