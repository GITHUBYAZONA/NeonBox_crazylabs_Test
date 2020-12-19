using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    //Rigidbody rb;
    public Vector3 MoveToTarget;
    public float speed;
    public Vector3 startPosition;
    public Vector3 gameStartPos;

    //walls
    public Transform wallOne;
    public Transform wallTwo;
    public Transform wallObs;

    //walls Obsticles variations
    public GameObject ObsRegular;
    public GameObject ObsTall;
    public GameObject ObsSmall;


    private Vector3 rightLane; //Right lane position
    private Vector3 middleLane; // Middle lane position
    private Vector3 leftLane;  // Left lane position

    //public float wallScale = 1f;
    // Start is called before the first frame update
    void Start()
    {        
        rightLane = new Vector3 (2.49f, 0f, 0f);
        middleLane = new Vector3(0, 0f, 0f);
        leftLane = new Vector3(-2.49f, 0f, 0f);
        randomWallPosition();
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == MoveToTarget)
        {
            transform.position = startPosition;
            randomWallPosition();
        }

        //Debug Tool: which size to make the wall
        //-------------------------------------------------------------------------------------------
        /*  foreach (Transform child in gameObject.transform)
          {
              child.localScale = new Vector3(wallScale, child.transform.localScale.y, child.transform.localScale.z); ;
          }
        */
        transform.position = Vector3.MoveTowards(transform.position, MoveToTarget, Time.deltaTime * speed);
    }
    
    private void FixedUpdate()
    {
       

        
    }

    public void randomWallPosition()
    {
        int whichLane = Random.Range(-1, 2);
        randomObs();
       // Debug.Log(whichLane);
        switch (whichLane)
        {
            case -1: // Obs Left
                wallObs.transform.localPosition = leftLane;
                wallOne.transform.localPosition = middleLane;
                wallTwo.transform.localPosition = rightLane;
                break;

            case 0: // Obs Middle
                wallOne.transform.localPosition = leftLane;
                wallObs.transform.localPosition = middleLane;
                wallTwo.transform.localPosition = rightLane;
                break;

            case 1: // Obs Right
                wallOne.transform.localPosition = middleLane;
                wallTwo.transform.localPosition = leftLane;
                wallObs.transform.localPosition = rightLane;
                break;

        }
    }

    private void randomObs()
    {
        int ObsRandomize = Random.Range(0, 3);

        switch (ObsRandomize)
        {
            case 0:
                ObsRegular.SetActive(true);
                ObsSmall.SetActive(false);
                ObsTall.SetActive(false);
                break;
            case 1:
                ObsRegular.SetActive(false);
                ObsSmall.SetActive(true);
                ObsTall.SetActive(false);
                break;
            case 2:
                ObsRegular.SetActive(false);
                ObsSmall.SetActive(false);
                ObsTall.SetActive(true);
                break;
        }
    }
}
