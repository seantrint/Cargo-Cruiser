using UnityEngine;
using System.Collections;

public class eraChangeCam : MonoBehaviour {
    public Camera mainCamera;
    public Camera driversCamera;
    public Camera backCamera;

    void Start()
    {
        mainCamera.enabled = true;
        driversCamera.enabled = false;
        backCamera.enabled = false;
    }

    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {

            mainCamera.enabled = true;
            driversCamera.enabled = false;
            backCamera.enabled = false;


        }

        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {

            mainCamera.enabled = false;
            driversCamera.enabled = true;
            backCamera.enabled = false;

        }

        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            mainCamera.enabled = false;
            driversCamera.enabled = false;
            backCamera.enabled = true;
        }



    }
}
