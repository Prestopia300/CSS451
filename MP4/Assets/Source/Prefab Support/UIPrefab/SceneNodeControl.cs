using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneNodeControl : MonoBehaviour {
    public Dropdown TheMenu = null;
    public SceneNode TheRoot = null;
    public XfromControl XformControl = null;
    public Transform SelectedObjAxisFrame = null;
    public Camera MainCamera = null;
    private SceneNode previousSceneNode = null;

    const string kChildSpace = "  ";
    List<Dropdown.OptionData> mSelectMenuOptions = new List<Dropdown.OptionData>();
    

    List<Transform> mSelectedTransform = new List<Transform>();
    List<Transform> mDecorationTransforms = new List<Transform>();
    List<Vector3> mOrigionalDecorationRotations = new List<Vector3>();  
    float rSpeed = 10f/6; // 10 degrees per second
    float Direction = 1f;  
    // every x time
    private float nextActionTime = 0.0f;
    public float period = 2f;
    private bool first = true;


    // Use this for initialization
    void Start () {
        TheMenu.ClearOptions();

        Debug.Assert(TheMenu != null);
        Debug.Assert(TheRoot != null);
        Debug.Assert(XformControl != null);
        Debug.Assert(SelectedObjAxisFrame != null);       

        mSelectMenuOptions.Add(new Dropdown.OptionData(TheRoot.transform.name));
        mSelectedTransform.Add(TheRoot.transform);
        GetChildrenNames("", TheRoot.transform);
        TheMenu.AddOptions(mSelectMenuOptions);
        TheMenu.onValueChanged.AddListener(SelectionChange);

        XformControl.SetSelectedObject(TheRoot.transform);
        SceneNode cn = TheRoot.GetComponent<SceneNode>();
        cn.AxisFrame = SelectedObjAxisFrame;
        previousSceneNode = cn;

        // CameraManipulation cm = MainCamera.GetComponent<CameraManipulation>();
        // cm.SelectedObject = TheRoot.transform;
    }

    void Update() {
        // period = 0.1f;
        for (int j = mDecorationTransforms.Count - 1; j >= 0; j--)
        {
            // float t = time.deltaTime;
            
            // // change rotation direction
            // float delta = (mOrigionalDecorationRotations[j].z - mDecorationTransforms[j].eulerAngles.z);
            // // if (Mathf.Abs(mOrigionalDecorationRotations[j].z - mDecorationTransforms[j].eulerAngles.z) > 0.7071f) // this is about 45-degrees
            // if (delta < -45f) // this is about 45-degrees
            // {
            //     Direction = 1f;
            // }
            // else if (delta > 45f)
            // {
            //     Direction = -1f;
            //     // newRot.z = 22f;
            //     // mDecorationTransforms[j].eulerAngles = newRot;
                
            // }


            if (Time.time > nextActionTime ) { // && !first
                nextActionTime += period;
                // execute block of code here
                Direction *= -1f;
            }


            // if(first){
            //     if (Time.time > nextActionTime/2 ) {
            //         nextActionTime += period/2;
            //         // execute block of code here
            //         Direction *= -1f;
            //         first = false;
            //     }
            // }


            // rotate object
            Vector3 newRot = mDecorationTransforms[j].eulerAngles;
            newRot.z += rSpeed*Direction;
            mDecorationTransforms[j].eulerAngles = newRot;
            // mDecorationTransforms[j].eulerAngles.z = mDecorationTransforms[j].eulerAngles.z + rSpeed*Direction;
        }
    }

    void GetChildrenNames(string blanks, Transform node)
    {
        string space = blanks + kChildSpace;
        for (int i = node.childCount - 1; i >= 0; i--)
        {
            Transform child = node.GetChild(i);
            SceneNode cn = child.GetComponent<SceneNode>();
            if (cn != null)
            {
                if (child.name != "LeftDecor-Node" && child.name != "RightDecor-Node" && child.name != "TopDecor-Node")
                {
                    mSelectMenuOptions.Add(new Dropdown.OptionData(space + child.name));
                    mSelectedTransform.Add(child);
                    GetChildrenNames(blanks + kChildSpace, child);
                }
                if (child.name == "LeftDecor-Node" || child.name == "RightDecor-Node" || child.name == "TopDecor-Node") // CHANGE TO ELSE ???
                {
                    // Add to decorations list

                    NodePrimitive np = cn.PrimitiveList[0].GetComponent<NodePrimitive>();
                    Transform decor_transform = np.gameObject.transform;

                    mDecorationTransforms.Add(decor_transform);
                    mOrigionalDecorationRotations.Add(decor_transform.eulerAngles);

                }
            }
        }
    }

    void SelectionChange(int index)
    {
        XformControl.SetSelectedObject(mSelectedTransform[index]);

        // previous scene node (if any) axis frame is set to null.
        // Get Scene node component from new transform.GameObject
        // Set that scene nodes axis frame to axis frame transform.
        // set previous scene node
        previousSceneNode.AxisFrame = null;
        SceneNode cn = mSelectedTransform[index].GetComponent<SceneNode>();
        cn.AxisFrame = SelectedObjAxisFrame;
        previousSceneNode = cn;

        
        // CameraManipulation cm = MainCamera.GetComponent<CameraManipulation>();
        // cm.SelectedObject = mSelectedTransform[index];

    }
}
