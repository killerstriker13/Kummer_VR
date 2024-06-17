using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    private Vector3 pos;
    private Vector3 down;
    private int function;
    private SurfaceControl surfaceControl;

    void Start()
    {
        pos = transform.position;
        down = new Vector3(0, -.01f, 0);
        function = transform.parent.gameObject.GetComponent<Surface>().function;
    }

    private void OnTriggerEnter(Collider other)
    {
        surfaceControl = transform.parent.gameObject.transform.parent.gameObject.GetComponent<SurfaceControl>();
        if (other.gameObject.CompareTag("hand"))
        {
            transform.position = pos + down;
            //      transform.parent.gameObject.GetComponent<Surface>().ChangeVisibility();
            surfaceControl.setFunction(function);
            surfaceControl.buttonStatus(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("hand"))
        {
            surfaceControl = transform.parent.gameObject.transform.parent.gameObject.GetComponent<SurfaceControl>();
            transform.position = pos;
            surfaceControl.buttonStatus(false);
        }
    }
}


