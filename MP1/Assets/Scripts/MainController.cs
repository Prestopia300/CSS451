using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainController : MonoBehaviour
{
    public GameObject PlatformQuad;
    public GameObject BlackBall;

    public GameObject myPrefab1;
    public GameObject myPrefab2;
    public GameObject myPrefab3;
    public GameObject myPrefab4;

    public Dropdown m_Dropdown;

    Vector3 newPosition;

    public GameObject[] _list;

    CylinderMovement script1;
    CylinderMovement script2;

    // Start is called before the first frame update
    void Start()
    {
        // DROPDOWN
        //m_Dropdown = transform.GetComponent<Dropdown>();       
        m_Dropdown.options.Clear();

        List<string> items = new List<string>();
        items.Add("Object To Create");
        items.Add("Sphere");
        items.Add("Cube");
        items.Add("Cylinder");
        items.Add("Ocilating Sphere");

        // Fill dropdown with items
        foreach (var item in items)
        {
            m_Dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }

        m_Dropdown.onValueChanged.AddListener(delegate {
            DropdownItemSelected(m_Dropdown);
        });


        // MOVE ON CLICK
        newPosition = BlackBall.transform.position;

        // CYLINDER COLISIONS
        _list = GameObject.FindGameObjectsWithTag("MovingCylinder");
    }

    // Update is called once per frame
    void Update()
    {
        DeleteOnClick();

        MoveOnClick();

        manageColisions(_list);

    }



    void DeleteOnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // if object is not off limits
                if (hit.collider.gameObject != PlatformQuad && hit.collider.gameObject != BlackBall)
                {
                    Destroy(hit.collider.gameObject);
                    // print("Destroyed object at " + Input.mousePosition);
                }

            }
        }
    }

    void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;

        if (index == 1)
        {
            // myPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/MovingSphere.prefab", typeof(GameObject));
            Instantiate(myPrefab1, new Vector3(BlackBall.transform.position.x, BlackBall.transform.position.y, BlackBall.transform.position.z), Quaternion.identity);
        }
        else if (index == 2)
        {
            Instantiate(myPrefab2, new Vector3(BlackBall.transform.position.x, BlackBall.transform.position.y, BlackBall.transform.position.z), Quaternion.identity);
        }
        else if (index == 3)
        {
            Instantiate(myPrefab3, new Vector3(BlackBall.transform.position.x, BlackBall.transform.position.y, BlackBall.transform.position.z), Quaternion.identity);
        }
        else if (index == 4)
        {
            Instantiate(myPrefab4, new Vector3(BlackBall.transform.position.x, BlackBall.transform.position.y, BlackBall.transform.position.z), Quaternion.identity);
        }

        // Reset Selected Option to default ("Object To Create")
        dropdown.value = 0;
    }

    void MoveOnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                // if object is ball or platform
                if (hit.collider.gameObject == PlatformQuad || hit.collider.gameObject == BlackBall)
                {
                    // Move Ball
                    newPosition = hit.point;
                    // newPosition.y = 0.25;
                    newPosition = new Vector3(newPosition.x, 0.25f, newPosition.z);
                    BlackBall.transform.position = newPosition;
                }
            }
        }
    }


    void manageColisions(GameObject[] _list)
    {
        int n = _list.Length;

        for (int i = 0; i < n; i++)
        {
            for (int p = 0; p < n; p++)
            {
                if (i != p)
                {
                    if (_list[i].transform.position == _list[p].transform.position)
                    {
                        // Change Obj Directions
                        script1 = _list[i].GetComponent<CylinderMovement>();
                        script1.dirForward = !script1.dirForward;

                        script2 = _list[i].GetComponent<CylinderMovement>();
                        script2.dirForward = !script2.dirForward;
                    }
                }
            }
        }
    }



}
