using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshBoundTracker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GetComponent<MeshFilter>().mesh.bounds);
    }

}
