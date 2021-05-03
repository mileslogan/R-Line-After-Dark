using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    [SerializeField] Text startText, settingsText, tutorialText, quitText;

    public Color white, greyedOut;

    int textSelect;

    AudioSource clicker;

    GameManager bigManager;

    // Start is called before the first frame update
    void Start()
    {
        bigManager = FindObjectOfType<GameManager>();
        textSelect = 1;
        ChangeTextColor();
        clicker = GameObject.Find("Clicker").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("MenuLeft"))
        {
            textSelect -= 1;
            ChangeTextColor();
            clicker.Play();
        }

        if (Input.GetButtonDown("MenuRight"))
        {
            textSelect += 1;
            ChangeTextColor();
            clicker.Play();
        }

        if (Input.GetButtonDown("MenuSelect"))
        {
            clicker.Play();
            Select();
        }
    }

    void ChangeTextColor()
    {
        if(textSelect < 1)
        {
            textSelect = 3;
        }
        else if(textSelect > 4)
        {
            textSelect = 1;
        }

        switch (textSelect)
        {
            case 1:
                startText.color = white;
                settingsText.color = greyedOut;
                tutorialText.color = greyedOut;
                quitText.color = greyedOut;
                break;
            case 2:
                startText.color = greyedOut;
                settingsText.color = white;
                quitText.color = greyedOut;
                tutorialText.color = greyedOut;
                break;
            case 3:
                tutorialText.color = white;
                startText.color = greyedOut;
                settingsText.color = greyedOut;
                quitText.color = greyedOut;
                break;
            case 4:
                tutorialText.color = greyedOut;
                startText.color = greyedOut;
                settingsText.color = greyedOut;
                quitText.color = white;
                break;
            default:
                startText.color = greyedOut;
                settingsText.color = greyedOut;
                quitText.color = greyedOut;
                tutorialText.color = greyedOut;
                break;
        }
    }

    void Select()
    {
        if(textSelect == 4)
        {
            bigManager.QuitGame();
        }
        else
        {
            if (textSelect == 3)
            {
                bigManager.ChangeScene(5);
                GameObject[] menuMusic = GameObject.FindGameObjectsWithTag("MenuMusic");

                for (int i = 0; i < menuMusic.Length; i++)
                {
                    Destroy(menuMusic[i]);
                }
            }
            else
            {
                bigManager.ChangeScene(textSelect);
            }
        }
    }
}
