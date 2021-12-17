using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectingLine : MonoBehaviour
{
    private Transform cylinder;
    private Transform ball;

    private GameObject cyllinderPon;

    // Start is called before the first frame update
    void Start()
    {        
        cyllinderPon = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        cyllinderPon.transform.position = cylinderPonTransform();
        cyllinderPon.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        manageConnectingLineTransform();
    }

    // Update is called once per frame
    void Update()
    {
        cyllinderPon.transform.position = cylinderPonTransform();

        if (connectingLineOutOfBounds(10f, cylinder, ball))
        {
            Destroy(transform.gameObject);
            Destroy(cyllinderPon);
        }

        manageConnectingLineTransform();

        // // destroy when ball outside of bounds
        // if (!ballInsidePerpendicularLength(5f, ball, cylinder))
        // {
        //     Destroy(transform.gameObject);
        // }
        
    }

    private void manageConnectingLineTransform()
    {
        Vector3 V = cylinder.up.normalized;
        
        // line from bottom of cylinder to ball
        Vector3 bottomOfCylinder = cylinder.localPosition - V * (cylinder.up.magnitude * 0.5f);
        Vector3 Va = ball.localPosition - bottomOfCylinder;

        // Pon
        float d = Vector3.Dot(Va, cylinder.up);
        Vector3 Pon = bottomOfCylinder + d * V;

        // connecting line
        Vector3 connectingLine = ball.localPosition - Pon;
        transform.up = connectingLine;

        // position
        transform.localPosition = Pon + 0.5f*connectingLine ; //ball.localPosition - Pon*0.5f ; //+ ball.localPosition*0.5f;

        // scale
        transform.localScale = new Vector3(0.02f, connectingLine.magnitude*0.5f, 0.02f);
    }

    private Vector3 cylinderPonTransform()
    {
        Vector3 V = cylinder.up.normalized;
        
        // line from bottom of cylinder to ball
        Vector3 bottomOfCylinder = cylinder.localPosition - V * (cylinder.up.magnitude * 0.5f);
        Vector3 Va = ball.localPosition - bottomOfCylinder;

        // Pon
        float d = Vector3.Dot(Va, cylinder.up);
        Vector3 Pon = bottomOfCylinder + d * V;

        // connecting line
        Vector3 connectingLine = ball.localPosition - Pon;

        Pon = Pon + 1f * connectingLine.normalized;

        return Pon;
    }

    public void SetCylinder(Transform c)
    {
        cylinder = c;
    }

    public void SetBall(Transform b)
    {
        ball = b;
    }

    // public bool ballInsidePerpendicularLength(float max, Transform cylinder, Transform ball)
    // {
    //     Vector3 V = cylinder.up.normalized;
    //     Vector3 bottomOfCylinder = cylinder.localPosition - V * (cylinder.up.magnitude * 0.5f);
    //     Vector3 Va = ball.localPosition - bottomOfCylinder;

    //     float d = Vector3.Dot(Va, cylinder.up);

    //     if (d < max) return true;
        
    //     return false;
    // }


    private bool connectingLineOutOfBounds(float max, Transform cylinder, Transform ball)
    {
        Vector3 V = cylinder.up.normalized;
        
        // line from bottom of cylinder to ball
        Vector3 bottomOfCylinder = cylinder.localPosition - V * (cylinder.up.magnitude * 0.5f);
        Vector3 Va = ball.localPosition - bottomOfCylinder;

        // Pon
        float d = Vector3.Dot(Va, cylinder.up);
        Vector3 Pon = bottomOfCylinder + d * V;

        // connecting line
        Vector3 connectingLine = ball.localPosition - Pon;

        if (connectingLine.magnitude > max)
        {
            return true;
        }
        return false;
    }

}
