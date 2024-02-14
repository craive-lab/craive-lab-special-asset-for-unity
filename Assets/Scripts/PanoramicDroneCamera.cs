using UnityEngine;
using System.Collections;

public class PanoramicDroneCamera : MonoBehaviour
{

    GameObject[] subCameras;

    int cameraCount;

    private void Awake()
    {
        cameraCount = transform.childCount;
        Debug.Log(cameraCount);
    }

    private void Start()
    {
       subCameras = new GameObject[cameraCount];
       for (int i = 0; i < cameraCount; i++)
        {
            subCameras[i] = transform.GetChild(i).gameObject;
            if (subCameras[i].activeSelf == false) 
                subCameras[i].SetActive(true);
        }
    }

}
