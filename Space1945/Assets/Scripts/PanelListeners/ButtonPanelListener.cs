using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPanelListener : MonoBehaviour
{
    AudioSource audio_source;
    public AudioClip button_clicked;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.SetActive(true);

        audio_source = GetComponent<AudioSource>();
        audio_source.clip = button_clicked;
        audio_source.playOnAwake = false;
    }
    void Start()
    {
        
    }

    public void PlayEffectSound()
    {
        if (PlayerPrefs.GetString("es") == "true")
            audio_source.Play();
    }
}
