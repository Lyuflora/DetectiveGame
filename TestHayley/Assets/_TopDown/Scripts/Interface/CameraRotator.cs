using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Dec
{


public class CameraRotator : MonoBehaviour
{

       public float speed;
       public float rotateTime = 2f;
        private void Start()
        {
            speed = 0;
        }

        void Update()
    {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                LeftRotateSpeed(90f);
                StartCoroutine(CameraRotate(speed, rotateTime));
                
            }
                
            else if (Input.GetKeyDown(KeyCode.E))
            {
                RightRotateSpeed(90f);
                StartCoroutine(CameraRotate(speed, rotateTime));
            }

            transform.Rotate(0, speed * Time.deltaTime, 0);

        }

        public void LeftRotateSpeed(float totalDegree)
        {
            speed = totalDegree / rotateTime;
            
        }

        public void RightRotateSpeed(float totalDegree)
        {
            speed = -totalDegree / rotateTime;
            
        }

        IEnumerator CameraRotate(float r_speed, float r_time)
        {
            Debug.Log("Start Cou");
            yield return new WaitForSeconds(r_time);
            speed = 0;
            Debug.Log("Speed 0");
        }
        internal static Bounds GetBound(GameObject go)
        {
            Bounds b = new Bounds(go.transform.position, Vector3.zero);
            var rList = go.GetComponentsInChildren(typeof(Renderer));
            foreach (Renderer r in rList)
            {
                b.Encapsulate(r.bounds);
            }
            return b;
        }
        internal static void ZoomFit(Camera c, GameObject go, bool ViewFromRandomDirecion = false)
        {
            Bounds b = GetBound(go);
            Vector3 max = b.size;
            float radius = Mathf.Max(max.x, Mathf.Max(max.y, max.z));
            float dist = radius / (Mathf.Sin(c.fieldOfView * Mathf.Deg2Rad / 2f));
            Debug.Log("Radius = " + radius + " dist = " + dist);

            Vector3 view_direction = ViewFromRandomDirecion ? UnityEngine.Random.onUnitSphere : c.transform.InverseTransformDirection(Vector3.forward);

            Vector3 pos = view_direction * dist + b.center;
            c.transform.position = pos;
            c.transform.LookAt(b.center);
        }
    }

}