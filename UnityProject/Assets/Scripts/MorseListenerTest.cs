using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MorseListenerTest : MonoBehaviour {

    public Text text;

    private MorseListener morseListener;

    void Awake()
    {
        morseListener = GetComponent<MorseListener>();
    }

    void OnEnable()
    {
        MorseListener.OnAddMorseLetter += Letter;
        MorseListener.OnAddMorseWord += Letter;

        MorseListener.OnLetterDone += Letter;
        MorseListener.OnLetterDone += LetterDone;
    }

    void OnDisable()
    {
        MorseListener.OnAddMorseLetter -= Letter;
        MorseListener.OnAddMorseWord -= Letter;

        MorseListener.OnLetterDone -= Letter;
        MorseListener.OnLetterDone -= LetterDone;
    }

    void Letter()
    {
        string morse = morseListener.GetMorse();
        string english = morseListener.Morse2English(morse);

        text.text = english;
    }

    void LetterDone()
    {
        if (!text.text.EndsWith("_"))
        {
            text.text += "_";
        }
    }
}
