using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimLine : MonoBehaviour
{
    private Transform sibling0, sibling1;
    private Vector3 d;

    private float scale;

    public Transform TheBarrier;
    // private GameObject ricocheCylinder;
    // private bool ricocheCylinderActive = false;

    void Start()
    {
        // ricocheCylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        // ricocheCylinderActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        // sibling0 = transform.GetChild(0).GetChild(0);
        // sibling1 = transform.GetChild(0).GetChild(1);
                
        d = sibling1.localPosition - sibling0.localPosition; 
        transform.up = d.normalized;
        transform.localPosition = sibling0.localPosition + (0.5f * d);
        transform.localScale = new Vector3(scale, d.magnitude*0.5f, scale);

        // ---------------------------------------------------------------------------------------------------------
        // Richoche line 


        // // if (collisionDirectionInBounds() && !ricocheCylinderActive)
        // // {
        // //     // reflection()
        // //     ricocheCylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        // //     ricocheCylinderActive = true;
        // // }
        // // if (collisionDirectionInBounds())
        // // {

        // ricocheCylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        // ricocheCylinderActive = true;

        // ricocheCylinder.transform.position = reflectionPos();
        // ricocheCylinder.transform.up = reflection();
        // ricocheCylinder.transform.localScale = new Vector3(0.3f, reflection().magnitude*0.5f, 0.3f);
        // transform.gameObject.GetComponent<Renderer>().material.color = Color.green;
        // ricocheCylinderActive = true;
        // // }
        // // if (!collisionDirectionInBounds() && ricocheCylinderActive)
        // // {
        // Destroy(ricocheCylinder);
        // ricocheCylinderActive = false;
        // // }

    }

    public Vector3 getDirection()
    {
        return d.normalized;
    }

    public void SetEntPts(Transform s0, Transform s1)
    {
        sibling0 = s0;
        sibling1 = s1;
    }

    public Transform getLineEndPt()
    {
        return sibling0;
    }

    public void setScale(float s){
        scale = s;
    }


    // ---------------------------------------------------------------------------------------------------------
    // Richoche line 

    // public bool collisionDirectionInBounds()
    // {
    //     Vector3 Vn = TheBarrier.forward;
    //     float D = Vector3.Dot(TheBarrier.localPosition, Vn);

    //     float d = ((D - Vector3.Dot(sibling0.localPosition, Vn)) / Vector3.Dot(getDirection(), Vn)  );

    //     Vector3 Pon = sibling0.localPosition + d * getDirection();

    //     Vector3 Va = Pon - TheBarrier.localPosition;

    //     // TESTING
    //     // sphere.transform.localPosition = TheBarrier.localPosition;//-d*Vn;

    //     if (Va.magnitude < TheBarrier.localScale.y*0.5){ // (future) ball inpact is within circle radius
    //         // if (d < 0.5f*TheBarrier.transform.localScale.z) { // distance to plane is less than TheBarrier thickness
    //         return true;
    //         // }
    //     }
    //     return false;
    // }

    // public Vector3 reflection()
    // {
    //     Vector3 Von = -getDirection();
    //     Vector3 Vn = TheBarrier.forward;

    //     Vector3 Vr = 2f*(Vector3.Dot(Von, Vn)*Vn - Von);
    //     return Vr;
    // }

    // public Vector3 reflectionPos()
    // {
    //     Vector3 Vn = TheBarrier.forward;
    //     float D = Vector3.Dot(TheBarrier.localPosition, Vn);

    //     float d = ((D - Vector3.Dot(sibling0.localPosition, Vn)) / Vector3.Dot(getDirection(), Vn)  );

    //     Vector3 Pon = sibling0.localPosition + d * getDirection();

    //     Vector3 position = Pon + 0.5f*reflection().magnitude * reflection();
    //     return position;
    // }

}


// Dir = transform.up
// Size = 2 (becuase cylinder)
// d (desired effect vector) = P1-P0
// d.normalized keeps direction but changes magnitude. We could set transform.up to d and get the same direction results
// mid = P0 - d/2
// d.magnitude * 0.5 becuase the size is 2, so the .5 and 2 cancel out and the magnitude is left.
// sidenote, localPositon is your position considering your parent

// Resource for scaling only parent : https://answers.unity.com/questions/147816/how-to-avoid-scaling-heritage-when-parenting.html