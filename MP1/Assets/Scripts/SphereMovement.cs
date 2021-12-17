using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{

    public bool dirRight;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        dirRight = true;
        speed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (dirRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
        }

        else
        {
            transform.Translate(-Vector3.right * speed * Time.deltaTime);
            gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 1);
        }


        if (transform.position.x >= 5.0f)
        {
            dirRight = false;
        }

        if (transform.position.x <= 0)
        {
            dirRight = true;
        }


    }
}


// Useful Resources:
// https://answers.unity.com/questions/690884/how-to-move-an-object-along-x-axis-between-two-poi.html
// https://docs.unity3d.com/ScriptReference/Vector3.html