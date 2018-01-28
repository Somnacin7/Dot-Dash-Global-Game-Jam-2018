using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BootScreenTrigger : MonoBehaviour {

    public GameObject NewUIPanel;
    public GameObject OldUIPanel;


    private void OnEnable()
    {
        TextTyper.onBootDone += loadScreen;
    }


    void loadScreen()
    {
            OldUIPanel.SetActive(false);
            NewUIPanel.SetActive(true);
    }
 
 
}
