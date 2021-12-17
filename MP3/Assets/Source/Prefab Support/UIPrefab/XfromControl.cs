using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class XfromControl : MonoBehaviour
{
    public Toggle T, R, S;
    public SliderWithEcho X, Y, Z;
    private GameObject Plane;
    private Vector3 mPreviousSliderValues = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        T.onValueChanged.AddListener(SetToTranslation);
        R.onValueChanged.AddListener(SetToRotation);
        S.onValueChanged.AddListener(SetToScaling);
        X.SetSliderListener(XValueChanged);
        Y.SetSliderListener(YValueChanged);
        Z.SetSliderListener(ZValueChanged);

        T.isOn = true;
        R.isOn = false;
        S.isOn = false;
        SetToTranslation(true);
    }

    //---------------------------------------------------------------------------------
    // Initialize slider bars to specific function
    void SetToTranslation(bool v)
    {
        Vector3 p = ReadObjectXfrom();
        mPreviousSliderValues = p;
        X.InitSliderRange(-20, 20, p.x);
        Y.InitSliderRange(-20, 20, p.y);
        Z.InitSliderRange(-20, 20, p.z);
    }

    void SetToScaling(bool v)
    {
        Vector3 s = ReadObjectXfrom();
        mPreviousSliderValues = s;
        X.InitSliderRange(0.1f, 20, s.x);
        Y.InitSliderRange(0.1f, 20, s.y);
        Z.InitSliderRange(0.1f, 20, s.z);
    }

    void SetToRotation(bool v)
    {
        Vector3 r = ReadObjectXfrom();
        mPreviousSliderValues = r;
        X.InitSliderRange(-180, 180, r.x);
        Y.InitSliderRange(-180, 180, r.y);
        Z.InitSliderRange(-180, 180, r.z);
        mPreviousSliderValues = r;
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

    // new object selected
    public void SetPlaneObject(GameObject g)
    {
        Plane = g;
        mPreviousSliderValues = Vector3.zero;
        ObjectSetUI();
    }
    
    public void ObjectSetUI()
    {
        Vector3 p = ReadObjectXfrom();
        X.SetSliderValue(p.x);  // do not need to call back for this comes from the object
        Y.SetSliderValue(p.y);
        Z.SetSliderValue(p.z);
    }
    
    private Vector3 ReadObjectXfrom()
    {
        Vector3 p;
        
        if (T.isOn)
        {
            if (Plane != null)
                p = Plane.transform.localPosition;
            else
                p = Vector3.zero;
        }
        else if (S.isOn)
        {
            if (Plane != null)
                p = Plane.transform.localScale;
            else
                p = Vector3.one;
        }
        else
        {
            if (Plane != null)
                p = Plane.transform.localRotation.eulerAngles;
            else
                p = Vector3.zero;
        }
        return p;
    }

    private void UISetObjectXform(ref Vector3 p)
    {
        if (Plane == null)
            return;

        if (T.isOn)
        {
            Plane.transform.localPosition = p;
        }
        else if (S.isOn)
        {
            Plane.transform.localScale = p;
        } else
        {
            Quaternion q = new Quaternion();
            q.eulerAngles = p;
            Plane.transform.localRotation = q;
        }
    }

}
