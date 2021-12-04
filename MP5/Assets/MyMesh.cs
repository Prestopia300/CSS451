﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyMesh : MonoBehaviour {

    int cylinderRes = 8;
    int verticesPerRow = 8;
    int mesh_num = 7;

    int numTriangles;
    int trianglesPerRow;
    int verticesNum;

	// Use this for initialization
	void Start () {
        
        // For 2x2 Mesh
        // int cylinderRes = 3;
        // int verticesPerRow = 3;
        // int mesh_num = 2;
        // int cylinderRotation = 360;

        numTriangles = (cylinderRes-1)*(cylinderRes-1)*2; // Number of triangles: 2x2 mesh and 2x triangles on each mesh-unit
        trianglesPerRow = mesh_num*2;
        verticesNum = cylinderRes*cylinderRes; // 2x2 mesh needs 3x3 vertices

        Mesh theMesh = GetComponent<MeshFilter>().mesh;   // get the mesh component
        theMesh.Clear();    // delete whatever is there!!

        Vector3[] v = new Vector3[verticesNum]; // 9   
        int[] t = new int[numTriangles*3]; // 8*3         
        Vector3[] n = new Vector3[verticesNum]; // 9


        // 3x3 vectors for 2x2 mesh

        // 4x4 vectors for 3x3 mesh
        // lets say the mesh must be size 10x10 pixles,
        // to get this, we need to do find num vectors per row,
        // then find distance between each, which would be 10/(mesh_num=3)
        // set first pixle as -5,-5, and last pixle as 5,5,
        // and for each go x distance, so it would look like:

        // -5,0,-5                -5+(10/3),0,-5                   -5+(10/3)+(10/3),0,-5
        // -5,0,-5+(10/3)         -5+(10/3),0,-5+(10/3)            -5+(10/3)+(10/3),0,-5+(10/3)
        // -5,0,-5+(10/3)+(10/3)  -5+(10/3),0,-5+(10/3)+(10/3)     -5+(10/3)+(10/3),0,-5+(10/3)+(10/3)

        // v
        float quad_size_n = 3f;
        float deltaDis = quad_size_n/mesh_num;
        float kStartPos = -0.5f*quad_size_n;

        for (int i = 0; i < cylinderRes; i++)
        {
            for (int j = 0; j < cylinderRes; j++)
            {
                v[i*cylinderRes + j] = new Vector3(kStartPos+deltaDis*i,0, kStartPos+deltaDis*j);
                int temp_index = i*cylinderRes + j;
                // Debug.Log(temp_index.ToString() + " = " + v[i*cylinderRes + j]);

            }
        }

        // n
        for (int k = 0; k < verticesNum; k++)
        {
            n[k] = new Vector3(0, 1, 0);
        }

        // t
        for (int l = 0; l < numTriangles; l++)
        {
            int row_num = l/trianglesPerRow;
            int pos_in_lst = l/2;
            // Debug.Log( "pos_in_lst = " + pos_in_lst);

            if(l%2 == 0){ // even
                t[l*3+0] = row_num + pos_in_lst; // row # + triangle pos in even list
                t[l*3+1] = (row_num + pos_in_lst) + verticesPerRow; // 1st + vertices per row
                t[l*3+2] = ((row_num + pos_in_lst) + verticesPerRow) + 1; // 2nd + 1
            }
            else // odd
            {
                t[l*3+0] = row_num + pos_in_lst; // row # + triangle pos in odd list
                t[l*3+1] = (row_num + pos_in_lst) + verticesPerRow + 1; // 1st + vertices per row + 1
                t[l*3+2] = (row_num + pos_in_lst) + 1; // 1st + 1
            }

            Debug.Log( (l*3+0).ToString() + " = " + t[l*3+0]);
            Debug.Log( (l*3+1).ToString() + " = " + t[l*3+1]);
            Debug.Log( (l*3+2).ToString() + " = " + t[l*3+2]);

        }

        // v[0] = new Vector3(-1, 0, -1);
        // v[1] = new Vector3( 0, 0, -1);
        // v[2] = new Vector3( 1, 0, -1);

        // v[3] = new Vector3(-1, 0, 0);
        // v[4] = new Vector3( 0, 0, 0);
        // v[5] = new Vector3( 1, 0, 0);

        // v[6] = new Vector3(-1, 0, 1);
        // v[7] = new Vector3( 0, 0, 1);
        // v[8] = new Vector3( 1, 0, 1);


        // n[0] = new Vector3(0, 1, 0);
        // n[1] = new Vector3(0, 1, 0);
        // n[2] = new Vector3(0, 1, 0);
        // n[3] = new Vector3(0, 1, 0);
        // n[4] = new Vector3(0, 1, 0);
        // n[5] = new Vector3(0, 1, 0);
        // n[6] = new Vector3(0, 1, 0);
        // n[7] = new Vector3(0, 1, 0);
        // n[8] = new Vector3(0, 1, 0);


        // // First triangle
        // t[0] = 0; t[1] = 3; t[2] = 4;  // 0th triangle
        // t[3] = 0; t[4] = 4; t[5] = 1;  // 1st triangle

        // t[6] = 1; t[7] = 4; t[8] = 5;  // 2nd triangle
        // t[9] = 1; t[10] = 5; t[11] = 2;  // 3rd triangle

        // t[12] = 3; t[13] = 6; t[14] = 7;  // 4th triangle
        // t[15] = 3; t[16] = 7; t[17] = 4;  // 5th triangle

        // t[18] = 4; t[19] = 7; t[20] = 8;  // 6th triangle
        // t[21] = 4; t[22] = 8; t[23] = 5;  // 7th triangle

        theMesh.vertices = v; //  new Vector3[3];
        theMesh.triangles = t; //  new int[3];
        theMesh.normals = n;

        InitControllers(v);
        InitNormals(v, n);

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
        Mesh theMesh = GetComponent<MeshFilter>().mesh;
        Vector3[] v = theMesh.vertices;
        Vector3[] n = theMesh.normals;
        int[] t = theMesh.triangles;
        for (int m = 0; m<mControllers.Length; m++)
        {
            v[m] = mControllers[m].transform.localPosition;
        }
        ComputeNormals(v, n, t);

        theMesh.vertices = v;
        theMesh.normals = n;
	}
}
