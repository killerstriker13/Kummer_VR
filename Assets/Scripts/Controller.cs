using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LockPlane;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using Unity.VisualScripting;

public class Controller : MonoBehaviour
{

    [SerializeField, Range(0, 20)] public float muSquared = 2.0f;
    public SurfaceControl surfaceControl;
    public Surface centralSurface;
    public SlicePlane slicePlane;
    public GameObject player;
    public NoiseGenerator noiseGenerator;
    public InputActionProperty rightTrigger;
    public InputActionProperty leftTrigger;
    public InputActionProperty rightGrip;
    public InputActionProperty leftGrip;
    private GameObject marker;
    private List<GameObject> markers = new List<GameObject>();
    public Marker newMarkers;
    Vector3 pos = new Vector3(0.1f, -0.1f, 0.1f);
    private int count = 0;
    




    // Start is called before the first frame update
    void Start()
    {
        centralSurface.Render();
        
    }

    // Update is called once per frame
    void Update()
    { 
        newMarkers.CreateMarker(pos);

        if (LeftGripCheck() || RightGripCheck()) {
            if (LeftGripCheck())
            {
                centralSurface.RenderLeft();
            }
            if (RightGripCheck())
            {
                centralSurface.RenderRight();
            }
        } else if (leftTrigger.action.triggered || rightTrigger.action.triggered)
        {
            if (leftTrigger.action.triggered)
            {
                centralSurface.RenderSmall();
            }

            if (rightTrigger.action.triggered)
            {
                centralSurface.RenderBig();
            }
        }
        else
        {
            centralSurface.Render();
        }
        if (count % 20 == 0)
        {
            newMarkers.DestroyMarkers();
        }
        count++;
    }

    private bool LeftGripCheck()
    {
        return (leftGrip.action.ReadValue<float>() > .01);

    }
    private bool RightGripCheck()
    {
        return (rightGrip.action.ReadValue<float>() > .01);

    }
}
