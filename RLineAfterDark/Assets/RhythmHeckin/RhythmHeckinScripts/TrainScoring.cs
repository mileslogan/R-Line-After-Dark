using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainScoring : MonoBehaviour
{

    public int perfectWindow;
    public int goodWindow;
    int missedTime;

    //time in beats people appear ahead of the beat the train approaches them on
    public int personAppearanceOffset = 12;

    public Text scoreText;

    public SpriteRenderer feedbackSpriteRenderer;
    public Sprite[] feedbackSprites;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
