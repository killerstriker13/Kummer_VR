using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LockPlane;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;


public class SceneThreeController : GameController
{

    public SurfaceControl surfaceControl;
    public TextController rightHandText;
    public GameObject hand;
    public GameObject Marker;
    public Surface surface;

    public SlicePlane slicePlane;
    private readonly Renderer ren;
    private Renderer scren;

    private Vector3 rightHandPosition;
    private readonly Vector3 axisOffset = new Vector3 (0f, 1.3f, 0f);
    private readonly float axisSize = .5f;
    private float x;
    private float y;
    private float z;
    private float rightFunctionValue;
    private GameObject rightCanvas;

    private GameObject marker;
    private List<GameObject> markers = new List<GameObject>();
    public Marker newMarkers;
    private string step = "intro_1";
//    private string step = "exercise 2b";
    private string substep = "";
    private float time;
    private bool timer = false;
    private Vector3 difference;
    private Vector3 target;
    private Vector3 target1 = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 target2 = new Vector3(1.0f, 0.0f, 0.0f);
    private Vector3 target3 = new Vector3(0.0f, 0.0f, 1.0f);
    private Vector3 target4 = new Vector3(1.0f, 2.0f, -1.0f);
    private float wait = 2.0f;
    private bool pause = false;
    public InputActionProperty rightTrigger;
    public InputActionProperty leftTrigger;

    public void Awake()
    {
        messageControl.Initialize("03");
        surfaceControl.InitializeSurfaces();
        GetCanvases();
        SetCanvasPositions(new Vector3(-1.16f, 0.38f, -0.76f), new Vector3(-1.3f, 0.2f, -0.7f));
        scren = sceneChanger.GetComponent<Renderer>();
        scren.enabled = false;

    }


    public void Update()
    {

        TriggerForOlderMessages();

        if (pause)
        {
            return;
        }
        switch (step)
        {
            case "intro_1":
                if (Elapsed())
                {
                    wait = messageControl.Play(1);
                    step = "intro_2";
                }
                break;

            case "intro_2":
                if (Elapsed())
                {
                    wait = messageControl.Play(2);
                    step = "pause";
                }
                break;
            case "pause":
                DisplayHandCoordinates();
                if (substep != "")
                {
                    if (Elapsed())
                    {
                        step = "exercise 1a";
                    }
                }
                else
                {
                    if (Elapsed())
                    {
                        step = "exercise 1a";
                        substep = "first target";
                    }
                }
                break;
            case "exercise 1a":
                DisplayHandCoordinates();
                switch (substep)
                {
                    case ("first target"):
                        wait = messageControl.Play(3);
                        break;
                    case ("second target"):
                        wait = messageControl.Play(4);
                        break;
                    case ("third target"):
                        wait = messageControl.Play(5);
                        break;
                    case ("fourth target"):
                        wait = messageControl.Play(6);
                        break;
                }
                step = "exercise 1b";
                break;

            case "exercise 1b":
                DisplayHandCoordinates();
                switch (substep)
                {
                    case ("first target"):
                        target = target1;
                        break;
                    case ("second target"):
                        target = target2;
                        break;
                    case ("third target"):
                        target = target3;
                        break;
                    case ("fourth target"):
                        target = target4;
                        break;
                }
                difference = VectorConvert(hand.transform.position - axisOffset, 20.0f) - target;
                if (difference.magnitude < 0.1f)
                {
                    marker = Instantiate(Marker, VectorConvert(target, 1000.0f, true) + axisOffset, Quaternion.identity);
                    marker.transform.localScale = new Vector3(.02f, .02f, .02f);
                    markers.Add(marker);

                    if (substep == "fourth target")
                    {
                        wait = messageControl.Play(13);
                        step = "move canvas";
                    }
                    else
                    {
                        wait = messageControl.Play(7);
                        step = "pause";
                        switch (substep)
                        {
                            case "first target":
                                substep = "second target";
                                break;
                            case "second target":
                                substep = "third target";
                                break;
                            case "third target":
                                substep = "fourth target";
                                break;
                        }
                    }
                }
                break;

            case "move canvas":
                SetCanvasPositions(new Vector3(-1.16f, 0.8f, -1.1f), new Vector3(-1.3f, 0.62f, -1.09f));
                step = "exercise 2a";
                break;

            case "exercise 2a":
                if (Elapsed())
                {
                    for (int i = 0; i < markers.Count; i++)
                    {
                        Destroy(markers[i]);
                    }
                    markers.Clear();
                    wait = messageControl.Play(8);
                    step = "exercise 2b";
                }
                break;

            case "exercise 2b":
                DisplayFunctionValue();
                if (Elapsed())
                {
                    wait = messageControl.Play(9);
                    step = "exercise 2c";
                }
                break;

            case "exercise 2c":
                DisplayFunctionValue();
                if (Function(VectorConvert(hand.transform.position - axisOffset, 100.0f)) == 1.0f)
                {
                    wait = messageControl.Play(10);
                    newMarkers.CreateMarker(hand.transform.position-axisOffset);
                    step = "exercise 2c2";
                    time = Time.fixedTime;
                }
                break;

            case "exercise 2c2": //this makes the user wait until the message has been stated before trying to find more points to mark
                DisplayFunctionValue();
                if (Elapsed())
                {
                    step = "exercise 2d";
                }
                break;

            case "exercise 2d":
                DisplayFunctionValue();

                if (newMarkers.Count() == 50)
                {
                    wait = messageControl.Play(11);
                    step = "prereveal";
                    break;
                }

                if (Time.fixedTime > time + 0.2f)
                {
                    if (Function(VectorConvert(hand.transform.position - axisOffset, 1000.0f)) == 1.0f)
                    {
                        newMarkers.CreateMarker(hand.transform.position - axisOffset);
                        if (newMarkers.Count() == 15)
                        {
                            wait = messageControl.Play(14);
                        }

          
                        time = Time.fixedTime;
                    }
                }
                break;

            case "prereveal":
                DisplayFunctionValue();
                if (Elapsed())
                {
                    wait = messageControl.Play(12);
                    step = "reveal";
                }
                break;

            case "reveal":
                DisplayFunctionValue();
                if (Elapsed())
                {                    
                    wait = messageControl.Play(15);
                    surface.ChangeVisibility();
                    step = "viewing lens";
                }
                break;

            case "viewing lens":
                DisplayFunctionValue();
                if (Elapsed())
                {
                    newMarkers.DestroyMarkers();
                    wait = messageControl.Play(16);
                    slicePlane.ChangeVisibility();
                    step = "wait for grab";
                }
                break;

            case "wait for grab":
                DisplayFunctionValue();
                break;

            case "lens on surface":
                rightHandText.NewText("");
                if (Elapsed())
                {
                    wait = messageControl.Play(18);
                    step = "lens on surface 2";
                }
                break;

            case "lens on surface 2":
                if (Elapsed())
                {
                    wait = messageControl.Play(19);
                    step = "lens on surface 3";
                }
                break;

            case "lens on surface 3":
                SetCanvasPositions(new Vector3(-1.16f, 0.8f, -1.1f), new Vector3(-1.3f, 0.98f, -1.86f));
                DisplaySliceValue();
                if (Elapsed())
                {
                    wait = messageControl.Play(20);
                    step = "lens on surface 4";
                }
                break;

            case "lens on surface 4":
                DisplaySliceValue();
                if (Elapsed())
                {
                    wait = messageControl.Play(21);
                    step = "lens on surface 5";
                }
                break;

            case "lens on surface 5":
                DisplaySliceValue();
                if (Elapsed())
                {
                    wait = messageControl.Play(22);
                    step = "time to investigate";
                }
                break;


            case "time to investigate":

                DisplaySliceValue();
                if (Elapsed())
                {
                    wait = messageControl.Play(23);
                    slicePlane.gameObject.GetComponent<XRGrabInteractable>().trackRotation = true;
                    slicePlane.transform.SetPositionAndRotation(new Vector3(0, 1.5f, 0), Quaternion.AngleAxis(71, new Vector3(1, 5, 11)));
                    step = "prepare for next scene";
                }
                break;

            case "prepare for next scene":

                DisplaySliceValue();
                if (Elapsed())
                {
                    wait = messageControl.Play(24);
                    step = "next scene";
                }
                break;

            case "next scene":

                scren.enabled = true;
                this.Ready("Fourth Scene");
                step = "finished";
                break;
        }
    }




