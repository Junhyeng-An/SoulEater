using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimp_Camera_Size : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera targetCamera = FindCameraByName("MinimapCamera");

        targetCamera.orthographicSize  = 300;


    }

    
    Camera FindCameraByName(string cameraName)
    {
        // 모든 카메라 찾기
        Camera[] cameras = Camera.allCameras;

        // 주어진 이름과 일치하는 카메라 찾기
        foreach (Camera camera in cameras)
        {
            if (camera.name == cameraName)
            {
                return camera;
            }
        }

        // 일치하는 카메라를 찾지 못했을 경우
        return null;
    }
   
}
