using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject cameraTarget;
    private float cameraPositionX;
    private float cameraPositionY;
    public float lowestPoint = 0;
    public float mostRightPoint = 0;

    void Start()
    {
        
    }

    void Update()
    {
        cameraPosition();
    }

    private void cameraPosition()
    {
        if (cameraTarget.transform.position.y > lowestPoint)
        {
            cameraPositionY= cameraTarget.transform.position.y;
        }
        else
        {
            cameraPositionY = lowestPoint;
        }


        if (cameraTarget.transform.position.x > mostRightPoint)
        {
            cameraPositionX = cameraTarget.transform.position.x;
        }
        else
        {
            cameraPositionX = mostRightPoint;
        }

        this.transform.position = new Vector3(cameraPositionX, cameraPositionY, this.transform.position.z);
    }
}