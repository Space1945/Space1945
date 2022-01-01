using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainPanelListener : MonoBehaviour
{
    public Button Start_button;
    public Button Select_button;
    public Button Configuration_button;

    public GameObject Select_panel;
    public GameObject Configuration_panel;

    AudioSource audio_source;
    public AudioClip button_clicked;

    void Awake()
    {
        gameObject.SetActive(true);
        Select_panel.SetActive(false);

        audio_source = GetComponent<AudioSource>();
        audio_source.clip = button_clicked;
        audio_source.playOnAwake = false;
    }
    void Start()
    {
        
    }
    public void Chapter_Scene_Load()
    {
        SceneManager.LoadScene("Chapter");
    }
    public void Active_Select_Panel()
    {
        foreach (Transform child in gameObject.transform)
            child.GetComponent<Button>().interactable = false;
        Select_panel.SetActive(true);
    }
    public void Active_Configuration_Panel()
    {
        foreach (Transform child in gameObject.transform)
            child.GetComponent<Button>().interactable = false;
        Configuration_panel.SetActive(true);
    }

    public void PlayEffectSound()
    {
        if (PlayerPrefs.GetString("es") == "true")
            audio_source.Play();
    }
}
