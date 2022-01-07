using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    private float x, y;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x = target.transform.position.x;
        y = target.transform.position.y;
        transform.position = new Vector3(x, y, 0) + offset;
    }
}
