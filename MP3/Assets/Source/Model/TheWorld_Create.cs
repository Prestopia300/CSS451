using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class will be able to create objects when the right areas are clicked/held
public partial class TheWorld : MonoBehaviour
{
    public void MoveCreatedTo(Vector3 pos)
    {
        mSelectedSphere.transform.localPosition = pos;
    }

    // One Prerequisite for this is that the wall clicked was the left wall.
    public void createAimLine(Vector3 clickedPos)
    {
        // create objects at pos
        GameObject newLineSegment = Instantiate(lineSegmentPrefab, clickedPos, Quaternion.identity);
        GameObject newLineEndPt0 = Instantiate(lineEndPtPrefab, clickedPos, Quaternion.identity);
        Vector3 pos = new Vector3(clickedPos.x+34f, clickedPos.y, clickedPos.z);
        GameObject newLineEndPt1 = Instantiate(lineEndPtPrefab, pos, Quaternion.identity);
        
        AimLine l0 = newLineSegment.GetComponent<AimLine>();
        BallBehavior b0 = newLineEndPt0.GetComponent<BallBehavior>();
        BallBehavior b1 = newLineEndPt1.GetComponent<BallBehavior>();
        
        b0.SetLine(l0);
        b1.SetLine(l0);
        l0.SetEntPts(b0.transform, b1.transform);

        l0.setScale(0.1f);


        b0.SetTravelingBallTarget(newLineEndPt1.transform);
        b0.ballIsShooter = true;
        b0.SetTheBarrier(TheBarrier);
        //b0.ballAttributes = ballAttributes;
        b0.BallAttributesContainer = BallAttributesContainer;

        b1.ballIsShooter = false;





        // --- After Attempting to make a parent child relationshiip between these objects, transforms were buggy, especially localScale ---
        // We use this tree format so that we can scale parent seperate from child
        // MyLineSegment
        //      EmptyObject
        //          LineEndPt1
        //          LineEndPt2

        // var emptyObject = new GameObject();
        // emptyObject.transform.localPosition = new Vector3(1,1,1);
        // emptyObject.transform.localScale = new Vector3(1,1,1);
        // emptyObject.transform.parent = newLineSegment.transform;
        // newLineEndPt0.transform.parent = emptyObject.transform;
        // newLineEndPt1.transform.parent = emptyObject.transform;

        // newLineEndPt1.transform.SetParent(newLineSegment.transform);
        // newLineEndPt2.transform.SetParent(newLineSegment.transform);

    }


    public void createBigLine(Vector3 clickedPos)
    {
        // create objects at pos
        GameObject newLineSegment = Instantiate(lineSegmentPrefab, clickedPos, Quaternion.identity);
        GameObject newLineEndPt0 = Instantiate(lineEndPtPrefab, clickedPos, Quaternion.identity);
        Vector3 pos = new Vector3(clickedPos.x, 0f, 0f);
        GameObject newLineEndPt1 = Instantiate(lineEndPtPrefab, pos, Quaternion.identity);
        
        AimLine l0 = newLineSegment.GetComponent<AimLine>();
        BallBehavior b0 = newLineEndPt0.GetComponent<BallBehavior>();
        BallBehavior b1 = newLineEndPt1.GetComponent<BallBehavior>();
        
        b0.SetLine(l0);
        b1.SetLine(l0);
        l0.SetEntPts(b0.transform, b1.transform);

        l0.setScale(2f);

        newLineSegment.name = "BigLine";

        newLineSegment.GetComponent<Renderer>().material.color = Color.magenta;

        b0.ballIsShooter = false;
        b1.ballIsShooter = false;
    }


    // function that creates a new line, including 2 end points, a line segment. 
    // End points should be connected to MyLine line segment and the left ball should know that it shoots balls.

    // Grandparent parent child tree:
    // MyLine
    //      lineEndPt
    //      lineEndPt
    //          ball
    //          ball
    //          ball...


    // private Vector3 kDeltaVector = new Vector3(0.1f, 0.1f, 0.1f);

    // // This method can : create object, set parent
    // public void CreatePrimitive(PrimitiveType type) {
    //     GameObject g = GameObject.CreatePrimitive(type);
    //     if (mSelected != null)
    //     {
    //         g.transform.SetParent(mSelected.transform);
    //         g.transform.localPosition = mSelected.transform.localPosition + kDeltaVector;
    //         int siblingCount = mSelected.transform.childCount;
    //         if (siblingCount > 0)
    //         {
    //             Transform sibling = mSelected.transform.GetChild(0);
    //             g.GetComponent<Renderer>().material = sibling.gameObject.GetComponent<Renderer>().material;
    //         }
    //     } else
    //     {
    //         // no parent
    //         g.GetComponent<Renderer>().material.color = Color.black;
    //         g.transform.localPosition = Vector3.one;
    //     }
    // }




    // This was in MP2 Solution DropDownSelection :

    // PrimitiveType[] kLoadType = {
    //     PrimitiveType.Cube,     // this is used as menu label, not used
    //     PrimitiveType.Cube,
    //     PrimitiveType.Sphere,
    //     PrimitiveType.Cylinder };

    // void UserSelection(int index) 
    // {
    //     if (index == 0)
    //         return;
        
    //     mCreateMenu.value = 0; // always show the menu function: Object to create

    //     // inform the world of user's action
    //     TheWorld.CreatePrimitive(kLoadType[index]);
    // }
}
