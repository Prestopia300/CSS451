using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public GameObject TravelingBallPrefab;
    public float FixeScale =1 ;
    public AimLine line;
    public Transform target;

    public float interval, speed, aliveSec;
    //public Vector3 ballAttributes;
    public Transform BallAttributesContainer;

    public bool ballIsShooter;

    // Do every x seconds
    private float nextActionTime = 0.0f;
    public Transform TheBarrier;

    // Start is called before the first frame update
    void Start()
    {
        interval = BallAttributesContainer.localPosition.x;
        nextActionTime = Time.time - (Time.time % interval) + interval;
    }

    // Update is called once per frame
    void Update()
    {
        interval = BallAttributesContainer.localPosition.x;
        if (Time.time > nextActionTime ) 
        {
            if (ballIsShooter)
            {
                ShootBall();
            }
            nextActionTime += interval;
        }
    }


    public void SetAsSelected()
    {
        transform.gameObject.GetComponent<Renderer>().material.color = Color.black;
    }

    public void ReleaseSelected()
    {
        transform.gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public void SetLine(AimLine l)
    {
        line = l;
    }

    public void SetTravelingBallTarget(Transform t)
    {
        target = t;
    }

    public void SetTheBarrier(Transform b)
    {
        TheBarrier = b;
    }

    // public void SetPosition(float x, float z)
    // {
    //     transform.localPosition = new Vector3(x, 0, z);
    // }

    public void SetBallAttributes(float i, float s, float als)
    {
        interval = i;
        speed = s;
        aliveSec = als;
    }

    public void ShootBall()
    {        
        speed = BallAttributesContainer.localPosition.y;
        aliveSec = BallAttributesContainer.localPosition.z;
        
        // This functon should create prefab object, and give it a reference to the line
        GameObject newTravelingBall = Instantiate(TravelingBallPrefab, transform.position, Quaternion.identity);
        TravelingBallBehavior tbb = newTravelingBall.GetComponent<TravelingBallBehavior>();
        tbb.SetlineEndPt(target.localPosition);
        tbb.SetSpeed(speed);
        Destroy(newTravelingBall, aliveSec);
        tbb.SetTheBarrier(TheBarrier);
    }

}
