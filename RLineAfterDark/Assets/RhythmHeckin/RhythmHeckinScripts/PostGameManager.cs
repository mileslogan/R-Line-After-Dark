using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostGameManager : MonoBehaviour
{
    public Text trackNameText, modeNameText, scoreText, backText, retryText, highScoreText;

    public string[] modeNames;

    GameManager bigManager;

    bool backSelect;

    public Color white, greyedOut;

    AudioSource clicker;

    public GameObject newHighScore;

    // Start is called before the first frame update
    void Start()
    {
        bigManager = FindObjectOfType<GameManager>();
        clicker = GameObject.Find("Clicker").GetComponent<AudioSource>();

        scoreText.text = GameManager.recentScore.ToString();
        trackNameText.text = GameManager.trackNames[GameManager.trackNum];
        modeNameText.text = modeNames[GameManager.gameMode];

        if(GameManager.recentScore > PlayerPrefs.GetInt(GameManager.trackNum.ToString() + GameManager.gameMode.ToString() + "hs"))
        {
            PlayerPrefs.SetInt(GameManager.trackNum.ToString() + GameManager.gameMode.ToString() + "hs", GameManager.recentScore);
            newHighScore.SetActive(true);
            PlayerPrefs.Save();
            switch (GameManager.trackNum)
            {
                case 0:
                    GameManager.trackOneHighScores[GameManager.gameMode] = GameManager.recentScore;
                    break;
                case 1:
                    GameManager.trackTwoHighScores[GameManager.gameMode] = GameManager.recentScore;
                    break;
                case 2:
                    GameManager.trackThreeHighScores[GameManager.gameMode] = GameManager.recentScore;
                    break;
            }
            highScoreText.text = GameManager.recentScore.ToString();
        }
        else
        {
            newHighScore.SetActive(false);
            highScoreText.text = PlayerPrefs.GetInt(GameManager.trackNum.ToString() + GameManager.gameMode.ToString() + "hs").ToString();
        }

        UpdateText();

    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetButtonDown("MenuLeft") || Input.GetButtonDown("MenuRight"))
        {
            clicker.Play();
            backSelect = !backSelect;
            UpdateText();
        }

        if (Input.GetButtonDown("MenuSelect"))
        {
            clicker.Play();
            if (backSelect)
            {
                GameManager.ChangeScene(1);
            }
            else
            {
                bigManager.ToGameScene();
            }
        }
    }

    void UpdateText()
    {
        if (backSelect)
        {
            backText.color = white;
            retryText.color = greyedOut;
        }
        else
        {
            backText.color = greyedOut;
            retryText.color = white;
        }
    }
}
