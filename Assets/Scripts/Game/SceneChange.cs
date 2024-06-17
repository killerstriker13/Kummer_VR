using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public GameController gameControl;

    private void OnTriggerEnter(Collider other)
    {
     //   Debug.Log(other.gameObject.name);
        gameControl.Advance();
    }

    private void OnTriggerStay(Collider other)
    {
        gameControl.Advance();
    }
}
