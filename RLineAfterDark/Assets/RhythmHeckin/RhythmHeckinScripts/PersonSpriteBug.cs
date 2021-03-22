using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonSpriteBug : MonoBehaviour
{
    float timer;
    bool timerStarted;
    SpriteRenderer myRenderer;

    float offset = 6.5f;
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!timerStarted && myRenderer.enabled)
        {
            timer = offset * RhythmHeckinWwiseSync.secondsPerBeat;
            timerStarted = true;
        }
        else if(timerStarted && myRenderer.enabled)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                myRenderer.enabled = false;
            }
        }

        if (!myRenderer.enabled)
        {
            timerStarted = false;
        }
    }
}
