using UnityEngine;
using System.Collections;
using System;

public class followTest : MonoBehaviour {

    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform newtarget;
    [SerializeField]
    private Transform myTransform;
    [SerializeField]
    private Transform newSpawn;
    public Animator anim;
    [SerializeField]
    private float distance;
    [SerializeField]
    private AudioSource aud;
    private bool next;
    private bool onetime;
    private float nextUsage;
    private float delay = 3f;
    [SerializeField]
    private Vehicle playervehicle;
    [SerializeField]
    private float speed = 12f;
    // Use this for initialization
    void SetupAnimator()
    {
        anim = GetComponent<Animator>();
        foreach (var childAnimator in GetComponentsInChildren<Animator>())
        {
            if (childAnimator != anim)
            {
                anim.avatar = childAnimator.avatar;
                Destroy(childAnimator);
                break;
            }
        }
    }
    IEnumerator MyCoroutine()
    {
        changeState();
        yield return new WaitForSecondsRealtime(5f);    //Wait one frame
    }
    void changeState()
    {
        if ((distance < 3))
        {
            anim.Play("Attack");
        }
        else if((distance > 80)&&(onetime == false))
        {
            transform.position = newSpawn.transform.position;
            onetime = true;
        }
        else
        {
            anim.Play("Movement");
        }
    }
	void Start () {
        SetupAnimator();
        next = false;
        onetime = false;

	}
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "inrange")
        {
            if (!aud.isPlaying)
            {
                aud.Play();
            }
            
        }
    }
	public void getDistance()
    {
        /*returning the distance between the player and enemy will allow us to change the animation state
         *and control the speed
         */
        distance = Vector3.Distance(target.transform.position, myTransform.transform.position);
    }
    void setSpeed()
    {
        if(onetime == true)
        {
            if (distance > 50)
            {
                speed = 30f;
            }
            else if (distance < 50 && distance > 25)
            {
                speed = 26f;
            }
            else if (distance < 10)
            {
                speed = 12f;
            }
        }
   
    }
    // Update is called once per frame
    void Update () {
        getDistance();
        /*rotate the chaser to face the player*/
        setSpeed();
        transform.LookAt(target);
        /*move the objects position forward, it will always be facing the player, therefore it will follow*/
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //changeState();
        StartCoroutine(MyCoroutine());
        if(playervehicle.enabled == false)
        {
            target = newtarget;
        }
    }
}
