using UnityEngine;
using System.Collections;
using System;

public class TossTest : MonoBehaviour {

   

    ArrayList list = new ArrayList();
    //String[] list = new String[3] { "one", "two", "three" };
    [SerializeField]
    private Rigidbody m_rigidBody;
    [SerializeField]
    private float Score = 9850;
    [SerializeField]
    private float NCScore;
    [SerializeField]
    private float Value;
    private float nextUsage;
    [SerializeField]
    private float delay = 3f;
    [SerializeField]
    private GameObject cargothrow1;
    [SerializeField]
    private float speed = 700f;
    String arrayString;
    // Use this for initialization
    void Start () {
        list.Add("Bananas");
        list.Add("Gold");
        list.Add("Metal");
        list.Add("Salt");
        list.Add("Cement");
        arrayString = list.ToString();
    }
	
    IEnumerator MyCoroutine()
    {
            tossCargo();
            yield return new WaitForSecondsRealtime(5f);    //Wait one frame
        if (list.Count < 1)
        {
            NCScore = Score;
        }
    }

    void tossCargo()
    {
        //create a random number to be used as the value of cargo
        Value = UnityEngine.Random.Range(100, 500) * 2 - 1;

        /*    we want to make functionality that occurs on the tap of a button
              when this button is pressed, we want cargo to be thrown out of the vehicle and value to be subtracted from score
              we also don't want to be able to press it more than once every 3 seconds (earlier we declared float delay = 3f)
              time.time gives us the total elapsed time since the game was run
              by default, nextusage is 0, meaning time.time is > nextUsage and we can perform the function instantly
         */
        if ((Input.GetKeyDown(KeyCode.Space))&&(Time.time>nextUsage))
            {
            /*     when we press space, we set nextUsage to equal the current elapsed time and add delay to it(3 seconds)
              this means it will take time.time approx 3 seconds before it will be greater than nextUsage again 
        */
            nextUsage = Time.time + delay;
                if (Score <= 0 || list.Count<1)
                {
                    Score = NCScore;
                }
                else
                {
                    Score = Score - Value;
                    if (list.Count >= 1)
                    {
                        list.RemoveAt(0);
                        GameObject go = (GameObject)Instantiate(cargothrow1, transform.position, Quaternion.identity);
                        throwableObject ts = go.GetComponent<throwableObject>();
                        ts.Shoot(transform.forward * speed);
                    if(m_rigidBody.velocity.magnitude > 5 && m_rigidBody.velocity.magnitude < 10)
                    {
                        m_rigidBody.velocity = m_rigidBody.velocity * 2f;
                    }
                    else
                    {
                        m_rigidBody.velocity = m_rigidBody.velocity * 1.2f;
                    }    
               
                }
                
                }

            }

    }

    // Update is called once per frame
    void Update () {
        StartCoroutine(MyCoroutine());
        
    }
    
}
