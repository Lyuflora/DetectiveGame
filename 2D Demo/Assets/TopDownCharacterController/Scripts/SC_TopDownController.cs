using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class SC_TopDownController : MonoBehaviour
{
    //Player Camera variables
    public enum CameraDirection { x, z }
    public CameraDirection cameraDirection = CameraDirection.x;
    public float cameraHeight = 20f;
    public float cameraDistance = 7f;
    public Camera playerCamera;
    public GameObject targetIndicatorPrefab;
    //Player Controller variables
    public float speed = 5.0f;
    public float gravity = 14.0f;
    public float maxVelocityChange = 10.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    //Private variables
    [SerializeField]bool grounded = false;
    Rigidbody r;
    public GameObject targetObject;
    //Mouse cursor Camera offset effect
    Vector2 playerPosOnScreen;
    Vector2 cursorPosition;
    Vector2 offsetVector;
    //Plane that represents imaginary floor that will be used to calculate Aim target position
    Plane surfacePlane = new Plane();

    void Awake()
    {
        r = GetComponent<Rigidbody>();
        r.freezeRotation = true;
        r.useGravity = false;

        //Instantiate aim target prefab
        if (targetIndicatorPrefab)
        {
            targetObject = Instantiate(targetIndicatorPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            targetObject.SetActive(false);
        }

        //Hide the cursor
        //Cursor.visible = false;
    }
    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            targetObject.SetActive(true);
            GetComponent<PlayerNavMesh>().SetNewTarget(targetObject.transform.position);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            targetObject.SetActive(false);
        }
    }
    void FixedUpdate()
    {
        //Setup camera offset
        Vector3 cameraOffset = Vector3.zero;
        if (cameraDirection == CameraDirection.x)
        {
            cameraOffset = new Vector3(cameraDistance, cameraHeight, 0);
        }
        else if (cameraDirection == CameraDirection.z)
        {
            cameraOffset = new Vector3(0, cameraHeight, cameraDistance);
        }

        if (grounded)
        {
            Vector3 targetVelocity = Vector3.zero;
            // Calculate how fast we should be moving
            if (cameraDirection == CameraDirection.x)
            {
                targetVelocity = new Vector3(Input.GetAxis("Vertical") * (cameraDistance >= 0 ? -1 : 1), 0, Input.GetAxis("Horizontal") * (cameraDistance >= 0 ? 1 : -1));
            }
            else if (cameraDirection == CameraDirection.z)
            {
                targetVelocity = new Vector3(Input.GetAxis("Horizontal") * (cameraDistance >= 0 ? -1 : 1), 0, Input.GetAxis("Vertical") * (cameraDistance >= 0 ? -1 : 1));
            }
            targetVelocity *= speed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = r.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            r.AddForce(velocityChange, ForceMode.VelocityChange);

            // Jump
            if (canJump && Input.GetButtonDown("Jump"))
            {
                r.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
                Debug.Log("press Jump");
                grounded = false;
            }

        }
        
        // We apply gravity manually for more tuning control
        r.AddForce(new Vector3(0, -gravity * r.mass, 0));

        

        //Mouse cursor offset effect
        playerPosOnScreen = playerCamera.WorldToViewportPoint(transform.position);
        cursorPosition = playerCamera.ScreenToViewportPoint(Input.mousePosition);
        offsetVector = cursorPosition - playerPosOnScreen;

        //Camera follow
        playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, transform.position + cameraOffset, Time.deltaTime * 7.4f);
        playerCamera.transform.LookAt(transform.position + new Vector3(-offsetVector.y * 2, 0, offsetVector.x * 2));

        //Aim target position and rotation
        targetObject.transform.position = GetAimTargetPos();
        targetObject.transform.LookAt(new Vector3(transform.position.x, targetObject.transform.position.y, transform.position.z));

        //Player rotation
        transform.LookAt(new Vector3(targetObject.transform.position.x, transform.position.y, targetObject.transform.position.z));
    }

    public Vector3 GetAimTargetPos()
    {
        //Update surface plane
        surfacePlane.SetNormalAndPosition(Vector3.up, transform.position);

        //Create a ray from the Mouse click position
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        //Initialise the enter variable
        float enter = 0.0f;

        if (surfacePlane.Raycast(ray, out enter))
        {
            //Get the point that is clicked
            Vector3 hitPoint = ray.GetPoint(enter);

            //Move your cube GameObject to the point where you clicked
            return hitPoint;
        }

        //No raycast hit, hide the aim target by moving it far away
        return new Vector3(-5000, -5000, -5000);
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        Debug.Log("enter");
        if (collisionInfo.gameObject.tag == "Ground")
        {
            Debug.Log("hit Ground");
            grounded = true;

        }
            
    }

    private void OnCollisionExit(Collision collisionInfo)
    {
        Debug.Log("exit");
        if (collisionInfo.gameObject.tag == "Ground")
        {
            Debug.Log("exit Ground");
            //grounded = false;

        }
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        Debug.Log(Mathf.Sqrt(2 * jumpHeight * gravity));
        return Mathf.Sqrt(2 * jumpHeight * gravity);
        
    }
}