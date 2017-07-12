using UnityEngine;
using System.Collections;

public class throwableObject : MonoBehaviour {
    
    float timepassed = 0;
   
    // Use this for initialization
    void Start () {
	
	}
	public void Shoot(Vector3 force)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(force);

    }
	// Update is called once per frame
	void Update () {
        timepassed++;
        if (timepassed >= 300)
        {
            Destroy(gameObject);
        }
	}
}
