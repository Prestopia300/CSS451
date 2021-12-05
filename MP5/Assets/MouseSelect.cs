using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseSelect : MonoBehaviour
{
    MyMesh myMesh;
    LayerMask layerMask;
    GameObject selectedVertex;
    GameObject selectedAxis;

    Color cached;

    Vector3 origMousePos;
    // Start is called before the first frame update
    void Start()
    {
        myMesh = FindObjectOfType<MyMesh>();
        layerMask = LayerMask.GetMask("vertex", "axis");
    }

    // Update is called once per frame
    void Update()
    {
        MouseEvent();
    }

    void MouseEvent()
    {   
        if(Input.GetKey(KeyCode.LeftControl))
        {
            myMesh.ToggleController(true);

            if(EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if(Input.GetMouseButtonDown(0))
            {
                RaycastHit hitInfo = new RaycastHit();

                bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 1000f, layerMask.value);

                if(hit)
                {
                    GameObject g = hitInfo.transform.gameObject;

                    switch(g.layer)
                    {
                        case 8: // Vertex
                            if(selectedVertex != null && g != selectedVertex)
                            {
                                selectedVertex.GetComponent<MeshRenderer>().material.color = Color.white;
                                selectedVertex.transform.GetChild(0).gameObject.SetActive(false);
                            }

                            selectedVertex = g;
                            g.transform.GetChild(0).gameObject.SetActive(true);
                            g.GetComponent<MeshRenderer>().material.color = Color.black;
                        return;
                        case 9: // Axis
                            origMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                    Camera.main.nearClipPlane));
                            if(selectedAxis != null)
                            {
                                selectedAxis.GetComponent<MeshRenderer>().material.color = cached;
                            }
                            selectedAxis = g;
                            cached = g.GetComponent<MeshRenderer>().material.color;

                            
                        return;
                    }
                }

            }
        }
        else
        {
            if(selectedVertex!= null)
            {
                selectedVertex.GetComponent<MeshRenderer>().material.color = Color.white;
                selectedVertex.transform.GetChild(0).gameObject.SetActive(false);
                selectedVertex = null;
            }

            myMesh.ToggleController(false);

        }

        if(selectedAxis != null)
        {
            if(Input.GetMouseButton(0))
            {
                selectedAxis.GetComponent<MeshRenderer>().material.color = Color.yellow;
                

                Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                    Camera.main.nearClipPlane));

                Vector3 delta = point - origMousePos;
                origMousePos = point;

                switch(selectedAxis.name)
                {
                    case "X":
                        delta = new Vector3(delta.x, 0, 0);
                        break;
                    case "Y":
                        delta = new Vector3(0, delta.y, 0);
                        break;
                    case "Z":
                        delta = new Vector3(0, 0, delta.z);
                        break;
                    default:
                        break;
                }

                selectedAxis.transform.parent.parent.localPosition += delta * 5f;

            }
            else
            {
                selectedAxis.GetComponent<MeshRenderer>().material.color = cached;
                selectedAxis = null;
            }
        }

        // if(Input.GetMouseButtonUp(0))
        // {
        //     if(selectedAxis != null)
        //     {
        //         selectedAxis.GetComponent<MeshRenderer>().material.color = cached;
        //     }
        // }
    }
        

        
}
