using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AK.Wwise.Event[] tracks;
    public static AK.Wwise.Event[] tracksRef;
    public static int gameMode;
    public static bool primaryInput;
    public static int trackNum;
    public static KeyCode[] inputs;
    public KeyCode[] modeOnePrimary, modeOneSecondary, modeTwoPrimary, modeTwoSecondary, modeThreePrimary, modeThreeSecondary;
    public static Sprite[] sprites;
    public Sprite[] modeOne, modeTwo;
    public static int[] trackOneHighScores, trackTwoHighScores, trackThreeHighScores;
    public static string[] trackNames;
    public static int recentScore;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Manager");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        tracksRef = tracks;
        primaryInput = true;
        gameMode = 0;
        trackNum = 0;

        //TEMP
        inputs = modeOnePrimary;
        sprites = modeOne;

        trackNames = new string[3];
        trackNames[0] = "L to Canarsie";
        trackNames[1] = "6 to Brooklyn Bridge";
        trackNames[2] = "J to Jamaica Center";

        trackOneHighScores = new int[3]; trackTwoHighScores = new int[3]; trackThreeHighScores = new int[3];

        for (int i = 0; i < 3; i++)
        {
            if(!PlayerPrefs.HasKey("0" + i.ToString() + "hs"))
            {
                PlayerPrefs.SetInt(("0" + i.ToString() + "hs"), 0);
            }

            if (!PlayerPrefs.HasKey("1" + i.ToString() + "hs"))
            {
                PlayerPrefs.SetInt(("1" + i.ToString() + "hs"), 0);
            }

            if (!PlayerPrefs.HasKey("2" + i.ToString() + "hs"))
            {
                PlayerPrefs.SetInt(("2" + i.ToString() + "hs"), 0);
            }
        }

        for (int i = 0; i < 3; i++)
        {
            trackOneHighScores[i] = PlayerPrefs.GetInt("0" + i.ToString() + "hs");
            trackTwoHighScores[i] = PlayerPrefs.GetInt("1" + i.ToString() + "hs");
            trackThreeHighScores[i] = PlayerPrefs.GetInt("2" + i.ToString() + "hs");
        }
            

    }

    // Update is called once per frame
    void Update()
    {

    }

    //PRE FADE
    public void ChangeScene(int sceneNum)
    {
        //Fadeout mayhaps???
        if(sceneNum != 3)
        {
            StartCoroutine(FindObjectOfType<TransitionManager>().Fade(sceneNum));
        }
    }

    //PRE FADE
    public void ToGameScene()
    {
        UpdateSpritesAndInput();
        GameObject[] menuMusic = GameObject.FindGameObjectsWithTag("MenuMusic");

        for(int i = 0; i < menuMusic.Length; i++)
        {
            Destroy(menuMusic[i]);
        }

        StartCoroutine(FindObjectOfType<TransitionManager>().Fade(3));
    }

    public void UpdateSpritesAndInput()
    {
        switch (gameMode)
        {
            case 0:
                sprites = modeOne;
                if (primaryInput)
                {
                    inputs = modeOnePrimary;
                }
                else
                {
                    inputs = modeOneSecondary;
                }
                break;
            case 1:
                sprites = modeTwo;
                if (primaryInput)
                {
                    inputs = modeTwoPrimary;
                }
                else
                {
                    inputs = modeTwoSecondary;
                }
                break;
            case 2:
                sprites = modeTwo;
                if (primaryInput)
                {
                    inputs = modeThreePrimary;
                }
                else
                {
                    inputs = modeThreeSecondary;
                }
                break;
        }
    }

    public void QuitGame()
    {
        StartCoroutine(FindObjectOfType<TransitionManager>().Fade(-1));
    }
}
