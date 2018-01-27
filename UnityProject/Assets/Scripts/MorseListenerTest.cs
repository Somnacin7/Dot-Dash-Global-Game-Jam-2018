using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseListenerTest : MonoBehaviour {

    private MorseListener morseListener;

    void Awake()
    {
        morseListener = GetComponent<MorseListener>();
    }

    void OnEnable()
    {
        MorseListener.OnAddMorseLetter += Letter;
    }

    void OnDisable()
    {
        MorseListener.OnAddMorseLetter -= Letter;

    }

    void Letter()
    {
        print(morseListener.GetMorse());
    }
}
