using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    public Camera cam;
    public GameObject cinemachine;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void cameraKapatVeBüyüt()
    {
        cam.fieldOfView = 85f;
        cinemachine.SetActive(false);

    }
}
