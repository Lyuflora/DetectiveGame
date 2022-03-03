using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dec
{
    // Œ¥ µœ÷

    [RequireComponent(typeof(NodeLine))]
    public class LineCollision : MonoBehaviour
    {
        NodeLine nl;
        // The points to draw a collision shape between points

        private void Start()
        {
            nl = GetComponent<NodeLine>();
        }


    }

}