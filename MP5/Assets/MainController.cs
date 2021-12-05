using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    XformControl xformControl;
    // Start is called before the first frame update
    void Start()
    {
        xformControl = FindObjectOfType<XformControl>();
        xformControl.SetSelectedObject(FindObjectOfType<MyMesh>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
