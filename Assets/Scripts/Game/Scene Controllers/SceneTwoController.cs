using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LockPlane;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneTwoController : GameController
{

    public int numFunctions = 8;
    public SurfaceControl surfaceControl;
    public Surface centralSurface;
    public SlicePlane slicePlane;


    private string step;
    private float time;
    private bool timer;
    private bool played;
    private Renderer scren;

    private List<int> randomFunctions;
    private int function;
    private int selectedFunction;

    private int function1;
    private int function2;
    private bool buttonDown;
    private float wait = 2.0f;
    private bool pause = false;
    public InputActionProperty rightTrigger;
    public InputActionProperty leftTrigger;

    public void Awake()
    {
        messageControl.Initialize("02");
        RandomFunctions(); //Selects functions for outer surfaces
        surfaceControl.SetFunctions(randomFunctions); //Sends information to SurfaceControl
        surfaceControl.InitializeSurfaces(); //SurfaceControl gets surfaces and sends information to surfaces
        function = Random.Range(0, numFunctions);  //choose the function to apply to the central stand
        function1 = function;
        centralSurface.function = function;
        scren = sceneChanger.GetComponent<Renderer>();
        scren.enabled = false;

    }
    public void Start()
    {
        time = Time.fixedTime;
        step = "introduction";
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
            case "introduction":
                if (Elapsed())
                {
                    wait = messageControl.Play(1);
                    step = "wait for grab";
                }
                break;

            //step==1 is taken care of in AdvanceToSecond

            case "find invisible surface":

                if (Elapsed())
                {
                    wait = messageControl.Play(3);
                    step = "first exercise";
                }
                break;

            case "first exercise":

                buttonDown = surfaceControl.checkButton();

                if (buttonDown)
                {
                    selectedFunction = surfaceControl.getFunction();

                    if (selectedFunction == function)
                    {
                        wait = messageControl.Play(5);
                        step = "second exercise 1";
                    }
                    else
                    {
                        wait = messageControl.Play(4);
                    }
                }
                break;

            case "second exercise 1":

                if (Elapsed())
                {
                    wait = messageControl.Play(6);
                    slicePlane.transform.SetPositionAndRotation(new Vector3(0, 1.5f, 0), Quaternion.AngleAxis(90, Vector3.forward));
                    while (function == function1)
                    {
                        function = Random.Range(0, 8);  //choose the function to apply to the central stand
                    }
                    function2 = function;
                    centralSurface.function = function;
                    centralSurface.Render();
                    step = "second exercise 2";
                }
                break;

            case "second exercise 2":

                if (Elapsed() && played==false)
                {
                    wait = messageControl.Play(7);
                    played = true;
                }

                buttonDown = surfaceControl.checkButton();

                if (buttonDown)
                {
                    selectedFunction = surfaceControl.getFunction();

                    if (selectedFunction == function)
                    {
                        wait = messageControl.Play(8);
                        step = "third exercise 1";
                        played = false;
                        timer = false;
                    }
                    else
                    {
                        wait = messageControl.Play(4);
                    }
                }
                break;

            case "third exercise 1":

                if (Elapsed())
                {
                    wait = messageControl.Play(9);
                    slicePlane.transform.SetPositionAndRotation(new Vector3(0, 1.5f, 0), Quaternion.AngleAxis(90, Vector3.left));
                    while (function == function1 || function == function2)
                    {
                        function = Random.Range(0, 8);  //choose the function to apply to the central stand
                    }
                    centralSurface.function = function;
                    centralSurface.Render();
                    step = "third exercise 2";
                }
                break;

            case "third exercise 2":

                if (Elapsed() && played==false) // Just in case, player is prompted again to look for the matching surface
                {
                    wait = messageControl.Play(7);
                    played = true;
                }

                buttonDown = surfaceControl.checkButton();

                if (buttonDown)
                {
                    selectedFunction = surfaceControl.getFunction();

                    if (selectedFunction == function)
                    {
                        wait = messageControl.Play(10);
                        played = false;
                        timer = false;
                        step = "time to investigate";
                    }
                    else
                    {
                        wait = messageControl.Play(4);
                    }
                }
                break;

            case "time to investigate":

                wait = messageControl.Play(11);
                slicePlane.gameObject.GetComponent<XRGrabInteractable>().trackRotation = true;
                slicePlane.transform.SetPositionAndRotation(new Vector3(0, 1.5f, 0), Quaternion.AngleAxis(71, new Vector3(1, 5, 11)));
                centralSurface.Render();
                step = "prepare for next scene";
                time = Time.fixedTime;
                break;

            case "prepare for next scene":

                if (Elapsed())
                {
                    wait = messageControl.Play(12);
                    step = "next scene";
                }
                break;

            case "next scene":

                scren.enabled = true;
                this.Ready("Third Scene");
                step = "finished";
                break;
        }
  
        return;
    }

    public void AdvanceToSecond()
    {
        if (step == "wait for grab")
        {
            wait = messageControl.Play(2);
            step = "find invisible surface";
        }
    }

    public int GetFunction()
    {
        return function;
    }

    public void SetFunction(int x)
    {
        function = x;
    }

    private void RandomFunctions()
    {
        randomFunctions = new List<int>();
        int j;
        bool newInt;
        for (int i = 0; i < numFunctions; i++)
        {
            j = Random.Range(0, numFunctions);
            newInt = false;
            while (newInt == false)
            {
                newInt = true;
                for (int k = 0; k < i; k++)
                {
                    if (randomFunctions[k] == j)
                    {
                        newInt = false;
                        j = Random.Range(0, numFunctions);
                    }
                }
            }
            randomFunctions.Add(j);
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
