using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Person
{
    public int personIndex;
    public int windowStart;
    public int windowEnd;
    public int windowStartMissed;
    public int windowEndMissed;
    public int perfWindowStart;
    public int perfWindowEnd;
    public int personSpriteNum;
    public int timing;
}

public class PersonToggle : MonoBehaviour
{
    int score;
    int currentcombo;
    int currentPerfCombo;

    int thisBeat = 1;

    public Text scoreText, comboText, currentRoute;

    public SpriteRenderer[] people;
    //Queue<Person> passengerQueue;
    public List<Person> passengerList;
    int offset = 6;

    public int perfectWindow = 75;
    public int goodWindow = 100;
    public int missedTime = 200;

    public SpriteRenderer feedbackRenderer;
    public Sprite[] feedbackSprite;

    public ParticleSystem[] stationParticles;

    public Sprite[] personSprites;

    public AK.Wwise.Event goodSound, perfSound, missSound;

    public GameObject scoreObject, comboObject, circleObject, inicatorObject, loadingObject;

    GameManager bigManager;

    public int Currentcombo { get => currentcombo; set => currentcombo = value; }



    // Start is called before the first frame update
    void Start()
    {
        bigManager = FindObjectOfType<GameManager>();
        scoreObject.SetActive(false); comboObject.SetActive(false); circleObject.SetActive(false); inicatorObject.SetActive(false);
        loadingObject.SetActive(true);

        RhythmHeckinWwiseSync.TogglePerson += TogglePerson;
        //passengerQueue = new Queue<Person>();
        passengerList = new List<Person>();
        score = 0;
        currentcombo = 0;
        scoreText.text = "Score: " + score;
        comboText.text = "Combo: " + currentcombo;
        missedTime = Mathf.RoundToInt((RhythmHeckinWwiseSync.secondsPerBeat * 1000));
        currentRoute.text = GameManager.trackNames[GameManager.trackNum];
        UpdateSprites();
    }

    public void UpdateSprites()
    {
        personSprites = GameManager.sprites;
    }

    // Update is called once per frame
    void Update()
    {
        if (//passengerQueue.Count > 0
            passengerList.Count > 0)
        {
            int currentTime = RhythmHeckinWwiseSync.GetMusicTimeInMS();
            //Person nextPerson = passengerQueue.Peek();
            Person nextPerson = passengerList[0];
            KeyCode correctInput = GameManager.inputs[nextPerson.personSpriteNum];

            if (Input.GetKeyDown(correctInput))
            {

                if (nextPerson.perfWindowStart < currentTime && nextPerson.perfWindowEnd > currentTime)
                {
                    PerfectPress();
                    stationParticles[nextPerson.personIndex].Play();
                    TogglePerson(nextPerson.personIndex, false, nextPerson.personSpriteNum);
                    //passengerQueue.Dequeue();
                    passengerList.Remove(nextPerson);
                }
                else if (nextPerson.windowStart < currentTime && nextPerson.windowEnd > currentTime)
                {
                    
                        GoodPress();
                        stationParticles[nextPerson.personIndex].Play();
                        TogglePerson(nextPerson.personIndex, false, nextPerson.personSpriteNum);
                        //passengerQueue.Dequeue();
                        passengerList.Remove(nextPerson);

                }
                else if (nextPerson.windowStartMissed < currentTime)
                {
                    BadPress(true, nextPerson.timing);
                    Debug.Log("Too Early!!!");
                    TogglePerson(nextPerson.personIndex, false, nextPerson.personSpriteNum);
                    //passengerQueue.Dequeue();
                    passengerList.Remove(nextPerson);
                }
            }

            if(currentTime > nextPerson.windowEnd)
            {
                BadPress(false, nextPerson.timing);
                Debug.Log("Too Late!!!");
                TogglePerson(nextPerson.personIndex, false, nextPerson.personSpriteNum);
                //passengerQueue.Dequeue();
                passengerList.Remove(nextPerson);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void TogglePerson(int person, bool on, int pickup)
    {
        people[person].sprite = personSprites[pickup];
        people[person].enabled = on;
        if (on)
        {
            Person newPerson = new Person();
            newPerson.personIndex = person;
            newPerson.personSpriteNum = pickup;
            newPerson.windowStart = (RhythmHeckinWwiseSync.GetMusicTimeInMS() + Mathf.RoundToInt(offset * RhythmHeckinWwiseSync.secondsPerBeat * 1000) - goodWindow / 2);
            newPerson.windowEnd = newPerson.windowStart + goodWindow;
            newPerson.windowStartMissed = (RhythmHeckinWwiseSync.GetMusicTimeInMS() + Mathf.RoundToInt(offset * RhythmHeckinWwiseSync.secondsPerBeat * 1000) - missedTime / 2);
            newPerson.windowEndMissed = newPerson.windowStartMissed + missedTime;
            newPerson.perfWindowStart = (RhythmHeckinWwiseSync.GetMusicTimeInMS() + Mathf.RoundToInt(offset * RhythmHeckinWwiseSync.secondsPerBeat * 1000) - perfectWindow / 2);
            newPerson.perfWindowEnd = newPerson.windowStart + perfectWindow;
            newPerson.timing = thisBeat;
            thisBeat++;


            //passengerQueue.Enqueue(newPerson);
            passengerList.Add(newPerson);
        }
    }

    private void OnDisable()
    {
        RhythmHeckinWwiseSync.TogglePerson -= TogglePerson;
    }

    void GoodPress()
    {
        goodSound.Post(gameObject);
        currentcombo += 1;
        currentPerfCombo = 0;
        score += 250 + (10 * (currentcombo - 1));
        feedbackRenderer.sprite = feedbackSprite[1];
        feedbackRenderer.color = new Color(feedbackRenderer.color.r, feedbackRenderer.color.g, feedbackRenderer.color.b, 1f);
        scoreText.text = "Score: " + score;
        UpdateCombo();
    }

    public void UpdateCombo()
    {
        comboText.text = "Combo: " + currentcombo;
    }
    void BadPress(bool playSound, int noteMissed)
    {
        if (playSound)
        {
            missSound.Post(gameObject);
        }
        if(GameManager.gameMode == 1)
        {
            Tinylytics.AnalyticsManager.LogCustomMetric("NoteLost", noteMissed.ToString());
            Tinylytics.AnalyticsManager.LogCustomMetric("ComboLost", currentcombo.ToString());
        }
        currentcombo = 0;
        currentPerfCombo = 0;
        feedbackRenderer.sprite = feedbackSprite[2];
        feedbackRenderer.color = new Color(feedbackRenderer.color.r, feedbackRenderer.color.g, feedbackRenderer.color.b, 1f);
        UpdateCombo();
    }

    void PerfectPress()
    {
        perfSound.Post(gameObject);
        currentcombo += 1;
        currentPerfCombo += 1;
        score += 400 + (10 * (currentcombo - 1)) + (20 * (currentPerfCombo - 1));
        feedbackRenderer.sprite = feedbackSprite[0];
        feedbackRenderer.color = new Color(feedbackRenderer.color.r, feedbackRenderer.color.g, feedbackRenderer.color.b, 1f);
        scoreText.text = "Score: " + score;
        UpdateCombo();
    }

    public void ToPostGame()
    {
        GameManager.recentScore = score;
        bigManager.ChangeScene(4);
    }

    public void Loaded()
    {
        scoreObject.SetActive(true); comboObject.SetActive(true); circleObject.SetActive(true); inicatorObject.SetActive(true);
        loadingObject.SetActive(false);
    }

    //IEnumerator FadeCombo()
    //{
    //    yield return null;
    //}
}
