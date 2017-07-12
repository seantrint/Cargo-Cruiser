using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carAI : MonoBehaviour {

    [SerializeField]
    private Transform path;
    private List<Transform> nodes;
    private int currentnode = 0;
    [SerializeField]
    private float steer = 45f;
    [SerializeField]
    private WheelCollider[] wheelCols = new WheelCollider[4];
    [SerializeField]
    private Transform[] tireMeshes = new Transform[4];
    [SerializeField]
    private float maxengTorque = 350f;
    private float maxbrakeTorque = 600f;
    private float currentSpeed;
    [SerializeField]
    private float maxSpeed = 400f;
    [SerializeField]
    private Transform centerOfMass;
    [SerializeField]
    private bool isBraking = false;
    private bool avoiding = false;
    private Rigidbody m_rigidBody;
    [SerializeField]
    private float sensorLength = 5f;
    private Vector3 frontSensorPosition = new Vector3(0f, 0.2f, 0.5f);
    [SerializeField]
    private float frontSideSensorPosition = 0.2f;
    [SerializeField]
    private float frontSensorAngle = 30f;
    [SerializeField]
    private float frontsensPosition = 0.5f;
    private float targetSteerAng = 0;
    private float turnSpeed = 55;
    [SerializeField]
    private float health = 100;
    [SerializeField]
    private carAI thiscar;

    // Use this for initialization

    void Start () {
        Transform[] pathtransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        m_rigidBody = GetComponent<Rigidbody>();
        m_rigidBody.centerOfMass = centerOfMass.localPosition;
        for (int i = 0; i < pathtransforms.Length; i++)
        {
            if (pathtransforms[i] != path.transform)
            {
                nodes.Add(pathtransforms[i]);
            }
        }
    }

	void FixedUpdate () {
        applySteer();
        Drive();
        checkWaypointDist();
        Braking();
        sensorCheck();
        LerpToSteerAngle();
    }
    void Update()
    {
        UpdateMeshesPositions();
    }
    void sensorCheck()
    {
        RaycastHit hit;
        Vector3 sensorStartPos = transform.position;
        sensorStartPos += transform.forward * frontSensorPosition.z;
        sensorStartPos += transform.up * frontSensorPosition.y;
        float avoidMultiplier = 0f;
        avoiding = false;

        //front right sensor
        sensorStartPos += transform.right * frontSideSensorPosition;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                avoidMultiplier -= 1f;
            }
        }

        //front right angle sensor
        else if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                avoidMultiplier -= 0.5f;
            }
        }

        //front left sensor
        sensorStartPos -= transform.right * frontSideSensorPosition*2;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                avoidMultiplier += 1f;
            }
        }

        //front left angle sensor
        else if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                avoidMultiplier += 0.5f;
            }
        }
        if (avoidMultiplier == 0)
        {
            if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
            {
                if (!hit.collider.CompareTag("Terrain"))
                {
                    Debug.DrawLine(sensorStartPos, hit.point);
                    avoiding = true;
                    if (hit.normal.x < 0)
                    {
                        avoidMultiplier = -1;
                    }
                    else
                    {
                        avoidMultiplier = 1;
                    }
                }

            }
        }
        //front center sensor
       
        if (avoiding)
        {
            targetSteerAng = steer * avoidMultiplier;
        }
    }
    void UpdateMeshesPositions()
    {
        for (int i = 0; i < 4; i++)
        {
            Quaternion quat;
            Vector3 pos;
            wheelCols[i].GetWorldPose(out pos, out quat);

            tireMeshes[i].position = pos;
            tireMeshes[i].rotation = quat;

        }
    }
    void applySteer()
    {
        if (avoiding) return;
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentnode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * steer;
        targetSteerAng = newSteer;
    }


    void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelCols[0].radius * wheelCols[1].radius * 60 / 1000;
        if(currentSpeed < maxSpeed && !isBraking)
        {
            wheelCols[0].motorTorque = maxengTorque;
            wheelCols[1].motorTorque = maxengTorque;
        }
        else
        {
            wheelCols[0].motorTorque = 0;
            wheelCols[1].motorTorque = 0;
        }
       
    }
    void checkWaypointDist()
    {
        if (Vector3.Distance(transform.position, nodes[currentnode].position) < 2.5)
        {
            if(currentnode == nodes.Count - 1)
            {
                currentnode = 0;
            }
            else
            {
                currentnode++;
            }
        }
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
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "slowDown")
        {
            isBraking = true;
        }
        if(other.gameObject.tag == "speedUp")
        {
            isBraking = false;
        }
        if (other.gameObject.tag == "gorilla")
        {
            if (health > 0)
            {
                health -= 20;
            }
        }
    }
    void LerpToSteerAngle()
    {
        wheelCols[0].steerAngle = Mathf.Lerp(wheelCols[0].steerAngle, targetSteerAng, Time.deltaTime * turnSpeed);
        wheelCols[1].steerAngle = Mathf.Lerp(wheelCols[1].steerAngle, targetSteerAng, Time.deltaTime * turnSpeed);
    }
    void death()
    {

        if (health == 0){
            thiscar.enabled = false;
        }
     }
            
        
    
}
