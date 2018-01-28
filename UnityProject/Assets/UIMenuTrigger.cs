using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuTrigger : MonoBehaviour {
    public Text username;
    public Text password;
    public GameObject OldUIPanel;
    public GameObject NewUIPanel;
    private void OnGUI()
    {
       
        if (username.text.ToString() != null && password.text.ToString() != null)
        {
            NewUIPanel.SetActive(true);
            OldUIPanel.SetActive(false);
        }
    }
}
