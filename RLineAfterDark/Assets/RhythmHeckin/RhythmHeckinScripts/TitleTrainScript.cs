using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleTrainScript : MonoBehaviour
{
    public Vector3 velocty, startPos, rotationPosOne, rotationPosTwo;
    public float endPos, trailPos;
    public TrailRenderer trail;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocty * Time.deltaTime);
        if (transform.position.x > endPos && transform.position.y == startPos.y)
        {
            transform.position = rotationPosOne;
        }
        else if (transform.position.y == rotationPosOne.y && transform.position.x > endPos)
        {
            transform.position = rotationPosTwo;
        }
        else if (transform.position.y == rotationPosOne.y && transform.position.x < endPos)
        {
            transform.position = startPos;
        }
    }
}
