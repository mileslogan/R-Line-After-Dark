using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongSelectManager : MonoBehaviour
{
    [Header("Categories")]
    public Text modeText, trackText;

    public Text[] modeSpecificText, trackSpecificText;

    [Header("Start+Back")]
    public Text startText, backText;

    public Text descText, highScoreText;

    public Color white, offwhite, greyedOut, greyedOutMore;

    int row, modeSelect, trackSelect, startSelect;

    // Start is called before the first frame update
    void Start()
    {
        row = 1;
        modeSelect = GameManager.gameMode;
        trackSelect = GameManager.trackNum;
        startSelect = 0;

        for (int i = 0; i < trackSpecificText.Length; i++)
        {
            trackSpecificText[i].color = greyedOutMore;
        }
        startText.color = greyedOutMore;
        backText.color = greyedOutMore;

        UpdateText(true);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateText(bool updateRows)
    {
        switch (row)
        {
            case 1:
                for(int i = 0; i < modeSpecificText.Length; i++)
                {
                    if(i == modeSelect)
                    {
                        modeSpecificText[i].color = white;
                    }
                    else
                    {
                        modeSpecificText[i].color = greyedOut;
                    }
                }
                break;
            case 2:
                for (int i = 0; i < trackSpecificText.Length; i++)
                {
                    if (i == trackSelect)
                    {
                        trackSpecificText[i].color = white;
                    }
                    else
                    {
                        trackSpecificText[i].color = greyedOut;
                    }
                }
                break;
            case 3:
                if(startSelect == 0)
                {
                    startText.color = white;
                    backText.color = greyedOut;
                }
                else
                {
                    startText.color = greyedOut;
                    backText.color = white;
                }
                break;
        }
        if (updateRows)
        {
            if (row == 2)
            {
                for (int i = 0; i < modeSpecificText.Length; i++)
                {
                    if (modeSpecificText[i].color == white)
                    {
                        modeSpecificText[i].color = offwhite;
                    }
                    else
                    {
                        modeSpecificText[i].color = greyedOutMore;
                    }
                }
                if (startText.color == white)
                {
                    startText.color = offwhite;
                }
                else
                {
                    startText.color = greyedOutMore;
                }
                if (backText.color == white)
                {
                    backText.color = offwhite;
                }
                else
                {
                    backText.color = greyedOutMore;
                }
            }
            else
            {
                for (int i = 0; i < trackSpecificText.Length; i++)
                {
                    if (trackSpecificText[i].color == white)
                    {
                        trackSpecificText[i].color = offwhite;
                    }
                    else
                    {
                        trackSpecificText[i].color = greyedOutMore;
                    }
                }
            }
        }
    }
}
