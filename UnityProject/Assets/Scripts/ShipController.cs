using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour {


    public Transform leftSpline;
    public Transform rightSpline;
    public Transform forwardSpline;

    public MorseListener morseListener;

    public Text text;

    private SplineWalker splineWalker;
    private bool running = false;

    bool left;
    bool right;
    bool forward;
    bool isDead = false;


    void Awake () {
        splineWalker = GetComponent<SplineWalker>();
	}
	
	// Update is called once per frame
	void Update () {

        if (isDead)
        {
            if (Input.anyKey)
            {
                ReloadScene();
            }
        }
        if (!running && !isDead)
        {
            if (left)
            {
                StartSpline(Direction.LEFT);
            }
            else if (right)
            {
                StartSpline(Direction.RIGHT);
            }
            else if (forward)
            {
                StartSpline(Direction.FORWARD);
            }
        }
        
    }
    // 0 = left, 1 = right, 2 = forward
    void StartSpline(Direction dir)
    {
        Transform splineToSpawn;
        if (dir == Direction.LEFT)
        {
            splineToSpawn = leftSpline;
        }
        else if (dir == Direction.RIGHT)
        {
            splineToSpawn = rightSpline;
        }
        else
        {
            splineToSpawn = forwardSpline;
        }
        var newSpline = Instantiate(splineToSpawn, transform.position, Quaternion.LookRotation(-transform.right));
        splineWalker.spline = newSpline.GetComponent<BezierSpline>();
        splineWalker.ResetSpline();
        running = true;
        left = right = forward = false;
    }

    void ResetSpline()
    {
        running = false;
    }

    private void OnEnable()
    {
        SplineWalker.OnSplineDone += ResetSpline;
        MorseListener.OnAddMorseWord += CheckMorseWord;
    }

    private void OnDisable()
    {
        SplineWalker.OnSplineDone -= ResetSpline;
        MorseListener.OnAddMorseWord -= CheckMorseWord;
    }

    void CheckMorseWord()
    {
        var morse = morseListener.Morse2English(morseListener.GetMorse());

        morse = morse.Trim();

        if (morse.ToUpper() == "LEFT")
        {
            left = true;
        }
        else if (morse.ToUpper() == "RIGHT")
        {
            right = true;
        }
        else if (morse.ToUpper() == "FORWARD")
        {
            forward = true;
        }

        morseListener.ClearMorse();
    }

    private void OnTriggerEnter(Collider other)
    {
        var go = other.gameObject;
        if (go.tag == "wall")
        {
            morseListener.enabled = false;
            text.text = "ERR: CRASH";
            isDead = true;
        }
    }

    void ReloadScene()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
