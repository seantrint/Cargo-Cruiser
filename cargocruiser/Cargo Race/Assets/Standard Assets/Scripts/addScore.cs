using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class addScore : MonoBehaviour {
    [SerializeField]
    private Text nameTF;
    [SerializeField]
    private int Score;
    [SerializeField]
    private Text scoreField;
    [SerializeField]
    private GameObject uploadbtn;
    [SerializeField]
    private Text success;
    private bool ifuploaded = false;
    private string scoreholder;
    private string scoreURL = "cargorace.x10host.com/addscore.php";
	// Use this for initialization
	void Start () {
        success.enabled = false;	
	}
	
	// Update is called once per frame
	void Update () {
		if(ifuploaded == true)
        {
            uploadbtn.SetActive(false);
        }
	}
    public void insertScore()
    {
        WWWForm form = new WWWForm();
        form.AddField("namePost",nameTF.text);
        form.AddField("scorePost", scoreField.text);
        WWW www = new WWW(scoreURL, form);
        uploadbtn.SetActive(false);
        success.enabled = true;
        ifuploaded = true;
    }
}
