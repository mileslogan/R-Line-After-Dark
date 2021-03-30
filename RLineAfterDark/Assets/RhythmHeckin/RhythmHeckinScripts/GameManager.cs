using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AK.Wwise.Event[] tracks;
    public static int gameMode;
    public static bool primaryInput;
    public static int trackNum;
    public static KeyCode[] inputs;
    public KeyCode[] modeOnePrimary, modeOneSecondary, modeTwoPrimary, modeTwoSecondary, modeThreePrimary, modeThreeSecondary;

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
        primaryInput = true;
        gameMode = 0;
        trackNum = 0;

        //TEMP
        inputs = modeOnePrimary;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void ChangeScene(int sceneNum)
    {
        //Fadeout mayhaps???
        SceneManager.LoadScene(sceneNum);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
