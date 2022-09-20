using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuVoice : MonoBehaviour
{
    private static GameObject instance;

    public AudioSource Voice;
    void Start()
    {
        Voice.volume = PlayerPrefs.GetFloat("MenuVoice");
        DontDestroyOnLoad(gameObject);

        if (instance == null)
            instance = gameObject;
        else
        {
            Destroy(gameObject);
        }
    }


    void Update()
    {
        Voice.volume = PlayerPrefs.GetFloat("MenuVoice");
    }
}
