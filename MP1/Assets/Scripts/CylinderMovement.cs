using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderMovement : MonoBehaviour
{
    public bool dirForward;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        dirForward = true;
        speed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (dirForward)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
        }

        else
        {
            transform.Translate(-Vector3.forward * speed * Time.deltaTime);
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 0);
        }


        if (transform.position.z >= 5.0f)
        {
            dirForward = false;
        }

        if (transform.position.z <= 0)
        {
            dirForward = true;
        }
    }
}
// Resources Used:
// https://docs.unity3d.com/ScriptReference/Vector3.html