using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Person
{
    public int personIndex;
    public int windowStart;
    public int windowEnd;
    public int windowStartMissed;
    public int windowEndMissed;
}

public class PersonToggle : MonoBehaviour
{
    int score;
    int currentcombo;

    public Text scoreText, comboText;

    public SpriteRenderer[] people;
    Queue<Person> passengerQueue;
    int offset = 6;

    public int perfectWindow = 50;
    public int goodWindow = 100;
    public int missedTime = 200;

    public SpriteRenderer feedbackRenderer;
    public Sprite[] feedbackSprite;

    public ParticleSystem[] stationParticles;

    

    // Start is called before the first frame update
    void Start()
    {
        RhythmHeckinWwiseSync.TogglePerson += TogglePerson;
        passengerQueue = new Queue<Person>();
        score = 0;
        currentcombo = 0;
        scoreText.text = "Score: " + score;
        comboText.text = "Combo: " + currentcombo;

    }

    // Update is called once per frame
    void Update()
    {
        if(passengerQueue.Count > 0)
        {
            int currentTime = RhythmHeckinWwiseSync.GetMusicTimeInMS();
            Person nextPerson = passengerQueue.Peek();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(nextPerson.windowStart < currentTime && nextPerson.windowEnd > currentTime)
                {
                    GoodPress();
                    stationParticles[nextPerson.personIndex].Play();
                    TogglePerson(nextPerson.personIndex, false);
                    passengerQueue.Dequeue();
                }
                else if(nextPerson.windowStartMissed < currentTime)
                {
                    BadPress();
                    Debug.Log("Too Early!!!");
                    TogglePerson(nextPerson.personIndex, false);
                    passengerQueue.Dequeue();
                }
            }

            if(currentTime > nextPerson.windowEnd)
            {
                BadPress();
                Debug.Log("Too Late!!!");
                TogglePerson(nextPerson.personIndex, false);
                passengerQueue.Dequeue();
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

    public void TogglePerson(int person, bool on)
    {
        people[person].enabled = on;
        if (on)
        {
            Person newPerson = new Person();
            newPerson.personIndex = person;
            newPerson.windowStart = (RhythmHeckinWwiseSync.GetMusicTimeInMS() + Mathf.RoundToInt(offset * RhythmHeckinWwiseSync.secondsPerBeat * 1000) - goodWindow / 2);
            newPerson.windowEnd = newPerson.windowStart + goodWindow;
            newPerson.windowStartMissed = (RhythmHeckinWwiseSync.GetMusicTimeInMS() + Mathf.RoundToInt(offset * RhythmHeckinWwiseSync.secondsPerBeat * 1000) - missedTime / 2);
            newPerson.windowEndMissed = newPerson.windowStartMissed + missedTime;


            passengerQueue.Enqueue(newPerson);
        }
    }

    private void OnDisable()
    {
        RhythmHeckinWwiseSync.TogglePerson -= TogglePerson;
    }

    void GoodPress()
    {
        currentcombo += 1;
        score += 250 + (10 * (currentcombo - 1));
        feedbackRenderer.sprite = feedbackSprite[0];
        scoreText.text = "Score: " + score;
        comboText.text = "Combo: " + currentcombo;
    }

    void BadPress()
    {
        currentcombo = 0;
        feedbackRenderer.sprite = feedbackSprite[1];
        comboText.text = "Combo: " + currentcombo;
    }
}
