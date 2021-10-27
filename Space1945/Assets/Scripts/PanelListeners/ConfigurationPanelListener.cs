using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationPanelListener : MonoBehaviour
{
    public Button BGM_on_button;
    public Button BGM_off_button;
    public Button ES_on_button;
    public Button ES_off_button;

    public Sprite[] sprites;

    AudioSource audio_source_se;
    public AudioClip button_clicked;

    void Awake()
    {
        gameObject.SetActive(false);

        audio_source_se = GetComponent<AudioSource>();
        audio_source_se.clip = button_clicked;
        audio_source_se.playOnAwake = false;

        try
        {
            if (PlayerPrefs.GetString("bgm") == "true")
                ConvertImageWhenBGMOnClicked();
            else
                ConvertImageWhenBGMOffClicked();

            if (PlayerPrefs.GetString("es") == "true")
                ConvertImageWhenESOnClicked();
            else
                ConvertImageWhenESOffClicked();
        }
        catch
        {
            Debug.Log("ConfigurtionPanelListener/Start() Error");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ActivateBGM()
    {
        Camera.main.GetComponent<AudioSource>().mute = false;
        PlayerPrefs.SetString("bgm", "true");
        ConvertImageWhenBGMOnClicked();
    }
    public void DeactivateBGM()
    {
        Camera.main.GetComponent<AudioSource>().mute = true;
        PlayerPrefs.SetString("bgm", "false");
        ConvertImageWhenBGMOffClicked();
    }
    public void ActivateES()
    {
        audio_source_se.Play();
        PlayerPrefs.SetString("es", "true");
        ConvertImageWhenESOnClicked();
    }
    public void DeactivateES()
    {
        PlayerPrefs.SetString("es", "false");
        ConvertImageWhenESOffClicked();
    }

    void ConvertImageWhenBGMOnClicked()
    {
        BGM_on_button.GetComponent<Image>().sprite = sprites[3];
        BGM_off_button.GetComponent<Image>().sprite = sprites[0];
    }
    void ConvertImageWhenBGMOffClicked()
    {
        BGM_on_button.GetComponent<Image>().sprite = sprites[1];
        BGM_off_button.GetComponent<Image>().sprite = sprites[2];
    }
    void ConvertImageWhenESOnClicked()
    {
        ES_on_button.GetComponent<Image>().sprite = sprites[3];
        ES_off_button.GetComponent<Image>().sprite = sprites[0];
    }
    void ConvertImageWhenESOffClicked()
    {
        ES_on_button.GetComponent<Image>().sprite = sprites[1];
        ES_off_button.GetComponent<Image>().sprite = sprites[2];
    }
}
