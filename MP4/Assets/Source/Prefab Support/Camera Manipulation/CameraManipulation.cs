using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManipulation : MonoBehaviour {

    public enum LookAtCompute {
        QuatLookRotation = 0,
        TransformLookAt = 1
    };

    public Transform LookAtPosition = null;
    public LookAtCompute ComputeMode = LookAtCompute.QuatLookRotation;
    public Transform WorldTransform = null; 
    public Transform SelectedObject = null;

    Vector3 mouseDownPos = Vector3.zero;

	// Use this for initialization
	void Start () {
        Debug.Assert(LookAtPosition != null);
	}
	
	// Update is called once per frame
	void Update () {
        // Look at
        switch (ComputeMode)
        {
            case LookAtCompute.QuatLookRotation:
                // Viewing vector is from transform.localPosition to the lookat position
                Vector3 V = LookAtPosition.localPosition - transform.localPosition;
                Vector3 W = Vector3.Cross(-V, Vector3.up);
                Vector3 U = Vector3.Cross(W, -V);
                // transform.localRotation = Quaternion.LookRotation(V, U);
                transform.localRotation = Quaternion.FromToRotation(Vector3.up, U);
                Quaternion alignU = Quaternion.FromToRotation(transform.forward, V);
                transform.localRotation = alignU * transform.localRotation;
                break;

            case LookAtCompute.TransformLookAt:
                transform.LookAt(LookAtPosition);
                break;
        }  

        // Zoom/Dolly
        float zoomDelta = 0f;
        zoomDelta = Input.GetAxis("Mouse ScrollWheel");
        if (zoomDelta != 0f){
            ProcesssZoom(zoomDelta);
        }

        // Orbit/Tumble
        // this rotates camera around LookAtPosition
        Vector3 rotateDelta = Vector3.zero;
        if (Input.GetMouseButtonDown(2))
        {
            mouseDownPos = Input.mousePosition;
            rotateDelta = Vector3.zero;
        } if (Input.GetMouseButton(2))
        {
            rotateDelta = mouseDownPos - Input.mousePosition;
            mouseDownPos = Input.mousePosition;
            ComputeHorizontalOrbit(rotateDelta.y);
            ComputeVerticalOrbit(rotateDelta.x);
        }

        // Track
        // this uses right mouse, and moves the camera up and right.
        Vector3 trackDelta = Vector3.zero;
        if (Input.GetMouseButtonDown(1))
        {
            mouseDownPos = Input.mousePosition;
            trackDelta = Vector3.zero;
        } if (Input.GetMouseButton(1))
        {
            trackDelta = mouseDownPos - Input.mousePosition;
            mouseDownPos = Input.mousePosition;
            ProcessTrack(trackDelta);
        }
    }

    public void ProcessTrack(Vector3 delta)
    {
        // senstivity, move left/right flip
        float sensitivity =  0.1f;

        // Instead of moving world, move Camera and LookAtPosition.
        LookAtPosition.localPosition += transform.right * delta.x * sensitivity;
        LookAtPosition.localPosition += transform.up * delta.y * sensitivity;
        transform.localPosition += transform.right * delta.x * sensitivity;
        transform.localPosition += transform.up * delta.y * sensitivity;
    }

    public void ProcesssZoom(float delta)
    {
        // senstivity, scroll in/out flip
        float sensitivity = -8f;
        // distance to look at pos
        Vector3 v = LookAtPosition.localPosition - transform.localPosition;
        float dist = v.magnitude;
        // change distance by delta
        dist += delta*sensitivity;
        transform.localPosition = LookAtPosition.localPosition - dist * v.normalized;
    }


    // const float RotateDelta = 10f / 60;  // about 10-degress per second
    //float Direction = 1f;
    void ComputeHorizontalOrbit(float rotateDelta)
    {
        // senstivity, move left/right flip
        float sensitivity = 0.1f;
        rotateDelta *= sensitivity;

        // orbit with respect to the transform.right axis

        // 1. Rotation by delta around right axis
        Quaternion q = Quaternion.AngleAxis(rotateDelta, transform.right);

        // 2. we need to rotate the camera position
        Matrix4x4 r = Matrix4x4.Rotate(q);
        Matrix4x4 invP = Matrix4x4.TRS(-LookAtPosition.localPosition, Quaternion.identity, Vector3.one);
        r = invP.inverse * r * invP;
        Vector3 newCameraPos = r.MultiplyPoint(transform.localPosition);
        
        transform.localPosition = newCameraPos;
        transform.localRotation = q * transform.localRotation;

        transform.LookAt(LookAtPosition);        
    }

    void ComputeVerticalOrbit(float rotateDelta)
    {
        // senstivity, move left/right flip
        float sensitivity = -0.1f;
        rotateDelta *= sensitivity;

        // orbit with respect to the transform.right axis

        // 1. Rotation by delta around right axis
        Quaternion q = Quaternion.AngleAxis(rotateDelta, transform.up);

        // 2. we need to rotate the camera position
        Matrix4x4 r = Matrix4x4.Rotate(q);
        Matrix4x4 invP = Matrix4x4.TRS(-LookAtPosition.localPosition, Quaternion.identity, Vector3.one);
        r = invP.inverse * r * invP;
        Vector3 newCameraPos = r.MultiplyPoint(transform.localPosition);
        
        transform.localPosition = newCameraPos;
        transform.localRotation = q * transform.localRotation;

        transform.LookAt(LookAtPosition);        
    }
}
