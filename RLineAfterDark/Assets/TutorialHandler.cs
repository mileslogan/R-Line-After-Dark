using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The bare minimum this script should do is make sure the player can 
/// understand each mode in the game with their chosen inputs
/// so first show pick up, drop off, and then drop off express
/// essentially we're stopping and restarting the song each time the place completes
/// a section of the tutorial or messes up
/// </summary>
public class TutorialHandler : MonoBehaviour
{
    [SerializeField]Image textbox;
    [SerializeField] Text _text;

    int currentLine; // line in tutorialText

    [TextArea(2,5)]
    [SerializeField] string[] tutorialText;

    [SerializeField] RhythmHeckinWwiseSync wwiseSync;

    // Start is called before the first frame update
    void Awake()
    {
        ShowText();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowText()
    {

    }

    public void ShowNextLine()
    {
        // get primary input for this
    }

    public void HideText()
    {

    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
    }
}
