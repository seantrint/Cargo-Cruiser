using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
    float timeRemaining = 60;

    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
        timeRemaining -= Time.deltaTime;
	
	}
    void OnGUI()
    {
        if(timeRemaining > 0)
        {
            GUI.Label(new Rect(550, 50, 150, 150), "Time left: " + timeRemaining);
        }
        else
        {
            GUI.Label(new Rect(550, 50, 150, 150), "Time is up");
        }
    }
}
