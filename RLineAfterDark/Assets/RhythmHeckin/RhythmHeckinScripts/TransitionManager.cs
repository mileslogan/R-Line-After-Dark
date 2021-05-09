using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    public Image blackBox;
    public Animator fadeAnimator;

    private void Awake()
    {
        if(blackBox != blackBox.enabled)
        {
            blackBox.enabled = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //not implemented yet
    public IEnumerator Fade(int sceneNum)
    {
        fadeAnimator.SetBool("In", false);
        yield return new WaitUntil(() => blackBox.color.a == 1);
        if(sceneNum >= 0)
        {
            SceneManager.LoadScene(sceneNum);
        }
        else
        {
            Application.Quit();
        }
    }
}
