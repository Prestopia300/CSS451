using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TheWorld : MonoBehaviour
{
    private BallBehavior mSelectedSphere = null;
    private Color kSelectedColor = new Color(0.8f, 0.8f, 0.1f, 0.5f);
    public GameObject lineSegmentPrefab;
    public GameObject lineEndPtPrefab;
    public Transform TheBarrier;
    //public Vector3 ballAttributes;
    //public float interval, speed, aliveSec;
    public Transform BallAttributesContainer = null;


	// Use this for initialization
	void Start () 
    {
        createAimLine(new Vector3(-17f, 8f, 8f));
    }

    public GameObject SelectObject(GameObject obj, Vector3 pos)
    {
        // checks if selection is not off limits (in future, selecting different objects does different things).
        if (obj != null)
        {
            // clicked wall
            if (pos != Vector3.zero && obj.name == "LeftWall")
            {
                createAimLine(pos);
            }
            if (pos != Vector3.zero && obj.name == "BackWall")
            {
                createBigLine(pos);
            }
            
            
            if ((obj.name == "LeftWall") || (obj.name == "RightWall") || (obj.name == "BackWall") || (obj.name == "Floor"))
                obj = null;
        }
        
        SetEndPtSelection(obj);

        return obj;
    }

    // Sets new selected (for the world and the ball), and changes colors
    private void SetEndPtSelection(GameObject g) {
        ReleaseSelected();
        mSelectedSphere = g.GetComponent<BallBehavior>();
        Debug.Assert(mSelectedSphere != null);
        mSelectedSphere.SetAsSelected();
    }

    // ---------------------------------------------
    // This is where I can click and drag an object. I need to be able to set selected, release selected

    // good
    public void ReleaseSelected()
    {
        if (mSelectedSphere != null)
        {
            mSelectedSphere.ReleaseSelected();
            mSelectedSphere = null;
        }
    }

    // ?
    public Vector3 GetSelectedPosition()
    {
        if (mSelectedSphere != null)
            return mSelectedSphere.transform.localPosition;
        else
            return Vector3.zero;
    }

    // ?
    public bool HasSelected() { return mSelectedSphere != null; }

    // Set Selected, Release Selected

    // public void SetBallAttributes(float i, float s, float als)
    // {
    //     interval = i;
    //     speed = s;
    //     aliveSec = als;
    // }

}
