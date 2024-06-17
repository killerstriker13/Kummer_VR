using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LockPlane;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;


public class SceneFourController : GameController
{

    public SurfaceControl surfaceControl;

    private Renderer scren;

    private List<int> randomFunctions;

    private string step = "1a";
    private float time;
    private bool timer = false;
    private int selectedFunction;
    private bool buttonDown;
    private float wait = 2.0f;
    private float sceneDelay = 2.0f;
    private bool pause = false;
    public InputActionProperty rightTrigger;
    public InputActionProperty leftTrigger;

    public void Awake()
    {
        messageControl.Initialize("04");
        RandomFunctions(10,15); //Selects functions for outer surfaces
        surfaceControl.SetFunctions(randomFunctions); //Sends information to SurfaceControl
        surfaceControl.InitializeSurfaces();
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

            case "1a":

                if (Elapsed())
                {
                    wait = sceneDelay + messageControl.Play(1);
                    step = "1b";
                }
                break;

            case "1b":
                if (Elapsed())
                {
                    wait = sceneDelay + messageControl.Play(2);
                    step = "1c";
                }
                break;

            case "1c":
                if (Elapsed())
                {
                    wait = sceneDelay + messageControl.Play(3);
                    step = "1d";
                }
                break;

            case "1d":
                if (Elapsed())
                {
                    wait = sceneDelay + messageControl.Play(4);
                    step = "1e";
                }
                break;

            case "1e":
                if (Elapsed())
                {
                    wait = sceneDelay + messageControl.Play(5);
                    step = "first exercise";
                }
                break;

            case "first exercise":

                buttonDown = surfaceControl.checkButton();

                if (buttonDown)
                {
                    selectedFunction = surfaceControl.getFunction();
                    switch (selectedFunction)
                    {
                        case 10:
                            wait = sceneDelay + messageControl.Play(6);
                            step = "1e";
                            break;

                        case 11:
                            wait = sceneDelay + messageControl.Play(7);
                            step = "1e";
                            break;

                        case 12:
                            wait = sceneDelay + messageControl.Play(8);
                            step = "1e"; 
                            break;

                        case 13:
                            wait = sceneDelay + messageControl.Play(9);
                            step = "2a";
                            break;

                        case 14:
                            wait = sceneDelay + messageControl.Play(10);
                            step = "1e"; 
                            break;

                        case 15:
                            wait = sceneDelay + messageControl.Play(11);
                            step = "1e"; 
                            break;
                    }

                }
                break;

            case "2a":

                if (Elapsed())
                {
                    wait = sceneDelay + messageControl.Play(12);
                    step = "2b";
                }
                break;

            case "2b":
                if (Elapsed())
                {
                    wait = sceneDelay + messageControl.Play(13);
                    step = "2c";
                }
                break;

            case "2c":
                if (Elapsed())
                {
                    wait = sceneDelay + messageControl.Play(14);
                    step = "second exercise";
                }
                break;

            case "second exercise":

                buttonDown = surfaceControl.checkButton();

                if (buttonDown)
                {
                    selectedFunction = surfaceControl.getFunction();

                    switch (selectedFunction)
                    {
                        case 10:
                            wait = sceneDelay + messageControl.Play(15);
                            step = "2c";
                            break;

                        case 11:
                            wait = sceneDelay + messageControl.Play(16);
                            step = "2c";
                            break;

                        case 12:
                            wait = sceneDelay + messageControl.Play(17);
                            step = "2c";
                            break;

                        case 13:
                            wait = sceneDelay + messageControl.Play(18);
                            step = "2c";
                            break;

                        case 14:
                            wait = sceneDelay + messageControl.Play(19);
                            step = "2c";
                            break;

                        case 15:
                            wait = sceneDelay + messageControl.Play(20);
                            step = "3a";
                            break;
                    }

                }
                break;

            case "3a":

                if (Elapsed())
                {
                    wait = sceneDelay + messageControl.Play(21);
                    step = "3b";
                }
                break;

            case "3b":
                if (Elapsed())
                {
                    wait = sceneDelay + messageControl.Play(22);
                    step = "3c";
                }
                break;

            case "3c":
                if (Elapsed())
                {
                    wait = sceneDelay + messageControl.Play(23);
                    step = "third exercise";
                }
                break;

            case "third exercise":

                buttonDown = surfaceControl.checkButton();

                if (buttonDown)
                {
                    selectedFunction = surfaceControl.getFunction();

                    switch (selectedFunction)
                    {
                        case 10:
                            wait = sceneDelay + messageControl.Play(24);
                            step = "3c";
                            break;

                        case 11:
                            wait = sceneDelay + messageControl.Play(25);
                            step = "4a";
                            break;

                        case 12:
                            wait = sceneDelay + messageControl.Play(26);
                            step = "3c";
                            break;

                        case 13:
                            wait = sceneDelay + messageControl.Play(27);
                            step = "3c";
                            break;

                        case 14:
                            wait = sceneDelay + messageControl.Play(28);
                            step = "3c";
                            break;

                        case 15:
                            wait = sceneDelay + messageControl.Play(29);
                            step = "3c";
                            break;
                    }

                }
                break;



            case "4a":

                if (Elapsed())
                {
                    wait = sceneDelay + messageControl.Play(30);
                    step = "4b";
                }
                break;

            case "4b":
                if (Elapsed())
                {
                    wait = sceneDelay + messageControl.Play(31);
                    step = "4c";
                }
                break;

            case "4c":
                if (Elapsed())
                {
                    wait = sceneDelay + messageControl.Play(32);
                    step = "fourth exercise";
                }
                break;

            case "fourth exercise":

                buttonDown = surfaceControl.checkButton();

                if (buttonDown)
                {
                    selectedFunction = surfaceControl.getFunction();

                    switch (selectedFunction)
                    {
                        case 10:
                            wait = sceneDelay + messageControl.Play(33);
                            step = "4c";
                            break;

                        case 11:
                            wait = sceneDelay + messageControl.Play(34);
                            step = "4c";
                            break;

                        case 12:
                            wait = sceneDelay + messageControl.Play(35);
                            step = "5a";
                            break;

                        case 13:
                            wait = sceneDelay + messageControl.Play(36);
                            step = "4c";
                            break;

                        case 14:
                            wait = sceneDelay + messageControl.Play(37);
                            step = "4c";
                            break;

                        case 15:
                            wait = sceneDelay + messageControl.Play(38);
                            step = "4c";
                            break;
                    }

                }
                break;



            case "5a":

                if (Elapsed())
                {
                    wait = sceneDelay + messageControl.Play(39);
                    step = "5b";
                }
                break;

            case "5b":
                if (Elapsed())
                {
                    wait = sceneDelay + messageControl.Play(40);
                    step = "5c";
                }
                break;

            case "5c":
                if (Elapsed())
                {
                    wait = sceneDelay + messageControl.Play(41);
                    step = "fifth exercise";
                }
                break;

            case "fifth exercise":

                buttonDown = surfaceControl.checkButton();

                if (buttonDown)
                {
                    selectedFunction = surfaceControl.getFunction();

                    switch (selectedFunction)
                    {
                        case 10:
                            wait = sceneDelay + messageControl.Play(42);
                            step = "5c";
                            break;

                        case 11:
                            wait = sceneDelay + messageControl.Play(43);
                            step = "5c";
                            break;

                        case 12:
                            wait = sceneDelay + messageControl.Play(44);
                            step = "5c";
                            break;

                        case 13:
                            wait = sceneDelay + messageControl.Play(45);
                            step = "5c";
                            break;

                        case 14:
                            wait = sceneDelay + messageControl.Play(46);
                            step = "6a";
                            break;

                        case 15:
                            wait = sceneDelay + messageControl.Play(47);
                            step = "5c";
                            break;
                    }

                }
                break;



            case "6a":

                if (Elapsed())
                {
                    wait = sceneDelay + messageControl.Play(48);
                    step = "6b";
                }
                break;

            case "6b":
                if (Elapsed())
                {
                    wait = sceneDelay + messageControl.Play(49);
                    step = "6c";
                }
                break;

            case "6c":
                if (Elapsed())
                {
                    wait = sceneDelay + messageControl.Play(50);
                    step = "sixth exercise";
                }
                break;

            case "6c2":
                if (Elapsed())
                {
                    wait = sceneDelay + messageControl.Play(50);
                    step = "sixth exercise";
                }
                break;

            case "sixth exercise":

                buttonDown = surfaceControl.checkButton();

                if (buttonDown)
                {
                    selectedFunction = surfaceControl.getFunction();

                    switch (selectedFunction)
                    {
                        case 10:
                            wait = sceneDelay + messageControl.Play(51);
                            step = "done";
                            break;

                        case 11:
                            wait = sceneDelay + messageControl.Play(52);
                            step = "6c2";
                            break;

                        case 12:
                            wait = sceneDelay + messageControl.Play(53);
                            step = "6c2";
                            break;

                        case 13:
                            wait = sceneDelay + messageControl.Play(54);
                            step = "6c2";
                            break;

                        case 14:
                            wait = sceneDelay + messageControl.Play(55);
                            step = "6c2";
                            break;

                        case 15:
                            wait = sceneDelay + messageControl.Play(56);
                            step = "6c2";
                            break;
                    }
                }
                break;

            case "done":
                if (Elapsed())
                {
                    wait = sceneDelay + messageControl.Play(57);
                    step = "no more";
                }
                break;
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


    private void RandomFunctions(int start, int end)
    {
        randomFunctions = new List<int>();
        int j;
        bool newInt;
        for (int i = 0; i < end + 1 - start; i++)
        {
            j = Random.Range(start, end+1);
            newInt = false;
            while (newInt == false)
            {
                newInt = true;
                for (int k = 0; k < i; k++)
                {
                    if (randomFunctions[k] == j)
                    {
                        newInt = false;
                        j = Random.Range(start,end+1);
                    }
                }
            }
            randomFunctions.Add(j);
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