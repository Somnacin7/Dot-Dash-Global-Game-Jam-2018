﻿using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class MorseListener : MonoBehaviour
{

    /// <summary>
    /// The tempo of the morse code. One beat is one  unit of time.
    /// </summary>
    [Range(60, 360)] public int bpm = 60;

    private float beatLength;

    private bool buttonHeld = false;
    private float duration = 0;

    private StringBuilder morse = new StringBuilder();

    // Morse parts
    public Dictionary<string, char> morseAlphabet = new Dictionary<string, char>()
    {
        {". -",'A'},
        {"- . . .",'B'},
        {"- . - .",'C'},
        {"- . .",'D'},
        {".",'E'},
        {". . - .",'F'},
        {"- - .",'G'},
        {". . . .",'H'},
        {". .",'I'},
        {". - - -",'J'},
        {"- . -",'K'},
        {". - . .",'L'},
        {"- -",'M'},
        {"- .",'N'},
        {"- - -",'O'},
        {". - - .",'P'},
        {"- - . -",'Q'},
        {". - .",'R'},
        {". . .",'S'},
        {"-",'T'},
        {". . - -",'U'},
        {". . . -",'V'},
        {". - -",'W'},
        {"- . . -",'X'},
        {"- . - -",'Y'},
        {"- - . .",'Z'}
    };
    public const string DOT = ".";
    public const string DASH = "-";
    public const string SGAP = " "; // gap between parts of a char
    public const string MGAP = "&"; // gap between letters
    public const string LGAP = "_"; // gap between words
    // Events
    public delegate void AddMorseLetter();
    /// <summary>
    /// Called whenever a piece of a char is added 
    /// </summary>
    public static event AddMorseLetter OnAddMorseLetter;

    // Update is called once per frame
    void Update()
    {
        beatLength = 60 / bpm; // one morse code time unit in seconds; set this here so we can dynamically update the bpm

        bool button = Input.GetButton("MorseButton");


        if (button == buttonHeld) // no change in input, just add duration and move on
        {
            duration += Time.deltaTime;
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
        }
        else if (button) // button was not down but now is, parse space
        {
            if (duration >= 7 * beatLength) // space between words
            {
                morse.Append(LGAP);
            }
            else if (duration >= 3 * beatLength) // space between letters
            {
                morse.Append(MGAP);
            }
            else // space between parts of a letters
            {
                morse.Append(SGAP);
            }
        }

        if (OnAddMorseLetter != null)
        {
            OnAddMorseLetter();
        }
    }

    public string GetMorse()
    {
        return morse.ToString();
    }


    public string MorseToEnglish(string morse)
    {
        string ret;
        List<char> english = new List<char>();
        List<string> words = new List<string>();

        words.AddRange(morse.Split('_'));

        foreach (string word in words)
        {
            List<string> letters = new List<string>();
            letters.AddRange(word.Split('&'));
            foreach (string var in letters)
            {
                english.Add(morseAlphabet[var]);
            }
            english.Add(' ');
        }

        ret = english.ToString();
        return ret;

    }
}

