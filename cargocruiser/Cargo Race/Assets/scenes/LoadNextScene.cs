using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadNextScene : MonoBehaviour {
    [SerializeField]
    private Text controlTitle;

    [SerializeField]
    private Text scores;
    [SerializeField]
    private Text leaderTitle;
    [SerializeField]
    private Text control1;
    [SerializeField]
    private Text control2;
    [SerializeField]
    private Text control3;
    [SerializeField]
    private Text control4;
    [SerializeField]
    private Text control5;
    private CursorLockMode show;
    private string content = "cargorace.x10host.com/displayUnity.php";
    // Use this for initialization
    void Start()
    {
        controlTitle.enabled = false;
        control1.enabled = false;
        control2.enabled = false;
        control3.enabled = false;
        control4.enabled = false;
        control5.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = show = CursorLockMode.None;
        StartCoroutine(HandleWWWRequest());
        scores.enabled = false;
        leaderTitle.enabled = false;
        Time.timeScale = 1;
    }
    public void ChangeSceneTest()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void showcontrols()
    {
        leaderTitle.enabled = false;
        scores.enabled = false;
        if (controlTitle.enabled == false)
        {
            control1.enabled = true;
            control2.enabled = true;
            control3.enabled = true;
            control4.enabled = true;
            control5.enabled = true;

        }
        else if(controlTitle.enabled == true)
        {
            controlTitle.enabled = false;
            control1.enabled = false;
            control2.enabled = false;
            control3.enabled = false;
            control4.enabled = false;
            control5.enabled = false;

        }

    }
    public void quit()
    {
        Application.Quit();
    }
    public void returnmenu()
    {
        SceneManager.LoadScene(0);

    }
    IEnumerator HandleWWWRequest()
    {
        WWW www = new WWW(content);
        yield return www;
        string scoresDataString = www.text;
        scores.text = scoresDataString;
    }
    public void hideLeaderBoards()
    {
        controlTitle.enabled = false;
        control1.enabled = false;
        control2.enabled = false;
        control3.enabled = false;
        control4.enabled = false;
        control5.enabled = false;
        if (scores.enabled == false)
        {
            leaderTitle.enabled = true;
            scores.enabled = true;
        }
        else if (scores.enabled == true)
        {
            leaderTitle.enabled = false;
            scores.enabled = false;
        }
    }

}
