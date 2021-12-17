using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    bool dirUp;
    public float speed;

    float degreesPerSecond;


    // Start is called before the first frame update
    void Start()
    {
        dirUp = true;
        speed = 1.0f;

        degreesPerSecond = 90;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);

        if (dirUp)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
        }

        else
        {
            transform.Translate(-Vector3.up * speed * Time.deltaTime);
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 1);
        }


        if (transform.position.y >= 5.0f)
        {
            dirUp = false;
        }

        if (transform.position.y <= 0)
        {
            dirUp = true;
        }
    }
}

// Resources Used :
// https://gamedevbeginner.com/how-to-rotate-in-unity-complete-beginners-guide/
// https://docs.unity3d.com/ScriptReference/Vector3.html