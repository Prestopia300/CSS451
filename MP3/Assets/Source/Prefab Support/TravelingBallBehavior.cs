using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;



public class TravelingBallBehavior : MonoBehaviour
{
    //float interval;
    float movementSpeed;
    //float LifeSpan;

    // MyLine line;
    Vector3 lineEndPt;

    public GameObject ConnectingLinePrefab;
    public GameObject IndicatingLinePrefab;
    public GameObject ShadowPrefab;
    public Transform TheBarrier;
    private List<GameObject> ConnectingLines = new List<GameObject>();
    private List<GameObject> pointingTo = new List<GameObject>();
    private bool indicatingLineActive = false;


    // Testing Variables
    //GameObject sphere;
    private Vector3 direction;

    
    // Start is called before the first frame update
    void Start(){
        // TESTING
        //sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        // sphere.GetComponent<Renderer>().material.color = Color.red;

        direction = lineEndPt - transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = direction;
        transform.position += transform.up * Time.deltaTime * movementSpeed;

        manageConnectingLines();
        manageIndicatingLine();

        if (detectCollision())
        {
            transform.gameObject.GetComponent<Renderer>().material.color = Color.black;
            direction = reflection();
        }

    }

    public void SetlineEndPt(Vector3 pos)
    {
        lineEndPt = pos;
    }

    public void SetSpeed(float s)
    {
        movementSpeed = s;
    }

    
    // Info I need: this balls position, position of other cylinders, position of their end points 
    
    // Ideas:
    // I could constantly be feeding each traveling ball the location of all cylinders, and have lines to all of them, only visible if it is within a range.
    public void manageConnectingLines()
    {
        foreach(GameObject gameObj in GameObject.FindObjectsOfType<GameObject>())
        {
            if(gameObj.name == "BigLine")
            {                
                // in bounds. 
                if (connectingLineInBounds(10f, gameObj.transform, transform))
                {
                    // not connected
                    if (!pointingTo.Contains(gameObj))
                    {
                        // instantiate
                        GameObject newConnectingLine = Instantiate(ConnectingLinePrefab, Vector3.zero, Quaternion.identity);
                        ConnectingLine l0 = newConnectingLine.GetComponent<ConnectingLine>();

                        // set
                        l0.SetCylinder(gameObj.transform);
                        l0.SetBall(transform);

                        // update lists
                        pointingTo.Add(gameObj);
                        ConnectingLines.Add(newConnectingLine);
                    }
                }
                // not in bounds and connected
                else if (pointingTo.Contains(gameObj))
                {
                    pointingTo.Remove(gameObj);
                    //ConnectingLines.Remove(newConnectingLine);
                }
            }
        }
    }

    // connecting line below max - connecting line in bounds
    public bool connectingLineInBounds(float max, Transform cylinder, Transform ball)
    {
        Vector3 V = cylinder.up.normalized;
        
        // line from bottom of cylinder to ball
        Vector3 bottomOfCylinder = cylinder.localPosition - V * (cylinder.up.magnitude * 0.5f);
        Vector3 Va = ball.localPosition - bottomOfCylinder;

        // Pon
        float d = Vector3.Dot(Va, cylinder.up);
        Vector3 Pon = bottomOfCylinder + d * V;

        // connecting line
        Vector3 ConnectingLine = ball.localPosition - Pon;

        if (ConnectingLine.magnitude <= max)
        {
            return true;
        }
        return false;
    }

    public void manageIndicatingLine()
    {
        // in bounds and not active
        if (indicatingLineInBounds() && !indicatingLineActive)
        {
            // instantiate
            GameObject newIndicatingLine = Instantiate(IndicatingLinePrefab, Vector3.zero, Quaternion.identity);
            IndicatingLine l0 = newIndicatingLine.GetComponent<IndicatingLine>();

            // set
            l0.SetPlane(TheBarrier);
            l0.SetBall(transform);
            l0.ShadowPrefab = ShadowPrefab;

            // Update bool
            indicatingLineActive = true;
        }
        // out of bounds
        else if (!indicatingLineInBounds())
        {
            indicatingLineActive = false;
        } 
    }
    public bool indicatingLineInBounds()
    {
        // Transform Plane = TheBarrier;
        // Transform Ball = transform;

        // Plane Normal Vector
        Vector3 Vn = TheBarrier.forward;
        // Distance from plane position to origin via plane normal vector
        float D = Vector3.Dot(TheBarrier.localPosition, Vn);
        // ball direction
        Vector3 Vb = transform.up;

        // distance of ball to plane (using plane normal vector)
        float d = ((D - Vector3.Dot(transform.localPosition, Vn)) / Vector3.Dot(Vn, Vn)  );
        
        // position of ball impact (using plane normal vector)
        Vector3 Pon = transform.localPosition + d * Vn;
        // distance from imact position to plane center
        Vector3 Va = Pon - TheBarrier.localPosition;

        if (Va.magnitude < TheBarrier.localScale.y*0.5){ // (future) ball inpact is within circle radius
            return true;
        }
        return false;
    }


    public void destroyAllConnectedObjects()
    {
        foreach(GameObject gameObj in ConnectingLines)
        {
            Destroy(gameObj);
    
        }
        pointingTo.Clear();

    }

    void OnDestroy()
    {
        destroyAllConnectedObjects();
    }

    // ------------------------- PLANE COLLISION ----------------------------

    public bool detectCollision()
    {
        Vector3 Vn = TheBarrier.transform.forward;
        float D = Vector3.Dot(TheBarrier.transform.localPosition, Vn);

        float d = ((D - Vector3.Dot(transform.localPosition, Vn)) / Vector3.Dot(transform.up, Vn)  );

        Vector3 Pon = transform.localPosition + d * transform.up;

        Vector3 Va = Pon - TheBarrier.localPosition;

        // TESTING
        // sphere.transform.localPosition = TheBarrier.localPosition;//-d*Vn;

        if (Va.magnitude < TheBarrier.localScale.y*0.5){ // (future) ball inpact is within circle radius
            if (d < 0.5f*TheBarrier.transform.localScale.z) { // distance to plane is less than TheBarrier thickness
                return true;
            }
        }
        return false;
    }

    public Vector3 reflection()
    {
        Vector3 Von = -transform.up;
        Vector3 Vn = TheBarrier.transform.forward;

        Vector3 Vr = 2f*(Vector3.Dot(Von, Vn)*Vn - Von);
        return Vr;
    }

    public void SetTheBarrier(Transform b)
    {
        TheBarrier = b;
    }

}
