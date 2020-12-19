using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    public GameObject _GM;
    public bool gameStarted = false;
    private AudioSource Beat;

    //touch screen vars
    //---------------------------------------------------------------------------------
    private Vector2 startTouchPos, endTouchPos;
    

    
    private int laneNumber; // which lane am I. (-1 - left, 0 = middle, 1 = right)
    private bool isPlayerMoving; // checks if the player reached to his lane

    // lanes
    // --------------------------------------------------------------------------------
    private Vector3 currentLane; // current lane    
    private Vector3 rightLane = new Vector3 (2.5f, 0f, 0f); //Right lane position
    private Vector3 middleLane = new Vector3(0, 0f, 0f);  // Middle lane position
    private Vector3 leftLane = new Vector3(-2.5f, 0f, 0f);  // Left lane position
    public float laneMovingTime = 1f; //moving time

    //Animations
    //----------------------------------------------------------------------------------
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        Beat = GetComponent<AudioSource>();
        laneNumber = 0; // Starts at middle lane.
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        //  Player movement
        //----------------------------------------------------------------------------------
        if (gameStarted)
        {
            
        
        //Detect touch on the screen and its position
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
            startTouchPos = Input.GetTouch(0).position;

        
        //detect when the touch has ended and calculate where to go
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) 
        {
            //position of the last place on the screen
            endTouchPos = Input.GetTouch(0).position;

            //right, plus+ -- if i want to change a lane to the right
            if (endTouchPos.x > startTouchPos.x && endTouchPos.x - startTouchPos.x > 200f)
            {
                if (laneNumber != 1)
                {
                    isPlayerMoving = true;
                    laneNumber++;
                    Debug.Log(laneNumber);
                    Beat.pitch = 1.5f;
                    Beat.Play();


                }

            }

            //move left, minus- -- if i want to change a lane to the left
            if (endTouchPos.x < startTouchPos.x && endTouchPos.x - startTouchPos.x < -200f)
            {
                if (laneNumber != -1)
                {
                    isPlayerMoving = true;
                    laneNumber--;
                    Debug.Log(laneNumber);
                    Beat.pitch = 1f;
                    Beat.Play();
                }
            }
            
            //move up
            if (endTouchPos.y > startTouchPos.y && endTouchPos.y - startTouchPos.y > 200f)
            {
                anim.Play("PlayerAnimTall");
                Beat.pitch = 2f;
                Beat.Play();
            }
            
            //move down
            if (endTouchPos.y < startTouchPos.y && endTouchPos.y - startTouchPos.y < -200f)
            {
                anim.Play("PlayerAnimSmall");
                Beat.pitch = 0.5f;
                Beat.Play();
            }

            //Debug.Log(endTouchPos.y - startTouchPos.y);
        }

                
        //PC debug keys
        //----------------------------------------------------------------------------
        
        if (Input.GetKeyDown("d")) // right, plus+ -- if i want to change a lane to the right
        {
            if (laneNumber != 1)
            {
                isPlayerMoving = true;
                laneNumber++;
                Debug.Log(laneNumber);
                Beat.pitch = 1.5f;
                Beat.Play();
            }
           
            
        }
        


        if (Input.GetKeyDown("a")) // left, minus- -- if i want to change a lane to the left
        {
            if (laneNumber != -1)
            {
                isPlayerMoving = true;
                laneNumber--;
                Debug.Log(laneNumber);
                Beat.pitch = 1f;
                Beat.Play();
            }
            
        }


        // Animations
        //----------------------------------------------------------------------------------

        if (Input.GetKeyDown("w"))
        {
            anim.Play("PlayerAnimTall");
            Beat.pitch = 2f;
            Beat.Play();
        }

        if (Input.GetKeyDown("s"))
        {
            anim.Play("PlayerAnimSmall");
            Beat.pitch = 0.5f;
            Beat.Play();
        }
        }

    }

    private void FixedUpdate()
    {
        //Player Movement
        if (isPlayerMoving == true) // Call movement only when called
            movingLane(laneNumber);
    }


    //checks which lane to move torowards to
    private void movingLane(int whichLane)
    {
        switch (whichLane)
        {
            case -1: // if left
                rb.transform.position =  Vector3.Lerp(transform.position, leftLane, laneMovingTime);
                break;

            case 0: // if middle
                rb.transform.position = Vector3.Lerp(transform.position, middleLane, laneMovingTime);
                break;

            case 1: // if right
                rb.transform.position = Vector3.Lerp(transform.position, rightLane, laneMovingTime);
                break;
           
        }
        if (rb.transform.position == leftLane || rb.transform.position == middleLane || rb.transform.position == rightLane)
        {
            isPlayerMoving = false;
        }

       // Debug.Log("1");
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            _GM.GetComponent<GameManager>().gameOver();     
        }
        
    }


}
