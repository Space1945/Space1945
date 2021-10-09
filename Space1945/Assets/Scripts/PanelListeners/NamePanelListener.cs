using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NamePanelListener : MonoBehaviour
{
    public InputField name_field;
    public Button submit_btn;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Initiate()
    {
        PlayerPrefs.SetString("pilot_name", name_field.text);
        PlayerPrefs.SetInt("pilot_level", 1);
        PlayerPrefs.SetInt("pilot_exp", 0);
        PlayerPrefs.SetString("pilot_skill", "100000000000000000000000000000000000000000000"); // ±Ê¿Ã 45
        PlayerPrefs.SetInt("gold", 10000);
        PlayerPrefs.SetInt("max_level", 30);
        PlayerPrefs.SetInt("max_exp", 200);

        PlayerPrefs.SetString("chapter1", "10000");
        PlayerPrefs.SetString("chapter2", "00000");
        PlayerPrefs.SetString("chapter3", "00000");
        PlayerPrefs.SetString("chapter4", "00000");
        PlayerPrefs.SetString("bgm", "true");
        PlayerPrefs.SetString("es", "true");
        PlayerPrefs.SetString("refresh", "20200101120000");

        PlayerPrefs.SetString("Stage1_1Type", "Robot");
        PlayerPrefs.SetString("Stage1_1Normals", "Normal1 Normal2");
        PlayerPrefs.SetString("Stage1_1NormalsTime", "2 2 2 2 2 2 2 2 2 2 2 2");
        PlayerPrefs.SetString("Stage1_1Elites", "Elite1");
        PlayerPrefs.SetInt("Stage1_1ElitesEmerCnt ", 10);
        PlayerPrefs.SetString("Stage1_5Boss", "1");
        PlayerPrefs.SetInt("Stage1_1Score ", 0);

        PlayerPrefs.SetString("Atk1", "using");
        PlayerPrefs.SetString("Ship1", "using");

        gameObject.SetActive(false);
    }
}
