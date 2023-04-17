using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolderPosition : MonoBehaviour
{
    public Transform cameraPosition;

    private void Update()
    {
        transform.position = cameraPosition.transform.position;
    }
}
