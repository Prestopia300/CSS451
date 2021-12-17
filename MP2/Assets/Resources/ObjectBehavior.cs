using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehavior : MonoBehaviour
{
    public Material mat;


    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void SetAsSelected()
    {
        transform.gameObject.GetComponent<Renderer>().material = mat;
        // GetComponent<Renderer>().material.color = new Color(2.8f, 0.8f, 0.1f, 0.1f);
    }

    public void ReleaseSelection(Material m)
    {
        transform.gameObject.GetComponent<Renderer>().material = m;
    }

    public void SetPosition(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
        // transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    public void SetRotation(float x, float y, float z)
    {
        // transform.Rotate(x, y, z, Space.Self);
        transform.eulerAngles = new Vector3(x, y, z);
    }

    public void SetScale(float x, float y, float z)
    {
        // transform.localScale.z
        transform.localScale = new Vector3(x, y, z);
    }
}
