using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    // Update is called once per frame
    [Header("Cameras")]
    public Camera mainCam;
    public Camera portalCam;

    void Update()
    {

        // Move portal camera position based on main camera distance from the portal.
        Vector3 cameraOffset = mainCam.transform.position - transform.position;
        portalCam.transform.position = transform.position + cameraOffset;

        // Make portal cam face the same direction as the main camera.
        portalCam.transform.rotation = Quaternion.LookRotation(mainCam.transform.forward, Vector3.up);
    }
}