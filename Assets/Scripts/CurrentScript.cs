using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CurrentScript : MonoBehaviour
{
    public float currentSpeed;
    private UnityEngine.Vector3 currentDirection;
    private UnityEngine.Quaternion currentRotation;

    void Start()
    {
        currentRotation = UnityEngine.Quaternion.Euler(0, 0, this.transform.rotation.eulerAngles.z);
        //UnityEngine.Debug.Log(rotation);
    }

    void Update()
    {
        currentDirection = currentRotation * UnityEngine.Vector3.up * currentSpeed;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.attachedRigidbody.AddForce(currentDirection);
            //UnityEngine.Debug.Log(direction);
        }
    }
}
