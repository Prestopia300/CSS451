using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyMesh : MonoBehaviour
{
    void SetRes(float res)
    {
        resolution = (int)res;
        SetVerticies(res);
    }

    
}
