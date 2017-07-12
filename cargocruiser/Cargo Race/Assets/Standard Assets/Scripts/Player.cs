using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    [SerializeField]
    private Stat c_Health;
    [SerializeField]
    private Stat v_Health;

    [SerializeField]
    private Text timeTaken;
    [SerializeField]
    private Text endScore;
    [SerializeField]
    private Text endScoreHolder;
    [SerializeField]
    private Text finishTitle;
    [SerializeField]
    private Text deadTitle;
    [SerializeField]
    private Text cargop;
    [SerializeField]
    private Text cargoLeft;
    [SerializeField]
    private Text countDown;
   
    [SerializeField]
    private Image ulim;
    [SerializeField]
    private GameObject replaybtn;
    [SerializeField]
    private GameObject retrybtn;
    [SerializeField]
    private GameObject menubtn;
    [SerializeField]
    private GameObject exitbtn;
    [SerializeField]
    private GameObject fiftyhp;
    [SerializeField]
    private GameObject sevenfhp;
    [SerializeField]
    private GameObject twentyfhp;
    [SerializeField]
    private GameObject exhaustone;
    [SerializeField]
    private GameObject exhausttwo;
    [SerializeField]
    private GameObject gorilla;
    [SerializeField]
    private GameObject uploadBtn;
    [SerializeField]
    private GameObject nameTF;
    [SerializeField]
    private Vehicle vehicle;
    [SerializeField]
    private followTest follow;
    [SerializeField]
    private carAI aicar;

    private float countDownclock = 5;
    private float cargoper;
    private float timepassedCalc = 0;
    private float scoreCalc = 0;
    private float timepassed = 0;
    private float mins = 0;
    private float nextUsage;
    private float delay = 3f;
    private float cargo;
    private CursorLockMode hide;
    private CursorLockMode show;

    private bool escneeded;
    private bool finished;
    private bool start;
    // Use this for initialization
    private void Awake()
    {
        c_Health.Initialize();
        v_Health.Initialize();
        cargoLeft.enabled = false;
        finished = false;
        timeTaken.enabled = false;
        endScore.enabled = false;
        endScoreHolder.enabled = false;
        finishTitle.enabled = false;
        deadTitle.enabled = false;
        ulim.enabled = false;
        cargop.enabled = false;
        replaybtn.SetActive(false);
        menubtn.SetActive(false);
        exitbtn.SetActive(false);
        retrybtn.SetActive(false);
        Cursor.lockState = hide = CursorLockMode.Locked;
        Cursor.visible = false;
        fiftyhp.SetActive(false);
        sevenfhp.SetActive(false);
        twentyfhp.SetActive(false);
        escneeded = true;
        vehicle.enabled = false;
        gorilla.SetActive(false);
        nameTF.SetActive(false);
        uploadBtn.SetActive(false);
        aicar.enabled = false;
        Time.timeScale = 1;
    }
    void countingDown()
    {
        countDownclock -= Time.deltaTime;
        if (countDownclock > 0)
        {
            countDown.text = countDownclock.ToString("N0");
            start = false;
            if(countDownclock < 1)
            {
                countDown.text = "Go!";
            }
        }
        else
        {
            start = true;
           
        }
    }
    // Update is called once per frame
    void Update () {

        if (finished == false)
        {
            if ((Input.GetKeyDown(KeyCode.Space)) && (Time.time > nextUsage))
            {
                nextUsage = Time.time + delay;
                if (c_Health.CurrentValue > 0)
                {
                    c_Health.CurrentValue -= 20; 
                }
            }
            timepassed += Time.deltaTime;
            timepassedCalc += Time.deltaTime;
            if (timepassed >= 60)
            {
                mins += 1;
                timepassed = 0;
            }
        
        }
        if(start == true)
        {
            vehicle.enabled = true;
            gorilla.SetActive(true);
            countDown.enabled = false;
            aicar.enabled = true;
        }
        if(finished == true)
        {
            follow.enabled = false;
        }
        countingDown();
        pressesc();
        death();
        if(replaybtn.activeInHierarchy == true)
        {
            exitbtn.SetActive(false);
            retrybtn.SetActive(false);
        }
    }
    void death()
    {
        if (v_Health.CurrentValue < 75)
        {
            sevenfhp.SetActive(true);
            if (v_Health.CurrentValue < 50)
            {
                fiftyhp.SetActive(true);
                if (v_Health.CurrentValue < 25)
                {
                    twentyfhp.SetActive(true);
                    exhaustone.SetActive(false);
                    exhausttwo.SetActive(false);
                    if (v_Health.CurrentValue == 0)
                    {
                        escneeded = false;
                        Cursor.visible = true;
                        Cursor.lockState = show = CursorLockMode.None;
                        deadTitle.enabled = true;
                        vehicle.enabled = false;
                        menubtn.SetActive(true);
                        replaybtn.SetActive(true);
                       // follow.enabled = false;
                    }
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "gorilla")
        {
            
            if (v_Health.CurrentValue > 0)
            {
                v_Health.CurrentValue -= 20;
            }
        }
        if (other.gameObject.tag == "end")
        {
            start = false;
            escneeded = false;
            Cursor.visible = true;
            Cursor.lockState = show = CursorLockMode.None;
            cargo = c_Health.CurrentValue;
            cargoLeft.text = "Cargo Left: " + cargo.ToString();
            cargoLeft.enabled = true;
            finished = true;
            timeTaken.enabled = true;
            timeTaken.text = "Time " + mins.ToString() + "m " + timepassed.ToString("N0") + "s";
            endScore.enabled = true;
            endScoreHolder.enabled = true;
            cargoper = cargo / 100;
            scoreCalc = ((cargoper / timepassedCalc) * 1000000);
            int newScore = Mathf.RoundToInt(scoreCalc);
            endScore.text = newScore.ToString();
            finishTitle.enabled = true;
            cargop.enabled = true;
            ulim.enabled = true;
            replaybtn.SetActive(true);
            menubtn.SetActive(true);
            nameTF.SetActive(true);
            uploadBtn.SetActive(true);
        }
    }
  
    public void pressesc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(escneeded == true)
            {
                if (Cursor.visible == false && Time.timeScale == 1)
                {
                    Cursor.visible = true;
                    Cursor.lockState = show = CursorLockMode.Confined;
                    Time.timeScale = 0;
                    exitbtn.SetActive(true);
                    retrybtn.SetActive(true);
                    
                }
                else if (Cursor.visible == true&& Time.timeScale == 0)
                {
                    Cursor.visible = false;
                    Cursor.lockState = hide = CursorLockMode.Locked;
                    exitbtn.SetActive(false);
                    retrybtn.SetActive(false);
                    Time.timeScale = 1;
                }
            } 
        }
    }
    public void returntoMenu()
    {

        SceneManager.LoadScene(0);
        Debug.Log("Hello");
    }
   
    public void replay()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
 
}
