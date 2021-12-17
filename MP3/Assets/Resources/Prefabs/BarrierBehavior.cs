using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierBehavior : MonoBehaviour
{
    public GameObject TheBarrier;
    public Transform ShowNormal;
    private Vector3 Vn;
    private float D;

    // Start is called before the first frame update
    void Start()
    {
        // Vn = transform.forward;
        // Vn.Normalize();
        // D = Vector3.Dot(transform.localPosition, Vn);

        // ShowNormal = Instantiate(ShowNormalPrefab, new Vector3(0f,0f,0f), Quaternion.identity) ; // D * Vn // new Vector3(0f,0f,0f)
        // ShowNormal.transform.up = Vn;

        // ShowNormal.transform.localScale = new Vector3(0.2f, 5f, 0.2f);


    }

    // Update is called once per frame
    void Update()
    {
        Vn = transform.forward;
        Vn.Normalize();
        D = Vector3.Dot(transform.localPosition, Vn);
        
        ShowNormal.transform.localPosition = transform.localPosition - (ShowNormal.transform.localScale.y * ShowNormal.transform.up);
        ShowNormal.transform.up = Vn;
    }
}
// transform.forward = Vn = (Quad) Plane normal vector