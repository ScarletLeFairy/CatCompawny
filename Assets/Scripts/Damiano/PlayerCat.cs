using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class PlayerCat : MonoBehaviour {

    public Camera cam;

    [Header("Spring")]
    public float distance = 2;
    public Vector3 spring = Vector3.zero;

    public float movspeed = 10;
    public float jump = 40;
    public float gravity = 9.81f / 2f;

    Quaternion yaw = Quaternion.identity;
    Quaternion pitch = Quaternion.identity;

    Quaternion model = Quaternion.identity;

    float maxangle = 65;

    float mouse_sensitivity = 3f;

    public Quaternion view
    {
        get
        {
            return yaw * pitch;
        }
    }

    CharacterController _body;
    public CharacterController body
    {
        get
        {
            if (_body == null)
                _body = GetComponent<CharacterController>();
            if (_body == null)
                _body = gameObject.AddComponent<CharacterController>();
            return _body;
        }
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        RotateView();

        MoveCharacter();

        if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            Attack();
        }
    }


    void Attack()
    {
        
    }

    Vector3 velocity = Vector3.zero;
    void MoveCharacter()
    {
        Vector3 movedir = new Vector3(Input.GetAxis("Move Horizontal"), 0, Input.GetAxis("Move Vertical"));
        

        //ROTATE MESH
        if (movedir.magnitude > 0)
        {
            Quaternion q = Quaternion.LookRotation(yaw * movedir);
            
            transform.rotation = Quaternion.Lerp(transform.rotation, q, Time.deltaTime * 3f * movedir.magnitude);

            Debug.DrawLine(transform.position, transform.position + q * Vector3.forward, Color.red);
            
        }

        //GRAVITY
        velocity.y = body.isGrounded ? -0.1f : velocity.y - gravity * Time.deltaTime;

        if (body.isGrounded)
        {
            velocity.x = movedir.x;
            velocity.z = movedir.z;

            velocity *= movspeed * Time.deltaTime;


            if (Input.GetKeyDown(KeyCode.JoystickButton0)) {
                velocity.y = jump * Time.deltaTime;
            }
            
        }

        

        body.Move(yaw * velocity);
    }

    void RotateView()
    {
        float vertical = Input.GetAxis("View Vertical") * mouse_sensitivity;
        float horizontal = Input.GetAxis("View Horizontal") * mouse_sensitivity;

        yaw = Quaternion.AngleAxis(horizontal, Vector3.up) * yaw; //HORIZONTAL
        pitch = Quaternion.AngleAxis(vertical, -Vector3.right) * pitch; //VERTICAL

        //LOCK ANGLE
        float angle = Quaternion.Angle(Quaternion.identity, pitch);
        if (angle > maxangle)
        {
            float value = (angle - maxangle);
            Quaternion max = Quaternion.AngleAxis(value, Vector3.right);
            Quaternion min = Quaternion.AngleAxis(-value, Vector3.right);

            pitch = Mathf.Abs(Quaternion.Angle(max, pitch) - 45) > Mathf.Abs(Quaternion.Angle(min, pitch) - 45) ? pitch * max : pitch * min;
        }
    }


    
    void LateUpdate()
    {
        //Vector3 target = transform.position + spring + view * -Vector3.forward * distance;
        //cam.transform.position = Vector3.SmoothDamp(new Vector3(target.x, cam.transform.position.y, target.z), target, ref velo, 0.2f);


        cam.transform.position = transform.position + spring + view * -Vector3.forward * distance;
        cam.transform.rotation = view;
    }

}
