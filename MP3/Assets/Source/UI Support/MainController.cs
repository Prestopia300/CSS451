using System; // for assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for GUI elements: Button, Toggle

public partial class MainController : MonoBehaviour
{
    // reference to all UI elements in the Canvas
    public Camera MainCamera = null;
    public XfromControl mXform = null;
    //public TravelingBallControl TBControl = null;
    public TheWorld mModel = null;
    public GameObject Plane;    

    // Use this for initialization
    void Start() {
        Debug.Assert(MainCamera != null);
        Debug.Assert(mXform != null);
        Debug.Assert(mModel != null);

        mXform.SetPlaneObject(Plane);

        //mModel.ballAttributes = TBControl.ballAttributes;
        //mModel.SetBallAttributes(TBControl.GetInterval(), TBControl.GetSpeed(), TBControl.GetAliveSec());
    }

    // Update is called once per frame
    void Update() {
        LMBSelect();
    }

    private void SelectObject(GameObject g, Vector3 point)
    {
        GameObject a = mModel.SelectObject(g, point);
        
    }

    private void ReleaseObject()
    {
        mModel.ReleaseSelected();
    }
}
