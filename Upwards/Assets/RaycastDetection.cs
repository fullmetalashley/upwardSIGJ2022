using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Detects what objects were clicked on using raycasting.
//Attached to the camera.
public class RaycastDetection : MonoBehaviour
{
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        DetectObject();
    }

    public void DetectObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Object detected: " + hit.collider.gameObject.name);
            }

            /*Saving this for when we need to detect multiple objects in one click.
             * RaycastHit[] hits = Physics.RaycastAll(ray, 100f);

            // For each object that the raycast hits.
            foreach (RaycastHit hit in hits) {

            if (hit.collider.CompareTag("Test")) {
            // Do something.
            } else if (hit.collider.CompareTag("Test1")) {
            // Do something.
            } else if (hit.collider.CompareTag("Test2")) {
            // Do something.
            }
            */
        }
    }
}
