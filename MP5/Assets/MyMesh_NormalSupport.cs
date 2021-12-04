using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyMesh : MonoBehaviour {
    LineSegment[] mNormals;

    // create cylinder w/ normal, set parent as mesh
    void InitNormals(Vector3[] v, Vector3[] n)
    {
        mNormals = new LineSegment[v.Length];
        for (int i = 0; i < v.Length; i++)
        {
            GameObject o = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            mNormals[i] = o.AddComponent<LineSegment>();
            mNormals[i].SetWidth(0.05f);
            mNormals[i].transform.SetParent(this.transform);
        }
        UpdateNormals(v, n);
    }

    void UpdateNormals(Vector3[] v, Vector3[] n)
    {
        for (int i = 0; i < v.Length; i++)
        {
            mNormals[i].SetEndPoints(v[i], v[i] + 1.0f * n[i]);
        }
    }

    Vector3 FaceNormal(Vector3[] v, int i0, int i1, int i2)
    {
        Vector3 a = v[i1] - v[i0];
        Vector3 b = v[i2] - v[i0];
        return Vector3.Cross(a, b).normalized;
    }

    void ComputeNormals(Vector3[] v, Vector3[] n, int[] t)
    {
        Vector3[] triNormal = new Vector3[numTriangles];

        for (int o = 0; o < numTriangles; o++)
        {
            int row_num = o/trianglesPerRow;
            int pos_in_lst = o/2;

            int first = 0;
            int second = 0;
            int third = 0;

            if(o%2 == 0){ // even
                first = verticesPerRow + row_num + pos_in_lst;
                second = first + 1;
                third = pos_in_lst + row_num;
                triNormal[o] = FaceNormal(v, first, second, third);
            }
            else // odd
            {
                first = pos_in_lst + row_num;
                second = first + verticesPerRow + 1;
                third = first + 1;
                triNormal[o] = FaceNormal(v, first, second, third);
            }

            // Debug.Log( "T" + o.ToString() + " 1 - = " + first.ToString());
            // Debug.Log( "T" + o.ToString() + " 2 - = " + second.ToString());
            // Debug.Log( "T" + o.ToString() + " 3 - = " + third.ToString());
        }


        // given 2x2 mesh
        // Vector3[] triNormal = new Vector3[8];
        // triNormal[0] = FaceNormal(v, 3, 4, 0);
        // triNormal[1] = FaceNormal(v, 0, 4, 1);
        // triNormal[2] = FaceNormal(v, 4, 5, 1);
        // triNormal[3] = FaceNormal(v, 1, 5, 2);
        // triNormal[4] = FaceNormal(v, 6, 7, 3);
        // triNormal[5] = FaceNormal(v, 3, 7, 4);
        // triNormal[6] = FaceNormal(v, 7, 8, 4);
        // triNormal[7] = FaceNormal(v, 4, 8, 5);

        // n[0] = (triNormal[0] + triNormal[1]).normalized;
        // n[1] = (triNormal[1] + triNormal[2] + triNormal[3]).normalized;
        // n[2] = triNormal[3].normalized;
        // n[3] = (triNormal[0] + triNormal[4] + triNormal[5]).normalized;
        // n[4] = (triNormal[0] + triNormal[1] + triNormal[2] + triNormal[5] + triNormal[6] + triNormal[7]).normalized;
        // n[5] = (triNormal[2] + triNormal[3]).normalized;
        // n[6] = triNormal[4].normalized;
        // n[7] = (triNormal[4] + triNormal[5] + triNormal[6]).normalized;
        // n[8] = (triNormal[6] + triNormal[7]).normalized;
        // UpdateNormals(v, n);


        // Algorithm : include all triangles with vertex
        // Steps: go through vertices, go through triangles, if triangle has vertex, include triangle

        for (int p = 0; p < verticesNum; p++){ // cur vertex
            // List<Vector3> averageNormals = new List<Vector3>();
            Vector3 averageNormals = Vector3.zero;
            
            for(int q = 0; q < numTriangles; q++){ // cur triangle
                
                if (t[q*3+0] == p){ // vertex 0 in cur trianlge
                    // averageNormals.Add(triNormal[q]); // add triangle to lst
                    averageNormals += triNormal[q];
                }
                else if (t[q*3+1] == p){ 
                    // averageNormals.Add(triNormal[q]);
                    averageNormals += triNormal[q];
                }
                else if (t[q*3+2] == p){ 
                    // averageNormals.Add(triNormal[q]);
                    averageNormals += triNormal[q];
                }
            }
            
            n[p] = -averageNormals.normalized;
        }
        UpdateNormals(v, n);
    }
}
