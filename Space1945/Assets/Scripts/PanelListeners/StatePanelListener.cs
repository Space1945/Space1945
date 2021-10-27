using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatePanelListener : MonoBehaviour
{
    public Text gold;
    public Text level;
    public Image exp_bar;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.SetActive(true);

        gold.fontSize = 50;
        level.fontSize = 100;
    }
    void Start()
    {
        DisplayStateInfo();
    }

    public void DisplayStateInfo()
    {
        DisplayGold();
        DisplayExp();
        DisplayLevel();
    }
    public void DisplayGold()
    {
        gold.text = PlayerPrefs.GetInt("gold").ToString();
    }
    public void DisplayExp()
    {
        exp_bar.fillAmount = PlayerPrefs.GetInt("pilot_exp") / (float)PlayerPrefs.GetInt("max_exp");
    }
    public void DisplayLevel()
    {
        level.text = PlayerPrefs.GetInt("pilot_level").ToString();
    }
}
