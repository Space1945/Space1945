using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraListener : MonoBehaviour
{
    AudioSource audio_source;

    void Start()
    {
        audio_source = GetComponent<AudioSource>();
        audio_source.mute = PlayerPrefs.GetString("bgm") == "true" ? false : true;
    }
}
