using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostGameManager : MonoBehaviour
{
    public Text trackNameText, modeNameText, scoreText, backText, retryText, highScoreText, newHighScoreText, scoreWords, highScoreWords;

    public Text[] hitText;
    public Animator[] textAnimators;

    public string[] modeNames;

    GameManager bigManager;

    bool backSelect;

    public Color white, greyedOut;

    AudioSource clicker;

    public GameObject newHighScore;

    int[] finalHits = new int[5];

    public Image fadeObject;

    bool textFadeActive;
    bool textFadeStarted;

    bool highScoreAchieved;

    // Start is called before the first frame update
    void Start()
    {
        bigManager = FindObjectOfType<GameManager>();
        clicker = GameObject.Find("Clicker").GetComponent<AudioSource>();
        finalHits = GameManager.hits;

        scoreText.text = GameManager.recentScore.ToString();
        if(GameManager.trackNames != null)
        {
            trackNameText.text = GameManager.trackNames[GameManager.trackNum];
        }
        
        modeNameText.text = modeNames[GameManager.gameMode];

        hitText[0].text = "Perfect: " + finalHits[0];
        hitText[1].text = "Good: " + finalHits[1];
        hitText[2].text = "Early: " + finalHits[2];
        hitText[3].text = "Miss: " + finalHits[3];
        hitText[4].text = "Max Combo: " + finalHits[4];

        if(GameManager.recentScore > PlayerPrefs.GetInt(GameManager.trackNum.ToString() + GameManager.gameMode.ToString() + "hs"))
        {
            highScoreAchieved = true;
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
            highScoreAchieved = false;
            newHighScore.SetActive(false);
            highScoreText.text = PlayerPrefs.GetInt(GameManager.trackNum.ToString() + GameManager.gameMode.ToString() + "hs").ToString();
        }

        UpdateText();

        textFadeActive = true;

    }

    // Update is called once per frame
    void Update()
    {
        if(!textFadeStarted && fadeObject.color.a == 0)
        {
            StartCoroutine("TextFadeIn");
            textFadeStarted = true;
        }

        if (Input.GetButtonDown("MenuLeft") || Input.GetButtonDown("MenuRight"))
        {
            clicker.Play();
            backSelect = !backSelect;
            UpdateText();
        }

        if (Input.GetButtonDown("MenuSelect"))
        {
            clicker.Play();
            if (!textFadeActive)
            {
                if (backSelect)
                {
                    bigManager.ChangeScene(1);
                }
                else
                {
                    bigManager.ToGameScene();
                }
            }
            else
            {
                StopAllCoroutines();
                for(int i = 0; i < textAnimators.Length; i++)
                {
                    if(i == 9 && !highScoreAchieved)
                    {

                    }
                    else
                    {
                        textAnimators[i].SetBool("In", false);
                        textAnimators[i].Play("SkipFade");
                    }
                }
                foreach (Text text in hitText)
                {
                    text.color = white;
                }
                scoreText.color = white;
                highScoreText.color = white;
                scoreWords.color = white;
                highScoreWords.color = white;
                if (highScoreAchieved)
                {
                    newHighScoreText.color = white;
                }
                textFadeActive = false;
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

    IEnumerator TextFadeIn()
    {
        textAnimators[0].SetBool("In", true);
        yield return new WaitUntil(() => hitText[0].color.a == 1);

        textAnimators[1].SetBool("In", true);
        yield return new WaitUntil(() => hitText[1].color.a == 1);

        textAnimators[2].SetBool("In", true);
        yield return new WaitUntil(() => hitText[2].color.a == 1);

        textAnimators[3].SetBool("In", true);
        yield return new WaitUntil(() => hitText[3].color.a == 1);

        textAnimators[4].SetBool("In", true);
        yield return new WaitUntil(() => hitText[4].color.a == 1);

        textAnimators[5].SetBool("In", true);
        textAnimators[6].SetBool("In", true);
        yield return new WaitUntil(() => scoreText.color.a == 1);

        textAnimators[7].SetBool("In", true);
        textAnimators[8].SetBool("In", true);
        yield return new WaitUntil(() => highScoreText.color.a == 1);

        if(newHighScore.activeInHierarchy == true)
        {
            textAnimators[9].SetBool("In", true);
            yield return new WaitUntil(() => newHighScoreText.color.a == 1);
        }

        textFadeActive = false;
        yield break;

    }
}
