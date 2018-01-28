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
<<<<<<< HEAD
<<<<<<< HEAD
    public const string MGAP = "&"; // gap between letters
    public const string LGAP = "_"; // gap between words
=======
    public const string SGAP = " "; // gap between parts of a char
    public const string MGAP = "&"; // gap between letters
    public const string LGAP = "_"; // gap between words

>>>>>>> fed0daa94aa69ee19c2e43bc98308f2784aee0f3
=======
    public const string MGAP = "&"; // gap between letters
    public const string LGAP = "_"; // gap between words
>>>>>>> somn
    // Events
    public delegate void AddMorseLetter();
    /// <summary>
    /// Called whenever a piece of a char is added 
    /// </summary>
    public static event AddMorseLetter OnAddMorseLetter;

    public delegate void AddMorseWord();
    /// <summary>
    /// Called whenever a piece of a char is added 
    /// </summary>
    public static event AddMorseWord OnAddMorseWord;

    private void Awake()
    {
        beatLength = 60f / bpm;
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
        else if (button && morseStarted) // button was not down but now is, parse space
        {
            if (duration >= 7 * beatLength) // space between words
            {
                morse.Append(LGAP);
                if (OnAddMorseWord != null)
                {
                    OnAddMorseWord();
                }
            }
            else if (duration >= 3 * beatLength) // space between letters
            {
                morse.Append(MGAP);
                if (OnAddMorseLetter != null)
                {
                    OnAddMorseLetter();
                }
            }
<<<<<<< HEAD
<<<<<<< HEAD
=======
            else // space between parts of a letters
            {
                morse.Append(SGAP);
            }
            duration = 0;
>>>>>>> fed0daa94aa69ee19c2e43bc98308f2784aee0f3
=======
            duration = 0;
>>>>>>> somn
        }

        buttonHeld = button;

        print(GetPlainMorse());

        
    }

    public string GetMorse()
    {
        return morse.ToString();
    }

<<<<<<< HEAD
<<<<<<< HEAD
=======
    public string GetPlainMorse()
    {
        return morse.Replace("&", "   ").Replace("_", "       ").ToString();
    }
>>>>>>> somn

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
            foreach (string var in letters)
            {
                english.Append(morseAlphabet[var]);
            }
            english.Append(' ');
        }
        ret = english.ToString();
        return ret;
<<<<<<< HEAD

=======
    public string GetPlainMorse()
    {
        return morse.Replace("&", "   ").Replace("_", "       ").ToString();
>>>>>>> fed0daa94aa69ee19c2e43bc98308f2784aee0f3
=======
>>>>>>> somn
    }
}

