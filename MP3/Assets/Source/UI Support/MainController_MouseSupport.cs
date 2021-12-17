using System; // for assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for GUI elements: Button, Toggle
using UnityEngine.EventSystems;

public partial class MainController : MonoBehaviour
{
    void LMBSelect()
    {
        GameObject selectedObj;
        Vector3 hitPoint;
        if (Input.GetMouseButtonDown(0)) 
        {
            // Debug.Log("Mouse is down");

            if (!EventSystem.current.IsPointerOverGameObject()) // check for GUI
            {                
                // Select Object
                RaycastHit hitInfo = new RaycastHit();
                bool hit = Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, 1);
                // 1 is the mask for default layer
                if (hit)
                    SelectObject(hitInfo.transform.gameObject, hitInfo.point);
                else
                    SelectObject(null, Vector3.zero);

                
                // if (GetRoomSelected(out selectedObj, out hitPoint, 1 << 0) == "LeftWall")))
                // {
                //     SelectObject(null, null);
                // }
            }
        }
        else if (Input.GetMouseButton(0)) // Mouse Drag 
        {
            if (MouseSelectRoomAt(out selectedObj, out hitPoint, 1 << 0)) // same as default layer
            {
                mModel.MoveCreatedTo(hitPoint);
            }
        }
        else
        {
            ReleaseObject();
        }
    }

    void ProcessMouseEvents()
    {
        GameObject selectedObj;
        Vector3 hitPoint;
        /*if (Input.GetMouseButtonDown(0)) // Click event
        {
            // (create if clicking a wall)
            if (MouseSelectObjectAt(out selectedObj, out hitPoint, LayerMask.GetMask("Default")))
            {
                TheWorld.CreateBallAt(hitPoint);
            }

        }*/
        // Was else if
        if (Input.GetMouseButton(0)) // Mouse Drag 
        {
            if (MouseSelectRoomAt(out selectedObj, out hitPoint, 1 << 0)) // Notice the two ways of getting the mask
            {
                mModel.MoveCreatedTo(hitPoint);
            }
        }
        /*else if (Input.GetMouseButtonUp(0)) // Mouse Release
        {
            // set balls into motion of vector direction
            TheWorld.EnableCreatedMotion();
        }*/

        // ? 
        /*if (Input.GetMouseButtonDown(1))  // RMB
        {
            if (MouseSelectObjectAt(out selectedObj, out hitPoint, 1 << 8))  // or LayerMask.GetMask("Spheres")
            {
                TheWorld.SetSelected(ref selectedObj);
            }
        }*/
    }


    // This finds mouse click position. It returns true if 
    bool MouseSelectRoomAt(out GameObject g, out Vector3 p, int layerMask)
    {
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, layerMask);
        // Debug.Log("MouseSelect:" + layerMask + " Hit=" + hit);
        if (hit)
        {
            g = hitInfo.transform.gameObject;
            p = hitInfo.point;
        }
        else
        {
            g = null;
            p = Vector3.zero;
        }

        // position can only be a wall or floor
        if (!((g.name == "LeftWall") || (g.name == "RightWall") || (g.name == "BackWall") || (g.name == "Floor")))
                hit = false;

        return hit;
    }

    // String GetRoomSelected(out GameObject g, out Vector3 p, int layerMask)
    // {
    //     RaycastHit hitInfo = new RaycastHit();
    //     bool hit = Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, layerMask);
    //     // Debug.Log("MouseSelect:" + layerMask + " Hit=" + hit);
    //     if (hit)
    //     {
    //         g = hitInfo.transform.gameObject;
    //         p = hitInfo.point;
    //     }
    //     else
    //     {
    //         g = null;
    //         p = Vector3.zero;
    //     }

    //     // position can only be a wall or floor
        
    //     if ((g.name == "LeftWall") || (g.name == "RightWall") || (g.name == "BackWall") || (g.name == "Floor"))
    //         return g.name;

    //     return "";
    // }
}
