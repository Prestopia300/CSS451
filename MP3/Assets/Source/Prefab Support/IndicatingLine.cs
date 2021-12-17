using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatingLine : MonoBehaviour
{
    public Transform Plane;
    public Transform Ball;
    public GameObject ShadowPrefab;
    public GameObject Shadow;


    // Start is called before the first frame update
    void Start()
    {
        // instantiate shadow
        GameObject newShadow = Instantiate(ShadowPrefab, Vector3.zero, Quaternion.identity);
        Shadow = newShadow;

        manageIndicatingLineTransform();
        manageShadowTransform();
    }

    // Update is called once per frame
    void Update()
    {
        if (indicatingLineOutOfBounds())
        {
            Destroy(transform.gameObject);
            Destroy(Shadow);
        }

        manageIndicatingLineTransform();
        manageShadowTransform();
    }

    public bool indicatingLineOutOfBounds()
    {
        // Plane Normal Vector
        Vector3 Vn = Plane.transform.forward;
        // Distance from plane position to origin via plane normal vector
        float D = Vector3.Dot(Plane.transform.localPosition, Vn);
        Vector3 Vb = Ball.transform.up;

        // distance of ball to plane (using plane normal vector)
        float d = ((D - Vector3.Dot(Ball.transform.localPosition, Vn)) / Vector3.Dot(Vn, Vn)  );
        
        
        // position of ball impact (using plane normal vector)
        Vector3 Pon = Ball.transform.localPosition + d * Vn;
        // distance from imact position to plane center
        Vector3 Va = Pon - Plane.localPosition;

        if (Va.magnitude > Plane.localScale.y*0.5){ // (future) ball inpact is within circle radius
            return true;
        }
        return false;
    }

    private void manageIndicatingLineTransform()
    {
        // Plane Normal Vector
        Vector3 Vn = Plane.transform.forward;
        // Distance from plane position to origin via plane normal vector
        float D = Vector3.Dot(Plane.transform.localPosition, Vn);
        Vector3 Vb = Ball.transform.up;

        // distance of ball to plane (using plane normal vector)
        float d = ((D - Vector3.Dot(Ball.transform.localPosition, Vn)) / Vector3.Dot(Vn, Vn)  );
        
        
        // position of ball impact (using plane normal vector)
        Vector3 Pon = Ball.transform.localPosition + d * Vn;
        // distance from imact position to plane center
        Vector3 Va = Pon - Plane.localPosition;

        // indicating line
        Vector3 indicatingLine = Ball.localPosition - Pon;
        transform.up = indicatingLine;

        // position
        transform.localPosition = Pon + 0.5f*indicatingLine ; //ball.localPosition - Pon*0.5f ; //+ ball.localPosition*0.5f;

        // scale
        transform.localScale = new Vector3(0.02f, indicatingLine.magnitude*0.5f, 0.02f);
    }

    private void manageShadowTransform()
    {
        // Plane Normal Vector
        Vector3 Vn = Plane.transform.forward;
        // Distance from plane position to origin via plane normal vector
        float D = Vector3.Dot(Plane.transform.localPosition, Vn);
        Vector3 Vb = Ball.transform.up;

        // distance of ball to plane (using plane normal vector)
        float d = ((D - Vector3.Dot(Ball.transform.localPosition, Vn)) / Vector3.Dot(Vn, Vn)  );
        
        
        // position of ball impact (using plane normal vector)
        Vector3 Pon = Ball.transform.localPosition + d * Vn;
        // distance from imact position to plane center
        Vector3 Va = Pon - Plane.localPosition;

        // indicating line
        Vector3 indicatingLine = Ball.localPosition - Pon;


        // Shadow Pos
        Shadow.transform.localPosition = Pon - 0.2f*Vn;
        // Shadow Rotation
        Shadow.transform.up = indicatingLine;
        // Shadow Scale (max = 1, min > 0)
        float max_magnitude = 20f;
        if (indicatingLine.magnitude < max_magnitude)
        {
            float scale = (max_magnitude - indicatingLine.magnitude) / max_magnitude;
            Shadow.transform.localScale = new Vector3(scale, 0.1f, scale);
        }
        else Shadow.transform.localScale = new Vector3(0.1f, 0.02f, 0.1f);
    }
    
    public void SetPlane(Transform c)
    {
        Plane = c;
    }

    public void SetBall(Transform b)
    {
        Ball = b;
    }

}
