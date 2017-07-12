using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setupPlayer : MonoBehaviour {
    private CursorLockMode hide;
    private CursorLockMode show;
    [SerializeField]
    private GameObject exitbtn;
    [SerializeField]
    private Transform[] spawnpoints1;
    private Transform[] usedvalues;
    [SerializeField]
    private GameObject[] cargo;
    private int randompos;
    private int randomcargo;
    private ArrayList newlist = new ArrayList();

    
    // Use this for initialization
    void Start () {
        exitbtn.SetActive(false);
        int i;
        for (i = 0; i < 5; i++)
        {
            randomcargo = Random.Range(0, cargo.Length - 1);
            randompos = Random.Range(0, spawnpoints1.Length );
            GameObject go = Instantiate(cargo[randomcargo], spawnpoints1[randompos].position, Quaternion.identity); 
          
        }
       
    }

	// Update is called once per frame
	void Update () {
        pressesc();
       
    }

    public void pressesc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
                if (exitbtn.activeInHierarchy == false)
                {
                 
                    exitbtn.SetActive(true);
                }
                else if (exitbtn.activeInHierarchy == true)
                {
         
                    exitbtn.SetActive(false);
                }
        }
    }

}
