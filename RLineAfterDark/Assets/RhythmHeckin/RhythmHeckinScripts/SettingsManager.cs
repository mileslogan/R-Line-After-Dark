using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Text primaryText, secondaryText, descriptionText;

    public Color white, greyedOut;

    bool textSelect;

    [TextArea(3,4)]
    public string primaryDesc, secondaryDesc;

    // Start is called before the first frame update
    void Start()
    {
        textSelect = GameManager.primaryInput;
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("MenuLeft") || Input.GetButtonDown("MenuRight"))
        {
            textSelect = !textSelect;
            UpdateText();
        }

        if (Input.GetButtonDown("MenuSelect"))
        {
            GameManager.primaryInput = textSelect;
            GameManager.ChangeScene(0);
        }
    }

    void UpdateText()
    {
        if (textSelect)
        {
            primaryText.color = white;
            secondaryText.color = greyedOut;
            descriptionText.text = primaryDesc;
        }
        else
        {
            primaryText.color = greyedOut;
            secondaryText.color = white;
            descriptionText.text = secondaryDesc;
        }
    }
}
