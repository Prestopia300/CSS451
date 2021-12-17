using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcillatingMovement : MonoBehaviour
{
    float timeCounter;

    float speed;
    float width;
    float height;

    float startingX;
    float startingY;
    float startingZ;

    // Start is called before the first frame update
    void Start()
    {
        timeCounter = 0;

        speed = 4;
        width = 2;
        height = 2;


        startingX = transform.position.x;
        startingY = transform.position.y;
        startingZ = transform.position.z;

    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime * speed;

        float x = startingX;
        float y = Mathf.Cos(timeCounter) * width + (height / 2) + startingY;
        float z = Mathf.Sin(timeCounter) * height + startingZ;

        transform.position = new Vector3(x, y, z);
    }
}
