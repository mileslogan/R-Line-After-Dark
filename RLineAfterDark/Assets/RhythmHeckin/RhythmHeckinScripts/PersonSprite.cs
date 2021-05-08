using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonSprite : MonoBehaviour
{
    float timer;
    bool timerStarted;
    SpriteRenderer myRenderer;
    Animator myAnimator;

    float offset = 7f;
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!timerStarted && myRenderer.color.a == 1)
        {
            timer = offset * RhythmHeckinWwiseSync.secondsPerBeat;
            timerStarted = true;
        }
        else if (timerStarted && myRenderer.color.a == 1)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                myAnimator.SetBool("In", false);
            }
        }

        if (myRenderer.color.a != 1)
        {
            timerStarted = false;
        }
    }

    public IEnumerator PersonFade(bool fadeIn, Sprite updatedSprite)
    {
        myRenderer.sprite = updatedSprite;

        if (fadeIn)
        {
            //myRenderer.enabled = true;
            myAnimator.SetBool("In", true);
            yield return new WaitUntil(() => myRenderer.color.a == 1);
        }
        else
        {
            myAnimator.SetBool("In", false);
            yield return new WaitUntil(() => myRenderer.color.a == 0);
            //myRenderer.enabled = false;
        }
    }
}
