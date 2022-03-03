using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.1f;
    public bool isRight = false;
    public float smooth = 1f;
    private Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)||Input.GetKey(KeyCode.RightArrow))
        {
            if (!isRight)
            {
                //targetRotation = Quaternion.LookRotation(-transform.forward, Vector3.up);
                //transform.rotation = targetRotation;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            isRight = true;
            transform.position += new Vector3(-speed*Time.deltaTime, 0, 0);
            
        }

        if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (isRight)
            {
                //targetRotation = Quaternion.LookRotation(-transform.forward, Vector3.up);
                //transform.rotation = targetRotation;
                transform.localScale = new Vector3(1, 1, 1);
            }
            isRight = false;
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
           
        }
    }
}
