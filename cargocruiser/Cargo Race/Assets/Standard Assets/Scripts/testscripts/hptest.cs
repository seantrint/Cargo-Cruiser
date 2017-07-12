using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hptest : MonoBehaviour {
    
    public float health = 100;
    public Collider col;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "gorilla")
        {
            health -= 30;
            
        }
        if(health <= 0)
        {
            col.enabled = false;
        }
    }
}
