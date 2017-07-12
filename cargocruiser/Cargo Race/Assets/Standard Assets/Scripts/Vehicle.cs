using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Vehicle : MonoBehaviour {
    [SerializeField]
    private float maxTorque = 50f;
    [SerializeField]
    private float accelerate;
    [SerializeField]
    private float steer;
    [SerializeField]
    private Transform centerOfMass;
    [SerializeField]
    private WheelCollider[] wheelCols = new WheelCollider[4];
    [SerializeField]
    private Transform[] tireMeshes = new Transform[4];
    private Rigidbody m_rigidBody;
    private float speed;
    [SerializeField]
    private Text text;
    private bool stopCar;
    private float maxbrakeTorque = 6000f;
    private bool isBraking = false;
    [SerializeField]
    private Animator monkanim;
    [SerializeField]
    private Animator vehicleanim;
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
		/*the centre of mass acts as a point of origin or an anchor, provides balance to the object and stops it tipping over in sharp turns */
        m_rigidBody.centerOfMass = centerOfMass.localPosition;
        stopCar = false;
    
    }
    void animControl()
    {
        if (Input.GetKeyDown("a"))
        {
            monkanim.Play("Left");
            vehicleanim.Play("Left");
        }
        if (Input.GetKeyDown("d"))
        {
            monkanim.Play("Right");
            vehicleanim.Play("Right");
        }
    }
    void Update()
    {
        
        UpdateMeshesPositions();
        if (stopCar == true)
        {
            m_rigidBody.velocity = Vector3.zero; 
        }
        if (Input.GetKeyDown("s"))
        {
            isBraking = true;
        }
        
        else
        {
            isBraking = false;
        }
        animControl();
    }
    void Braking()
    {
     
        if (isBraking)
        {
            wheelCols[2].brakeTorque = maxbrakeTorque;
            wheelCols[3].brakeTorque = maxbrakeTorque;
        }
        else
        {
            wheelCols[2].brakeTorque = 0;
            wheelCols[3].brakeTorque = 0;
        }
    }
    void FixedUpdate()
    {
        Driving();
        speed = m_rigidBody.velocity.magnitude*1.8f;
        Braking();
    }
    void Driving()
    {
        /*these input axes are by default assigned to  w,s (vertical) and a,d(horizontal)
         *the variables steer and accelerate control the car's movement by looping through
         *the wheelcollider list and multiplying it by the desired speed
		 */
        if (stopCar == false)
        {
            steer = Input.GetAxis("Horizontal");
            accelerate = Input.GetAxis("Vertical");
        }
        /*turning on the front 2 wheels
         * finalAngle is how sharp of a turn we can make*/
        float finalAngle = steer * 30f;
        wheelCols[0].steerAngle = finalAngle;
        wheelCols[1].steerAngle = finalAngle;
        /*the wheel colliders are what we use to drive the vehicle
         *they provide rotation speed and angles to our wheel meshes
         *the meshes will try to fill the space of the wheel collider
		 */
        if (!isBraking)
        {
            for (int i = 0; i < 4; i++)
            {
                wheelCols[i].motorTorque = accelerate * maxTorque;

            }
        }
    }

    void UpdateMeshesPositions()
    {
        for (int i = 0; i < 4; i++)
        {
            Quaternion quat;
            Vector3 pos;
            wheelCols[i].GetWorldPose(out pos, out quat);
            /*get the position of the wheel colliders
             *loop of 4, equal to size of cols and mesh lists
             *quaternions are for rotation, vectors are for positions
             *set the meshes rotation and positions to equal that of the wheel colliders
             */
            tireMeshes[i].position = pos;
            tireMeshes[i].rotation = quat;

        }
    }
    void OnGUI()
    {
        text.text = speed.ToString("N3")+"mph";
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "end")
        {
            string test = "IT WORKS";
            Debug.Log(test);
            stopCar = true;
          
        }
    }


   
}
