using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dec
{
    public class NodeLine : MonoBehaviour, IClickable
    {
        //  µ ©ªÊ÷∆
        [SerializeField]
        private LineRenderer lr;
        private List<Transform> points= new List<Transform>();

        private void Awake()
        {
            lr = GetComponent<LineRenderer>();
            lr.positionCount = 0;
            //points = new List<Transform>();
        }

        public void SetUpLine(List<Transform> points)
        {
            lr.positionCount = points.Count;
            this.points = points;
        }
        public void AddPoint(Transform point)
        {
            lr.positionCount++;
            Debug.Log(points);
            points.Add(point);
        }

        private void LateUpdate()
        {
            if (points.Count < 2) return;

            for (int i = 0; i < points.Count; i++)
            {
                lr.SetPosition(i, points[i].position + new Vector3(0.0f, 0.0f, 0.5f));  // 
            }
        }

        public void OnLeftClick()
        {
                     
        }

        public void OnLeftClickUp()
        {
            
        }

        public void OnRightClickDown()
        {
            
        }

        public void OnHoverEnter()
        {
            Debug.Log("Show button");
        }

        public void OnHoverExit()
        {
            
        }
    }
}