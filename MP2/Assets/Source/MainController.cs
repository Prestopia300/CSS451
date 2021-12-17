using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainController : MonoBehaviour
{
    public GameObject GrandParent;
    public GameObject Parent;
    public GameObject Child;

    private GameObject selectedObj;
    
    
    Vector3 hitPoint;

    public Material materialChild;
    public Material materialParent;
    public Material materialGrandParent;
    //public Material materialWhenSelected;

    public Text xText;
    public Text yText;
    public Text zText;

    public Text ObjectName;

    public Slider xSlider;
    public Slider ySlider;
    public Slider zSlider;

    public Toggle TranslationToggle;
    public Toggle ScalingToggle;
    public Toggle RotationToggle;


    private BallBehavior childBallBehavior;
    
    //BallBehavior ballBehavior;
    ObjectBehavior objBehavior;
    
    private int lastToggle;

    private bool newObject;

    float min, max;


    // Start is called before the first frame update
    void Start()
    {

        // Initaialize slider min + max
        min = -180;
        max = 360;
        xSlider.minValue = min;
        xSlider.maxValue = max;
        
        ySlider.minValue = min;
        ySlider.maxValue = max;
        
        zSlider.minValue = min;
        zSlider.maxValue = max;

        selectedObj = null;
        lastToggle = 0;

        newObject = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Start with Selected object. Get Script for selected object.
        // change slider values if switching from radio button to radio button
        // there is a problem when switching from object to object
        // if object is switching to a smaller one, I need to set object to 000.  

        if (selectedObj != null)
        {
            // get object script
            objBehavior = selectedObj.GetComponent<ObjectBehavior>();


            if (newObject == true) ObjectName.text = selectedObj.name;

            // 1
            if (TranslationToggle.isOn)
            {
                setSlidersMinMax(-20f, 20f);

                // Update Sliders
                if (lastToggle != 1 || newObject == true)
                {
                    xSlider.value = selectedObj.transform.position.x;
                    ySlider.value = selectedObj.transform.position.y;
                    zSlider.value = selectedObj.transform.position.z;
                    newObject = false;
                }
                
                objBehavior.SetPosition(xSlider.value, ySlider.value, zSlider.value);
                
                lastToggle = 1;
            }
            // 2 
            else if (ScalingToggle.isOn)
            {
                setSlidersMinMax(0.1f, 20f);

                // Update Sliders
                if (lastToggle != 2 || newObject == true)
                {    
                    xSlider.value = selectedObj.transform.localScale.x;
                    ySlider.value = selectedObj.transform.localScale.y;
                    zSlider.value = selectedObj.transform.localScale.z;
                    newObject = false;
                }
                // transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z)

                objBehavior.SetScale(xSlider.value, ySlider.value, zSlider.value);

                lastToggle = 2;
            }
            // 3 (Problem occurs when setting rotation to a negative number. In Kelvin Sungs Solution, I can set a number to a negative, but when I come back to it, it has 360 added to it)
            else if (RotationToggle.isOn)
            {
                setSlidersMinMax(-180f, 360f);

                // Update Sliders
                if (lastToggle != 3 || newObject == true)
                {
                    xSlider.value = selectedObj.transform.eulerAngles.x;
                    ySlider.value = selectedObj.transform.eulerAngles.y;
                    zSlider.value = selectedObj.transform.eulerAngles.z;
                    newObject = false;
                }


                // Handles negative values in slider
                float a = xSlider.value;
                float b = ySlider.value;
                float c = zSlider.value;

                if (a < 0) a = a + 360;
                if (b < 0) b = b + 360;
                if (c < 0) c = c + 360;

                objBehavior.SetRotation(a, b, c);

                lastToggle = 3;
            }
        }

        // So. Highest to Lowest level, I need to seperate the 3 radio buttons (Translation, Scaling, or Rotation)

        xText.text = "X     " + xSlider.value;
        yText.text = "Y     " + ySlider.value;
        zText.text = "Z     " + zSlider.value;


        // ballBehavior.SetAsSelected();


        // I will need layers, and do different things fofr each layer. I will have a GrandParent layer, Parent Layer, and Child layer. 
        // Another approach : I will have only 1 added layer for objects. I will check if the object is the same as GrandParent, Parent, Child, and if so, do appropriate action.
        // These actions would take place if one of the sliders were to be used.

        // Change Selected Obj
        
        MouseSelectObjectAt(selectedObj, hitPoint, LayerMask.GetMask("Objects"));

        // Check UI Slider Value(s)

        // Update Object Model(s)
    }

    void setSlidersMinMax(float min, float max)
    {
        xSlider.minValue = min;
        xSlider.maxValue = max;

        ySlider.minValue = min;
        ySlider.maxValue = max;

        zSlider.minValue = min;
        zSlider.maxValue = max;
    }


    void MouseSelectObjectAt(GameObject g, Vector3 p, int layerMask)
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse was clicked over a UI element
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit hitInfo;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                bool hit = Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask);
                // Debug.Log("MouseSelect:" + layerMask + " Hit=" + hit);
                if (hit)
                {

                    if (selectedObj != null)
                    {
                        objBehavior = selectedObj.GetComponent<ObjectBehavior>();

                        // update newObject
                        if (selectedObj != hitInfo.transform.gameObject) newObject = true;

                        // set color back to default; release selectetd
                        if (selectedObj.name == "Child") objBehavior.ReleaseSelection(materialChild);
                    
                        else if (selectedObj.name == "Parent") objBehavior.ReleaseSelection(materialParent);
                    
                        else if (selectedObj.name == "GrandParent") objBehavior.ReleaseSelection(materialGrandParent);

                        objBehavior = null;
                    }
                
                    // select object
                    selectedObj = hitInfo.transform.gameObject;

                    /*if (selectedObj == Child)
                    {
                        Child.transform.eulerAngles = new Vector3(Time.deltaTime, 0, 0);
                    }*/


                    // change color
                    objBehavior = selectedObj.GetComponent<ObjectBehavior>();
                    objBehavior.SetAsSelected();

                    p = hitInfo.point;
                }
                else
                {
                    g = null;
                    p = Vector3.zero;

                    Child.transform.eulerAngles = new Vector3(Time.deltaTime, 0, 0);
                }
                //return hit;
            }
        }
    }
}

// Resources : 
// https://www.youtube.com/watch?v=292AoLyKkVc
// https://answers.unity.com/questions/1359714/transformlocalscalex-cant-change-it.html