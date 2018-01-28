using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextTyper : MonoBehaviour
{

    public float letterPause = 0.2f;
    public AudioClip typeSound1;
    public AudioClip typeSound2;
    public float pauseMultiplier = 1f;

    public delegate void BootDone();
    public static event BootDone onBootDone;
    string message;
    Text textComp;

    // Use this for initialization
    void Start()
    {
        textComp = GetComponent<Text>();
        message = textComp.text;
        textComp.text = "";
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        foreach (char letter in message.ToCharArray())
        {
            textComp.text += letter;
            if (typeSound1 && typeSound2 && letter != ' ' && letter != '>')
                SoundManager.instance.RandomizeSfx(typeSound1, typeSound2);
            yield return 0;
            if (letter == '*' || letter == '.')
            {
                yield return new WaitForSeconds(letterPause * pauseMultiplier);
            }
            else if (letter == '>')
            {
                yield return new WaitForSeconds(letterPause * 0);
            }
            else
            {
                yield return new WaitForSeconds(letterPause);
            }
            if (Input.anyKey)
            {
                break;
            }
        }
        onBootDone();
    }
}