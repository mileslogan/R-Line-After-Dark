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

    int row, modeSelect, trackSelect;

    bool startSelect;

    [SerializeField] string[] modeAnims;
    [SerializeField] string[] trackAnims;
    [SerializeField] Animator modeAnim, trackAnim;
    int trackNum, modeNum; // for the animator. very hacky sorry abt this - Geneva

    GameManager bigManager;
    AudioSource clicker;

    // Start is called before the first frame update
    void Start()
    {
        bigManager = FindObjectOfType<GameManager>();
        clicker = GameObject.Find("Clicker").GetComponent<AudioSource>();
        row = 1;
        modeSelect = GameManager.gameMode;
        trackSelect = GameManager.trackNum;
        startSelect = true;

        for (int i = 0; i < trackSpecificText.Length; i++)
        {
            if (i == trackSelect)
            {
                trackSpecificText[i].color = offwhite;
            }
            else
            {
                trackSpecificText[i].color = greyedOutMore;
            }
        }
        startText.color = greyedOutMore;
        backText.color = greyedOutMore;

        UpdateText(1);

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("MenuLeft"))
        {
            clicker.Play();
            UpdateText(0);
        }
        else if (Input.GetButtonDown("MenuRight"))
        {
            clicker.Play();
            UpdateText(2);
        }
        else if (Input.GetButtonDown("MenuUp"))
        {
            if(row > 1)
            {
                clicker.Play();
                row -= 1;
                UpdateText(1);
            }
        }
        else if (Input.GetButtonDown("MenuDown"))
        {
            if(row < 3)
            {
                clicker.Play();
                row += 1;
                UpdateText(1);
            }
        }
        else if (Input.GetButtonDown("MenuSelect"))
        {
            clicker.Play();
            if(row == 3 && startSelect)
            {
                GameManager.trackNum = trackSelect;
                GameManager.gameMode = modeSelect;
                bigManager.ToGameScene();
            }
            else if(row == 3 && !startSelect)
            {
                bigManager.ChangeScene(0);
            }
            else
            {
                row += 1;
                UpdateText(1);
            }
        }
    }

    void UpdateText(int dir)
    {
        int prevMode = modeSelect; // hacky gen stuff
        int prevTrack = trackSelect;
        switch (row)
        {
            case 1:
                if(dir == 0)
                {
                    modeSelect--;
                    if(modeSelect < 0)
                    {
                        modeSelect = modeSpecificText.Length - 1;
                    }
                }
                else if(dir == 2)
                {
                    modeSelect++;
                    if (modeSelect >= modeSpecificText.Length)
                    {
                        modeSelect = 0;
                    }
                }

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
                if (dir == 0)
                {
                    trackSelect--;
                    if (trackSelect < 0)
                    {
                        trackSelect = trackSpecificText.Length - 1;
                    }
                }
                else if (dir == 2)
                {
                    trackSelect++;
                    if (trackSelect >= trackSpecificText.Length)
                    {
                        trackSelect = 0;
                    }
                }

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
                if(dir != 1)
                {
                    startSelect = !startSelect;
                }

                if(startSelect)
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






        //hacky stuff from gen oopsies
        if (prevMode != modeSelect)
        {
            if (prevMode < modeSelect)
            {
                if (modeSelect != 0)
                {
                    modeNum = modeSelect - 1;
                    modeAnim.Play(modeAnims[modeNum]);
                }

            }
            else
            {
                switch (modeSelect)
                {
                    case 0:
                        modeNum = 3;
                        modeAnim.Play(modeAnims[modeNum]);
                        break;
                    case 1:
                        modeNum = 2;
                        modeAnim.Play(modeAnims[modeNum]);
                        break;
                }
            }
        }

        //hacky stuff from gen oopsies pt 2
        if (trackSelect != prevTrack)
        {
            if (prevTrack < trackSelect)
            {
                if (trackSelect != 0)
                {
                    trackNum = (trackSelect - 1);
                    trackAnim.Play(trackAnims[trackNum]);
                }
            }
            else
            {
                switch (trackSelect)
                {
                    case 0:
                        trackNum = 5;
                        trackAnim.Play(trackAnims[trackNum]);
                        break;
                    case 1:
                        trackNum = 4;
                        trackAnim.Play(trackAnims[trackNum]);
                        break;
                    case 2:
                        trackNum = 3;
                        trackAnim.Play(trackAnims[trackNum]);
                        break;
                }
            }

        }

        if (dir == 1)
        {
            if (row == 2)
            {
                trackText.color = white;
                for (int i = 0; i < modeSpecificText.Length; i++)
                {
                    if (modeSpecificText[i].color == white)
                    {
                        modeSpecificText[i].color = offwhite;
                    }
                    else if (modeSpecificText[i].color == greyedOut)
                    {
                        modeSpecificText[i].color = greyedOutMore;
                    }
                }
                startText.color = greyedOutMore;
                backText.color = greyedOutMore;
                modeText.color = greyedOut;
            }
            else
            {
                trackText.color = greyedOut;
                if(row == 1)
                {
                    modeText.color = white;
                }
                else
                {
                    modeText.color = greyedOut;
                }
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
