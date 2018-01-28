using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wakeText : MonoBehaviour {

    public delegate void GUIButtonPressed();
    public GUI button;
    public static event GUIButtonPressed onGUIButtonPressed;

    private void OnGUI()
    {
        if (onGUIButtonPressed != null)
        {

            print("What did you just say...");
            onGUIButtonPressed();
        }
    }
}
