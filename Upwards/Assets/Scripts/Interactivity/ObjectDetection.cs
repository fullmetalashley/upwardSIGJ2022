using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Detect whether or not the player has entered the area for this object.
//Corresponds to an object with at least two colliders attached--one as a trigger for detection.
public class ObjectDetection : MonoBehaviour
{
    public bool inArea;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inArea = false;
        }
    }
}
