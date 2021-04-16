using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicScript : MonoBehaviour
{
    AudioSource mySource;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MenuMusic");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        mySource = GetComponent<AudioSource>();
        mySource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