    private Vector3 VectorConvert(Vector3 In, float round, bool inverse = false) //set round to false if you want the unrounded vector.  It's best to plug the exact vector into the function before rounding.
    {
        float fac;
        if (inverse)
        {
            fac = axisSize / 3.0f;
        }
        else
        {
            fac = 3.0f / axisSize;
        }
        return new Vector3(Round(fac * In.x, round), Round(fac * In.z, round), Round(fac * In.y, round));
    }

    private float Round(float x, float part)
    {
        return Mathf.Round(x * part) * 1.0f / part;
    }

    private void DisplayHandCoordinates()
    {
        rightHandPosition = VectorConvert(hand.transform.position - axisOffset, 20.0f);
        rightHandText.NewText(rightHandPosition.ToString());
    }


    private void DisplayFunctionValue()
    {
        rightHandPosition = VectorConvert(hand.transform.position - axisOffset, 20.0f);
        rightFunctionValue = Function(VectorConvert(hand.transform.position - axisOffset, 1000.0f));
        rightHandText.NewText(rightHandPosition.ToString() + "\n x<sup>2</sup> + y<sup>2</sup> - z<sup>2</sup> = " + rightFunctionValue.ToString());
    }

    private void DisplaySliceValue()
    {
        z = (VectorConvert(slicePlane.transform.position - axisOffset,100.0f).z);
        rightHandText.NewText("<pos=20%>z <indent=27%>= " + z.ToString() + "</indent>\n x<sup>2</sup> + y<sup>2</sup> <indent=27%>= 1 + z<sup>2</sup></indent>\n <indent=27%>= " + Round((1 + z * z), 100.0f).ToString() + "</indent>");
    }

    public void GetCanvases()
    {
        int rightNumberOfChildren = hand.transform.childCount;
        for (int i = 0; i < rightNumberOfChildren; i++)
        {
            rightCanvas = hand.transform.GetChild(i).gameObject;
            if (rightCanvas.CompareTag("canvas"))
            {
                break;
            }
        }
    }

    private void SetCanvasPositions(Vector3 posLeft, Vector3 posRight)
    {
        rightCanvas.transform.localPosition = posRight;
    }

    private float Function(Vector3 pos)
    {
        x = pos.x;
        y = pos.y;
        z = pos.z;
        return Round(x * x + y * y - z * z, 20.0f);
    }

    public void Grabbed()
    {
        if (step == "wait for grab")
        {
            wait = messageControl.Play(17);
            time = Time.fixedTime;
            step = "lens on surface";
        }
    }



    public bool Elapsed()
    {
        if (timer == false)
        {
            timer = true;
            time = Time.fixedTime;
            return false;
        }
        else
        {
            if (Time.fixedTime > time + wait)
            {
                timer = false;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private void TriggerForOlderMessages()
    {
        if (rightTrigger.action.triggered)
        {
            pause = messageControl.ShowNewer();
        }
        if (leftTrigger.action.triggered)
        {
            pause = messageControl.ShowOlder(pause);
        }
        return;
    }

}