using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookAtControl : MonoBehaviour {
    public SliderWithEcho X, Y, Z;
    public Transform mSelected; // This is look at position
    private Vector3 mPreviousSliderValues = Vector3.zero;

	// Use this for initialization
	void Start () {
        X.SetSliderListener(XValueChanged);
        Y.SetSliderListener(YValueChanged);
        Z.SetSliderListener(ZValueChanged);
        SetToTranslation(true);
	}
	
    //---------------------------------------------------------------------------------
    // Initialize slider bars to specific function
    void SetToTranslation(bool v)
    {
        Vector3 p = ReadObjectXfrom();
        mPreviousSliderValues = p;
        X.InitSliderRange(-30, 30, p.x);
        Y.InitSliderRange(-30, 30, p.y);
        Z.InitSliderRange(-30, 30, p.z);
    }

    //---------------------------------------------------------------------------------

    //---------------------------------------------------------------------------------
    // resopond to sldier bar value changes
    void XValueChanged(float v)
    {
        Vector3 p = ReadObjectXfrom();
        p.x = v;
        UISetObjectXform(ref p);
    }
    
    void YValueChanged(float v)
    {
        Vector3 p = ReadObjectXfrom();
        p.y = v;        
        UISetObjectXform(ref p);
    }

    void ZValueChanged(float v)
    {
        Vector3 p = ReadObjectXfrom();
        p.z = v;
        UISetObjectXform(ref p);
    }
    //---------------------------------------------------------------------------------

    public void ObjectSetUI()
    {
        Vector3 p = ReadObjectXfrom();
        X.SetSliderValue(p.x);  // do not need to call back for this comes from the object
        Y.SetSliderValue(p.y);
        Z.SetSliderValue(p.z);
    }

    private Vector3 ReadObjectXfrom()
    {
        Vector3 p = mSelected.localPosition;
        return p;
    }

    private void UISetObjectXform(ref Vector3 p)
    {
        if (mSelected == null)
            return;
        mSelected.localPosition = p;
    }
}