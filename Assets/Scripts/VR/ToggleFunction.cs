using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ToggleFunction : MonoBehaviour
{
    public InputActionProperty AButton;
    
    bool AButtonPressed;

    public SlicePlane slicePlane;

    void Update(){
        if(AButton.action.triggered && !AButtonPressed)
        {
            AButtonPressed = true;
        } 
        else
        {
            AButtonPressed = false;
        }
    }
}
