using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainTrigger : MonoBehaviour {
    public Button quit;
    public Button start;
    public string MazeGame;

    void Start()
    {
        quit.onClick.AddListener(() => OnQuit());
        start.onClick.AddListener(() => OnClickStart());
    }


    private void OnClickStart()
    {
        SceneManager.LoadScene(MazeGame);

    }
    private void OnQuit()
    {
        Application.Quit();
        
    }

}
