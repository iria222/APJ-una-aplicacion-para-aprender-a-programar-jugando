using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRectManager : MonoBehaviour
{
    private Camera mainCamera;
    private Camera secondaryCamera;


    private void Awake()
    {
        mainCamera = Camera.main;
        secondaryCamera = GetComponent<Camera>();
        Rect rect = secondaryCamera.rect;
    }
  

    // Update is called once per frame
    void Update()
    {
        secondaryCamera.rect = mainCamera.rect;
    }
}
