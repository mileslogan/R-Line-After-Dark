using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public Text startText, settingsText, quitText;

    public Color white, greyedOut;

    int textSelect;

    AudioSource clicker;

    // Start is called before the first frame update
    void Start()
    {
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
        else if(textSelect > 3)
        {
            textSelect = 1;
        }

        switch (textSelect)
        {
            case 1:
                startText.color = white;
                settingsText.color = greyedOut;
                quitText.color = greyedOut;
                break;
            case 2:
                startText.color = greyedOut;
                settingsText.color = white;
                quitText.color = greyedOut;
                break;
            case 3:
                startText.color = greyedOut;
                settingsText.color = greyedOut;
                quitText.color = white;
                break;
            default:
                startText.color = greyedOut;
                settingsText.color = greyedOut;
                quitText.color = greyedOut;
                break;
        }
    }

    void Select()
    {
        if(textSelect == 3)
        {
            GameManager.QuitGame();
        }
        else
        {
            GameManager.ChangeScene(textSelect);
        }
    }
}
