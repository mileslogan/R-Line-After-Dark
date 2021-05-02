using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// The bare minimum this script should do is make sure the player can 
/// understand each mode in the game with their chosen inputs
/// so first show pick up, drop off, and then drop off express
/// essentially we're stopping and restarting the song each time the player completes
/// a section of the tutorial 
/// </summary>
public class TutorialHandler : MonoBehaviour
{
    [SerializeField]Image textbox;
    [SerializeField] Text _text;

    int currentLine; // line in tutorialText. Every 2 lines we continue

    [TextArea(2,5)]
    [SerializeField] string[] tutorialText;

    [SerializeField] RhythmHeckinWwiseSync wwiseSync;
    bool canShowText;
    GameManager gm;

    // Start is called before the first frame update
    void Awake()
    {
        ShowText();
        _text.text = tutorialText[currentLine];
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && canShowText)
        {
            ShowNextLine();
        }

        if(wwiseSync.gameSceneManager.Currentcombo >= 9)
        {
            ShowText();
        }
    }

    public void ShowText()
    {
        textbox.gameObject.SetActive(true);
        wwiseSync.StopSong();
        canShowText = true;
        // if u fuck up a lot put oh no try again there
    }

    public void ShowNextLine()
    {
        if(currentLine == 1 || currentLine == 3 || currentLine == 5)
        {
            HideText();
            StartCoroutine(wwiseSync.LoadAndStartSong(2));

            if (currentLine == 3)
                GameManager.gameMode = 1;
            if (currentLine == 5)
                GameManager.gameMode = 2;

            gm.UpdateSpritesAndInput();
            wwiseSync.gameSceneManager.UpdateSprites();
            wwiseSync.gameSceneManager.passengerList = new List<Person>();
        }

        currentLine++;
        if(currentLine >= tutorialText.Length)
        {
            // to the song select!!
            // StartCoroutine(FindObjectOfType<TransitionManager>().Fade(1)); // tis broke ;-;
            SceneManager.LoadScene(1);
            return;
        }
        _text.text = tutorialText[currentLine];
    }

    public void HideText()
    {
        wwiseSync.gameSceneManager.Currentcombo = 0;
        canShowText = false;
        textbox.gameObject.SetActive(false);
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
