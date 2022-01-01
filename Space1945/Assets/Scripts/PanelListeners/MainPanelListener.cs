using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainPanelListener : MonoBehaviour
{
    public Button start_or_buy_button;
    public Button select_button;
    public Button configuration_button;

    GameObject state_panel;

    AudioSource audio_source;
    public AudioClip button_clicked;

    int gold;
    GameObject cur_airframe;

    void Awake()
    {
        gameObject.SetActive(true);
        state_panel = transform.parent.GetComponent<MainCanvasListener>().state_panel;

        audio_source = GetComponent<AudioSource>();
        audio_source.clip = button_clicked;
        audio_source.playOnAwake = false;

        select_button.transform.Find("AirframeImage").GetComponent<Image>().sprite = DB_Manager.Instance.using_airframe.GetComponent<SpriteRenderer>().sprite;
    }
    void Start()
    {
        
    }
    public void Clicked_Start_Or_Buy()
    {
        if (start_or_buy_button.transform.Find("Text").GetComponent<Text>().text == "Start")
            SceneManager.LoadScene("Chapter");
        else
        {
            int cur_gold = Int32.Parse(state_panel.GetComponent<StatePanelListener>().gold.GetComponent<Text>().text);
            if (cur_gold >= gold)
            {
                cur_gold -= gold;
                state_panel.GetComponent<StatePanelListener>().gold.GetComponent<Text>().text = cur_gold.ToString();
                start_or_buy_button.transform.Find("Text").GetComponent<Text>().text = "Start";

                DB_Manager.Instance.unlocked_airframes.Add(DB_Manager.Instance.using_airframe);
                PlayerPrefs.SetString(DB_Manager.Instance.using_airframe.name, "unlocked");
                DB_Manager.Instance.using_airframe = cur_airframe;
                DB_Manager.Instance.locked_airframes.Remove(cur_airframe);
                PlayerPrefs.SetString(cur_airframe.name, "using");
            }
        }
    }
    public void Update_Airframe(GameObject airframe, bool able)
    {
        cur_airframe = airframe;
        select_button.transform.Find("AirframeImage").GetComponent<Image>().sprite = airframe.GetComponent<SpriteRenderer>().sprite;
        if (able)
            start_or_buy_button.transform.Find("Text").GetComponent<Text>().text = "Start";
        else
        {
            gold = airframe.GetComponent<AirframeScript>().gold;
            start_or_buy_button.transform.Find("Text").GetComponent<Text>().text = gold + "\n" + "Buy";
        }
    }

    public void PlayEffectSound()
    {
        if (PlayerPrefs.GetString("es") == "true")
            audio_source.Play();
    }
}
