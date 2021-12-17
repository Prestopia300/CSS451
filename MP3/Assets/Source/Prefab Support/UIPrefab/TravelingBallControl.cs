using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelingBallControl : MonoBehaviour
{
    public SliderWithEcho X, Y, Z;
    //public float interval, speed, aliveSec;
    public Transform BallAttributesContainer;
    
    // Start is called before the first frame update
    void Start()
    {
        // ballAttributes = new Vector3(1f,5f,10f);
        BallAttributesContainer.localPosition = new Vector3(1f,5f,10f);

        X.InitSliderRange(0.5f, 4f, 1f);
        Y.InitSliderRange(0.5f, 15f, 5f);
        Z.InitSliderRange(1f, 15f, 10f);

        X.SetSliderListener(XValueChanged);
        Y.SetSliderListener(YValueChanged);
        Z.SetSliderListener(ZValueChanged);
    }

    //---------------------------------------------------------------------------------
    // resopond to sldier bar value changes
    void XValueChanged(float v)
    {
        Vector3 p = BallAttributesContainer.localPosition;
        p.x = v;
        UISetObjectXform(ref p);
    }
    
    void YValueChanged(float v)
    {
        Vector3 p = BallAttributesContainer.localPosition;
        p.y = v;
        UISetObjectXform(ref p);
    }

    void ZValueChanged(float v)
    {
        Vector3 p = BallAttributesContainer.localPosition;
        p.z = v;
        UISetObjectXform(ref p);
    }
    //---------------------------------------------------------------------------------

    private void UISetObjectXform(ref Vector3 p)
    {
        BallAttributesContainer.localPosition = p;
    }

    // public void SetBallAttributes(float i, float s, float als)
    // {
    //     interval = i;
    //     speed = s;
    //     aliveSec = als;
    // }

    // public float GetInterval(){ return interval; }
    // public float GetSpeed(){ return speed; }
    // public float GetAliveSec(){ return aliveSec; }

}
