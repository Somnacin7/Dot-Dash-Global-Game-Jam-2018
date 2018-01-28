using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class MorseListener : MonoBehaviour
{

    /// <summary>
    /// The tempo of the morse code. One beat is one  unit of time.
    /// </summary>
    [Range(60, 500)] public int bpm = 60;
    [Range(0.5f, 10)] public float mgapMultiplier = 1f;
    [Range(0.5f, 10)] public float lgapMultiplier = 1f;

    private float beatLength;

    private bool buttonHeld = false;
    private bool morseStarted = false;
    private float duration = 0;

    private StringBuilder morse = new StringBuilder();

    // Morse parts
    public Dictionary<string, string> morseAlphabet = new Dictionary<string, string>()
    {
        {".-","A"},
        {"-...","B"},
        {"-.- .","C"},
        {"-..","D"},
        {".","E"},
        {"..-.","F"},
        {"--.","G"},
        {"....","H"},
        {"..","I"},
        {".---","J"},
        {"-.-","K"},
        {".-..","L"},
        {"--","M"},
        {"-.","N"},
        {"---","O"},
        {".--.","P"},
        {"--.-","Q"},
        {".-.","R"},
        {"...","S"},
        {"-","T"},
        {"..--","U"},
        {"...-","V"},
        {".--","W"},
        {"-..-","X"},
        {"-.--","Y"},
        {"--..","Z"}
    };
    public const string DOT = ".";
    public const string DASH = "-";
    public const string MGAP = "&"; // gap between letters
    public const string LGAP = "_"; // gap between words
    // Events
    public delegate void AddMorseLetter();
    /// <summary>
    /// Called whenever an entire letter is added 
    /// </summary>
    public static event AddMorseLetter OnAddMorseLetter;

    public delegate void AddMorseWord();
    /// <summary>
    /// Called whenever a word is added
    /// </summary>
    public static event AddMorseWord OnAddMorseWord;

    public delegate void LetterDone();
    /// <summary>
    /// Called whenever a new letter is ready to be typed
    /// </summary>
    public static event LetterDone OnLetterDone;

    private void Awake()
    {
        beatLength = 60f / bpm;

        if (3 * beatLength * mgapMultiplier >= 7 * beatLength * lgapMultiplier)
        {
            throw new System.Exception("MGAP MUST BE SMALLER THAN LGAP");
        }
    }

    // Update is called once per frame
    void Update()
    {
        beatLength = 60f / bpm; // one morse code time unit in seconds; set this here so we can dynamically update the bpm

        ProcessButton();
    }

    void ProcessButton()
    {
        bool button = Input.GetButton("MorseButton");

        if (button == buttonHeld) // no change in input, just add duration and move on
        {
            if (morseStarted)
            {
                duration += Time.deltaTime;

                
                if (!button && duration >= 7 * beatLength * lgapMultiplier)
                {
                    morse.Append(LGAP);
                    if (OnAddMorseWord != null)
                    {
                        OnAddMorseWord();
                    }
                    duration = 0;
                    morseStarted = false;
                }
                else if (!button && duration >= 3 * beatLength * mgapMultiplier)
                {
                    if (OnLetterDone != null)
                    {
                        OnLetterDone();
                    }
                }
            }
            return;
        }
        else if (!button) // button was down but now is not, parse dot/dash
        {
            if (duration >= 3 * beatLength) // dash
            {
                morse.Append(DASH);
            }
            else // dot
            {
                morse.Append(DOT);
            }
            duration = 0;
            morseStarted = true;
        }
        else if (button) // button was not down but now is, parse space
        {
            if (duration >= 7 * beatLength * lgapMultiplier && morseStarted) // space between words
            {
                morse.Append(LGAP);
                if (OnAddMorseWord != null)
                {
                    OnAddMorseWord();
                }
            }
            else if (duration >= 3 * beatLength * mgapMultiplier && morseStarted) // space between letters
            {
                morse.Append(MGAP);
                if (OnAddMorseLetter != null)
                {
                    OnAddMorseLetter();
                }
            }
            duration = 0;
            morseStarted = true;
        }

        print(morse);
        buttonHeld = button;

    }

    public string GetMorse()
    {
        return morse.ToString();
    }

    public string GetPlainMorse()
    {
        return morse.Replace("&", "   ").Replace("_", "       ").ToString();
    }

    public string MorseToEnglish(string morse)
    {
        string ret;
        StringBuilder english = new StringBuilder();
        List<string> words = new List<string>();

        words.AddRange(morse.Split('_'));

        foreach (string word in words)
        {
            List<string> letters = new List<string>();
            letters.AddRange(word.Split('&'));
            letters.RemoveAll(s => string.IsNullOrEmpty(s));
            foreach (string var in letters)
            {
                if (morseAlphabet.ContainsKey(var))
                {
                    english.Append(morseAlphabet[var]);
                }
            }
            english.Append(' ');
        }
        ret = english.ToString();
        return ret;
    }

    public string Morse2English(string morse)
    {
        StringBuilder english = new StringBuilder();
        var morseWords = new List<string>(morse.Split('_'));
        morseWords.RemoveAll(s => string.IsNullOrEmpty(s));

        foreach (var word in morseWords)
        {
            var morseLetters = new List<string>(word.Split('&'));
            morseLetters.RemoveAll(s => string.IsNullOrEmpty(s));

            foreach (var letter in morseLetters)
            {
                if (morseAlphabet.ContainsKey(letter))
                {
                    english.Append(morseAlphabet[letter]);
                }
            }
            english.Append(" ");
        }

        return english.ToString();
    }

}

