using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class loadCargo : MonoBehaviour {
    [SerializeField]
    private int cargoCount = 0;
    [SerializeField]
    private Text time;
    [SerializeField]
    private Text cargo;
    [SerializeField]
    private Text countDown;
    [SerializeField]
    private Text instructtext;
    private bool end;
    private bool start;
    private float timeRemaining = 45;
    private float countDownclock = 5f;
    [SerializeField]
    private FirstPersonController fp;
    // Use this for initialization
    void Start () {
        end = false;
        cargo.enabled = false;
        time.enabled = false;
        fp.enabled = false;
        
	}
    
    void countingDown()
    {
        countDownclock -= Time.deltaTime;
        if (countDownclock > 0)
        {
            countDown.text = countDownclock.ToString("N0");
            start = false;
            if (countDownclock < 1)
            {
                countDown.text = "Go!";
            }
        }
        else
        {
            start = true;

        }
    }

    void Update () {

       

        if (timeRemaining > 0)
        {
            end = false;
        }
        else
        {
            end = true;
        }
        newScene();
        countingDown();
        if (start == true)
        {
            timeRemaining -= Time.deltaTime;
            cargo.enabled = true;
            time.enabled = true;
            countDown.enabled = false;
            fp.enabled = true;
            instructtext.enabled = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (cargoCount < 5)
        {
            if (other.gameObject.tag == "cargo")
            {
                cargoCount++;
                Destroy(other.gameObject);
            }
        }
    }
    void newScene()
    {
        if (end == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    void OnGUI()
    {
        //int showamount = cargoCount + 0;
        cargo.text = "Cargo saved: " + cargoCount;
        time.text = timeRemaining.ToString("N0");
    }
}
