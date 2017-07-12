using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlaymodeTests;
using UnityEngine.UI;

public class textactivate : MonoBehaviour {
    [SerializeField]
    private Text testtext;
	// Use this for initialization
	void Start () {
        testtext.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    [Test]
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "canwepause")
        {
            testtext.enabled = true;
        }
    }
}
