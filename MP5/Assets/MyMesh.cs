using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyMesh : MonoBehaviour {

    public SliderWithEcho ResSlider;

    public Vector3 UVt = Vector3.zero;
    public Vector3 UVs = Vector3.one;
    public float UVr = 0f;

    public const int MinRes = 2;
    public const int MaxRes = 20;

    int resolution = 5;

    int numTriangles;
    int trianglesPerRow;
    int verticesNum;

    Vector3[] vertices;
    Vector3[] normals;
    int[] triangles;

    Vector2[] origUV;
    Vector2[] uv;

    Mesh theMesh;

    bool isCylinder = false;
    
    // void Start() {
    //     theMesh = GetComponent<MeshFilter>().mesh;
    //     Debug.Assert(theMesh != null);

    //     ResSlider.InitSliderRange(MinRes, MaxRes, 5);

    //     ResSlider.SetSliderListener(SetRes);  

    //     SetVerticies(resolution);
    // }

	// Use this for initialization
	void Start () {
        theMesh = GetComponent<MeshFilter>().mesh;
        Debug.Assert(theMesh != null);

        

        vertices = new Vector3[verticesNum]; // 9   
        triangles = new int[numTriangles*3]; // 8*3         
        normals = new Vector3[verticesNum]; // 9

        InitControllers(vertices);
        InitNormals(vertices, normals);

        CalculateEverything();

        ToggleController(false);

        ResSlider.InitSliderRange(MinRes, MaxRes, 5);

        ResSlider.SetSliderListener(SetRes);

        UVs = Vector3.one;
        UVt = Vector3.zero;
        UVr = 0f;
        FindObjectOfType<XformControl>().SetSelectedObject(this);
        #region define a circle
        //{
        //    Vector3 initSize = new Vector3(0.2f, 0.2f, 0.2f);
        //    Vector3 p;
        //    const int kNumVertex = 30;
        //    const float kDeltaTheta = (360f / kNumVertex) * Mathf.Deg2Rad;  // dTheta in randian
        //    const float kRadius = 5f;
        //    for (int i = 0; i < kNumVertex; i++)
        //    {
        //        GameObject s = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //        s.transform.localScale = initSize;
        //        p.x = kRadius * Mathf.Cos(i * kDeltaTheta);
        //        p.y = 0f;
        //        p.z = kRadius * Mathf.Sin(i * kDeltaTheta);
        //        s.transform.localPosition = p;
        //    }
        //}
        #endregion 

    }

    // Update is called once per frame
    void Update () {
        vertices = theMesh.vertices;
        normals = theMesh.normals;
        triangles = theMesh.triangles;
        for (int m = 0; m<mControllers.Length; m++)
        {
            vertices[m] = mControllers[m].transform.localPosition;
        }
        ComputeNormals(vertices, normals, triangles);

        theMesh.vertices = vertices;
        theMesh.normals = normals;

        if(!isCylinder)
        {
            Vector2[] oldUV = theMesh.uv;

            ComputeUV(ref oldUV);
            theMesh.uv = oldUV;
        }
	}

    void CalculateEverything()
    {
        theMesh.Clear();
        ClearControllers();
        ClearNormals();
        //AxisFrame.gameObject.SetActive(false);
        //isActive = false;

        numTriangles = (resolution-1)*(resolution-1)*2; // Number of triangles: 2x2 mesh and 2x triangles on each mesh-unit
        trianglesPerRow = (resolution - 1)*2;
        verticesNum = resolution*resolution; // 2x2 mesh needs 3x3 vertices

        vertices = new Vector3[resolution * resolution];
        normals = new Vector3[resolution * resolution];
        triangles = new int[numTriangles*3]; // 8*3   

        uv = new Vector2[resolution * resolution];
        origUV = new Vector2[resolution * resolution];

        float quad_size_n = 3f;
        float deltaDis = quad_size_n/(resolution - 1);
        float kStartPos = -0.5f*quad_size_n;

        for (int i = 0; i < resolution; i++)
        {
            for (int j = 0; j < resolution; j++)
            {
                vertices[i*resolution + j] = new Vector3(kStartPos+deltaDis*i,0, kStartPos+deltaDis*j);
                int temp_index = i*resolution + j;
                // Debug.Log(temp_index.ToString() + " = " + v[i*cylinderRes + j]);

            }
        }

        for (int k = 0; k < verticesNum; k++)
        {
            normals[k] = new Vector3(0, 1, 0);
        }

        for (int l = 0; l < numTriangles; l++)
        {
            int row_num = l/trianglesPerRow;
            int pos_in_lst = l/2;
            // Debug.Log( "pos_in_lst = " + pos_in_lst);

            if(l%2 == 0){ // even
                triangles[l*3+0] = row_num + pos_in_lst; // row # + triangle pos in even list
                triangles[l*3+1] = (row_num + pos_in_lst) + resolution; // 1st + vertices per row
                triangles[l*3+2] = ((row_num + pos_in_lst) + resolution) + 1; // 2nd + 1
            }
            else // odd
            {
                triangles[l*3+0] = row_num + pos_in_lst; // row # + triangle pos in odd list
                triangles[l*3+1] = (row_num + pos_in_lst) + resolution + 1; // 1st + vertices per row + 1
                triangles[l*3+2] = (row_num + pos_in_lst) + 1; // 1st + 1
            }

            //Debug.Log( (l*3+0).ToString() + " = " + triangles[l*3+0]);
            //Debug.Log( (l*3+1).ToString() + " = " + triangles[l*3+1]);
            //Debug.Log( (l*3+2).ToString() + " = " + triangles[l*3+2]);

        }

        for(int i = 0, k = 0; i < resolution; i++)
        {
            for(int j = 0; j < resolution; j++, k++)
            {
                uv[k] = new Vector2((float)j / (resolution - 1), (float)i / (resolution - 1));
                origUV[k] = uv[k];
            }
        }



        InitControllers(vertices);
        InitNormals(vertices, normals);

        for (int m = 0; m<mControllers.Length; m++)
        {
            vertices[m] = mControllers[m].transform.localPosition;
        }
        ComputeNormals(vertices, normals, triangles);

        theMesh.vertices = vertices; //  new Vector3[3];
        theMesh.triangles = triangles; //  new int[3];
        theMesh.normals = normals;
        theMesh.uv = uv;

        
    }

    void ClearControllers()
    {
        for(int i = 0; i < mControllers.Length; i++)
        {
            Destroy(mControllers[i].gameObject);
        }
    }

    void ClearNormals()
    {
        for(int i = 0 ; i < mNormals.Length; i++)
        {
            Destroy(mNormals[i].gameObject);
        }
    }

    void ComputeUV(ref Vector2[] uv)
    {
        Vector2 t = new Vector2(UVt.x, UVt.y);
        Vector2 s = new Vector2(UVs.x, UVs.y);
        float r = UVr;
        Matrix3x3 uvTRS = Matrix3x3Helpers.CreateTRS(t, r, s);

        for(int i = 0, y = 0; y < resolution; y++)
        {
            for(int x = 0; x < resolution; x++, i++)
            {
                uv[i] = Matrix3x3.MultiplyVector2(uvTRS, origUV[i]);
            }
        }
    }

    public void ToggleController(bool b)
    {
        foreach(GameObject g in mControllers)
        {
            g.SetActive(b);
        }

        foreach(var g in mNormals)
        {
            g.gameObject.SetActive(b);
        }

        //Debug.Log("Toggled" + b);
    }
}
